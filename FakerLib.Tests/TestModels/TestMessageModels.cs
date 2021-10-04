using System;

namespace FakerLib.Tests.TestModels
{
    public class TestMessage1
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public TestUser1 Sender { get; set; }
        public DateTime SentAt { get; set; }
    }
    
    public class TestMessage2
    {
        public TestMessage2(TestUser1 sender)
        {
            Sender = sender;
        }

        public Guid Id { get; set; }
        public string Content { get; set; }
        public TestUser1 Sender { get; }
        public DateTime SentAt { get; set; }
    }
    
    public class TestMessage3
    {
        public Guid Id { get; }
        public string Content { get; }
        public TestUser1 Sender { get; set; }
        public DateTime SentAt { get; set; }
    }
    
    public class TestMessage4
    {
        private Guid _id;
        public string Content;
        public TestUser1 Sender;
        public DateTime SentAt;
    }
}