using UnityEngine;
using UnityEngine.UI;

namespace fsp.ui.utility
{
    /// <summary>
    /// 給單純顯示的UI使用，不用每次都寫一個類別。需要交互物件的還是需要另外寫類別來實作。
    /// </summary>
    public class UiItem : MonoBehaviour
    {
        public int Index { get; protected set; }

        [SerializeField] protected GameObject[] gos = null;
        [SerializeField] protected UiButton[] buttons = null;
        [SerializeField] protected Image[] images = null;
        [SerializeField] protected Text[] texts = null;

        public int GoArrayCount
        {
            get
            {
                if (gos == null)
                {
                    return 0;
                }
                return gos.Length;
            }
        }

        public int ButtonArrayCount
        {
            get
            {
                if (buttons == null)
                {
                    return 0;
                }
                return buttons.Length;
            }
        }

        public int ImageArrayCount
        {
            get
            {
                if (images == null)
                {
                    return 0;
                }
                return images.Length;
            }
        }

        public int TextArrayCount
        {
            get
            {
                if (texts == null)
                {
                    return 0;
                }
                return texts.Length;
            }
        }


        public UiButton GetButton(int i)
        {
            return buttons[i];
        }

        public void SetSound(string soundId)
        {
            UiButton btn = transform.GetComponent<UiButton>();
            if (btn != null)
            {
                btn.SoundId = soundId;
            }
        }

        public Image GetImage(int i)
        {
            if (i < images.Length && images[i] != null)
            {
                return images[i];
            }

            return null;
        }

        public Text GetText(int i)
        {
            if (i < texts.Length && texts[i] != null)
            {
                return texts[i];
            }
            return null;
        }

        public GameObject GetGameObject(int i)
        {
            if (i >= gos.Length) return null;
            return gos[i];
        }

    }

    public abstract class UiItem<T> : MonoBehaviour
    {
        public int Index { get; protected set; }
        public T Data { get; protected set; }

        [SerializeField] protected UiButton[] buttons = null;
        [SerializeField] protected Image[] images = null;
        [SerializeField] protected Text[] texts = null;

        public UiButton GetButton(int i)
        {
            if (buttons.Length <= i)
            {
                return null;
            }
            return buttons[i];
        }

        public Image GetImage(int i)
        {
            if (images.Length <= i)
            {
                return null;
            }
            return images[i];
        }

        public Text GetText(int i)
        {
            if (texts.Length <= i)
            {
                return null;
            }
            return texts[i];
        }

        public abstract void UpdateItem(int index, T data);
    }
}