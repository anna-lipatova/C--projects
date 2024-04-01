using System;
using System.Collections.Generic;
using System.Text;

namespace ImmutablePeople
{
    public class Student: Person
    {
        public DateTime DateEnrolled { get; init; }
    }

    public class Teacher: Person
    {
        public int CoursesHeld { get; init; }
    }
}
