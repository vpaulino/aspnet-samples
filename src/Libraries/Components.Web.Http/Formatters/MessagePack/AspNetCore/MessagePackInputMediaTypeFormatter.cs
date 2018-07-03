#if NETSTD20
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Linq;
using System.IO;
using MessagePack;
 
using System.Reflection;
using System.Linq.Expressions;

namespace Components.Web.Http.MediaTypeFormatters.MsgPack
{
    public class MessagePackInputMediaTypeFormatter : InputFormatter
    {
        delegate object DeserializeMethod(Stream stream, IFormatterResolver resolver);


        public MessagePackInputMediaTypeFormatter()
        {
            this.SupportedMediaTypes.Add("application/msg-pack");
            
        }

        protected override bool CanReadType(Type type)
        {
            return !type.IsInterface && type.CustomAttributes.Any((attr) => attr.AttributeType.Equals(typeof(MessagePack.MessagePackObjectAttribute)));
        }


        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            IServiceProvider serviceProvider = context.HttpContext.RequestServices;
            MessagePackOptions options = serviceProvider.GetService(typeof(MessagePackOptions)) as MessagePackOptions;
            MessagePack.IFormatterResolver resolver = options?.Resolver;

            if (resolver == null)
                return InputFormatterResult.FailureAsync();

            Type actionModelType = context.ModelType;
             
            var serializerType = typeof(MessagePackSerializer);

            var deserializeMethod = serializerType.GetMethod("Deserialize", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(Stream), typeof(IFormatterResolver) }, null);
            deserializeMethod = deserializeMethod?.MakeGenericMethod(actionModelType);

            var streamParameter = Expression.Parameter(typeof(Stream), "stream");
            var formatterResolver = Expression.Parameter(typeof(IFormatterResolver), "resolver");

             var deserializationExpression = Expression.Call(deserializeMethod, streamParameter, formatterResolver);

            var lambda =  Expression.Lambda<DeserializeMethod>(deserializationExpression, new ParameterExpression[] { streamParameter, formatterResolver }).Compile();

            var request = context.HttpContext.Request;

           object instance = lambda(request.Body, resolver);
            
            return InputFormatterResult.SuccessAsync(instance);
        }

    }
}
#endif
