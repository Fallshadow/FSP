using fsp.ui;

namespace fsp.modelshot.ui
{
    public class CaptureScreenShotCanvasBase : FullScreenCanvasBase
    {
        public CommonFuncMono commonFuncMono = null;
        public AvatarDragArea DragArea = null;
        
        public override void Initialize()
        {
            base.Initialize();
            commonFuncMono?.Initialize(this);
        }

        public override void Release()
        {
            base.Release();
            commonFuncMono?.Release();
        }
    }
}