using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace fsp.editor
{
    public class PopupMultiSelectWindow : EditorWindow
    {
        class PopupItem
        {
            public int index;
            public string name;
            public bool isSelect;
        }

        private readonly List<PopupItem> items = new List<PopupItem>();
        private Action<int[]> selectCallBack;
        private IList<string> datas;
        private PopupItem selectItem;

        private bool isInitedStype;
        private GUIStyle textStyle;
        private GUIStyle selectedBackgroundStyle;
        private GUIStyle normalBackgroundStyle;
        private GUIStyle searchToobar;
        private Vector2 scrollPos;
        private string searchText = string.Empty;
        private const float elementHeight = 16;

        public void Init(IList<int> selects, IList<string> targetsNames, Action<int[]> onSelectCallBack)
        {
            items.Clear();
            selectCallBack = onSelectCallBack;
            datas = targetsNames;

            for (int i = 0; i < datas.Count; i++)
            {
                string curName = datas[i];
                PopupItem item = new PopupItem
                {
                    name = curName,
                    index = i,
                    isSelect = false
                };

                foreach (var target in selects)
                {
                    if (i != target) continue;
                    item.isSelect = true;
                    selectItem = item;
                    break;
                }

                items.Add(item);
            }

            int realIndex = selectItem == null ? 0 : items.IndexOf(selectItem);
            const float scrollHeight = 20f;
            scrollPos.y = scrollHeight * realIndex;
            searchToobar = EditorStyles.toolbarSearchField;
            searchText = string.Empty;
            InitTextStyle();
        }

        void OnGUI()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUI.SetNextControlName("Search");
            searchText = EditorGUILayout.TextField("", searchText, searchToobar, GUILayout.MinWidth(95));
            EditorGUI.FocusTextInControl("Search");
            if (GUILayout.Button("A", EditorStyles.toolbarButton, GUILayout.Width(32)))
            {
                items.Sort((e1, e2) => string.Compare(e1.name, e2.name, StringComparison.Ordinal));
                scrollPos.y = elementHeight * (selectItem == null ? 0 : items.IndexOf(selectItem));
            }

            GUILayout.EndHorizontal();

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            int count = items.Count;
            drawEverything();
            drawNothing();
            for (int i = 0; i < count; i++)
            {
                PopupItem single = items[i];
                if (!string.IsNullOrEmpty(searchText)
                    && !single.name.ToLower().Contains(searchText.ToLower()))
                    continue;

                Rect rect = single.isSelect ? EditorGUILayout.BeginHorizontal(selectedBackgroundStyle) : EditorGUILayout.BeginHorizontal(normalBackgroundStyle);
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.Toggle(single.name, single.isSelect, textStyle);
                bool isChanged = EditorGUI.EndChangeCheck();
                GUILayout.FlexibleSpace();
                if (isChanged || rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
                {
                    single.isSelect = !single.isSelect;
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();

            if (focusedWindow != this)
            {
                Close();
            }
        }

        private void OnDisable()
        {
            selectCallBack?.Invoke(
                items
                    .Where(x => x.isSelect)
                    .Select(x => x.index)
                    .ToArray());
        }

        void drawNothing()
        {
            bool flag = true;
            for (var index = 0; index < items.Count; index++)
            {
                var item = items[index];
                if (!item.isSelect) continue;
                // 有一个不是 就break
                flag = false;
                break;
            }

            Rect rect = EditorGUILayout.BeginHorizontal(normalBackgroundStyle);
            EditorGUI.BeginChangeCheck();
            bool curToggle = EditorGUILayout.Toggle("Nothing", flag);
            bool isChanged = EditorGUI.EndChangeCheck();
            GUILayout.FlexibleSpace();
            if (isChanged || rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
            {
                for (var index = 0; index < items.Count; index++)
                {
                    var item = items[index];
                    item.isSelect = !curToggle;
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        void drawEverything()
        {
            bool flag = true;
            for (var index = 0; index < items.Count; index++)
            {
                var item = items[index];
                if (item.isSelect) continue;
                // 有一个不是 就break
                flag = false;
                break;
            }

            Rect rect = EditorGUILayout.BeginHorizontal(normalBackgroundStyle);
            EditorGUI.BeginChangeCheck();
            bool curToggle = EditorGUILayout.Toggle("EveryThing", flag);
            bool isChanged = EditorGUI.EndChangeCheck();
            GUILayout.FlexibleSpace();
            if (isChanged || rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
            {
                for (var index = 0; index < items.Count; index++)
                {
                    var item = items[index];
                    item.isSelect = curToggle;
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        void InitTextStyle()
        {
            if (isInitedStype) return;

            textStyle = new GUIStyle(EditorStyles.toggle)
            {
                fixedHeight = elementHeight,
                alignment = TextAnchor.MiddleLeft
            };

            selectedBackgroundStyle = new GUIStyle
            {
                normal = {background = PopupStyle.GetSelectImg()}
            };

            normalBackgroundStyle = new GUIStyle
            {
                hover = {background = PopupStyle.GetHeightLightImg()}
            };

            isInitedStype = true;
        }

        void Update()
        {
            Repaint();
        }
    }
}