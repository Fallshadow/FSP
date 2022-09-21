using System;
using UnityEditor;
using UnityEngine;

namespace fsp.eutility
{
    public struct GUIVerticalGroup : IDisposable
    {
        public GUIVerticalGroup(bool placeholder)
        {
            EditorGUILayout.BeginVertical();
        }

        public void Dispose()
        {
            EditorGUILayout.EndVertical();
        }
    }
}