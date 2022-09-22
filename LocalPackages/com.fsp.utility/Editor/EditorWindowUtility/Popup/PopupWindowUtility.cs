using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace fsp.editor
{
    public static class PopupWindowUtility
    {
        private static readonly Dictionary<Type, List<string>> enumNameCacheMap = new Dictionary<Type, List<string>>();

        public static void Show(Rect r, string title, string[] names, List<int> selects, Action<IList<int>> callback)
        {
            Show(r, title, names.ToList(), selects, callback);
        }

        public static void Show(Rect r, string title, List<String> names, List<int> selects, Action<IList<int>> callback
            , bool sortByName = false, float titleWidth = 150, float blankSpace = 5)
        {
            GUILayout.BeginHorizontal();

            if (title != null)
            {
                EditorGUI.LabelField(r, title, EditorStyles.label);
                r.x += titleWidth;
                r.width -= (titleWidth + blankSpace);
            }

            string defaultTitle = "";
            if (selects.IsNullOrEmpty()) defaultTitle = "Nothing";
            else defaultTitle = selects.Count > 1 ? "Mix.." : names[selects[0]];

            string allSelect = "Select: ";
            foreach (var t in selects) allSelect += $"{names[t]} |";

            GUIContent titleContent = new GUIContent(defaultTitle);
            titleContent.tooltip = allSelect;

            if (GUI.Button(r, titleContent, EditorStyles.popup))
            {
                PopupMultiSelectWindow window = ScriptableObject.CreateInstance<PopupMultiSelectWindow>();
                window.Init(selects, names, select => { callback?.Invoke(select); });
                window.titleContent = new GUIContent("请选择:");
                window.Show();
                window.AutoFixPos();
            }

            GUILayout.EndHorizontal();
        }


        /// <summary>
        /// List塞入筛选数据，返回选择的单条数据
        /// </summary>
        /// <param name="r">给当前绘制的rect即可</param>
        /// <param name="title">可选label标题</param>
        /// <param name="names">筛选数据</param>
        /// <param name="selectIndex">当前index</param>
        /// <param name="callback">返回选择的index进行回调处理</param>
        /// <param name="sortByName">默认不按名字排序</param>
        /// <param name="titleWidth">标题宽度</param>
        /// <param name="blankSpace">标题留白宽度</param>
        public static void Show(Rect r, string title, List<string> names, int selectIndex, Action<int> callback
            , bool sortByName = false, float titleWidth = 150, float blankSpace = 5)
        {
            GUILayout.BeginHorizontal();

            if (title != null)
            {
                EditorGUI.LabelField(r, title, EditorStyles.label);
                r.x += titleWidth;
                r.width -= (titleWidth + blankSpace);
            }

            if (GUI.Button(r, names[selectIndex], EditorStyles.popup))
            {
                PopupWindow window = ScriptableObject.CreateInstance<PopupWindow>();
                window.Init(selectIndex, names, select => { callback?.Invoke(names.IndexOf(select)); }, sortByName);
                window.titleContent = new GUIContent("请选择:");
                window.Show();
                window.AutoFixPos();
            }

            GUILayout.EndHorizontal();
        }


        public static void Show(Rect r, string title, Enum targetEnum, Action<int> callback
            , bool sortByName = false, float titleWidth = 150, float blankSpace = 5)
        {
            Type enumType = targetEnum.GetType();

            if (!enumNameCacheMap.TryGetValue(enumType, out List<string> names))
            {
                names = Enum.GetNames(enumType).ToList();
                names.Sort((n1, n2) => ((int) Enum.Parse(enumType, n1)).CompareTo((int) Enum.Parse(enumType, n2)));
                enumNameCacheMap.Add(enumType, names);
            }

            int selectIndex = names.IndexOf(targetEnum.ToString());
            if (selectIndex == -1) selectIndex = 0;
            Show(r, title, names, selectIndex, index => { callback?.Invoke((int) Enum.Parse(enumType, names[index])); }, sortByName, titleWidth, blankSpace);
        }
    }
}