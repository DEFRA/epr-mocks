namespace EPR.Mock.Antivirus.Functions.UnitTests.Support.Extensions;

using System.IO;
using Microsoft.AspNetCore.Http;
using Moq;

public static class HttpFileExtensions
{
    public static IFormFile CreateFormFile(string keyname, string filePath, string contentType)
    {
        var ms = new MemoryStream();
        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            stream.CopyTo(ms);
        }
        ms.Position = 0;
        var filename = Path.GetFileName(filePath);

        return new FormFile(ms, 0, ms.Length, keyname, filename)
        {
            Headers = new HeaderDictionary(),
            ContentType = contentType
        };
    }
}