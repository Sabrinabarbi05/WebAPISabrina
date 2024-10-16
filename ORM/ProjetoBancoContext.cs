using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiBia.ORM;

public partial class ProjetoBancoContext : DbContext
{
    public ProjetoBancoContext()
    {
    }

    public ProjetoBancoContext(DbContextOptions<ProjetoBancoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCliente> TbClientes { get; set; }

    public virtual DbSet<TbEndereco> TbEnderecos { get; set; }

    public virtual DbSet<TbProduto> TbProdutos { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    public virtual DbSet<TbVendum> TbVenda { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=lab205_8\\SQLEXPRESS;Database=PROJETO_BANCO;User Id=adminbia;Password=12345678;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TB_Cliente");

            entity.ToTable("TB_CLIENTE");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DocIdentificacao).HasColumnName("DOC_identificacao");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbEndereco>(entity =>
        {
            entity.ToTable("TB_ENDERECO");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cep)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cidade)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkCliente).HasColumnName("FK_Cliente");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.PontoReferencia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Ponto_Referencia");

            entity.HasOne(d => d.FkClienteNavigation).WithMany(p => p.TbEnderecos)
                .HasForeignKey(d => d.FkCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_ENDERECO_TB_CLIENTE");
        });

        modelBuilder.Entity<TbProduto>(entity =>
        {
            entity.ToTable("TB_PRODUTO");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NotaFiscal).HasColumnName("Nota_Fiscal");
            entity.Property(e => e.Preco).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.ToTable("TB_USUARIO");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Senha)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbVendum>(entity =>
        {
            entity.ToTable("TB_VENDA");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FkCliente).HasColumnName("FK_Cliente");
            entity.Property(e => e.FkProduto).HasColumnName("FK_Produto");
            entity.Property(e => e.NotaFiscal).HasColumnName("Nota_Fiscal");
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.FkClienteNavigation).WithMany(p => p.TbVenda)
                .HasForeignKey(d => d.FkCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_VENDA_TB_CLIENTE");

            entity.HasOne(d => d.FkProdutoNavigation).WithMany(p => p.TbVenda)
                .HasForeignKey(d => d.FkProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_VENDA_TB_PRODUTO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
