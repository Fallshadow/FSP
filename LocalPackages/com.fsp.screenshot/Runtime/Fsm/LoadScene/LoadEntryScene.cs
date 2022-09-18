using fsp.utility;
using UnityEngine;

namespace fsp.modelshot
{
    public class LoadEntryScene : LoadScene
    {
        public LoadEntryScene(MonoBehaviour monoP) : base(monoP)
        {
            scene_Name = "ModelShotEntry";
        }

        protected override void onEnter()
        {
            
        }

        protected override void onExit()
        {
            
        }

        protected override void onLoading()
        {
            
        }

        protected override void onLoadingEnd()
        {
            
        }

        protected override int levelId()
        {
            return -1;
        }
    }
}