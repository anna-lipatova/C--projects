using System;
using System.Collections.Generic;
using System.Text;

namespace ImmutablePeople
{
    /// <summary>
    /// i want only my children have access to me 
    /// </summary>
    /// <typeparam name="TPerson"></typeparam>
    abstract public class Person<TPerson> where TPerson : Person<TPerson>
    {
        public string Name { get; }
        public string Password { get; }
    }
}
