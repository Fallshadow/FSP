using System;
using UnityEditor;

namespace fsp.eutility
{
    public struct GUIChangeCheck : IDisposable
    {
        private readonly Action modifyAction;

        public GUIChangeCheck(Action modifyActionP)
        {
            EditorGUI.BeginChangeCheck();
            modifyAction = modifyActionP;
        }
        
        public void Dispose()
        {
            if (EditorGUI.EndChangeCheck())
            {
                modifyAction?.Invoke();
            }
        }
    }
}