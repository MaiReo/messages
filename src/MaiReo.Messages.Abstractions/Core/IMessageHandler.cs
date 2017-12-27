using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessageHandler
    {
    }

    public interface IMessageHandler<in T> : IMessageHandler
        where T : class, IMessage, new()
    {
        Task HandleMessageAsync( T message, DateTimeOffset timestamp );
    }
}
