using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace fsp.eutility
{
    public partial class EUtility
    {
        // TODO:DragHandler实现？
        public static void ListTEditorShowNewT<T>(List<T> list, string foldoutName, ref bool foldout, Action showT, int level) where T : new()
        {
            list ??= new List<T>();
            int listNewCount = list.Count;
            using (new GUIVerticalGroupDepth(level))
            {
                using (new GUIHorizontalGroupDepth(0))
                {
                    foldout = EditorGUILayout.Foldout(foldout, foldoutName);

                    using (new GUIChangeCheck(() => list.AdjustToConformCountNewT(listNewCount)))
                    {
                        listNewCount = EditorGUILayout.IntField("", list.Count);
                    }
                }

                if (!foldout) return;
                
                showT();

                // using (new GUIHorizontalGroupDepth(0))
                // {
                //     if (GUILayout.Button(GreenAddIcon))
                //     {
                //         list.Add(new T());
                //     }
                //
                //     if (GUILayout.Button(YellowRemoveIcon))
                //     {
                //         list.RemoveAt(list.Count - 1);
                //     }
                // }
            }
        }
        
        public static void ListTEditorShowDefaultT<T>(List<T> list, string foldoutName, ref bool foldout, Action showT, int level)
        {
            list ??= new List<T>();
            int listNewCount = list.Count;
            using (new GUIVerticalGroupDepth(level))
            {
                using (new GUIHorizontalGroupDepth(0))
                {
                    foldout = EditorGUILayout.Foldout(foldout, foldoutName);

                    using (new GUIChangeCheck(() => list.AdjustToConformCountDefaultT(listNewCount)))
                    {
                        listNewCount = EditorGUILayout.IntField("", list.Count);
                    }
                }

                if (!foldout) return;
                
                showT();
                    
                // using (new GUIHorizontalGroupDepth(0))
                // {
                //     if (GUILayout.Button(GreenAddIcon))
                //     {
                //         list.Add(default);
                //     }
                //
                //     if (GUILayout.Button(YellowRemoveIcon))
                //     {
                //         list.RemoveAt(list.Count - 1);
                //     }
                // }
            }
        }
    }
}