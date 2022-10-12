using fsp.utility;
using UnityEngine;

namespace fsp.modelshot
{
    public class LoadModelShotEntry : LoadScene
    {
        public LoadModelShotEntry(MonoBehaviour monoP) : base(monoP)
        {
            scene_Name = "ModelShot_Entry";
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
            m_fsm.SwitchToState((int)ModelShotGameFsmState.ENTRYDEBUG);
        }

        protected override int levelId()
        {
            return -1;
        }
    }
}