﻿using System.IO;
using fsp.modelshot.data;
using UnityEditor;
using UnityEngine;
using fsp.eutility;

namespace fsp.modelshot.editor
{
    public class ModelShotPrepareWork
    {
        [MenuItem("ModelShot/3：将Package Sample场景添加到BuildingSetting", false, 4)]
        public static void AddSampleToBuildingSetting()
        {
            string path = Path.Combine(Application.dataPath, ResourcesPathSetting.APPLICATIONPATH_SAMPLE_SCENE);
            path = path.Replace('\\', '/');
            string[] files = Directory.GetFiles(path, "*.unity", SearchOption.AllDirectories);
            
            EditorBuildSettingsScene[] nowScenes = EditorBuildSettings.scenes;
            int allLength = files.Length + nowScenes.Length;
            EditorBuildSettingsScene[] addScenes = new EditorBuildSettingsScene[allLength];
            for (int index = 0; index < nowScenes.Length; index++)
            {
                addScenes[index] = nowScenes[index];
            }
            for (int index = nowScenes.Length; index < allLength; index++)
            {
                addScenes[index] = new EditorBuildSettingsScene(files[index - nowScenes.Length].Replace($"{Application.dataPath}/../",""), true);
            }
            EditorBuildSettings.scenes = addScenes;
        }
        
        
        [MenuItem("ModelShot/4：打开截图场景", false, 5)]
        public static void OpenModelShotScene()
        {
            EUtility.OpenScenee($"{ResourcesPathSetting.Sample_FOLDER}ModelShot.unity");
        }
    }
}