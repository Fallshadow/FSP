using fsp.modelshot.Game;
using fsp.ui;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    [BindingResource_ModelShot(UiAssetIndex.DebugCanvas)]
    public class DebugCanvas : FullScreenCanvasBase
    {
        public Button Rex_Weapon_Btn;

        public void OpenSelectModelCanvas()
        {
        }
        
        public void OpenRexWeaponCanvas()
        {
            GameController_ModelShot.instance.FSM.SwitchToState((int) ModelShotGameFsmState.REX_WEAPON);
        }
        
        public override void Initialize()
        {
            Rex_Weapon_Btn.onClick.AddListener(OpenRexWeaponCanvas);
        }

        public override void Release()
        {
            Rex_Weapon_Btn.onClick.RemoveListener(OpenRexWeaponCanvas);
        }
    }
}