using Microsoft.Azure.Cosmos.Fluent;
using Xunit;

namespace CosmosDBPesistence;

public class Tests
{
    
    //private readonly CosmosDbContainer _cosmosDBContainer = new CosmosDbBuilder().Build();

    /*
    [Fact]
    public Task OneTimeSetup()
    {
        return Task.WhenAll([
            _cosmosDBContainer.StartAsync()
        ]);
    }

    [OneTimeTearDown]
    public Task OneTimeTearDown()
    {
        return Task.WhenAll([
            _cosmosDBContainer.DisposeAsync().AsTask()
        ]);
    }
    */

    [Fact]
    public async Task TestContainerIsCallable()
    {
        var connectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==;DisableServerCertificateValidation=true";

        using var client = new CosmosClientBuilder(connectionString).WithLimitToEndpoint(true).Build();
        var result = await client!.CreateDatabaseIfNotExistsAsync("database-1"); // throws HttpRequestException due to SSL
    }
    
}