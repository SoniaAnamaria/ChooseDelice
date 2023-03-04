using System;
using System.Collections.Generic;
using ChooseDelice.Models;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<Question> Questions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=123456;database=choose_delice");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("question");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Text)
                .HasMaxLength(100)
                .HasColumnName("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
