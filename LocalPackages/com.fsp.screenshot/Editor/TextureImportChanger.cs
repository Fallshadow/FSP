using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace fsp.modelshot.editor
{
    public class TextureImportChangerWindow : EditorWindow
    {
        [MenuItem("通用工具/001-099:资源修改类/修改图片压缩格式",false,1)]
        static void SearchRefrence()
        {
            TextureImportChangerWindow window = (TextureImportChangerWindow) GetWindow(typeof(TextureImportChangerWindow), false, "批量修改图片格式", true);
            window.Show();
        }

        private SerializedObject serializedObject;
        private SerializedProperty serializedProperty;

        private const string _standalone = "Standalone";
        private const string _iPhone = "iPhone";
        private const string _default = "Default";
        private const string _android = "Android";
        private static string assetPath = "Assets\\Resources";
        private static TextureImporterFormat formatAlpha = TextureImporterFormat.ASTC_5x5;
        private static TextureImporterFormat formatDefault = TextureImporterFormat.ASTC_5x5;
        private static int maxSize = 2048;

        [SerializeField] //必须要加
        public List<string> searchPatterns = new List<string>(2)
        {
            "*.png",
            "*.jpg",
            "*.tga",
        };

        
        void OnEnable()
        {
            serializedObject = new SerializedObject(this);
            serializedProperty = serializedObject.FindProperty("searchPatterns");
        }

        private void OnGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            assetPath = EditorGUILayout.TextField("文件目录: ", assetPath, GUILayout.Width(500));

            formatAlpha = (TextureImporterFormat) EditorGUILayout.EnumPopup("formatAlpha: ", formatAlpha, GUILayout.Width(300));
            formatDefault = (TextureImporterFormat) EditorGUILayout.EnumPopup("formatDefault: ", formatDefault, GUILayout.Width(300));
            maxSize = EditorGUILayout.IntField("MaxSize: ", maxSize, GUILayout.Width(300));

            EditorGUILayout.PropertyField(serializedProperty, true);

            GUILayout.Space(EditorGUIUtility.singleLineHeight);
            if (GUILayout.Button("开始修改", GUILayout.Width(300)))
            {
                if (assetPath.IsNullOrEmptyEx())
                    return;
                
                foreach (var searchPattern in searchPatterns.Where(searchPattern => !searchPattern.IsNullOrEmptyEx()))
                {
                    ImporterFiles(assetPath, searchPattern);
                }
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        private static void  ImporterFiles(string dir, string searchPattern)
        {
            string[] files = Directory.GetFiles(dir, searchPattern, SearchOption.AllDirectories);
            float i = 0;
            foreach (var file in files)
            {
                EditorUtility.DisplayProgressBar($"ImporterFiles by {searchPattern} count {files.Length}", $"{file}[{i}]", i / files.Length);
                SetTextureImporter(file);
                i++;
            }
            EditorUtility.ClearProgressBar();
        }

        private static void SetTextureImporter(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            TextureImporter ti = (TextureImporter) AssetImporter.GetAtPath(path);
            bool alpha = ti.DoesSourceTextureHaveAlpha();
            TextureImporterFormat format = alpha ? formatAlpha : formatDefault;
            //Debug.Log($"[更改图片规格]  path:{path}\nA通道:{alpha} 格式:{format}");
            TextureImporterSettings tis = new TextureImporterSettings();
            ti.ReadTextureSettings(tis);
            tis.spriteGenerateFallbackPhysicsShape = false;
            ti.SetTextureSettings(tis);
            ti.alphaSource = (alpha ? TextureImporterAlphaSource.FromInput : TextureImporterAlphaSource.None);
            ti.alphaIsTransparency = alpha;
            ti.spritesheet = new SpriteMetaData[0];
            var settings = ti.GetPlatformTextureSettings(_standalone);
            if (settings.format != format)
            {
                ti.SetPlatformTextureSettings(new TextureImporterPlatformSettings
                {
                    overridden = false,
                    name = _standalone,
                    maxTextureSize = maxSize,
                    format = TextureImporterFormat.Automatic,
                    textureCompression = TextureImporterCompression.Compressed,
                    resizeAlgorithm = TextureResizeAlgorithm.Mitchell
                });
            }
            
            settings = ti.GetPlatformTextureSettings(_default);
            if (settings.format != TextureImporterFormat.ARGB32)
            {
                ti.SetPlatformTextureSettings(new TextureImporterPlatformSettings
                {
                    name = _default,
                    maxTextureSize = maxSize,
                    format = TextureImporterFormat.Automatic,
                    textureCompression = TextureImporterCompression.Compressed,
                    resizeAlgorithm = TextureResizeAlgorithm.Mitchell
                });
            }
            
//#if UNITY_IOS 暂时设置为相同格式
            settings = ti.GetPlatformTextureSettings(_iPhone);
            if (settings.format != format)
            {
                ti.SetPlatformTextureSettings(new TextureImporterPlatformSettings
                {
                    overridden = true,
                    name = _iPhone,
                    maxTextureSize = maxSize,
                    format = format,
                    textureCompression = TextureImporterCompression.Compressed,
                    resizeAlgorithm = TextureResizeAlgorithm.Mitchell
                });
            }

//#elif UNITY_ANDROID 暂时设置为相同格式
            settings = ti.GetPlatformTextureSettings(_android);
            if (settings.format != format)
            {
                ti.SetPlatformTextureSettings(new TextureImporterPlatformSettings
                {
                    overridden = true,
                    name = _android,
                    maxTextureSize = maxSize,
                    format = format,
                    textureCompression = TextureImporterCompression.Compressed,
                    resizeAlgorithm = TextureResizeAlgorithm.Mitchell
                });
            }

//#endif
            EditorUtility.SetDirty(ti);
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }

    }
    
    
    
    public class TextureImportChangerWindow2 : EditorWindow
    {
        [MenuItem("OpenSceneTool/Open DEBUG/ModelViewerScene/ChangeTextureForAndroid")]
        static void SearchRefrence()
        {
            TextureImportChangerWindow2 window = (TextureImportChangerWindow2) GetWindow(typeof(TextureImportChangerWindow2), false, "批量修改图片格式", true);
            window.Show();
        }
        
        private SerializedObject serializedObject;
        private static string assetPath = "Assets\\Resources";
        
        void OnEnable() { serializedObject = new SerializedObject(this); }
        
        private void OnGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            assetPath = EditorGUILayout.TextField("文件目录: ", assetPath, GUILayout.Width(500));
            if (GUILayout.Button("开始修改", GUILayout.Width(300)))
            {
                if (assetPath.IsNullOrEmptyEx()) return;
                
                string[] allAssetsGuid = AssetDatabase.FindAssets("t:texture2D", new []{assetPath});
                List<string> allAssetsPath = allAssetsGuid.Select(AssetDatabase.GUIDToAssetPath).ToList();
                List<string> allNeedAssetsPath = allAssetsPath.ToList();

                int index = 0;
                foreach (var path in allNeedAssetsPath)
                {
                    TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                    ++index;
                    if (importer == null) continue;
                    var settingsD = importer.GetDefaultPlatformTextureSettings();
                    if (!IsNeedReimport(settingsD)) return;
                    
                    EditorUtility.DisplayProgressBar( $"ApplySetting{path}", $"{(float)index /allNeedAssetsPath.Count}" , (float)index /allNeedAssetsPath.Count);

                    SetPlatformSetting(settingsD);
                    importer.SetPlatformTextureSettings(settingsD);
        
                    var settingsA = importer.GetPlatformTextureSettings("Android");
                    if (settingsA.overridden)
                    {
                        if (IsNeedReimport(settingsA))
                        {
                            SetPlatformSetting(settingsA);
                            importer.SetPlatformTextureSettings(settingsA);
                        }
                    }
                    importer.SaveAndReimport();
                }
                EditorUtility.ClearProgressBar();
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }


        private static bool IsNeedReimport(TextureImporterPlatformSettings settings)
        {
            return !(settings.maxTextureSize == 2048 && settings.format == TextureImporterFormat.ARGB32);
        }

        private static void SetPlatformSetting(TextureImporterPlatformSettings settings)
        {
            settings.maxTextureSize = 2048;
            settings.format = TextureImporterFormat.RGBA32;
        }
    }
} 