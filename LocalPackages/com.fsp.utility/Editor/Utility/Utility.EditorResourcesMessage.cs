using UnityEditor;
using UnityEngine;

namespace fsp.eutility
{
    public partial class EUtility
    {
        public static readonly GUIContent GreenAddIcon = EditorGUIUtility.IconContent("winbtn_mac_max_h");
        public static readonly GUIContent YellowRemoveIcon = EditorGUIUtility.IconContent("winbtn_mac_min_h");
        
        public static Texture2D TexLightGrey => m_texLightGrey == null ? m_texLightGrey = Resources.Load<Texture2D>("Styles/light-grey-panel")
            : m_texLightGrey;
        private static Texture2D m_texLightGrey = null;
        


    }
}