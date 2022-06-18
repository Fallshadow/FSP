using UnityEditor;
using UnityEngine;

public static class UnityApiExtend
{
    private const float autoFixPosYBlank = 25f;
    private const float autoFixPosXBlank = 25f;

    // 防止window出现在Rect外面
    // 如果window出现在了外面，归位 + 额外留存XX pixel
    // 双屏情况处理
    public static void AutoFixPos(this EditorWindow window)
    {
        Vector2 curPos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        Vector2 curSiz = new Vector2(window.position.width, window.position.height);
        var hLimit = curPos.y + curSiz.y;
        var wLimit = curPos.x + curSiz.x;

        float screenWMaxW = EUtility.GetSystemMetrics(EUtility.SM_CXFULLSCREEN);
        float screenWMaxH = EUtility.GetSystemMetrics(EUtility.SM_CYFULLSCREEN);
        float screenNum = EUtility.GetSystemMetrics(EUtility.SM_CMONITORS);

        if (hLimit > screenWMaxH) hLimit -= (hLimit - screenWMaxH + autoFixPosYBlank);

        int scaler = Mathf.FloorToInt(wLimit / screenWMaxW);
        // 双屏并且超过了其中一个屏幕
        if (screenNum >= 1 && scaler >= 1)
        {
            if (wLimit > screenWMaxW * 2) wLimit -= (wLimit - screenWMaxW * 2 + autoFixPosXBlank);
        }
        else
        {
            if (wLimit > screenWMaxW) wLimit -= (wLimit - screenWMaxW + autoFixPosXBlank);
        }

        window.position = window.position.ChangeValue(wLimit - curSiz.x, hLimit - curSiz.y);
    }
}