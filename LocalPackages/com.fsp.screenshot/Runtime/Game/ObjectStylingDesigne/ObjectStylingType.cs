namespace fsp.ObjectStylingDesigne
{
    public enum ObjectStylingType
    {
        Empty = 0,                                      // 空策略，相当于加载指定文件夹下的所有指定后缀物体到场景根节点
        RexEditor_Weapon_Library = 1,                   // RexDebug下项目武器库策略
        RexEditor_Equipment_Library = 2,                // RexDebug下项目装备库策略
        RexEditor_Suit_Library = 3,                     // RexDebug下项目套装库策略
        RexEditor_Fashion_Pendant_Library = 4,          // RexDebug下项目时装挂件库策略
        RexEditor_Fashion_Weapon_Library = 5,           // RexDebug下项目时装武器库策略
        RexEditor_Pet_Library = 6,                      // RexDebug下项目宠物库策略
        Free_ScreenShot_Library = 7,                    // 自由选择模型截图
        RexEditor_Monster_Library = 8,                  // RexDebug下项目怪物库策略
    }
}