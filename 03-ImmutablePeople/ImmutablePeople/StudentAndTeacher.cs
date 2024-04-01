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
        public DateTime DateEnrolled { get; }

        public Student(string name, string password, DateTime dateEnrolled) : base(name, password)
        {
            DateEnrolled = dateEnrolled;
        }

        public Student() : this(string.Empty, string.Empty, default)
        {
        }
    }

    /// <summary>
    /// Teacher is an appropriate child for Person<Teacher>
    /// </summary>
    public class Teacher: Person<Teacher>
    {
        public int CoursesHeld { get; init; }

        public Teacher() : this(string.Empty, string.Empty, 0)
        {
        }

        public Teacher(string name, string password, int coursesHeld) : base(name, password)
        {
            CoursesHeld = coursesHeld;
        }
    }
}
