using UnityEngine;
using UnityEngine.UI;

namespace fsp.ui
{
    [RequireComponent(typeof(GraphicRaycaster))]
    public abstract class InteractableUiBase : UiBase
    {
        [SerializeField] protected GraphicRaycaster graphicRaycaster;

        protected override void Reset()
        {
            base.Reset();
            graphicRaycaster = GetComponent<GraphicRaycaster>();
        }
    }
}