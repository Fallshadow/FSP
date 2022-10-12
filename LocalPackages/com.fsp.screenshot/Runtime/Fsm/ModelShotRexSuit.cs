using fsp.LittleSceneEnvironment;
using fsp.modelshot.ui;
using fsp.ui;
using fsp.utility;

namespace fsp.modelshot
{
    public class ModelShotRexSuit : State<GameController>
    {
        public override void Enter()
        {
            UiManager.instance.OpenUi<RexSuitCanvas>();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}