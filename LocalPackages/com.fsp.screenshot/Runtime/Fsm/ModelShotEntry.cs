using fsp.modelshot.ui;
using fsp.ui;
using fsp.utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace fsp.modelshot
{
    public class ModelShotEntry : State<GameController>
    {
        public override void Enter()
        {
            UiManager.instance.OpenUi<DebugCanvas>();
        }
    }
}