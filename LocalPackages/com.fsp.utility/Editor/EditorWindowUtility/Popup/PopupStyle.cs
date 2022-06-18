using UnityEditor;
using UnityEngine;

internal static class PopupStyle
{
    private const string selectedBgPro = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAQklEQVRIDe3SsQkAAAgDQXWN7L+nOMFXdm8dIhzpJPV581l+3T5AYYkkQgEMuCKJUAADrkgiFMCAK5IIBTDgipBoAWXpAJEoZnl3AAAAAElFTkSuQmCC";
    private const string hightLightBgPro = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAQklEQVRIDe3SsQkAAAgDQXXD7L+MOMFXdm8dIhzpJPV581l+3T5AYYkkQgEMuCKJUAADrkgiFMCAK5IIBTDgipBoARFdATMHrayuAAAAAElFTkSuQmCC";
    private const string selectedBgLight = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAQUlEQVRIDe3SsQkAAAgDQXV/yMriBF/ZvXWIcKST1OfNZ/l1+wCFJZIIBTDgiiRCAQy4IolQAAOuSCIUwIArQqIF36EB7diYDg8AAAAASUVORK5CYII=";
    private const string hightLightBgLight = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAQklEQVRIDe3SsQkAAAgDQXX/ETOMOMFXdm8dIhzpJPV581l+3T5AYYkkQgEMuCKJUAADrkgiFMCAK5IIBTDgipBoAc9YAtQLJ3kPAAAAAElFTkSuQmCC";

    public static byte[] GetSelectImgBytes()
    {
        return EditorGUIUtility.isProSkin ? System.Convert.FromBase64String(selectedBgPro) : System.Convert.FromBase64String(selectedBgLight);
    }

    public static byte[] GetHeightLightImgBytes()
    {
        return EditorGUIUtility.isProSkin ? System.Convert.FromBase64String(hightLightBgPro) : System.Convert.FromBase64String(hightLightBgLight);
    }

    public static Texture2D GetSelectImg()
    {
        Texture2D resultBg = new Texture2D(1, 1, TextureFormat.RGB24, false);
        resultBg.LoadImage(GetSelectImgBytes());
        return resultBg;
    }

    public static Texture2D GetHeightLightImg()
    {
        Texture2D resultBg = new Texture2D(1, 1, TextureFormat.RGB24, false);
        resultBg.LoadImage(GetHeightLightImgBytes());
        return resultBg;
    }
}