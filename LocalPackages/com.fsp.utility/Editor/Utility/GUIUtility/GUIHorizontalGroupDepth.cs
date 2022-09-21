using System;
using UnityEditor;

namespace fsp.eutility
{
    public struct GUIHorizontalGroupDepth : IDisposable
    {
        readonly int depth;

        public GUIHorizontalGroupDepth(int depth)
        {
            this.depth = depth;

            for (int i = 0; i < depth; i++)
            {
                EditorGUI.indentLevel++;
            }

            EditorGUILayout.BeginHorizontal();
        }

        public void Dispose()
        {
            for (int i = 0; i < depth; i++)
            {
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}