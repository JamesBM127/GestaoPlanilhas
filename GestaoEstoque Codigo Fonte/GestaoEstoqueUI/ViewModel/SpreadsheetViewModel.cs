using GestaoEstoqueUI.Interfaces;
using GestaoPlanilhaBase;
using GestaoPlanilhaBase.Data;
using GestaoPlanilhaBase.Servico;

namespace GestaoEstoqueUI.ViewModel;

public partial class SpreadsheetViewModel : BaseViewModel
{
    public readonly PlanilhaContext Context;
    public readonly IFolderPicker FolderPicker;

    public SpreadsheetViewModel(PlanilhaContext planilhaContext, IFolderPicker folderPicker)
    {
        Title = "Title Spreadsheet";
        Context = planilhaContext;
        FolderPicker = folderPicker;
    }

    #region Criar
    [RelayCommand]
    async Task CreateSpreadsheetFromDb()
    {
        try
        {
            List<Estoque> estoqueList = await new ProdutoServico(Context).ListAsync();

            bool sucess = GetSpreadsheetFromDb(estoqueList);

            if (sucess)
                await Shell.Current.DisplayAlert("Criação", "Planilha criada com sucesso", "Ok");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Criação", "Falha na criação da planilha", "Ok");
        }
    }

    [RelayCommand]
    async Task CreateSpreadsheetTemplate()
    {
        try
        {
            bool sucess = GetSpreadsheetFromDb();

            if (sucess)
                await Shell.Current.DisplayAlert("Criação", "Planilha criada com sucesso", "Ok");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Criação", "Falha na criação da planilha", "Ok");
        }
    }

    [RelayCommand]
    async Task<string> GetFolderPath()
    {
        try
        {
            Path = await FolderPicker.PickFolder();

            return Path;
        }
        catch (Exception ex)
        {
        }

        return null;
    }

    private bool GetSpreadsheetFromDb(List<Estoque> estoqueList = null)
    {
        Operacoes<Estoque> operacoes = GetOperacoes();
        bool resultado = operacoes.CriarPlanilha(estoqueList, Path, SpreadsheetName);
        return resultado;
    }
    #endregion

    #region Salvar
    [RelayCommand]
    async Task GetFilePath(PickOptions options)
    {
        try
        {
            IsBusy = true;

            FileResult result = await FilePicker.Default.PickAsync(options);

            Path = result.FullPath;

        }
        catch (Exception ex)
        {

        }
    }

    [RelayCommand]
    async Task SaveSpreadsheetInDb()
    {
        try
        {
            bool sucess = await SalvarPlanilha(Path);

            if (sucess)
                await Shell.Current.DisplayAlert("Salvamento", "Planilha salva com sucesso", "Ok");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Salvamento", "Falha no Salvamento", "Ok");
        }
        finally
        {
            await GoToBack();
        }
    }

    private async Task<bool> SalvarPlanilha(string path)
    {
        Operacoes<Estoque> operacoes = GetOperacoes();
        bool resultado = await operacoes.SalvarPlanilhaNoDb(path);
        return resultado;
    }
    #endregion

    #region Navegar
    [RelayCommand]
    async Task GoToBack()
    {
        try
        {
            await Shell.Current.GoToAsync("..", false);
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            Path = string.Empty;
        }
    }
    #endregion

    #region Servicos Auxiliares
    private Operacoes<Estoque> GetOperacoes()
    {
        PlanilhaServico<Estoque> planilhaServico = GetServico();
        return new Operacoes<Estoque>(planilhaServico);
    }

    private PlanilhaServico<Estoque> GetServico()
    {
        PlanilhaServico<Estoque> planilhaServico = new PlanilhaServico<Estoque>(new ProdutoServico(Context));
        return planilhaServico;
    }

    private ProdutoServico GetProdutoServico()
    {
        return new ProdutoServico(Context);
    }
    #endregion
}
