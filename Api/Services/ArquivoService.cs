using System.Reflection;
using Api.Extensions;
using Api.Interfaces;
using Api.Models;
using Api.ViewModels;

namespace Api.Services;

public class ArquivoService : IArquivoService
{
    public string[] Codigo => new string[] { "SAP-0001" };
    private readonly IArquivoRepository _arquivoRepository;
    private readonly IArquivoStatusProcessamentoRepository _arquivoStatusProcessamentoRepository;
    public ArquivoService(
        IArquivoRepository arquivoRepository,
        IArquivoStatusProcessamentoRepository arquivoStatusProcessamentoRepository
        )
    {
        _arquivoRepository = arquivoRepository;
        _arquivoStatusProcessamentoRepository = arquivoStatusProcessamentoRepository;
    }

    public async Task Adicionar(IFormFileCollection arquivos)
    {
        foreach (var file in arquivos)
        {
            byte[] documentBytes = null;
            using (MemoryStream writeStream = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(writeStream))
            using (BinaryReader reader = new BinaryReader(file.OpenReadStream()))
            {
                var buffer = new Byte[1024];
                int readCount = 0;

                while ((readCount = reader.Read(buffer, 0, buffer.Length)) > 0)
                    write.Write(buffer, 0, readCount);

                documentBytes = writeStream.ToArray();
            }

            var arquivo = new Arquivo()
            {
                Nome = file.FileName,
                ArquivoOriginal = documentBytes,
                ArquivoStatusId = 1,
            };

            await _arquivoRepository.Add(arquivo);
        }
    }

    public async Task<bool> Processar(int idArquivo)
    {
        // listar arquivo
        var _arquivo = await _arquivoRepository.GetByIdAsync(idArquivo);

        if (_arquivo is null)
        {
            return false;
        }

        // atualizar status do arquivo
        _arquivo.ArquivoStatusId = 5;
        _arquivo.DataInicioProcessamento = DateTime.Now;
        await _arquivoRepository.Update(_arquivo);

        // criar status de processamento
        var arquivoStausProcessamento = CriarStatusProcessamento(_arquivo);

        var layoutArquivo = Processar(_arquivo.ArquivoOriginal, Codigo);

        // transformar de bytes em string

        // validar header

        // transformar operacao em objeto

        // com sucesso

        // com erro

        // contador

        // arquivo retorno

        // atualizar status do arquivo

        return true;
    }

    private static object Processar(byte[] arquivoOriginal, string[] codigo)
    {
        string[] arquivo = arquivoOriginal.ConverterParaTexto();

        var versaoLayout = codigo.First().Split('-').Last();

        switch (versaoLayout)
        {
            case "0001":
                return ConverterArquivo(arquivo);
            default:
                throw new ArgumentException("versão do layout arquivo não encontrada");
        }
    }

    private static object ConverterArquivo(string[] arquivo)
    {
        return ImportFile<ConfirmacaoCadastral>("persons.txt", ';');
    }

    private static List<T> ImportFile<T>(string FileName, char ColumnSeperator) where T : new()
    {
        var list = new List<T>();
        using (var str = File.OpenText(FileName))
        {
            int Line = 1;
            int Column = 0;
            try
            {
                var columnsHeader = str.ReadLine().Split(ColumnSeperator);
                var t = typeof(T);
                var plist = new Dictionary<int,
                    PropertyInfo>();
                for (int i = 0; i < columnsHeader.Length; i++)
                {
                    var p = t.GetProperty(columnsHeader[i]);
                    if (p != null && p.CanWrite && p.GetIndexParameters().Length == 0) plist.Add(i, p);
                }
                string s;
                while ((s = str.ReadLine()) != null)
                {
                    Line++;
                    var data = s.Split(ColumnSeperator);
                    var obj = new T();
                    foreach (var p in plist)
                    {
                        Column = p.Key;
                        if (p.Value.PropertyType == typeof(int)) p.Value.SetValue(obj, int.Parse(data[Column]), null);
                        else if (p.Value.PropertyType == typeof(string)) p.Value.SetValue(obj, data[Column], null);
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new ImportTextFileException(ex.Message, ex, Line, Column + 1);
            }
        }
    }

    private ArquivoStatusProcessamento CriarStatusProcessamento(Arquivo arquivo)
    {
        var qtdLinhas = 0;

        if (arquivo.ArquivoOriginal is not null)
        {
            qtdLinhas = CountLines(new MemoryStream(Compactador.TryDescompactar(arquivo.ArquivoOriginal))) - 1;
        }

        var statusProcessamento = new ArquivoStatusProcessamento()
        {
            ArquivoId = arquivo.Id,
            QtLinhas = qtdLinhas,
            QtLinhasErros = 0,
            QtLinhasProcessadas = 0
        };

        _arquivoStatusProcessamentoRepository.Add(statusProcessamento);

        return statusProcessamento;
    }

    private static int CountLines(Stream stream)
    {
        using (var r = new StreamReader(stream))
        {
            int lineCount = 0;
            while (r.ReadLine() is not null) { lineCount++; }

            return lineCount;
        }
    }
}