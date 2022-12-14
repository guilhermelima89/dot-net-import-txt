using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Mappings
{
    public class ArquivoSemErroValidacaoMapping : IEntityTypeConfiguration<ArquivoSemErroValidacao>
    {
        public void Configure(EntityTypeBuilder<ArquivoSemErroValidacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NumeroLinhaArquivoOriginal)
              .IsRequired()
              .HasColumnType("int");

            builder.Property(x => x.TextoLinhaArquivoOriginal)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.RazaoSocial)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.NomeAcesso)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.ContaPrincipal)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.Confirmacao)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.HasOne(x => x.Arquivo);

            /* ENTITY */
            builder.Property(x => x.DataCadastro)
                .HasColumnType("datetime");

            builder.Property(x => x.DataAlteracao)
                .HasColumnType("datetime");

            builder.ToTable("ArquivoSemErroValidacao");
        }
    }
}
