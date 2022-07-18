using Microsoft.EntityFrameworkCore;


namespace TrayCorp.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<Produto> Produtos { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Produto>()
				.Property(p => p.Nome)
				.HasMaxLength(80);

			modelBuilder.Entity<Produto>()
				.Property(p => p.Preco)
				.HasPrecision(10,2);


		}


	}
}
