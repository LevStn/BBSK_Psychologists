using BBSK_Psycho.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace BBSK_Psycho.DataLayer;

public class BBSK_PsychoContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Psychologist> Psychologists { get; set; }
    public DbSet<Schedule> Schedules { get; set; }

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
                .WithMany(c => c.Orders);
            entity
                .HasOne(o => o.Psychologist)
                .WithMany(c => c.Orders);

            entity
                .Property(p => p.Cost)
                .HasPrecision(18, 2);

        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable(nameof(Comment));
            entity.HasKey(o => o.Id);

            entity
                .HasOne(o => o.Client)
                .WithMany(c => c.Comments);

            entity
                .HasOne(o => o.Psychologist)
                .WithMany(c => c.Comments);

        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable(nameof(Client));
            entity.HasKey(c => c.Id);


        });

        modelBuilder.Entity<Psychologist>(entity =>
        {
            entity.ToTable(nameof(Psychologist));
            entity.HasKey(c => c.Id);

            entity.Property(p => p.Price)
                    .HasPrecision(18, 2);

        });

       

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable(nameof(Schedule));
            entity.HasKey(c => c.Id);

            entity
                .HasOne(o => o.Psychologist)
                .WithMany(c => c.Schedules);
        });


    }
} 