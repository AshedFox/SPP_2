using System;

namespace FakerLib.Generators.Simple.Base
{
    public class ULongGenerator : ITypeGenerator<ulong>
    {
        public ulong Generate()
        {
            var bytes = new byte[sizeof(ulong)];
            new Random().NextBytes(bytes);
            
            return BitConverter.ToUInt64(bytes);
        }
    }
}