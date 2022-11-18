namespace GestaoEstoqueUI.ViewModel;

public partial class MainPageViewModel : BaseViewModel
{
    [RelayCommand]
    async Task GoToCreateSpreadsheetAsync()
    {
        try
        {
            await Shell.Current.GoToAsync($"{nameof(CreateSpreadsheet)}", false);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    [RelayCommand]
    async Task GoToSaveSpreadsheetAsync()
    {
        try
        {
            await Shell.Current.GoToAsync($"{nameof(SaveSpreadsheet)}", false);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}

