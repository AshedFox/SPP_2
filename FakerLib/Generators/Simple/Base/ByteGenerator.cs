using System;

namespace FakerLib.Generators.Simple.Base
{
    public class ByteGenerator : ITypeGenerator<byte>
    {
        public byte Generate() => (byte)(new Random().NextDouble() * byte.MaxValue);
    }
}