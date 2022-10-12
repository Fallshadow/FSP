using fsp.LittleSceneEnvironment;
using fsp.modelshot.ui;
using fsp.ui;
using fsp.utility;

namespace fsp.modelshot
{
    public class ModelShotRexPet : State<GameController>
    {
        public override void Enter()
        {
            UiManager.instance.OpenUi<RexPetCanvas>();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}