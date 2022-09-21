using System;
using UnityEditor;
using UnityEngine;

namespace fsp.eutility
{
    public struct GUIButtonDepth : IDisposable
    {
        private readonly Action modifyAction;
        private readonly int depth;
        private readonly GUIContent content;

        public GUIButtonDepth(Action modifyAction, int depth, GUIContent content)
        {
            this.depth = depth;
            this.content = content;
            this.modifyAction = modifyAction;
            
            for (int i = 0; i < depth; i++)
            {
                EditorGUI.indentLevel++;
            }

            if (GUILayout.Button(content))
            {
                modifyAction?.Invoke();
            }
        }
        
        public void Dispose()
        {
            for (int i = 0; i < depth; i++)
            {
                EditorGUI.indentLevel--;
            }
        }
    }
}