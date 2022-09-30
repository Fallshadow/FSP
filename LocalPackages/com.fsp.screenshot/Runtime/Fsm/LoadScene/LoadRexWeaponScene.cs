using fsp.utility;
using UnityEngine;

namespace fsp.modelshot
{
    public class LoadRexWeaponScene : LoadScene
    {
        public LoadRexWeaponScene(MonoBehaviour monoP) : base(monoP)
        {
            scene_Name = "ModelShot_RexWeapon";
        }

        protected override void onEnter()
        {
            
        }

        protected override void onExit()
        {
            
        }

        protected override void onLoading()
        {
            
        }

        protected override void onLoadingEnd()
        {
            m_fsm.SwitchToState((int)ModelShotGameFsmState.REX_WEAPON);
        }

        protected override int levelId()
        {
            return -1;
        }
    }
}