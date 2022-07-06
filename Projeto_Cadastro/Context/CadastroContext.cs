using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Projeto_Cadastro.Domains;

#nullable disable

namespace Projeto_Cadastro.Context
{
    public partial class CadastroContext : DbContext
    {
        public CadastroContext()
        {
        }

        public CadastroContext(DbContextOptions<CadastroContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Default");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("PK__Tipo_Usu__06416392EF2BE969");

                entity.ToTable("Tipo_Usuario");

                entity.Property(e => e.IdTipo).HasColumnName("Id_Tipo");

                entity.Property(e => e.NomeTipo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Nome_Tipo");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuarios__63C76BE27D5A8DB4");

                entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534F3CA248C")
                    .IsUnique();

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.IdTipo).HasColumnName("Id_Tipo");

                entity.Property(e => e.Imagem)
                    .HasMaxLength(235)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StatusConta)
                    .HasColumnName("Status_Conta")
                    .HasDefaultValueSql("('True')");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdTipo)
                    .HasConstraintName("FK__Usuarios__Id_Tip__276EDEB3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
