using System;

namespace fsp.ObjectStylingDesigne
{
    public struct ObjectStringPath
    {
        public string FilterName;
        public string FilePath;
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var a = (ObjectStringPath)obj;
            return (string.Equals(a.FilePath,FilePath, StringComparison.Ordinal));
        }
    }
}