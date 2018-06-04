using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Linq;
using MessagePack;

namespace Components.Mvc.MediaTypeFormatters.MsgPack
{
   
    public class MessagePackOutputMediaTypeFormatter : OutputFormatter
    {

        public MessagePackOutputMediaTypeFormatter()
        {
            this.SupportedMediaTypes.Add("application/msg-pack");
            
        }

        protected override bool CanWriteType(Type type)
        {
             return !type.IsInterface && type.CustomAttributes.Any((attr) => attr.AttributeType.Equals(typeof(MessagePack.MessagePackObjectAttribute)));
        }

        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return base.CanWriteResult(context);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            IServiceProvider serviceProvider = context.HttpContext.RequestServices;
            MessagePackOptions options = serviceProvider.GetService(typeof(MessagePackOptions)) as MessagePackOptions;
            MessagePack.IFormatterResolver resolver = options?.Resolver;

            var response = context.HttpContext.Response;

            byte[] responseBytes = MessagePackSerializer.Serialize(context.Object, resolver);

            return response.Body.WriteAsync(responseBytes, 0, responseBytes.Count());
        }
    }
}
