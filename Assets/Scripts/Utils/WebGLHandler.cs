using System.Runtime.InteropServices;
public class WebGLHandler
{
    [DllImport("__Internal")]
    public static extern bool IsMobileBrowser();
}
