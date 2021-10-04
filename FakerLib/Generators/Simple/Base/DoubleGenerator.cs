using System;

namespace FakerLib.Generators.Simple.Base
{
    public class DoubleGenerator : ITypeGenerator<double>
    {
        public double Generate()
        {
            var bytes = new byte[sizeof(double)];
            new Random().NextBytes(bytes);
            
            return BitConverter.ToDouble(bytes);
        }
    }
}