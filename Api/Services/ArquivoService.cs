using System.Collections.Concurrent;
using System.Reflection;
using Api.Extensions;
using Api.Interfaces;
using Api.Models;

namespace Api.Services;

public class ArquivoService : IArquivoService
{
    public string[] Codigo => new string[] { "SAP-0001" };
    private readonly IArquivoRepository _arquivoRepository;
    private readonly IArquivoErroRepository _arquivoErroRepository;
    private readonly IArquivoStatusProcessamentoRepository _arquivoStatusProcessamentoRepository;
    public ArquivoService(
        IArquivoRepository arquivoRepository,
        IArquivoErroRepository arquivoErroRepository,
        IArquivoStatusProcessamentoRepository arquivoStatusProcessamentoRepository
        )
    {
        _arquivoRepository = arquivoRepository;
        _arquivoErroRepository = arquivoErroRepository;
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
        bool headerTemErro = false;
        var operacaoErro = new ConcurrentBag<Operacao>();

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
        var _arquivoStausProcessamento = CriarStatusProcessamento(_arquivo);

        var layoutArquivo = Processar(_arquivo.ArquivoOriginal, Codigo) as ConfirmacaoCadastralHeader;

        if (layoutArquivo.Tipo != "SAP" || layoutArquivo.Versao != "0001")
        {
            headerTemErro = true;

            var operacaoErroHeader = new Operacao
            {
                Arquivo = _arquivo,
                NumeroLinhaArquivoOriginal = layoutArquivo.NumeroLinhaArquivoOriginal,
                TextoLinhaArquivoOriginal = layoutArquivo.TextoLinhaArquivoOriginal,
                MensagemErro = "Layout invalido"
            };

            AdicionarArquivoErro(operacaoErroHeader);
        }

        if (headerTemErro == false)
        {
            Parallel.ForEach(layoutArquivo.ConfirmacaoCadastral, (ConfirmacaoCadastralOperacao linhaOperacao) =>
            {
                var operacao = new Operacao
                {
                    Arquivo = _arquivo,
                    NumeroLinhaArquivoOriginal = linhaOperacao.NumeroLinhaArquivoOriginal,
                    TextoLinhaArquivoOriginal = linhaOperacao.TextoLinhaArquivoOriginal
                };

                if (String.IsNullOrEmpty(linhaOperacao.RazaoSocial))
                {
                    operacao.MensagemErro = "RazaoSocial obrigatorio";
                    operacaoErro.Add(operacao);
                    return;
                }

                if (String.IsNullOrEmpty(linhaOperacao.NomeAcesso))
                {
                    operacao.MensagemErro = "NomeAcesso obrigatorio";
                    operacaoErro.Add(operacao);
                    return;
                }


                if (String.IsNullOrEmpty(linhaOperacao.ContaPrincipal))
                {
                    operacao.MensagemErro = "ContaPrincipal obrigatorio";
                    operacaoErro.Add(operacao);
                    return;
                }

            });
        }

        #region Finalizacao

        if (operacaoErro.Any())
        {

        }
        #endregion

        #region Arquivo Retorno

        #endregion




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
        var versaoLayout = codigo.First().Split('-').Last();

        switch (versaoLayout)
        {
            case "0001":
                return ConverterArquivo(arquivoOriginal);
            default:
                throw new ArgumentException("versão do layout arquivo não encontrada");
        }
    }

    private static object ConverterArquivo(byte[] arquivo)
    {
        var arquivoDescompactado = Compactador.TryDescompactar(arquivo);

        ConfirmacaoCadastralHeader confirmacaoCadastral = ProcessarHeader(arquivoDescompactado);

        confirmacaoCadastral.ConfirmacaoCadastral = ProcessarOperacao(arquivoDescompactado);

        return confirmacaoCadastral;
    }

    private static ConfirmacaoCadastralHeader ProcessarHeader(byte[] arquivo)
    {
        using (var str = new StreamReader(new MemoryStream(arquivo)))
        {
            try
            {
                var headerLine = str.ReadLine();

                var data = headerLine.Split(';');

                ConfirmacaoCadastralHeader obj;
                if (data.Length == 2)
                {
                    obj = new ConfirmacaoCadastralHeader
                    {
                        Tipo = data[0],
                        Versao = data[1],
                        NumeroLinhaArquivoOriginal = 1,
                        TextoLinhaArquivoOriginal = headerLine
                    };
                }
                else
                {
                    obj = new ConfirmacaoCadastralHeader
                    {
                        NumeroLinhaArquivoOriginal = 1,
                        TextoLinhaArquivoOriginal = headerLine
                    };
                }

                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    private static List<ConfirmacaoCadastralOperacao> ProcessarOperacao(byte[] arquivo)
    {
        var list = new List<ConfirmacaoCadastralOperacao>();

        using (var str = new StreamReader(new MemoryStream(arquivo)))
        {
            int Line = 1;

            try
            {
                var headerLine = str.ReadLine();
                string s;

                while ((s = str.ReadLine()) != null)
                {
                    Line++;
                    var data = s.Split(';');
                    ConfirmacaoCadastralOperacao obj;

                    if (data.Length == 4)
                    {
                        obj = new ConfirmacaoCadastralOperacao
                        {
                            RazaoSocial = data[0],
                            NomeAcesso = data[1],
                            ContaPrincipal = data[2],
                            Confirmacao = data[3],
                            NumeroLinhaArquivoOriginal = Line,
                            TextoLinhaArquivoOriginal = s
                        };
                    }
                    else
                    {
                        obj = new ConfirmacaoCadastralOperacao
                        {
                            NumeroLinhaArquivoOriginal = Line,
                            TextoLinhaArquivoOriginal = s
                        };
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    private static List<T> ImportFile<T>(byte[] arquivo, char ColumnSeperator, bool header) where T : new()
    {
        var list = new List<T>();

        using (var str = new StreamReader(new MemoryStream(arquivo)))
        {
            int Line = 0;
            int Column = 0;

            try
            {
                if (header == false)
                {
                    var headerLine = str.ReadLine();
                }
                var t = typeof(T);
                var columnsHeader = t.GetProperties();
                var plist = new Dictionary<int, PropertyInfo>();
                string s;

                for (int i = 0; i < columnsHeader.Length; i++)
                {
                    var p = columnsHeader[i];
                    if (p != null && p.CanWrite && p.GetIndexParameters().Length == 0) plist.Add(i, p);
                }

                while ((s = str.ReadLine()) != null)
                {
                    Line++;
                    var data = s.Split(ColumnSeperator);
                    var obj = new T();

                    try
                    {
                        foreach (var p in plist)
                        {
                            Column = p.Key;
                            if (p.Value.PropertyType == typeof(int)) p.Value.SetValue(obj, int.Parse(data[Column]), null);
                            else if (p.Value.PropertyType == typeof(string)) p.Value.SetValue(obj, data[Column], null);
                        }
                    }
                    catch
                    {

                    }

                    list.Add(obj);

                    if (header) return list;
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

    private void AdicionarArquivoErro(Operacao operacaoErroHeader)
    {
        var arquivoErro = new ArquivoErro
        {
            NumeroLinhaArquivoOriginal = operacaoErroHeader.NumeroLinhaArquivoOriginal,
            TextoLinhaArquivoOriginal = operacaoErroHeader.TextoLinhaArquivoOriginal,
            DataProcessamento = DateTime.Now,
            Erro = operacaoErroHeader.MensagemErro,
            ArquivoId = operacaoErroHeader.Arquivo.Id
        };

        _arquivoErroRepository.Add(arquivoErro);
    }
}