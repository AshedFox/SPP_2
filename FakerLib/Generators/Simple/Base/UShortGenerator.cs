using System;

namespace FakerLib.Generators.Simple.Base
{
    public class UShortGenerator : ITypeGenerator<ushort>
    {
        public ushort Generate()
        {
            var bytes = new byte[sizeof(ushort)];
            new Random().NextBytes(bytes);
            
            return BitConverter.ToUInt16(bytes);
        }
    }
}