using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Mappings
{
    public class ArquivoMapping : IEntityTypeConfiguration<Arquivo>
    {
        public void Configure(EntityTypeBuilder<Arquivo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.ArquivoOriginal)
                .IsRequired()
                .HasColumnType("varbinary(MAX)");

            builder.Property(x => x.ArquivoRetorno)
                .HasColumnType("varbinary(MAX)");

            builder.Property(x => x.DataInicioProcessamento)
                .HasColumnType("datetime");


            builder.Property(x => x.DataFimProcessamento)
                .HasColumnType("datetime");

            builder.HasOne(x => x.ArquivoStatus);

            /* ENTITY */
            builder.Property(x => x.DataCadastro)
                .HasColumnType("datetime");

            builder.Property(x => x.DataAlteracao)
                .HasColumnType("datetime");

            builder.ToTable("Arquivo");
        }
    }
}
