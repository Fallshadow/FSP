using System;
using UnityEditor;

namespace fsp.eutility
{
    public struct GUILabelWidth : IDisposable
    {
        private readonly float cacheWidth;

        public GUILabelWidth(int labelWidth)
        {
            cacheWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = labelWidth;
        }

        public void Dispose()
        {
            EditorGUIUtility.labelWidth = cacheWidth;
        }
    }
}