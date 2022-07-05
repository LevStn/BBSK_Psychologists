using BBSK_Psycho.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace BBSK_Psycho.DataLayer;

public class BBSK_PsychoContext : DbContext
{
    public DbSet<Client> clients { get; set; }
    public DbSet<Comment> comments { get; set; }

    public DbSet<Order> orders { get; set; }

    public BBSK_PsychoContext(DbContextOptions<BBSK_PsychoContext> option)
        : base(option)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable(nameof(Order));
            entity.HasKey(o => o.Id);

            entity
                .HasOne(o => o.Client)
                .WithMany(c => c.Order);

        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable(nameof(Client));
            entity.HasKey(c => c.Id);


        });

    }
} 