using GestaoEstoqueUI.Interfaces;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;

namespace GestaoEstoqueUI.Platforms.Windows
{
    public class WindowSizeTCC : IWindowSizeTCC
    {
        public void SetSize(ref MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(wndLifeCycleBuilder =>
                {
                    wndLifeCycleBuilder.OnWindowCreated(window =>
                    {
                        IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                        AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
                        if (winuiAppWindow.Presenter is OverlappedPresenter p)
                        {
                            p.IsResizable = false;
                            p.IsMaximizable = false;
                            p.IsMinimizable = false;

                            const int width = 900;
                            const int height = 450;
                            winuiAppWindow.MoveAndResize(new RectInt32(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height));
                        }
                        else
                        {
                            const int width = 600;
                            const int height = 600;
                            winuiAppWindow.MoveAndResize(new RectInt32(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height));
                        }
                    });
                });
            });
        }
    }
}
