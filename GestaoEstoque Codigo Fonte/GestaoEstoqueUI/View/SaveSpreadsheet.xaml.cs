namespace GestaoEstoqueUI.View;

public partial class SaveSpreadsheet : ContentPage
{
	public SaveSpreadsheet(SpreadsheetViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}