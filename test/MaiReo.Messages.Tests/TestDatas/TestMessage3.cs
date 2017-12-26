using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Tests
{
    [MessageTopic( "3TestTopic" )]
    public class TestMessage3 : IMessage
    {
        public string String { get; set; }
    }
}
