using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; }
        public string Surname { get; set; }
        public byte Age { get; set; }
        public int ActivitiesCount { get; set; }
        public bool IsActive { get; set; }
        public decimal Income { get; set; }
        public string Phone { get; }
        public IEnumerable<Chat> Chats { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        
        public User(string phone, string name)
        {
            Phone = phone;
            Name = name;
        }
    }
}