using MessagePack;

namespace WebApiComponents.MsgPack.MediaTypeFormatters
{
    public class MessagePackOptions
    {

        public MessagePackOptions()
        {
            
        }

        public void AddResolver(IFormatterResolver resolver)
        {
            DefaultFormatterResolver.Add(resolver);
        }

        public IFormatterResolver Resolver { get { return DefaultFormatterResolver.Instance; } }
    }
}