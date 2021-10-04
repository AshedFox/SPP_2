using System;

namespace ConsoleApp1.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public User Sender { get; set; }
        public DateTime SentAt { get; set; }
    }
}