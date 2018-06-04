using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using MessagePack.Formatters;

namespace WebApiComponents.MsgPack
{
    public class DefaultFormatterResolver : IFormatterResolver
    {
        public static readonly IFormatterResolver Instance = new DefaultFormatterResolver();


        public static readonly List<IFormatterResolver> Resolvers = new List<IFormatterResolver>
       {
                MessagePack.Resolvers.DynamicEnumAsStringResolver.Instance,
                 MessagePack.Resolvers.DynamicObjectResolver.Instance,
                MessagePack.Resolvers.StandardResolver.Instance,
                MessagePack.Resolvers.BuiltinResolver.Instance,
                MessagePack.Resolvers.DynamicGenericResolver.Instance,

        }; 

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.Formatter;
        }

        public static void Add(IFormatterResolver resolver)
        {
            if (!Resolvers.Contains(resolver))
                Resolvers.Insert(0, resolver);
        }

        public static void Register(IEnumerable<IFormatterResolver> resolvers)
        {
            Resolvers.AddRange(resolvers);
        }

        static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> Formatter;

            static FormatterCache()
            {

                foreach (var resolver in Resolvers)
                {
                    Formatter = resolver.GetFormatter<T>();
                    if (Formatter != null)
                    {
                        return;
                    }
                }
            }


        }
    }
}
