using UnityEngine;

namespace Tools
{
    /// <summary>
    /// 请将此脚本挂载在主相机下
    /// 该相机通过事件切换相机模式，应用CineMachine
    /// 平时用配置里默认的CineMachine，当接收到事件会切换到对应相机组
    /// 对应事件在 CameraEvent 里
    /// </summary>
    public partial class CCFreeLook : MonoBehaviour
    {
        [Header("是否开启上下移动")] public bool IsRotCameraXOpen = false;
        [Header("是否开启左右移动")] public bool IsRotCameraYOpen = false;
        
        
    }
}