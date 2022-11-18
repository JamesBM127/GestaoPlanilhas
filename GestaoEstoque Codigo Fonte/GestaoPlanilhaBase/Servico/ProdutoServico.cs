using GestaoPlanilhaBase.Data;
using Microsoft.EntityFrameworkCore;

namespace GestaoPlanilhaBase.Servico
{
    public class ProdutoServico
    {
        private readonly PlanilhaContext PlanilhaContext;

        public ProdutoServico(PlanilhaContext planilhaContext)
        {
            PlanilhaContext = planilhaContext;
        }

        public async Task<bool> AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await PlanilhaContext.AddAsync(entity);
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : Estoque
        {
            try
            {
                PlanilhaContext.Estoques.Update(entity);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
            return await SaveChangesAsync();
        }

        public async Task<List<Estoque>> ListAsync()
        {
            List<Estoque> produtos = new List<Estoque>();
            try
            {
                produtos = await PlanilhaContext.Estoques.AsNoTracking().Where(prod => prod.Id > 0).ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return produtos;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            Estoque? entity = await PlanilhaContext.Estoques.AsNoTracking().FirstOrDefaultAsync<Estoque>(x => x.Id == id);

            return entity != null;
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await PlanilhaContext.SaveChangesAsync() > 0;
        }
    }
}
