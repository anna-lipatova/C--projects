using System;
using System.Collections.Generic;
using System.Text;

namespace ImmutablePeople
{
    public abstract class Person
    {
        public abstract string Name { get; }
        public abstract string Password { get; }

        abstract public Person TPersonCopy(string copyName = "", string copyPassword = "");

        public Person WithPassword(string newPassword) => TPersonCopy(Name, newPassword);

        public Person WithName(string newName) => TPersonCopy(newName, Password);

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
        public abstract override TPerson TPersonCopy(string copyName, string copyPassword);

        public new TPerson WithName(string newName) => TPersonCopy(newName, Password);

        public new TPerson WithPassword(string newPassword) => TPersonCopy(Name, newPassword);
        //*/
    }
}
