using Grpc.Net.Client;
using System;

namespace GrpcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Account.AccountClient(channel);

            AccountClientImpl accountClientImpl = new AccountClientImpl(channel, client);
            if (Console.ReadLine() == "1")
            {
                accountClientImpl.Login();
            }
            else if (Console.ReadLine() == "2")
            {
                accountClientImpl.Logout();
            }
            Console.ReadKey();
        }
    }
}
