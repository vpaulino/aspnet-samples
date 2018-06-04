using Components.MsgPack;
using MessagePack;

namespace Components.Mvc.MediaTypeFormatters.MsgPack
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