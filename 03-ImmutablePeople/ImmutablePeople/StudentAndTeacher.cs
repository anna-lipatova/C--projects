using System;
using System.Collections.Generic;
using System.Text;

namespace ImmutablePeople
{
    /// <summary>
    /// Student is an appropriate child for Person<Student> 
    /// </summary>
    public class Student: Person<Student>
    {
        public DateTime DateEnrolled { get; init; }
    }

    /// <summary>
    /// Teacher is an appropriate child for Person<Teacher>
    /// </summary>
    public class Teacher: Person<Teacher>
    {
        public int CoursesHeld { get; init; }
    }
}
