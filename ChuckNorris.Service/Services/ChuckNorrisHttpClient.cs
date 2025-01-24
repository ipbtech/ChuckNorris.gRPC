namespace ChuckNorris.Service.Services
{
    public class ChuckNorrisHttpClient
    {
        private readonly HttpClient _httpClient;
        public ChuckNorrisHttpClient(HttpClient httpClient) 
        {
            _httpClient = httpClient;    
        }

        public async Task<JokeDto?> GetJokeAsync() => await _httpClient.GetFromJsonAsync<JokeDto>("/jokes/random");
    }

    public record JokeDto(string Id, string Url, string Value) { }
}
