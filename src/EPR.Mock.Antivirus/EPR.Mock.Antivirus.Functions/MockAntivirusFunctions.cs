using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace EPR.Mock.Antivirus.Functions;

public class MockAntivirusFunctions
{
    private readonly BlobContainerClient _containerClient;

    public MockAntivirusFunctions(BlobContainerClient blobContainerClient)
    {
        _containerClient = blobContainerClient;
    }

    [FunctionName("ScanFileForVirus")]
    public async Task<IActionResult> Run(
            [HttpTrigger(
            AuthorizationLevel.Function,
            nameof(HttpMethods.Put),
            Route = "files/stream/{collection}/{key}")] HttpRequest req,
            string collection,
            string key)
    {
        try
        {
            Stream fileCheckBlob = new MemoryStream();
            var file = req.Form.Files["file"];
            fileCheckBlob = file.OpenReadStream();
            var blob = _containerClient.GetBlobClient(file.FileName);
            await blob.UploadAsync(fileCheckBlob, true);
            return new OkObjectResult($"File check successful for {file.FileName}");
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }
}
