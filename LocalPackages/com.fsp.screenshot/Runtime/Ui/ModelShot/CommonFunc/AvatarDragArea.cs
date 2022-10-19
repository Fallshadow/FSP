using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace fsp.modelshot.ui
{
    public class AvatarDragArea : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        public event Action<Vector2> OnDragHandler = null;
        public event Action<Vector2> OnEndDragHandler = null;

        [Header("滑动阻力系数, 数值越小 旋转速度越慢")]
        [SerializeField] private float sensitivityFactor = 0.5f;

        public void OnDrag(PointerEventData eventData)
        {
            OnDragHandler?.Invoke(-1 * eventData.delta * sensitivityFactor);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnEndDragHandler?.Invoke(-1 * eventData.delta * sensitivityFactor);
        }

    }
}
