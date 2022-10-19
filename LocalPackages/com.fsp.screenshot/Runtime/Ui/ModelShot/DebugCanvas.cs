using fsp.modelshot.Game;
using fsp.ui;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    [BindingResource_ModelShot(UiAssetIndex.DebugCanvas)]
    public class DebugCanvas : FullScreenCanvasBase
    {
        public Button Rex_Weapon_Btn;
        public Button Rex_Equipment_Btn;
        public Button Rex_Suit_Btn;
        public Button Rex_FashionGuxJian_Btn;
        public Button Rex_FashionWeapon_Btn;
        public Button Rex_Pet_Btn;
        public Button Rex_Monster_Btn;
        public Button Free_Btn;

        public void OpenSelectModelCanvas()
        {
        }
        
        public void OpenRexWeaponCanvas()
        {
            GameController_ModelShot.instance.FSM.SwitchToState((int) ModelShotGameFsmState.LOAD_REX_Empty);
            GameController_ModelShot.instance.SetNextState((int) ModelShotGameFsmState.REX_WEAPON);
        }
        
        public void OpenRexEquipmentCanvas()
        {
            GameController_ModelShot.instance.FSM.SwitchToState((int) ModelShotGameFsmState.LOAD_REX_Empty);
            GameController_ModelShot.instance.SetNextState((int) ModelShotGameFsmState.REX_EQUIPMENT);
        }
        
        public void OpenRexSuitCanvas()
        {
            GameController_ModelShot.instance.FSM.SwitchToState((int) ModelShotGameFsmState.LOAD_REX_Empty);
            GameController_ModelShot.instance.SetNextState((int) ModelShotGameFsmState.REX_SUIT);
        }
        
        public void OpenRexFGuaJiananvas()
        {
            GameController_ModelShot.instance.FSM.SwitchToState((int) ModelShotGameFsmState.LOAD_REX_Empty);
            GameController_ModelShot.instance.SetNextState((int) ModelShotGameFsmState.REX_FGUAJIAN);
        }
        
        public void OpenRexFWeaponCanvas()
        {
            GameController_ModelShot.instance.FSM.SwitchToState((int) ModelShotGameFsmState.LOAD_REX_Empty);
            GameController_ModelShot.instance.SetNextState((int) ModelShotGameFsmState.REX_FWEAPON);
        }
        
        public void OpenRexPetCanvas()
        {
            GameController_ModelShot.instance.FSM.SwitchToState((int) ModelShotGameFsmState.LOAD_REX_Empty);
            GameController_ModelShot.instance.SetNextState((int) ModelShotGameFsmState.REX_PET);
        }
        
        public void OpenRexMonsterCanvas()
        {
            GameController_ModelShot.instance.FSM.SwitchToState((int) ModelShotGameFsmState.LOAD_REX_Empty);
            GameController_ModelShot.instance.SetNextState((int) ModelShotGameFsmState.REX_MONSTER);
        }
        
        public void OpenFreeShotCanvas()
        {
            GameController_ModelShot.instance.FSM.SwitchToState((int) ModelShotGameFsmState.LOAD_REX_Empty);
            GameController_ModelShot.instance.SetNextState((int) ModelShotGameFsmState.SELECT_MODEL);
        }
        
        public override void Initialize()
        {
            Rex_Weapon_Btn.onClick.AddListener(OpenRexWeaponCanvas);
            Rex_Equipment_Btn.onClick.AddListener(OpenRexEquipmentCanvas);
            Rex_Suit_Btn.onClick.AddListener(OpenRexSuitCanvas);
            Rex_FashionGuxJian_Btn.onClick.AddListener(OpenRexFGuaJiananvas);
            Rex_FashionWeapon_Btn.onClick.AddListener(OpenRexFWeaponCanvas);
            Rex_Pet_Btn.onClick.AddListener(OpenRexPetCanvas);
            Free_Btn.onClick.AddListener(OpenFreeShotCanvas);
            Rex_Monster_Btn.onClick.AddListener(OpenRexMonsterCanvas);
        }

        public override void Release()
        {
            Rex_Weapon_Btn.onClick.RemoveListener(OpenRexWeaponCanvas);
            Rex_Equipment_Btn.onClick.RemoveListener(OpenRexEquipmentCanvas);
            Rex_Suit_Btn.onClick.RemoveListener(OpenRexSuitCanvas);
            Rex_FashionGuxJian_Btn.onClick.RemoveListener(OpenRexFGuaJiananvas);
            Rex_FashionWeapon_Btn.onClick.RemoveListener(OpenRexFWeaponCanvas);
            Rex_Pet_Btn.onClick.RemoveListener(OpenRexPetCanvas);
            Free_Btn.onClick.RemoveListener(OpenFreeShotCanvas);
            Rex_Monster_Btn.onClick.RemoveListener(OpenRexMonsterCanvas);
        }
    }
}