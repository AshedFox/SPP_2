using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public User Sender { get; set; }
        public DateTime SentAt { get; set; }
        public List<int> AttachmentsIds { get; set; }
    }
}