namespace fsp.ObjectStylingDesigne
{
    public static class ObjectStylingFactory
    {
        public static ObjectStylingStrategyBase CreateStrategy(ObjectStylingStrategyInfo info)
        {
            switch (info.IdType)
            {
                case ObjectStylingType.Empty                             : return new ObjectStylingStrategyEmpty(info);
                case ObjectStylingType.RexEditor_Weapon_Library          : return new ObjectStylingStrategyRexEditorWeapon(info);
                case ObjectStylingType.RexEditor_Equipment_Library       : return new ObjectStylingStrategyRexEditorEquipment(info);
                case ObjectStylingType.RexEditor_Suit_Library            : return new ObjectStylingStrategyRexEditorSuit(info);
                case ObjectStylingType.RexEditor_Fashion_Pendant_Library : return new ObjectStylingStrategyRexEditorPendant(info);
                case ObjectStylingType.RexEditor_Fashion_Weapon_Library  : return new ObjectStylingStrategyRexEditorFashionWeapon(info);
                case ObjectStylingType.RexEditor_Pet_Library             : return new ObjectStylingStrategyRexEditorPet(info);
                case ObjectStylingType.Free_ScreenShot_Library           : return new ObjectStylingStrategyFreeScreenShot(info);
                case ObjectStylingType.RexEditor_Monster_Library         : return new ObjectStylingStrategyRexEditorMonster(info);
                default:
                {
                    return null;
                }
            }
        }
    }
}