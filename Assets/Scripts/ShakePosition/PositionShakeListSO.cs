using System.Collections.Generic;
using UnityEngine;

namespace fsp.shake
{
    [CreateAssetMenu]
    public class PositionShakeListSO : ScriptableObject
    {
        public List<PositionShakeConfig> ShakeConfigDatas = new List<PositionShakeConfig>();
    }
}