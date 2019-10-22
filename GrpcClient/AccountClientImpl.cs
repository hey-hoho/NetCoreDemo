using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Text;
using GrpcServer;

namespace GrpcClient
{
    public class AccountClientImpl
    {
        private readonly GrpcChannel _grpcChannel;
        private readonly Account.AccountClient _accountClient;
        public AccountClientImpl(GrpcChannel grpcChannel, Account.AccountClient accountClient)
        {
            _grpcChannel = grpcChannel;
            _accountClient = accountClient;
        }
        public void Login()
        {
            var result = _accountClient.Login(new LoginModel() { UserName = "1234", UserPsw = "1234" });
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }
        public void Logout()
        {
            var empty = new Google.Protobuf.WellKnownTypes.Empty();
            _accountClient.Logout(empty);
        }
    }
}
