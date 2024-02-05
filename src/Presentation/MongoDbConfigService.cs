using Infrastructure.Configuration;
using Infrastructure.Persistence.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Presentation;

public class MongoDbConfigService : IHostedService
{
    private readonly ILogger<MongoDbConfigService> _logger;
    private readonly IMongoContext _mongoContext;

    public MongoDbConfigService(IMongoClient mongoClient, IOptions<MongoDbConfig> dbConfigOptions,
        ILogger<MongoDbConfigService> logger)
    {
        _mongoContext = new MongoContext(mongoClient, dbConfigOptions);
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await MongoDbPersistence.ConfigureAsync(_mongoContext, _logger);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}