using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Tests
{
    [MessageTopic( "2TestTopic" )]
    public class TestMessage2 : IMessage
    {
        public string String { get; set; }
    }
}
