using GestaoEstoqueUI.ViewModel;

namespace GestaoEstoqueUI.View;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

