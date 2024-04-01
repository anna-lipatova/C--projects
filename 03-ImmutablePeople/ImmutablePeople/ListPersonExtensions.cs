using System;
using System.Collections.Generic;
using System.Text;

namespace ImmutablePeople
{
    public static class ListPersonExtensions
    {
        public static void PrintAll(this IEnumerable<Person> people)
        {
            foreach (var person in people)
            {
                Console.WriteLine($"{person.GetType().Name} {person.Name} has password \"{person.Password}\"");
            }
        }
    }
}
