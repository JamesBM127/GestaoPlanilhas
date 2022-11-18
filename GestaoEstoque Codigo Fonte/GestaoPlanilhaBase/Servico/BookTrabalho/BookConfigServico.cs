using ClosedXML.Excel;
using System.Reflection;
using System.Text;

namespace GestaoPlanilhaBase.Servico.BookTrabalho
{
    public static class BookConfigServico
    {
        public async static Task SalvarBook(this XLWorkbook workbook, ProdutoServico servico)
        {
            IXLWorksheet planilha = workbook.Worksheets.First(w => w.Name == typeof(Estoque).Name);

            int totalLinhas = planilha.Rows().Count();

            byte asciiNum = 65;
            byte blankSpace = 0;

            for (int linha = 2; linha <= totalLinhas && blankSpace < 10; linha++, asciiNum = 65)
            {

                bool isIsValid = int.TryParse(GetCell(ref planilha, linha, asciiNum++), out int id);

                if (!isIsValid)
                {
                    totalLinhas++;
                    blankSpace++;
                    continue;
                }
                blankSpace = 0;

                Estoque estoque = new Estoque();

                estoque.Id = id;
                estoque.Produto = GetCell(ref planilha, linha, asciiNum++);
                estoque.Quantidade = int.Parse(GetCell(ref planilha, linha, asciiNum++));
                estoque.ValorUnitario = double.Parse(GetCell(ref planilha, linha, asciiNum++));
                estoque.ValorTotal = estoque.Quantidade * estoque.ValorUnitario;

                if (await servico.ExistsAsync(estoque.Id))
                {
                    await servico.UpdateAsync<Estoque>(estoque);
                }
                else
                {
                    await servico.AddAsync<Estoque>(estoque);
                }
            }
        }

        public static void CriarBook(this XLWorkbook workbook)
        {
            IXLWorksheet entityPlanilha = workbook.Worksheets.Add(typeof(Estoque).Name);

            entityPlanilha.SetDataTypeDasColunas(1);
            PropertyInfo[] entityProperties = new Estoque().GetType().GetProperties();
            entityPlanilha.CriarSheet<Estoque>(entityProperties);
        }

        public static void CriarBook<TEntity>(this XLWorkbook workbook, List<TEntity> entities) where TEntity : class
        {
            IXLWorksheet entityPlanilha = workbook.Worksheets.Add(typeof(TEntity).Name);

            entityPlanilha.SetDataTypeDasColunas(entities.Count());

            PropertyInfo[] entityProperties = entities[0].GetType().GetProperties();
            entityPlanilha.CriarSheet<TEntity>(entityProperties);

            entityPlanilha.SeedSheet(entities);
        }

        private static void CriarSheet<TEntity>(this IXLWorksheet worksheet, PropertyInfo[] entityProperties) where TEntity : class
        {
            byte asciiNum = 65;

            foreach (PropertyInfo item in entityProperties)
            {
                worksheet.Cell($"{GetCellLetter(asciiNum++)}1").Value = item.Name;
            }
        }

        private static void SeedSheet<TEntity>(this IXLWorksheet worksheet, List<TEntity> entities) where TEntity : class
        {
            PropertyInfo[] entityProperties;
            try
            {
                entityProperties = entities.FirstOrDefault().GetType().GetProperties();
            }
            catch
            {
                return;
            }

            byte coluna = 65;
            int linha = 2;

            foreach (TEntity entity in entities)
            {
                foreach (PropertyInfo item in entityProperties)
                {
                    worksheet.Cell($"{GetCellLetter(coluna++)}{linha}").Value = item.GetValue(entity);
                }
                coluna = 65;
                linha++;
            }
        }

        private static string GetCell(ref IXLWorksheet planilha, int linha, byte asciiNum)
        {
            string cell = planilha.Cell($"{GetCellLetter(asciiNum++)}{linha}").Value.ToString();

            return cell;
        }

        public static string GetCellLetter(byte asciiNum)
        {
            byte[] bytes = new byte[] { asciiNum };
            string coluna = Encoding.ASCII.GetString(bytes);
            return coluna;
        }
    }
}
