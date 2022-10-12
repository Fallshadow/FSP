using System;

namespace fsp.CameraScheme
{
    [Serializable]
    public class CameraSchemeInfo
    {
        public string Name;
        public float XValue;
        public float YValue;
        public float ZValue;
        public float FOVValue;
        public float SpeedValue;
        public bool OrthoMode;
    }
}