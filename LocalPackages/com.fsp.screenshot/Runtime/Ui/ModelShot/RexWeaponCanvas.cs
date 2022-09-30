using fsp.modelshot.Game;
using fsp.modelshot.Game.ObjectStylingDesigne;
using fsp.ui;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    [BindingResource_ModelShot(UiAssetIndex.RexWeaponCanvas)]
    public class RexWeaponCanvas : FullScreenCanvasBase
    {
        public override void Initialize()
        {
            ObjectStylingDesigner.instance.CreateOrGetStrategy(ObjectStylingType.RexEditor_Weapon_Library);
        }

        public override void Release()
        {
            ObjectStylingDesigner.instance.ReleaseStrategy(ObjectStylingType.RexEditor_Weapon_Library);
        }
    }
}