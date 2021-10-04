using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public class Chat
    {
        public Chat(User creator)
        {
            Creator = creator;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public User Creator { get; }
        public ICollection<Message> Messages { get; set; }
        public User[] Users { get; set; }
    }
}