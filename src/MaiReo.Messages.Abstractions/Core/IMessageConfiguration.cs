#region 程序集 Version=2.1.1
/*
 * 消息配置定义
 * 关于librdkafka的详细配置
 * 参考CONFIGURATION.md 
 * https://github.com/edenhill/librdkafka
 */
#endregion
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
        /// <summary>
        /// Kafka地址
        /// Kafka配置"bootstrap.servers"
        /// 详见librdkafka的Configuration
        /// </summary>
        string BrokerAddress { get; set; }
        /// <summary>
        /// Kafka端口
        /// Kafka配置"bootstrap.servers"
        /// 详见librdkafka的Configuration
        /// </summary>
        int BrokerPort { get; set; }

        #endregion Broker

        #region Publisher
        /// <summary>
        /// 发送重试次数(次)
        /// Kafka配置"retries"
        /// 详见librdkafka的Configuration
        /// </summary>
        int? PublisherRetryCount { get; set; }
        /// <summary>
        /// 发送前等待时间(毫秒)
        /// Kafka配置"linger.ms"
        /// 详见librdkafka的Configuration
        /// </summary>
        int? PublisherDelayBeforeSend { get; set; }
        /// <summary>
        /// 消息发送超时(毫秒)
        /// Kafka配置"message.timeout.ms"
        /// 详见librdkafka的Configuration
        /// BUG:当kafka未启动时无效，超时为librdkafka默认值
        /// </summary>
        int? PublisherTimeout { get; set; }

        #endregion Publisher

        #region Receiver
        /// <summary>
        /// 接收方订阅的消息Topic
        /// </summary>
        ISet<string> Subscription { get; }
        /// <summary>
        /// 接收方分组Id，必填
        /// Kafka配置"group.id"
        /// 详见librdkafka的Configuration
        /// </summary>
        string ReceiverGroupId { get; set; }
        /// <summary>
        /// Kafka配置"enable.auto.commit"
        /// 详见librdkafka的Configuration
        /// </summary>
        bool? ReceiverAutoCommitEnabled { get; set; }
        /// <summary>
        /// Kafka配置"auto.commit.interval.ms"
        /// 详见librdkafka的Configuration
        /// </summary>
        int? ReceiverAutoCommitInterval { get; set; }

        #endregion Receiver

        #region Events
        /// <summary>
        /// 消息发布事件
        /// 向此事件添加处理程序可以在发布消息后做些什么
        /// 例如记录日志
        /// </summary>
        event MessagePublishingEventHandler MessagePublishing;
        /// <summary>
        /// 消息接收事件
        /// 向此事件添加处理程序可以处理消息
        /// </summary>
        event MessageReceivingEventHandler MessageReceiving;
        /// <summary>
        /// 用于在类外触发<see cref="MessagePublishing"/>事件。
        /// </summary>
        /// <param name="e">消息体</param>
        /// <param name="cancellationToken">异步任务取消令牌</param>
        /// <returns>可等待的任务句柄</returns>
        Task OnMessagePublishingAsync( MessagePublishingEventArgs e,
            CancellationToken cancellationToken = default( CancellationToken ) );
        /// <summary>
        /// 用于在类外触发<see cref="MessageReceiving">事件
        /// </summary>
        /// <param name="e">消息体</param>
        /// <param name="cancellationToken">异步任务取消令牌</param>
        /// <returns>可等待的任务句柄</returns>
        Task OnMessageReceivingAsync( MessageReceivingEventArgs e,
            CancellationToken cancellationToken = default( CancellationToken ) );

        #endregion Events

    }
}
