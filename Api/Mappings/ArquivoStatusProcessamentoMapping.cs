using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Mappings
{
    public class ArquivoStatusProcessamentoMapping : IEntityTypeConfiguration<ArquivoStatusProcessamento>
    {
        public void Configure(EntityTypeBuilder<ArquivoStatusProcessamento> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Arquivo);

            /* ENTITY */
            builder.Property(x => x.DataCadastro)
                .HasColumnType("datetime");

            builder.Property(x => x.DataAlteracao)
                .HasColumnType("datetime");

            builder.ToTable("ArquivoStatusProcessamento");
        }
    }
}
