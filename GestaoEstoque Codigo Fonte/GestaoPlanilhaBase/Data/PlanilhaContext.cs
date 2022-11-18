using Microsoft.EntityFrameworkCore;

namespace GestaoPlanilhaBase.Data
{
    public class PlanilhaContext : DbContext
    {
        public DbSet<Estoque> Estoques { get; set; }

        public PlanilhaContext(DbContextOptions<PlanilhaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Estoque>().Property(x => x.Id).HasDefaultValue(new int());

            modelBuilder.Entity<Estoque>(b =>
            {
                b.HasKey(x => x.Id);

                b.Property(p => p.Produto).HasColumnName("Nome");

                b.Property(p => p.Quantidade).HasColumnName("Quantidade");

                b.Property(p => p.ValorUnitario).HasColumnName("Valor Unitario");

                b.Property(p => p.ValorTotal).HasColumnName("Valor Total");

                b.Property(c => c.Id).HasColumnName("Id");

                b.ToTable("Produto");
            });
        }
    }
}
