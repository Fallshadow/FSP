namespace fsp.ui
{
    [System.Serializable]
    public enum UiAssetType
    {
        UAT_NONE = -1,
        UAT_PREFAB = 0,//预制体
        UAT_ATLAS = 1,//图集
        UAT_HIGH_DEFINITION_TEX = 2,//高清图
        UAT_TMP_FONTASSET = 3,//字体资源
        UAT_MATERIAL = 4,//材质
        UAT_ANIMATION = 5,//动画
        UAT_TXT = 6,//文本资源
    }
}