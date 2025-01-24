using ChuckNorris.Client;
using Grpc.Net.Client;

Console.WriteLine("Let's fun with Chuck Norris");

using var channel = GrpcChannel.ForAddress("http://localhost:5144");
var client = new ChuckNorris.Client.ChuckNorris.ChuckNorrisClient(channel);

for (int i = 0; i < 10; i++)
{
    var joke = await client.ToJokeAsync(new EmptyRequest());
    Console.WriteLine(joke.Value);
    Console.WriteLine();
}
Console.ReadLine();