using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MaiReo.Messages.Abstractions.Core
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>( T message ) where T : class, IMessage, new();
    }
   
}
