using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using WebApiComponents.MsgPack.MediaTypeFormatters;

namespace WebApiComponents.Extensions.DependencyInjection.MsgPack
{
    public static class MvcServicesExtensions
    {
        public static void AddMessagePack(this IMvcBuilder builder)
        {
            builder.Services.Add(new ServiceDescriptor(typeof(MessagePackOptions), new MessagePackOptions()));
            builder.AddMvcOptions((options) => options.InputFormatters.Add(new MessagePackInputMediaTypeFormatter()));
            builder.AddMvcOptions((options) => options.OutputFormatters.Add(new MessagePackOutputMediaTypeFormatter()));
        }

        public static void AddMessagePack(this IMvcBuilder builder, Action<MessagePackOptions> setupAction)
        {
            MessagePackOptions msgPackOptions = new MessagePackOptions();
            setupAction(msgPackOptions);
            builder.Services.Add(new ServiceDescriptor(typeof(MessagePackOptions), msgPackOptions));
            builder.AddMvcOptions((options) => options.InputFormatters.Add(new MessagePackInputMediaTypeFormatter()));
            builder.AddMvcOptions((options) => options.OutputFormatters.Insert(0, new MessagePackOutputMediaTypeFormatter()));
        }
    }
}
