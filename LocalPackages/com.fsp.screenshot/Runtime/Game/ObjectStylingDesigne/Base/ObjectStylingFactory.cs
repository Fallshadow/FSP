namespace fsp.ObjectStylingDesigne
{
    public static class ObjectStylingFactory
    {
        public static ObjectStylingStrategyBase CreateStrategy(ObjectStylingStrategyInfo info)
        {
            switch (info.IdType)
            {
                case ObjectStylingType.Empty                    : return new ObjectStylingStrategyEmpty(info);
                case ObjectStylingType.RexEditor_Weapon_Library : return new ObjectStylingStrategyRexEditorWeapon(info);
                default:
                {
                    return null;
                }
            }
        }
    }
}