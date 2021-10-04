using System;
using System.Text;

namespace FakerLib.Generators.Simple.Base
{
    public class StringGenerator : ITypeGenerator<string>
    {
        private static readonly string AvailableChars;

        static StringGenerator()
        {
            AvailableChars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
        }
        
        public string Generate()
        {
            var random = new Random();
            var length = random.Next(0, 50);
            var stringBuilder = new StringBuilder();
            
            for (var i = 0; i < length; i++)
            {
                var charIndex = random.Next(0, AvailableChars.Length);
                stringBuilder.Append(AvailableChars[charIndex]);
            }

            return stringBuilder.ToString();
        }
    }
}