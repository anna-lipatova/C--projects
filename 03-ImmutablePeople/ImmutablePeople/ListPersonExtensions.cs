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

        public static List<TPerson> WithPasswordResetByFirstName<TPerson>(
            this IEnumerable<TPerson> people,
            string firstName,
            string newPassword
            )
            where TPerson : Person
        {
            var list = new List<TPerson>();

            string firstNameWithSpace = firstName + " ";

            foreach ( var person in people )
            {
                if (person.Name.StartsWith(firstNameWithSpace))
                {
                    list.Add((TPerson)person.WithPassword(newPassword));
                }
                else { list.Add(person); }
            }

            return list;
        }
    }
}
