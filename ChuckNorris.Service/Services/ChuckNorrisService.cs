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

        public override async Task<JokeResponce> ToJoke(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                var joke = await _chuckNorrisClient.GetJokeAsync();
                return new JokeResponce
                {
                    Value = joke?.Value 
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception was occured with error: {@Error}", ex.Message);
                throw;
            }
        }
    }
}
