using GestaoPlanilhaBase.Servico;

namespace GestaoPlanilhaBase
{
    public class Operacoes<TEntity> where TEntity : class
    {
        private readonly PlanilhaServico<TEntity> PlanilhaServico;

        public Operacoes(PlanilhaServico<TEntity> planilhaServico)
        {
            PlanilhaServico = planilhaServico;
        }

        public bool CriarPlanilha(string pathPlanilhasXlsx, string fileName)
        {
            return PlanilhaServico.CriarPlanilha(pathPlanilhasXlsx, fileName);
        }

        public bool CriarPlanilha(List<TEntity> entities, string pathPlanilhasXlsx, string fileName)
        {
            bool created = false;

            if (entities == null || entities.Count == 0)
            {
                created = CriarPlanilha(pathPlanilhasXlsx, fileName);
            }
            else
            {
                created = PlanilhaServico.CriarPlanilha(entities, pathPlanilhasXlsx, fileName);
            }

            return created;
        }

        public async Task<bool> SalvarPlanilhaNoDb(string pathPlanilhasXlsx)
        {
            return await PlanilhaServico.SalvarPlanilha(pathPlanilhasXlsx);
        }
    }
}
