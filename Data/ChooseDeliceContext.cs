using ChooseDelice.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ChooseDelice.Data;

public partial class ChooseDeliceContext : DbContext
{
    public ChooseDeliceContext()
    {
    }

    public ChooseDeliceContext(DbContextOptions<ChooseDeliceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Delice> Delices { get; set; }
 

    public virtual DbSet<PartialDecision> PartialDecisions { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=123456;database=choose_delice");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Delice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("delice");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alune).HasColumnName("alune");
            entity.Property(e => e.Arahide).HasColumnName("arahide");
            entity.Property(e => e.Banane).HasColumnName("banane");
            entity.Property(e => e.BazaBiscuiti).HasColumnName("baza_biscuiti");
            entity.Property(e => e.Branza).HasColumnName("branza");
            entity.Property(e => e.Cafea).HasColumnName("cafea");
            entity.Property(e => e.Caju).HasColumnName("caju");
            entity.Property(e => e.Caramel).HasColumnName("caramel");
            entity.Property(e => e.Cocos).HasColumnName("cocos");
            entity.Property(e => e.CremaCiocolata).HasColumnName("crema_ciocolata");
            entity.Property(e => e.CremaVanilie).HasColumnName("crema_vanilie");
            entity.Property(e => e.DietaVegana).HasColumnName("dieta_vegana");
            entity.Property(e => e.Dovleac).HasColumnName("dovleac");
            entity.Property(e => e.GlazuraCiocolata).HasColumnName("glazura_ciocolata");
            entity.Property(e => e.IntGluten).HasColumnName("int_gluten");
            entity.Property(e => e.IntLactoza).HasColumnName("int_lactoza");
            entity.Property(e => e.Lamaie).HasColumnName("lamaie");
            entity.Property(e => e.Mac).HasColumnName("mac");
            entity.Property(e => e.Mere).HasColumnName("mere");
            entity.Property(e => e.Migdale).HasColumnName("migdale");
            entity.Property(e => e.Nuca).HasColumnName("nuca");
            entity.Property(e => e.Nume)
                .HasMaxLength(45)
                .HasColumnName("nume");
            entity.Property(e => e.Ovaz).HasColumnName("ovaz");
            entity.Property(e => e.Portocale).HasColumnName("portocale");
            entity.Property(e => e.Visine).HasColumnName("visine");
        });

        modelBuilder.Entity<PartialDecision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("partial_decision");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Conclusion)
                .HasMaxLength(45)
                .HasColumnName("conclusion");
            entity.Property(e => e.IntGluten).HasColumnName("int_gluten");
            entity.Property(e => e.IntLactoza).HasColumnName("int_lactoza");
            entity.Property(e => e.IntOua).HasColumnName("int_oua");
            entity.Property(e => e.Vegan).HasColumnName("vegan");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("question");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Attribute)
                .HasMaxLength(30)
                .HasColumnName("attribute");
            entity.Property(e => e.Text)
                .HasMaxLength(100)
                .HasColumnName("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
