using System.IO.Compression;
using System.Text;

namespace Api.Extensions;

public class Compactador
{
    private static readonly byte[] GzipBytes = { 0x1f, 0x8b };


    public static string Compacta(string text)
    {
        return Encoding.Default.GetString(Compacta(Encoding.Default.GetBytes(text)));
    }

    public static byte[] Compacta(byte[] bytes)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            using (GZipStream gzip = new GZipStream(ms, CompressionLevel.Optimal, true))
            {
                gzip.Write(bytes, 0, bytes.Length);
                gzip.Close();
            }
            return
                ms.ToArray();
        }
    }

    public static string Descompacta(string text)
    {
        return
            Encoding.Default.GetString(Descompacta(Encoding.Default.GetBytes(text)));
    }

    public static byte[] Descompacta(byte[] bytes)
    {
        using (var instream = new MemoryStream(bytes))
        using (var gzip = new GZipStream(instream, CompressionMode.Decompress))
        using (var outStream = new MemoryStream())
        {
            gzip.CopyTo(outStream);
            return
                outStream.ToArray();
        }
    }

    public static byte[] TryDescompactar(byte[] bytes)
    {
        try
        {
            if (IsCompressedData(GzipBytes, bytes))
            {
                using (var instream = new MemoryStream(bytes))
                using (var gzip = new GZipStream(instream, CompressionMode.Decompress))
                using (var outStream = new MemoryStream())
                {
                    gzip.CopyTo(outStream);
                    return
                        outStream.ToArray();
                }
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            return bytes;
        }

        return bytes;
    }

    private static bool IsCompressedData(byte[] headerBytes, byte[] dataBytes)
    {
        if (dataBytes.Length < headerBytes.Length)
            throw new ArgumentOutOfRangeException(nameof(dataBytes),
                $"Passed databytes length ({dataBytes.Length}) is shorter than the headerbytes ({headerBytes.Length})");

        for (var i = 0; i < headerBytes.Length; i++)
        {
            if (headerBytes[i] == dataBytes[i]) continue;

            return false;
        }

        return true;
    }
}