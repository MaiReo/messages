using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>( T message,
            DateTimeOffset? timestamp = null,
            [CallerMemberName]string callerMemberName = "")
            where T : class, IMessage, new();
    }

}
