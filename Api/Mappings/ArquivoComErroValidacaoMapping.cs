using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Mappings
{
    public class ArquivoComErroValidacaoMapping : IEntityTypeConfiguration<ArquivoComErroValidacao>
    {
        public void Configure(EntityTypeBuilder<ArquivoComErroValidacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NumeroLinha)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(x => x.TextoLinha)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.Erro)
                .IsRequired()
                .HasColumnType("varchar(250)");

            /* ENTITY */
            builder.Property(x => x.DataCadastro)
                .HasColumnType("datetime");

            builder.Property(x => x.DataAlteracao)
                .HasColumnType("datetime");

            builder.ToTable("ArquivoComErroValidacao");
        }
    }
}
