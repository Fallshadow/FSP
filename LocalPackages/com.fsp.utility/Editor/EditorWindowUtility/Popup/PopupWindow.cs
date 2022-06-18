using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PopupWindow : EditorWindow
{
    class PopupItem
    {
        public int index;
        public string name;
        public bool isSelect;
    }
    
    private readonly List<PopupItem> items = new List<PopupItem>();
    private Action<string> callback;
    private IList<string> datas;
    
    private string searchText = string.Empty;
    private PopupItem selectItem;
    private bool isSelected;
    
    private bool isInitedStype;
    private GUIStyle textStyle;
    private GUIStyle selectedBackgroundStyle;
    private GUIStyle normalBackgroundStyle;
    private GUIStyle searchToobar;
    private Vector2 scrollPos;
    private const float elementHeight = 16;

    public void Init(int target, IList<string> targetsNames, Action<string> onSelectCallBack, bool sortByName = false)
    {
        items.Clear();
        callback = onSelectCallBack;
        datas = targetsNames;

        for (int i = 0; i < datas.Count; i++)
        {
            string curName = datas[i];
            PopupItem item = new PopupItem { name = curName, index = i, isSelect = false };
            if (i == target) { item.isSelect = true; selectItem = item; }
            items.Add(item);
        }

        int realIndex = selectItem == null ? 0 : items.IndexOf(selectItem);
        const float scrollHeight = 20f;
        scrollPos.y = scrollHeight * realIndex;

        isSelected = false;
        searchToobar = EditorStyles.toolbarSearchField;
        searchText = string.Empty;
        InitTextStyle();

        if (sortByName)
        {
            items.Sort((e1, e2) => string.Compare(e1.name, e2.name, StringComparison.Ordinal));
            scrollPos.y = scrollHeight * (selectItem == null ? 0 : items.IndexOf(selectItem));
        }
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
        for (int i = 0; i < count; i++)
        {
            PopupItem single = items[i];
            if (!string.IsNullOrEmpty(searchText) && !single.name.ToLower().Contains(searchText.ToLower())) continue;

            Rect rect = single.isSelect ? EditorGUILayout.BeginHorizontal(selectedBackgroundStyle) : EditorGUILayout.BeginHorizontal(normalBackgroundStyle);
            GUILayout.Label(single.name, textStyle);
            GUILayout.FlexibleSpace();
            
            if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
            {
                callback?.Invoke(single.name);
                isSelected = true;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();

        if (isSelected)
        {
            isSelected = false;
            Close();
        }

        if (focusedWindow != this) Close();
    }

    void InitTextStyle()
    {
        if (isInitedStype) return;

        textStyle = new GUIStyle(EditorStyles.label)
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