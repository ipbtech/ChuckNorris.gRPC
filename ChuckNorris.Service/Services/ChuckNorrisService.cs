using Grpc.Core;

namespace ChuckNorris.Service.Services
{
    public class ChuckNorrisService : ChuckNorris.ChuckNorrisBase
    {
        private readonly ChuckNorrisHttpClient _chuckNorrisClient;
        private readonly ILogger<ChuckNorrisService> _logger;

        public ChuckNorrisService(ChuckNorrisHttpClient chuckNorrisClient, ILogger<ChuckNorrisService> logger)
        {
            _chuckNorrisClient = chuckNorrisClient;
            _logger = logger;
        }

        public override async Task ToJoke(JokeCountRequest request, 
            IServerStreamWriter<JokeResponce> responseStream, ServerCallContext context)
        {
            try
            {
                var countJokes = request.Count;
                for (int i = 1; i <= countJokes; i++)
                {
                    var joke = await _chuckNorrisClient.GetJokeAsync();
                    await responseStream.WriteAsync(new JokeResponce { Value = joke?.Value });
                    _logger.LogInformation($"Joke #{i} was arrived to client");
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception was occured with error: {@Error}", ex.Message);
                throw;
            }
        }
    }
}
