using System;

namespace FakerLib.Generators.Simple.Base
{
    public class FloatGenerator : ITypeGenerator<float>
    {
        public float Generate()
        {
            var bytes = new byte[sizeof(float)];
            new Random().NextBytes(bytes);
            
            return BitConverter.ToSingle(bytes);
        }
    }
}