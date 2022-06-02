﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace act.data
{
    

    [Serializable]
    [CreateAssetMenu]
    public class LogicFsmControlSO : ScriptableObject, ISerializationCallbackReceiver
    {
        Dictionary<int,int> MainLayerFsmControDic = new Dictionary<int,int>();
        [SerializeField] [HideInInspector] private List<int> keys0 = new List<int>();
        [SerializeField] [HideInInspector] private List<int> values0 = new List<int>();


        public void OnBeforeSerialize()
        {
            keys0.Clear();
            values0.Clear();

            foreach (var kvp in MainLayerFsmControDic)
            {
                keys0.Add(kvp.Key);
                values0.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            MainLayerFsmControDic.Clear();

            for (int i = 0; i != Math.Min(keys0.Count, values0.Count); i++)
                MainLayerFsmControDic.Add(keys0[i], values0[i]);
            if(MainLayerFsmControDic.Count == 0)MainLayerFsmControDic.Add(1, 1);
            Debug.Log("OnAfterDeserialize");
        }
    }
}