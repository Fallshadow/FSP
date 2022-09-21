using System.Collections.Generic;
using UnityEngine;

namespace fsp.modelshot.Game.ObjectStylingDesigne
{
    [CreateAssetMenu]
    public class CreateObjectPathSO : ScriptableObject
    {
        public List<CreateObjectPathInfo> ObjectPathStructs = new List<CreateObjectPathInfo>();
    }
}