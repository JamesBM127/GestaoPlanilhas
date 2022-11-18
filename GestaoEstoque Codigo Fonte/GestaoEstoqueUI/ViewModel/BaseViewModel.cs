namespace GestaoEstoqueUI.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    [ObservableProperty]
    string title;

    [ObservableProperty]
    string path;

    [ObservableProperty]
    string spreadsheetName;

    public bool IsNotBusy => !IsBusy;

    public BaseViewModel()
    {

    }
}