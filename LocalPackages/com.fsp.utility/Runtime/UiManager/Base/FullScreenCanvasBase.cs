using UnityEngine;
using UnityEngine.UI;

namespace fsp.ui
{
    [RequireComponent(typeof(GraphicRaycaster))]
    public abstract class FullScreenCanvasBase : InteractableUiBase
    {
        public override UiOpenType OpenType => UiOpenType.UOT_FULL_SCREEN;
    }
}