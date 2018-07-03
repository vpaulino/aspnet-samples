using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using MessagePack.Formatters;

namespace Components.Web.Server.Tests.Models.Formatters
{
    public class ModelsFormatterResolver : IFormatterResolver
    {
        public static readonly IFormatterResolver Instance = new ModelsFormatterResolver();
        
        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return new ProductMessagePackFormatter() as IMessagePackFormatter<T>;
        }

        static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> Formatter;

            static FormatterCache()
            {

                Formatter = Instance.GetFormatter<T>();
            }


        }

    }
}
