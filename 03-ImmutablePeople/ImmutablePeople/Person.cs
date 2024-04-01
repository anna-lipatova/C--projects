using System;
using System.Collections.Generic;
using System.Text;

namespace ImmutablePeople
{
    /// <summary>
    /// i want only my children have access to me 
    /// </summary>
    /// <typeparam name="TPerson"></typeparam>
    abstract public class Person<TPerson> where TPerson : Person<TPerson>, new()
    {
        public string Name { get; }
        public string Password { get; }

        protected Person(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public static TPerson Default
        {
            get
            {
                return new TPerson();
            }
        }
    }
}
