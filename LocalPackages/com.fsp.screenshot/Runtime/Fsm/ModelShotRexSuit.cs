using fsp.LittleSceneEnvironment;
using fsp.modelshot.ui;
using fsp.time;
using fsp.ui;
using fsp.utility;

namespace fsp.modelshot
{
    public class ModelShotRexSuit : State<GameController>
    {
        public override void Enter()
        {
            LittleEnvironmentCreator.instance.SwitchToEnvironment("环境——时装套装");
            TimeManager.instance.AddCountDownTimer(false, "Load", 1, 
                () => { UiManager.instance.OpenUi<RexSuitCanvas>(); });
        }
    }
}