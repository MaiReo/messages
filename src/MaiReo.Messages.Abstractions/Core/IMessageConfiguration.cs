using MaiReo.Messages.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaiReo.Messages.Abstractions
{
    public interface IMessageConfiguration
    {
        #region Broker
        string BrokerAddress { get; set; }

        int BrokerPort { get; set; }

        #endregion Broker

        #region Publisher
        /// <summary>
        /// 发送重试次数(次)
        /// </summary>
        int? PublisherRetryCount { get; set; }
        /// <summary>
        /// 发送前等待时间(毫秒)
        /// </summary>
        int? PublisherDelayBeforeSend { get; set; }
        /// <summary>
        /// 消息发送超时(毫秒)
        /// </summary>
        int? PublisherTimeout { get; set; }

        #endregion Publisher

        #region Receiver

        ISet<string> Subscription { get; }

        string ReceiverGroupId { get; set; }
        bool? ReceiverAutoCommitEnabled { get; set; }
        int? ReceiverAutoCommitInterval { get; set; }

        #endregion Receiver

        #region Events

        event MessagePublishingEventHandler MessagePublishing;

        event MessageReceivingEventHandler MessageReceiving;

        Task OnMessagePublishingAsync( MessagePublishingEventArgs e, CancellationToken cancellationToken = default( CancellationToken ) );

        Task OnMessageReceivingAsync( MessageReceivingEventArgs e, CancellationToken cancellationToken = default( CancellationToken ) );

        #endregion Events

    }
}
