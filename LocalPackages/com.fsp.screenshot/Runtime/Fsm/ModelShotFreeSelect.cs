using fsp.LittleSceneEnvironment;
using fsp.modelshot.ui;
using fsp.time;
using fsp.ui;
using fsp.utility;

namespace fsp.modelshot
{
    public class ModelShotFreeSelect : State<GameController>
    {
        public override void Enter()
        {
            LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——斩裂刀");
            TimeManager.instance.AddCountDownTimer(false, "Load", 1, 
                () => { UiManager.instance.OpenUi<SelectModelCanvas>(); });
        }
    }
}