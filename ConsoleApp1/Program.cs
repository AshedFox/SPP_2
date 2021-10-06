using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            
            var faker = new Faker(2, fakerConfig);
            
            var user = faker.Create<User>();

            var jsonOptions = new JsonSerializerOptions() { WriteIndented = true, IncludeFields = true };
            var json = JsonSerializer.Serialize(user,jsonOptions);
            
            Console.WriteLine(json);
            File.WriteAllText( Path.Combine(AppContext.BaseDirectory, "json.json"), json);
        }
    }
}