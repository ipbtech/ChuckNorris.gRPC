using ChuckNorris.Client;
using Grpc.Net.Client;

Console.WriteLine("Let's fun with Chuck Norris");
Console.WriteLine("How much jokes do you want?");

int count = 0;
while (!int.TryParse(Console.ReadLine(), out count) && count > 0)
{
    Console.WriteLine("Sorry, I don't understand. Enter any integer number greater than 0");
}
Console.WriteLine();


using var channel = GrpcChannel.ForAddress("http://localhost:5144");
var client = new ChuckNorris.Client.ChuckNorris.ChuckNorrisClient(channel);

var serverData = client.ToJoke(new JokeCountRequest() { Count = count });
var responseStream = serverData.ResponseStream;
while(await responseStream.MoveNext(new CancellationToken()))
{
    var joke = responseStream.Current;
    Console.WriteLine(joke.Value);
    Console.WriteLine();
}
Console.ReadLine();