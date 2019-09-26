using Microsoft.AspNetCore.Mvc.Formatters;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class ProtobufFormatter : OutputFormatter
    {
        public string ContentType { get; private set; }

        public ProtobufFormatter()
        {
            ContentType = "application/proto";
            SupportedMediaTypes.Add(Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/proto"));
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var response = context.HttpContext.Response;
            Serializer.Serialize(response.Body, context.Object);
            return Task.FromResult(0);
        }
    }

    [ProtoContract]
    public class ProtobufUser
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public int Age { get; set; }
    }
}
