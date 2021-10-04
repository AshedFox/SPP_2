using System;

namespace FakerLib.Generators.Simple.Advanced
{
    public class DateTimeGenerator: ITypeGenerator<DateTime>
    {
        public DateTime Generate()
        {
            return new DateTime(1970, 1, 1).AddYears(new Random().Next(0, 100))
                .AddMonths(new Random().Next(0, 12)).AddDays(new Random().Next(0, 30))
                .AddHours(new Random().Next(0, 24)).AddMinutes(new Random().Next(0, 60))
                .AddSeconds(new Random().Next(0, 60)).AddTicks(new Random().Next());
        }

        object ITypeGenerator.Generate()
        {
            return Generate();
        }
    }
}