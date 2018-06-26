using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace WebApiHost.MediaFormatters
{
    public class ExtendedJsonMediaFormatter : JsonMediaTypeFormatter
    {
        public ExtendedJsonMediaFormatter()
        {
            this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override object ReadFromStream(Type type, Stream readStream, Encoding effectiveEncoding, IFormatterLogger formatterLogger)
        {
            Stopwatch clock = new Stopwatch();
            clock.Start();
            var result= base.ReadFromStream(type, readStream, effectiveEncoding, formatterLogger);
            clock.Stop();
            Console.WriteLine($"ExtendedJsonMediaFormatter.End - Default formatter took : {clock.Elapsed}");

            return result;

        }
    }
}
