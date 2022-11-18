namespace GestaoEstoqueUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(SaveSpreadsheet), typeof(SaveSpreadsheet));
        Routing.RegisterRoute(nameof(CreateSpreadsheet), typeof(CreateSpreadsheet));
    }
}
