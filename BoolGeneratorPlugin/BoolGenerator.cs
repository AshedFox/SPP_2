using System;
using FakerLib.Generators;

namespace BoolGeneratorPlugin
{
    public class BoolGenerator : ITypeGenerator<bool>
    {
        public bool Generate() => new Random().Next(2) != 0;
    }
}