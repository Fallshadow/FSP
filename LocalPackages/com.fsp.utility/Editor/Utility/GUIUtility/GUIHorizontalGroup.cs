using System;
using UnityEditor;
using UnityEngine;

namespace fsp.eutility
{
    public struct GUIHorizontalGroup : IDisposable
    {
        public GUIHorizontalGroup(bool placeholder)
        {
            EditorGUILayout.BeginHorizontal();
        }

        public void Dispose()
        {
            EditorGUILayout.EndHorizontal();
        }
    }
}