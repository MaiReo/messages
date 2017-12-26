using MaiReo.Messages.Abstractions;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Publisher
{
    public class NetmqXPublisherWrapper : IMessagePublisherWrapper, IDisposable
    {
        private readonly IMessageConfiguration _configuration;
        public NetmqXPublisherWrapper(
            IMessageConfiguration configuration )
        {
            if (!configuration.IsValid())
                throw new ArgumentException( "configuration is invalid!",
                    nameof( configuration ) );
            this._configuration = configuration;
        }
        private PublisherSocket _publisher;
        public virtual void Connect()
        {
            if (disposedValue) throw new NotSupportedException( "Object has disposed." );
            if (_publisher != null)
                return;
            var connectionString = $">{_configuration.Schema}://{_configuration.ListenAddressForPubSub}:{_configuration.XSubPort}";
#if DEBUG
            System.Diagnostics.Debug.WriteLine( $"[{nameof( NetmqXPublisherWrapper )}] Pub on:{connectionString}" );
#endif
            _publisher = new PublisherSocket( connectionString );
            _publisher.Options.SendHighWatermark
                = _configuration.HighWatermark;

        }
        public virtual void Disconnect()
        {
            _publisher?.Dispose();
            _publisher = null;
        }

        public virtual void Send( IMessageWrapper message )
        {
            if (disposedValue) throw new NotSupportedException("Object has disposed."); 
            if (_publisher == null) Connect();
            _publisher.SendMoreFrame( message.Topic )
                .SendMoreFrame( BitConverter.GetBytes(
                    message.Timestamp.ToUnixTimeMilliseconds() ) )
                .SendFrame( message.Message );
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose( bool disposing )
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _publisher?.Dispose();
                }

                _publisher = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose( true );
        }
        #endregion

        
        
    }
}
