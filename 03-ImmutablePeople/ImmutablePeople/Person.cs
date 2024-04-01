using System;
using System.Collections.Generic;
using System.Text;

namespace ImmutablePeople
{
    public abstract class Person
    {
        public abstract string Name { get; }
        public abstract string Password { get; }

    }


    /// <summary>
    /// i want only my children have access to me 
    /// </summary>
    /// <typeparam name="TPerson"></typeparam>
    abstract public class Person<TPerson>: Person where TPerson : Person<TPerson>, new()
    {
        public override string Name { get; }
        public override string Password { get; }

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

        ///*
        //copy of the Person which will have a new value of Property
        //need copy due to immutability
        abstract public TPerson TPersonCopy(string copyName, string copyPassword);

        public TPerson WithName(string newName) => TPersonCopy(newName, Password);

        public TPerson WithPassword(string newPassword) => TPersonCopy(Name, newPassword);
        //*/
    }
}
