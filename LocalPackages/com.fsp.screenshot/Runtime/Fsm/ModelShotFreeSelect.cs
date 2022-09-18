using fsp.modelshot.ui;
using fsp.ui;
using fsp.utility;

namespace fsp.modelshot
{
    public class ModelShotFreeSelect : State<GameController>
    {
        private bool isSuccComplete = false;

        public override void Enter()
        {
            UiManager.instance.OpenUi<SelectModelCanvas>();
            isSuccComplete = false;
        }

        public override void Update()
        {
            base.Update();
            if (isSuccComplete) return;

            // Scene tempScene = SceneManager.GetSceneByName("NewFreeModelView");
            // GameObject[] gos = tempScene.GetRootGameObjects();
            // foreach (var go in gos)
            // {
            //     if (go.name == "MainCamera" || go.name == "Main Camera")
            //     {
            //         UiManager.instance.SetUiCameraAdditionData(true, go.transform.GetComponent<Camera>());
            //         isSuccComplete = true;
            //         break;
            //     }
            // }
        }
    }
}