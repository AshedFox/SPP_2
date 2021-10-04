using System;
using FakerLib.Generators;

namespace IntGeneratorPlugin
{
    public class IntGenerator: ITypeGenerator<int>
    {
        public int Generate() => new Random().Next();
    }
}