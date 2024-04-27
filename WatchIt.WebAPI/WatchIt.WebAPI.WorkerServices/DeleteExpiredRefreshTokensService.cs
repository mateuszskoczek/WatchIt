using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WatchIt.Database;
using WatchIt.Database.Model.Account;

namespace WatchIt.WebAPI.WorkerServices;

public class DeleteExpiredRefreshTokensService(ILogger<DeleteExpiredRefreshTokensService> logger, DatabaseContext database) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            
            Task delayTask = Task.Delay(300000, stoppingToken);
            Task actionTask = Action();

            await Task.WhenAll(delayTask, actionTask);
        }
    }

    protected async Task Action()
    {
        IEnumerable<AccountRefreshToken> tokens = database.AccountRefreshTokens.Where(x => x.ExpirationDate < DateTime.UtcNow);
        database.AccountRefreshTokens.AttachRange(tokens);
        database.AccountRefreshTokens.RemoveRange(tokens);
        await database.SaveChangesAsync();
    }
}