using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using ConsoleApp1.Models;
using FakerLib;
using FakerLib.Generators.Custom;
using Faker = FakerLib.Faker;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var fakerConfig = new FakerConfig();
            fakerConfig.Add<User, string, NameGenerator>(user => user.Name);
            
            var faker = new Faker(config: fakerConfig);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var chats = new List<Chat>();
            for (int i = 0; i < 10; i++)
            {
                chats.Add(faker.Create<Chat>());
            }
            
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}