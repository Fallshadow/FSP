namespace fsp.data
{
    public static class ResourcesPathSetting
    {
        public const string APPLICATIONPATH_SO_PACKAGE = "../Packages/com.fsp.utility/Resources/ScriptableObject";
        
        public const string RESOURCES_FOLDER = "ResourceRex/";
        public const string RESOURCES_PC_FOLDER = "ResourceRex_PC/";
        public const string RESOURCES_ANDROID_FOLDER = "ResourceRex_Android/";
        public const string RESOURCES_IOS_FOLDER = "ResourceRex_IOS/";
        public const string SCRIPTABLEOBJECT_FOLDER = "Packages/com.fsp.utility/Resources/ScriptableObject/";
        
        public const string ABDepInfo_DT_Application_Path_Suffix = "/../ABDepInfo_DT/";
        // file path
        public const string ASSETHASHMAP_UI_FILE_PATH = ""; // 日后填写UI Hash SO的文件路径
        public const string UI_STYLE_HELP_SO = SCRIPTABLEOBJECT_FOLDER + "UiStyleHelperSO.asset";
        public const string OBJECT_WORLD_INFO_SO = SCRIPTABLEOBJECT_FOLDER + "ObjectWorldInfoSO.asset";
        public const string CREATEOBJECTPATHSO_VIRTUAL_FILE_PATH = SCRIPTABLEOBJECT_FOLDER + "ObjectStylingStrategySO.asset";
        
#if UNITY_EDITOR
        public const string ABDepInfo_DT_Package_Application_Path_Suffix = "/../ABDepInfo_DT_Package/";
        public const string EDITOR_LIGHT_GREY_PANEL = "Packages/com.fsp.utility/Resources/light-grey-panel.png";
        public const string EDITOR_TIMELINE_LEFT_SHADOW = "Packages/com.fsp.utility/Resources/TimelineLeftShadow.png";
#endif
    }
}