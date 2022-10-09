using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using fsp.utility;
using UnityEngine;

namespace fsp.ui.utility
{
    /// <summary>
    /// 會將UI物件拓展至最大數量，避免一直新增刪除物件。
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    public class UiItemList<TData, TItem> where TItem : Component
    {
        private TItem prefab = null;
        private readonly Transform root = null;
        private readonly Action<TItem> onItemCreate = null;
        private readonly Action<TItem> onItemRecycled = null;
        public Action<int, TData, TItem> onItemUpdate = null;

        private readonly List<TItem> items = new List<TItem>(4);
        private readonly List<TItem> activeItems = new List<TItem>(4);
		public List<TItem> Items => items;
        private IEnumerator _showItems;

        public TItem this[int i] => items[i];
        public TItem Prefab
        {
            get => prefab;
            set => prefab = value;
        }
        public int Count
        {
            get
            {
                int activeCount = 0;
                for (int i = 0, count = items.Count; i < count; ++i)
                {
                    if (items[i].gameObject.activeSelf)
                    {
                        ++activeCount;
                    }
                }
                return activeCount;
            }
        }

        public List<TItem> ActiveItems
        {
            get
            {
                return activeItems;
            }
        }

        public UiItemList(TItem prefab, Transform root, Action<TItem> onItemCreate, Action<int, TData, TItem> onItemUpdate, Action<TItem> onItemRecycled = null)
        {
            this.prefab = prefab;
            this.root = root;
            this.onItemCreate = onItemCreate;
            this.onItemUpdate = onItemUpdate;
            this.onItemRecycled = onItemRecycled;
            if(prefab != null)
                prefab.SetActive(false);
        }

        public void Clear()
        {
            int count = items.Count;
            for (int i = 0; i < count; ++i)
            {
                UnityEngine.Object.Destroy(items[i].gameObject);
            }
            items.Clear();
            activeItems.Clear();
        }

        protected virtual TItem getPrefab(TData data)
        {
            return prefab;
        }

        public void UpdateItems(IList<TData> datas, float interval = 0f, System.Action callback = null)
        {
            int dataCount = 0;
            if (datas != null)
            {
                dataCount = datas.Count;
            }
            
            int itemCount = items.Count;
            activeItems.Clear();
            if (interval == 0f)
            {
                for (int i = itemCount; i < dataCount; ++i)
                {
                    TItem tPrefab = getPrefab(datas[i]);
                    TItem item = UnityEngine.Object.Instantiate(tPrefab, root);
                    onItemCreate?.Invoke(item);
                    item.name = $"{tPrefab.name}_{i}";
                    items.Add(item);
                }

                for (int i = 0; i < dataCount; ++i)
                {
                    items[i].gameObject.SetActive(true);
                    activeItems.Add(items[i]);
                    onItemUpdate?.Invoke(i, datas[i], items[i]);
                }

                for (int i = dataCount; i < itemCount; ++i)
                {
                    items[i].gameObject.SetActive(false);
                    onItemRecycled?.Invoke(items[i]);
                }
                callback?.Invoke();
            }
            else
            {
                StopShow();
                _showItems = _startShow(interval, datas, callback);
                GameController.instance.StartCoroutine(_showItems);
            }
        }

        public TItem GetItem(Predicate<TItem> predicate)
        {
            int itemcount = items.Count;
            for (int i = 0; i < itemcount; i++)
            {
                if (items[i].gameObject.activeSelf)
                {
                    if (predicate(items[i]))
                    {
                        return items[i];
                    }
                }
            }

            return null;
//            List<TItem> activeitems = items.FindAll(i => i.gameObject.activeSelf);
//            return activeitems.Find(predicate);
        }

        public int GetItemIndex(Predicate<TItem> predicate)
        {
            List<TItem> activeitems = items.FindAll(i => i.gameObject.activeSelf);
            return activeitems.FindIndex(predicate);
        }

        public TItem GetFirstItem()
        {
            if (GetActiveItems().Count > 0)
            {
                TItem item = GetActiveItems().First();
                if (item != null)
                {
                    return item;
                }
            }
            return null;
        }
        public TItem GetLastItem()
        {
            if (GetActiveItems().Count > 0)
            {
                TItem item = GetActiveItems().Last();
                if (item != null)
                {
                    return item;
                }
            }
            return null;
        }

        public List<TItem> GetActiveItems()
        {
            List<TItem> activeitems = items.FindAll(i =>
            {
                GameObject gameObject;
                return i != null && (gameObject = i.gameObject) != null && gameObject.activeSelf;
            });
            return activeitems;
        }

        public int GetItemIndex(TItem item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == item)
                {
                    return i;
                }
            }
            return 0;
        }

        public void StopShow()
        {
            if (null != _showItems)
            {
                GameController.instance.StopCoroutine(_showItems);
                _showItems = null; 
            }
        }

        IEnumerator _startShow(float interval, IList<TData> datas, System.Action callback = null)
        {
            int dataCount = datas.Count;
            int itemCount = items.Count;

            for (int i = 0; i < itemCount; i++)
            {
                items[i].gameObject.SetActive(false);
                onItemRecycled?.Invoke(items[i]);
            }

            for (int i = 0; i < dataCount; i++)
            {
                if(i >= itemCount)
                {
                    TItem tPrefab = getPrefab(datas[i]);
                    TItem item = UnityEngine.Object.Instantiate(tPrefab, root);
                    onItemCreate?.Invoke(item);
                    items.Add(item);
                }
                if(i>= datas.Count || i >= items.Count)//just in case
                {
                    yield break;
                }
                onItemUpdate?.Invoke(i, datas[i], items[i]);
                items[i].gameObject.SetActive(true);
                items[i].transform.SetAsLastSibling();
                activeItems.Add(items[i]);
                yield return new WaitForSeconds(interval);
            }
            callback?.Invoke();
        }

    }
}