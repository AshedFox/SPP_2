using System;
using FakerLib.Generators.Simple.Base;

namespace FakerLib.Generators.Parameterized
{
    public class ArrayGenerator : IParameterizedTypeGenerator
    {
        public object Generate(Type parameterType)
        {
            var size = new Random().Next(0, 20);
            var array = Array.CreateInstance(parameterType, size);

            return array;
        }
    }
}