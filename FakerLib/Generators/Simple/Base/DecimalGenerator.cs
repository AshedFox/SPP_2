using System;

namespace FakerLib.Generators.Simple.Base
{
    public class DecimalGenerator : ITypeGenerator<decimal>
    {
        public decimal Generate()
        {
            var random = new Random();
            
            var scale = (byte) random.Next(29);
            var sign = random.Next(2) == 1;

            return new decimal(random.Next(), random.Next(), random.Next(), sign, scale);
        }
    }
}