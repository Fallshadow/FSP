using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace fsp.eutility
{
    public static partial class EUtility
    {
        public static readonly FSPEditorStyle Style = new FSPEditorStyle();
        
        
        public static void OpenScenee(string sceneName)
        {
            if (EditorApplication.isPlaying) return;

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(sceneName);       
            }
            else
            {
                // user said no -> evtl. abort or do nothing?
            }        
        }
    }
    

}
