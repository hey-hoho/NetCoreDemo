using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcServer
{
    public class AccountService : Account.AccountBase
    {
        public override Task<UserModel> Login(LoginModel request, ServerCallContext context)
        {
            if (request.UserName == "1234" && request.UserPsd == "1234")
            {
                var userModel = new UserModel
                {
                    NickName = "测试用户",
                    Token = Guid.NewGuid().ToString(),
                };
                return Task.FromResult(new StringData() { Data = Newtonsoft.Json.JsonConvert.SerializeObject(userModel) });
            }
            else
            {
                var BadRequest = new BadRequest { ErrorCode = 1, ErrorDescription = "用户名或密码错误" };
                return Task.FromResult(new StringData() { Data = Newtonsoft.Json.JsonConvert.SerializeObject(BadRequest) });
            }
        }
        public override Task<Empty> Logout(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new Empty());
        }
    }

    public class BadRequest
    {
        public int ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
    }
}
