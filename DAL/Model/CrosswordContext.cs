using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Model;

public partial class CrosswordContext : DbContext
{
    public CrosswordContext()
    {
    }

    public CrosswordContext(DbContextOptions<CrosswordContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AllCrossword> AllCrosswords { get; set; }

    public virtual DbSet<CrosswordsUser> CrosswordsUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WordAndDefinition> WordAndDefinitions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS15;Database=crossword;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AllCrossword>(entity =>
        {
            entity.Property(e => e.Across).HasColumnName("across");
            entity.Property(e => e.AmountLetters).HasColumnName("amountLetters");
            entity.Property(e => e.CrosswordCode).HasColumnName("crosswordCode");
            entity.Property(e => e.DefinitionCode).HasColumnName("definitionCode");
            entity.Property(e => e.Down).HasColumnName("down");
            entity.Property(e => e.I).HasColumnName("i");
            entity.Property(e => e.J).HasColumnName("j");
            entity.Property(e => e.NumberLocation).HasColumnName("numberLocation");
            entity.Property(e => e.Solve)
                .HasMaxLength(50)
                .HasColumnName("solve");

            entity.HasOne(d => d.CrosswordCodeNavigation).WithMany(p => p.AllCrosswords)
                .HasForeignKey(d => d.CrosswordCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AllCrosswords_crosswords_users");

            entity.HasOne(d => d.DefinitionCodeNavigation).WithMany(p => p.AllCrosswords)
                .HasForeignKey(d => d.DefinitionCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AllCrosswords_wordAndDefinition");
        });

        modelBuilder.Entity<CrosswordsUser>(entity =>
        {
            entity.HasKey(e => e.CrosswordCode).HasName("PK_crosswords");

            entity.ToTable("crosswords_users");

            entity.Property(e => e.CrosswordCode).HasColumnName("crosswordCode");
            entity.Property(e => e.CrosswordName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("crosswordName");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.ProductionDate)
                .HasColumnType("date")
                .HasColumnName("productionDate");
            entity.Property(e => e.UserCode).HasColumnName("userCode");
            entity.Property(e => e.Width).HasColumnName("width");

            entity.HasOne(d => d.UserCodeNavigation).WithMany(p => p.CrosswordsUsers)
                .HasForeignKey(d => d.UserCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_crosswords_users_users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserCode);

            entity.ToTable("users");

            entity.Property(e => e.UserCode).HasColumnName("userCode");
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("userName");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("userPassword");
        });

        modelBuilder.Entity<WordAndDefinition>(entity =>
        {
            entity.HasKey(e => e.WordCode);

            entity.ToTable("wordAndDefinition");

            entity.Property(e => e.WordCode).HasColumnName("wordCode");
            entity.Property(e => e.Definition).HasColumnName("definition");
            entity.Property(e => e.Word)
                .HasMaxLength(50)
                .HasColumnName("word");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
