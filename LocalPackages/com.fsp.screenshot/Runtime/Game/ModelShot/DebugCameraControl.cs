using System.IO;
using fsp.evt;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace fsp.modelshot
{
    public class DebugCameraControl : SingletonMonoBehaviorNoDestroy<DebugCameraControl>
    {
        [Header("配置项")] public float ViewCameraSpeed = 2f;
        public GraphicsFormat MyGraphicsFormat = GraphicsFormat.R16G16B16_UInt;
        public TextureFormat MyTextureFormat = TextureFormat.ARGB32;
        public DefaultFormat MyDefaultFormat = DefaultFormat.HDR;
        [Header("观察项")] 
        public Camera ViewCamera = null;
        public bool isMove = false;
        
        private void Update()
        {
            isMove = false;
            if (ViewCamera == null)
            {
                ViewCamera = Camera.main;
            }

            if (ViewCamera == null)
            {
                return;
            }

            if (Input.GetKey(KeyCode.A))
            {
                isMove = true;
                Vector3 cachePos = ViewCamera.transform.position;
                ViewCamera.transform.position =
                    new Vector3(cachePos.x + ViewCameraSpeed * Time.deltaTime, cachePos.y, cachePos.z);
            }

            if (Input.GetKey(KeyCode.D))
            {
                isMove = true;
                Vector3 cachePos = ViewCamera.transform.position;
                ViewCamera.transform.position =
                    new Vector3(cachePos.x - ViewCameraSpeed * Time.deltaTime, cachePos.y, cachePos.z);
            }

            if (Input.GetKey(KeyCode.W))
            {
                isMove = true;
                Vector3 cachePos = ViewCamera.transform.position;
                ViewCamera.transform.position =
                    new Vector3(cachePos.x, cachePos.y + ViewCameraSpeed * Time.deltaTime, cachePos.z);
            }

            if (Input.GetKey(KeyCode.S))
            {
                isMove = true;
                Vector3 cachePos = ViewCamera.transform.position;
                ViewCamera.transform.position =
                    new Vector3(cachePos.x, cachePos.y - ViewCameraSpeed * Time.deltaTime, cachePos.z);
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                isMove = true;
                var position = ViewCamera.transform.position;
                position = new Vector3(
                    position.x,
                    position.y,
                    position.z - Input.GetAxis("Mouse ScrollWheel"));
                ViewCamera.transform.position = position;
            }

            if (isMove)
            {
                EventManager.instance.Send(EventGroup.CAMERA, (short)CameraEvent.DEBUG_CAMERA_MOVE);
            }
        }
        
        
        public void ExportCaptureScreenShot(string defaultName = "New PNG")
        {
            string path = getSelectExportPath($"导出截图",$"{defaultName}","png");
            if (path.Length != 0)
            {
                captureScreenShot(path);
            }
        }

        // 如果返回的路径不是"",说明已经选择好了路径，可以进行导出了
        private string getSelectExportPath(
            string title,
            string defaultName,
            string extension)
        {
            string path = EditorUtility.SaveFilePanel(title, "", defaultName, extension);
            return path;
        }
        TextureCreationFlags flags;
        private void captureScreenShot(string path)
        {
            bool allowHDR = ViewCamera.allowHDR;
            ViewCamera.allowHDR = false;
            int width = Screen.width, height = Screen.height;
            // Screen.SetResolution(width,height,FullScreenMode.Windowed);
            RenderTexture rt = new RenderTexture(4000, 2000, 24, MyGraphicsFormat);
            ViewCamera.targetTexture = rt;
            ViewCamera.Render();
            RenderTexture.active = rt;
            Texture2D screenShot = new Texture2D(4000, 2000, MyTextureFormat, false);
            screenShot.ReadPixels(new Rect(0, 0, 4000, 2000), 0, 0);
            screenShot.Apply();
            RenderTexture.active = null;
            ViewCamera.targetTexture = null;
            byte[] bytes = screenShot.EncodeToPNG();
            File.WriteAllBytes(path,bytes);
            // Screen.SetResolution(1920,1080,FullScreenMode.Windowed);
            ViewCamera.allowHDR = allowHDR;
        }
    }
}