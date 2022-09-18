using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Mappings
{
    public class ArquivoErroMapping : IEntityTypeConfiguration<ArquivoErro>
    {
        public void Configure(EntityTypeBuilder<ArquivoErro> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NumeroLinhaArquivoOriginal)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(x => x.TextoLinhaArquivoOriginal)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.Erro)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.DataProcessamento)
                .IsRequired()
                .HasColumnType("datetime");

            builder.HasOne(x => x.Arquivo);

            /* ENTITY */
            builder.Property(x => x.DataCadastro)
                .HasColumnType("datetime");

            builder.Property(x => x.DataAlteracao)
                .HasColumnType("datetime");

            builder.ToTable("ArquivoErro");
        }
    }
}
