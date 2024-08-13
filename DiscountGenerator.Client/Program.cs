// See https://aka.ms/new-console-template for more information
using DiscountClient;
using Grpc.Net.Client;

Console.WriteLine("Hello, World!");

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7219");
var client = new DiscountGenerator.DiscountGeneratorClient(channel);
var generatedReply = await client.GenerateAsync(new DiscountGeneratorRequest { Count = 2, Length = 7 });
 
Console.WriteLine("[client] Result: " + generatedReply.Result + " [obtained from server]");

var lastCode = await client.GetLastAsync(new Google.Protobuf.WellKnownTypes.Empty());

Console.WriteLine("[client] Last generated code: " + lastCode.Code + " [obtained from server]");

var codeApplierClient = new DiscountApplier.DiscountApplierClient(channel);
var applyResult = await codeApplierClient.ApplyAsync(new UseCodeRequest { Code = lastCode.Code });

Console.WriteLine("[client] Code apply result: " + applyResult.Result);

Console.WriteLine("Press any key to exit...");
Console.ReadKey();