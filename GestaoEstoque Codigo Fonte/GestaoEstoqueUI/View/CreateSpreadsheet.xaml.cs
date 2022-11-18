namespace GestaoEstoqueUI.View;

public partial class CreateSpreadsheet : ContentPage
{
	public CreateSpreadsheet(SpreadsheetViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}