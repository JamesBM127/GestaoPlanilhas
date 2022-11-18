using ClosedXML.Excel;
using GestaoPlanilhaBase.Servico.BookTrabalho;

namespace GestaoPlanilhaBase.Servico
{
    public class PlanilhaServico<TEntity> where TEntity : class
    {
        private readonly ProdutoServico ProdutoServico;

        public PlanilhaServico(ProdutoServico produtoServico)
        {
            ProdutoServico = produtoServico;
        }

        public bool CriarPlanilha(string pathPlanilhasXlsx, string fileName)
        {
            try
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    workbook.CriarBook();
                    Directory.CreateDirectory(pathPlanilhasXlsx);
                    string fullPath = Path.Combine(pathPlanilhasXlsx, fileName + ".xlsx");
                    workbook.SaveAs(fullPath);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool CriarPlanilha(List<TEntity> entities, string pathPlanilhasXlsx, string fileName)
        {
            try
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    workbook.CriarBook(entities);
                    Directory.CreateDirectory(pathPlanilhasXlsx);
                    string fullPath = Path.Combine(pathPlanilhasXlsx, fileName + ".xlsx");
                    workbook.SaveAs(fullPath);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SalvarPlanilha(string pathPlanilhasXlsx)
        {
            try
            {
                XLWorkbook xls = new XLWorkbook(pathPlanilhasXlsx);
                await xls.SalvarBook(ProdutoServico);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
