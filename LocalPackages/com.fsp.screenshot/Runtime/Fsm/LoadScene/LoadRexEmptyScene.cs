using fsp.LittleSceneEnvironment;
using fsp.modelshot.Game;
using fsp.utility;
using UnityEngine;

namespace fsp.modelshot
{
    public class LoadRexEmptyScene : LoadScene
    {
        public LoadRexEmptyScene(MonoBehaviour monoP) : base(monoP)
        {
            scene_Name = "ModelShot_RexEmpty";
        }

        protected override void onEnter()
        {
            
        }

        protected override void onExit()
        {
            
        }

        protected override void onLoading()
        {
            LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——斩裂刀");
        }

        protected override void onLoadingEnd()
        {
            m_fsm.SwitchToState(GameController_ModelShot.instance.GetNextState());
        }

        protected override int levelId()
        {
            return -1;
        }
    }
}