using fsp.modelshot.Game.ObjectStylingDesigne;
using fsp.modelshot.ui;
using fsp.ui;
using fsp.utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace fsp.modelshot
{
    public class ModelShotRexWeapon : State<GameController>
    {
        public override void Enter()
        {
            UiManager.instance.OpenUi<RexWeaponCanvas>();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}