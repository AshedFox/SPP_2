using System;

namespace FakerLib.Generators.Simple.Base
{
    public class CharGenerator : ITypeGenerator<char>
    {
        private static readonly string AvailableChars;

        static CharGenerator()
        {
            AvailableChars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
        }

        public char Generate()
        {
            var charIndex = new Random().Next(0, AvailableChars.Length);
            return AvailableChars[charIndex];
        }
    }
}