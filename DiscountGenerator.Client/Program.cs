// See https://aka.ms/new-console-template for more information
using DiscountClient;
using Grpc.Net.Client;

Console.WriteLine("Hello, World!");

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7219");
var client = new DiscountGenerator.DiscountGeneratorClient(channel);
var reply = await client.GenerateAsync(new DiscountGeneratorRequest { Count = 2, Length = 7 });

Console.WriteLine("[client] Result: " + reply.Result + " [obtained from server]");

var lastCode = await client.GetLastAsync(new Google.Protobuf.WellKnownTypes.Empty());

Console.WriteLine("[client] Last generated code: " + lastCode.Code + " [obtained from server]");

Console.WriteLine("Press any key to exit...");
Console.ReadKey();