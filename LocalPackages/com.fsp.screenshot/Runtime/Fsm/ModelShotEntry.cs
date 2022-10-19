using fsp.LittleSceneEnvironment;
using fsp.modelshot.ui;
using fsp.ui;
using fsp.utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace fsp.modelshot
{
    public class ModelShotEntry : State<GameController>
    {
        private bool isInit = false;
        public override void Enter()
        {
            LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——斩裂刀");
            isInit = false;
        }
        

        public override void LateUpdate()
        {
            base.LateUpdate();

            if (!isInit)
            {
                isInit = true;
                UiManager.instance.OpenUi<DebugCanvas>();
            }
        }
    }
}