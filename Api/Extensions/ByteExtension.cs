using System.Text;

namespace Api.Extensions;

public static class ByteExtension
{
    public static string[] ConverterParaTexto(this byte[] arquivo)
    {
        var arquivoDescompactado = Compactador.TryDescompactar(arquivo);
        List<string> lst = new List<string>();
        var stream = new StreamReader(new MemoryStream(arquivoDescompactado));

        try
        {
            string line = string.Empty;

            while ((line = stream.ReadLine()) != null)
            {
                lst.Add(line);
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            stream.Dispose();
        }

        return lst.ToArray();
    }

    public static byte[] RetornaArquivoCompactado(StringBuilder sb)
    {
        return RetornaArquivoCompactado(new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString())));
    }

    public static byte[] RetornaArquivoCompactado(Stream arquivoStream)
    {
        byte[] arquivo = null;
        byte[] arquivoCompactado = null;

        try
        {
            using (MemoryStream writeStream = new MemoryStream())
            using (BinaryWriter write = new BinaryWriter(writeStream))
            using (BinaryReader reader = new BinaryReader(arquivoStream))
            {
                var buffer = new Byte[1024];
                int readCount = 0;

                while ((readCount = reader.Read(buffer, 0, buffer.Length)) > 0)
                    write.Write(buffer, 0, readCount);

                arquivo = writeStream.ToArray();
            }

            arquivoCompactado = Compactador.Compacta(arquivo);
        }
        catch (Exception)
        {
            throw;
        }

        return arquivoCompactado;
    }
}