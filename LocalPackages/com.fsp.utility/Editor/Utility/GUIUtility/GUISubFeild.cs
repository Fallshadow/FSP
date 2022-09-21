using System;
using UnityEditor;
using UnityEngine;

namespace fsp.eutility
{
    public struct GUISubFeild : IDisposable
    {
        private readonly int level;
        
        private const float yoffsetSpace = 2f;
        private const float xLeftSpace = 5f;
        private const float indentLevelSpace = 15f;
        public GUISubFeild(int level)
        {
            this.level = level;
            EditorGUI.indentLevel += level;
            var rect = EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            EditorGUILayout.Separator();
            rect.y += yoffsetSpace;
            rect.x = rect.x - xLeftSpace + indentLevelSpace * level;
            rect.width += xLeftSpace + indentLevelSpace * level;
            GUI.Box(rect, GUIContent.none, EUtility.Style.LightGreyPanel);
            rect.x -= xLeftSpace;
            rect.width = xLeftSpace;
            GUI.Box(rect, GUIContent.none, EUtility.Style.LeftShadowStyle);
        }

        public void Dispose()
        {
            EditorGUILayout.Separator();
            EditorGUILayout.EndVertical();
            EditorGUI.indentLevel -= level;
        }
    }
}