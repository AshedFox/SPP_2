using System;
using System.Collections.Generic;

namespace FakerLib.Tests.TestModels
{
    public class TestUserRecursive1
    {
        public TestUserRecursive2 TestUserRecursive2 { get; set; }
    }
    
    public class TestUserRecursive2
    {
        public TestUserRecursive3 TestUserRecursive3 { get; set; }
        public TestUserRecursive1 TestUserRecursive1 { get; set; }
    }
    
    public class TestUserRecursive3
    {
        public TestUserRecursive4 TestUserRecursive4 { get; set; }
        public TestUserRecursive1 TestUserRecursive1 { get; set; }
    }
    
    public class TestUserRecursive4
    {
        public TestUserRecursive1 TestUserRecursive1 { get; set; }
    }

    public class TestUser1
    {
        public Guid Id { get; set; }
        public string Name { get; }
        public string Surname { get; set; }
        public byte Age { get; set; }
        public int ActivitiesCount { get; set; }
        public bool IsActive { get; set; }
        public decimal Income { get; set; }
        public string Phone { get; }
        public TestChat1[] Chats { get; set; }
        public IEnumerable<TestMessage1> Messages { get; set; }
        
        public TestUser1(string phone, string name)
        {
            Phone = phone;
            Name = name;
        }
    }
    
    public class TestUser2
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte Age { get; set; }
        public uint ActivitiesCount { get; set; }
        public bool IsActive { get; set; }
        public float Income { get; set; }
        public string Phone { get; set; }
    }
    
    public class TestUser3
    {
        public Guid Id { get; set; }
        public string Name { get; }
        public string Surname { get; }
        public byte Age { get; }
        public uint ActivitiesCount { get; set; }
        public bool IsActive { get; set; }
        public float Income { get; set; }
        public string Phone { get; set; }
    }
    
    public class TestUser4
    {
        public Guid Id;
        private readonly string _name;
        private readonly string _surname;
        private readonly byte _age;
        public uint ActivitiesCount;
        public bool IsActive;
        public float Income;
        public string Phone;
    }
}