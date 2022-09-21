using System.Runtime.InteropServices;

namespace fsp.eutility
{
    public partial class EUtility
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetSystemMetrics(int nIndex);

        public static int SM_CXSCREEN = 0; // 主屏幕分辨率宽度
        public static int SM_CYSCREEN = 1; // 主屏幕分辨率高度
        public static int SM_CYCAPTION = 4; // 标题栏高度
        public static int SM_CXFULLSCREEN = 16; // 最大化窗口宽度（减去任务栏）
        public static int SM_CYFULLSCREEN = 17; // 最大化窗口高度（减去任务栏）
        public static int SM_CMONITORS = 80; // 显示器数量
    }
}