using System.Collections.Generic;
using UnityEngine;

namespace fsp.ObjectStylingDesigne
{
    // 如果有很多方案（物件创造）是不是可以换成bytes 列表分类存储
    [CreateAssetMenu]
    public class ObjectStylingStrategySO : ScriptableObject
    {
        public List<ObjectStylingStrategyInfo> ObjectPathStructs = new List<ObjectStylingStrategyInfo>();
    }
}