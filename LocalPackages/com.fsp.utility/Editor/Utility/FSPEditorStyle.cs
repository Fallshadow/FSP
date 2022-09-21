using UnityEditor;
using UnityEngine;

namespace fsp.eutility
{
    public class FSPEditorStyle
    {
        // 淡灰色的样式，适合用作子区域背景色或者引用说明背景色
        public readonly GUIStyle LightGreyPanel;
        // 左阴影，从Timeline源代码样式截取出来的，适合强调层次用
        public readonly GUIStyle LeftShadowStyle;
        
        private Texture2D texLightGrey => m_texLightGrey == null ? m_texLightGrey = AssetDatabase.LoadAssetAtPath<Texture2D>(data.ResourcesPathSetting.EDITOR_LIGHT_GREY_PANEL)
            : m_texLightGrey;
        private Texture2D m_texLightGrey = null;
        
        private static Texture2D texLeftShadow => m_texLeftShadow == null ? m_texLeftShadow = AssetDatabase.LoadAssetAtPath<Texture2D>(data.ResourcesPathSetting.EDITOR_TIMELINE_LEFT_SHADOW)
            : m_texLeftShadow;
        private static Texture2D m_texLeftShadow = null;
        
        public FSPEditorStyle()
        {
            LightGreyPanel = new GUIStyle {normal = {background = texLightGrey, textColor = Color.white}};
            LeftShadowStyle = new GUIStyle {normal = {background = texLeftShadow}};
        }
    }
}