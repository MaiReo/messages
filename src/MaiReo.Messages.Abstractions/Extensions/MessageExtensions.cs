#region 程序集 Version=2.1.1
/*
 * 获取消息额外信息的扩展方法
 */
#endregion
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MaiReo.Messages.Abstractions
{
    public static class MessageExtensions
    {
        /// <summary>
        /// 获取消息的话题或者默认值（类名）
        /// 名字最长不可超过32个字符
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string GetMessageTopicOrDefault<T>( this T message )
            where T : class, IMessage, new()
        {
            var type = typeof( T );
            var topicName = type
                .GetCustomAttribute
                    <MessageTopicAttribute>()
                ?.TopicName
                ?? type.Name;
            if (topicName.Length > 32)
                throw new NotSupportedException(
                    "Does not support such topic type " +
                    "which length is greater than 32." );
            return topicName;
        }
    }
}
