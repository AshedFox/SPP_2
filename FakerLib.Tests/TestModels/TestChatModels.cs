using System;
using System.Collections.Generic;

namespace FakerLib.Tests.TestModels
{
    public class TestChat1
    {
        public TestChat1(TestUser1 creator)
        {
            Creator = creator;
        }
        /*public TestChat1(Guid id, string name)
        {
            Id = id;
            Name = name;
        }*/

        public Guid Id { get; }
        public string Name { get; }
        public DateTime CreatedAt { get; set; }
        public TestUser1 Creator { get; set; }
        public ICollection<TestMessage1> Messages { get; set; }
        public TestUser1[] Users { get; set; }
    }
    
    public class TestChat2
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public TestUser1 Creator { get; set; }
        public IEnumerable<TestMessage1> Messages { get; set; }
        public List<TestUser1> Users { get; set; }
    }
    
    public class TestChat3
    {
        public TestChat3(IEnumerable<TestMessage1> messages, List<TestUser1> users)
        {
            Messages = messages;
            Users = users;
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public TestUser1 Creator { get; set; }
        public IEnumerable<TestMessage1> Messages { get; }
        public List<TestUser1> Users { get; }
    }
    
    public class TestChat4
    {
        public Guid Id;
        public string Name;
        public DateTime CreatedAt;
        public TestUser1 Creator;
        public IEnumerable<TestMessage1> Messages;
        public IList<TestUser1> Users;
    }
}