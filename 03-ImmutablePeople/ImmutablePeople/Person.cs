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

        //copy of the Person which will have a new value of Property
        //need copy due to immutability
        abstract public TPerson TPersonCopy(string copyName, string copyPassword);

        public TPerson WithName(string newName) => TPersonCopy(newName, Password);

        public TPerson WithPassword(string newPassword) => TPersonCopy(Name, newPassword);

    }
}
