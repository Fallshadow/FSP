using System;
using UnityEditor;

namespace fsp.eutility
{
    public struct GUIVerticalGroupDepth : IDisposable
    {
        readonly int depth;

        public GUIVerticalGroupDepth(int depth)
        {
            this.depth = depth;

            for (int i = 0; i < depth; i++)
            {
                EditorGUI.indentLevel++;
            }

            EditorGUILayout.BeginVertical();
        }

        public void Dispose()
        {
            for (int i = 0; i < depth; i++)
            {
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndVertical();
        }
    }
}