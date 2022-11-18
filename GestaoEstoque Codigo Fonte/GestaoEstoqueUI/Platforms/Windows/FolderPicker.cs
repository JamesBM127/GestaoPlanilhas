using GestaoEstoqueUI.Interfaces;
using WPF = Windows.Storage.Pickers.FolderPicker;

namespace GestaoEstoqueUI.Platforms.Windows
{
    public class FolderPicker : IFolderPicker
    {
        public async Task<string> PickFolder()
        {
            WPF folderPicker = new WPF();

            folderPicker.FileTypeFilter.Add("*");

            var hwnd = ((MauiWinUIWindow)Application.Current.Windows[0].Handler.PlatformView).WindowHandle;

            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);

            var result = await folderPicker.PickSingleFolderAsync();

            return result?.Path;
        }
    }
}
