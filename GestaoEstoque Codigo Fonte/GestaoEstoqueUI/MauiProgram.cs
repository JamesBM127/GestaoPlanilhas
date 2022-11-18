using GestaoEstoqueUI.Interfaces;
using GestaoPlanilhaBase.Data;

namespace GestaoEstoqueUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        #region Criar DB
        string connectionString = "Server=.;Initial Catalog=Estoque;Integrated Security=true;";

        builder.Services.CriarDatabase<PlanilhaContext>(builder.Configuration, connectionString);
        #endregion

#if WINDOWS
		builder.Services.AddTransient<IFolderPicker, Platforms.Windows.FolderPicker>();
        IWindowSizeTCC windowSizeTCC = new Platforms.Windows.WindowSizeTCC();
        windowSizeTCC.SetSize(ref builder);
#endif

        builder.Services.AddTransient<PlanilhaContext>();

        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddTransient<SpreadsheetViewModel>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<CreateSpreadsheet>();
        builder.Services.AddTransient<SaveSpreadsheet>();

        return builder.Build();
    }
}
