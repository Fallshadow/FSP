using fsp.assetbundlecore;
using fsp.debug;
using fsp.utility;

namespace fsp.modelshot.Game
{
    public class GameController_ModelShot : GameController
    {
        protected override void initialize()
        {
            initializeStates();
            FSM.SwitchToState((int) ModelShotGameFsmState.ENTRYDEBUG);
        }

        private void initializeStates()
        {
            FSM.Initialize(this);
            FSM.AddState((int) ModelShotGameFsmState.ENTRYDEBUG, new ModelShotEntry());
            FSM.AddState((int) ModelShotGameFsmState.LOAD_REX_WEAPON, new LoadRexWeaponScene(this));
            FSM.AddState((int) ModelShotGameFsmState.REX_WEAPON, new ModelShotRexWeapon());
            FSM.AddState((int) ModelShotGameFsmState.ENTRY_SELECT_MODEL, new ModelShotFreeSelect());
        }

        protected override void InitResourceLoaderProxy()
        {
            if (DebugConfig.instance.IsDisableAssetBundle)
            {
                ResourceLoaderProxy.instance.InitEditorLoadManager(new FastModeLoaderManager_ModelShot());
            }
            else
            {
                ResourceLoaderProxy.instance.InitRunTimeLoadManager();
            }
        }

        protected override bool IsResourceLoaderProxyInitOk()
        {
            return ResourceLoaderProxy.instance.IsOK;
        }
    }
}