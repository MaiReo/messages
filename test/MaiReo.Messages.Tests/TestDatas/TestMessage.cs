using MaiReo.Messages.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Messages.Tests
{
    [MessageTopic( "TestTopic" )]
    public class TestMessage : IMessage
    {
        public string String { get; set; }
    }
}
