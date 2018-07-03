#if NET46
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MessagePack;
using System.Reflection;
using System.Linq.Expressions;

namespace Components.Web.Http.MediaTypeFormatters.MsgPack
{
    public class MessagePackMediaTypeFormatter : MediaTypeFormatter
    {
        MessagePackOptions options;

        delegate object DeserializeMethod(Stream stream, IFormatterResolver resolver);
         
        public MessagePackMediaTypeFormatter(MessagePackOptions options)
        {
            this.options = options;
            this.SupportedMediaTypes.Add( new System.Net.Http.Headers.MediaTypeHeaderValue("application/msg-pack"));
        }

        public byte[] Serialize<T>(T instance, MessagePack.IFormatterResolver resolver)
        {
            byte[] responseBytes = MessagePackSerializer.Serialize<T>(instance, resolver);
            return responseBytes;
        }

        public override bool CanReadType(Type type)
        {
            return !type.IsInterface && type.CustomAttributes.Any((attr) => attr.AttributeType.Equals(typeof(MessagePack.MessagePackObjectAttribute)));
        }

        public override bool CanWriteType(Type type)
        {
            return !type.IsInterface && type.CustomAttributes.Any((attr) => attr.AttributeType.Equals(typeof(MessagePack.MessagePackObjectAttribute)));
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {

            TaskCompletionSource<object> taskSource = new TaskCompletionSource<object>();
             
            Type actionModelType = type;

            var serializerType = typeof(MessagePackSerializer);

            var deserializeMethod = serializerType.GetMethod("Deserialize", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(Stream), typeof(IFormatterResolver) }, null);
            deserializeMethod = deserializeMethod?.MakeGenericMethod(actionModelType);

            var streamParameter = Expression.Parameter(typeof(Stream), "stream");
            var formatterResolver = Expression.Parameter(typeof(IFormatterResolver), "resolver");

            var deserializationExpression = Expression.Call(deserializeMethod, streamParameter, formatterResolver);

            var lambda = Expression.Lambda<DeserializeMethod>(deserializationExpression, new ParameterExpression[] { streamParameter, formatterResolver }).Compile();
             
            object instance = lambda(readStream, options.Resolver);

            taskSource.SetResult(instance);
            return taskSource.Task;
        }

        

        public override async Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            MessagePack.IFormatterResolver resolver = options?.Resolver;

            MethodInfo genericMethod = this.GetType().GetMethod("Serialize").MakeGenericMethod(new Type[] { type });

            byte[] responseBytes = genericMethod.Invoke(this, new object[] { value, resolver }) as byte[];
         
            await writeStream.WriteAsync(responseBytes, 0, responseBytes.Length);

        }
    }
}
#endif
