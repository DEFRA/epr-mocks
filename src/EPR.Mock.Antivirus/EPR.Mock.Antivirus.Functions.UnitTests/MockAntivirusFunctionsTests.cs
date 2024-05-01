namespace EPR.Mock.Antivirus.Functions.UnitTests;

using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using Mock.Antivirus.Functions.UnitTests.Support.Extensions;

[TestClass]
public class MockAntivirusFunctionsTests
{
    private MockAntivirusFunctions? _mockAntivirus;

    [TestInitialize]
    public void TestInitialize()
    {
        var mockBlobContainerClient = new Mock<BlobContainerClient>();
        mockBlobContainerClient
            .Setup(i => i.AccountName)
            .Returns("test account");
        var blobClientMock = new Mock<BlobClient>();
        mockBlobContainerClient
            .Setup(i => i.GetBlobClient("testfile.csv"))
            .Returns(blobClientMock.Object);
        _mockAntivirus = new MockAntivirusFunctions(mockBlobContainerClient.Object);
    }

    [TestMethod]
    public async Task DummyAntivirus_DoesNotThrowException_WhenHttpClientResponseIsCreated()
    {
        // Arrange
        var headers = new HeaderDictionary
        {
            { "Content-Type", "multipart/form-data" }
        };

        var formFileCollection = new FormFileCollection
        {
            HttpFileExtensions.CreateFormFile("file", "Functions/files/testfile.csv", "text/csv")
        };

        var formCollection = new FormCollection(
            new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>()
            {
                { "data", "Test data" },
            },
            formFileCollection);

        var requestMock = new Mock<HttpRequest>();
        requestMock.Setup(req => req.Method).Returns(HttpMethods.Put);
        requestMock.Setup(req => req.Headers).Returns(headers);
        requestMock.Setup(req => req.Form).Returns(formCollection);

        // Act
        var result = await _mockAntivirus!.Run(requestMock.Object, "testcollection", "testkey");

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        result.Should().BeEquivalentTo(new OkObjectResult($"File check successful for testfile.csv"));
    }
}