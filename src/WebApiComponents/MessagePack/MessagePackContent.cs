using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace WebApiComponents.MsgPack.Http
{
    public class MessagePackContent<T> : HttpContent
    {
        private IFormatterResolver resolver;
        T instance;
        public MessagePackContent(T instance, IFormatterResolver resolver) : this(instance)
        {
            this.resolver = resolver;
        }
        
        public MessagePackContent(T instance)
        {
            this.resolver = DefaultFormatterResolver.Instance;
            this.instance = instance;
            Headers.ContentType = new MediaTypeHeaderValue("application/msg-pack");
        }



        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            try
            {
                MessagePackSerializer.Serialize(stream, instance, resolver);
                tcs.SetResult(true);
            }
            catch (Exception ex)
            {

                tcs.SetException(ex);
                tcs.SetCanceled();
            }

            return tcs.Task;
            
        }

        
        protected override bool TryComputeLength(out long length)
        {
            length = 1;
            return true;
        }
    }
}
