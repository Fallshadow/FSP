using System.Collections.Generic;

namespace fsp.assetbundleeditor
{
    public class AssetItem
    {
        const int DEFAULT_IMPORTANCE = 4;

        public List<string> subLevelNames;
        public string assetPath;
        public int importance = DEFAULT_IMPORTANCE;
        public bool bAtlas;
    }
}