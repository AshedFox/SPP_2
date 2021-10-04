using System;

namespace FakerLib.Generators.Simple.Base
{
    public class SByteGenerator : ITypeGenerator<sbyte>
    {
        public sbyte Generate()
        {
            var bytes = new byte[1];
            new Random().NextBytes(bytes);

            return (sbyte)bytes[0];
        }
    }
}