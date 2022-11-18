using ClosedXML.Excel;

namespace GestaoPlanilhaBase.Servico
{
    public static class SheetConfigServico
    {
        public static void SetDataTypeDasColunas(this IXLWorksheet worksheet, int qtdChars)
        {
            IdColunaConfiguracao(ref worksheet, qtdChars);
            ProdutoColunaConfiguracao(ref worksheet);
            QuantidadeColunaConfiguracao(ref worksheet);
            ValorUnitarioColunaConfiguracao(ref worksheet);
            ValorTotalColunaConfiguracao(ref worksheet);
        }

        private static void IdColunaConfiguracao(ref IXLWorksheet worksheet, int qtdChars)
        {
            IXLColumn idColumn = worksheet.Column("A");
            qtdChars += 100;
            double largura = Convert.ToDouble(qtdChars.ToString().Length);
            idColumn.Width = largura;
        }

        private static void ProdutoColunaConfiguracao(ref IXLWorksheet worksheet)
        {
            IXLColumn produtoColumn = worksheet.Column("B");
            produtoColumn.Width = 30;
        }

        private static void QuantidadeColunaConfiguracao(ref IXLWorksheet worksheet)
        {
            IXLColumn quantidadeColumn = worksheet.Column("C");
            quantidadeColumn.Width = 12;
        }

        private static void ValorUnitarioColunaConfiguracao(ref IXLWorksheet worksheet)
        {
            IXLColumn valorUnitColumn = worksheet.Column("D");
            worksheet.Column("D").Style.NumberFormat.Format = "R$ #,##0.00_);R$ [Red](#,##0.00)";
            worksheet.Column("D").DataType = XLDataType.Number;
            valorUnitColumn.Width = 17;
        }

        private static void ValorTotalColunaConfiguracao(ref IXLWorksheet worksheet)
        {
            IXLColumn valorTotalColumn = worksheet.Column("E");
            worksheet.Column("E").Style.NumberFormat.Format = "R$ #,##0.00_);R$ [Red](#,##0.00)";
            worksheet.Column("E").DataType = XLDataType.Number;
            valorTotalColumn.Width = 17;
        }
    }
}
