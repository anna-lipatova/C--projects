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

        //for .Default
        public Student() : this(string.Empty, string.Empty, default)
        {
        }

        public override Student TPersonCopy(string copyName, string copyPassword)
        {
            return new Student(copyName, copyPassword, DateEnrolled);
        }
    }

    /// <summary>
    /// Teacher is an appropriate child for Person<Teacher>
    /// </summary>
    public class Teacher: Person<Teacher>
    {
        public int CoursesHeld { get; init; }

        //for .Default
        public Teacher() : this(string.Empty, string.Empty, 0)
        {
        }

        public Teacher(string name, string password, int coursesHeld) : base(name, password)
        {
            CoursesHeld = coursesHeld;
        }

        public override Teacher TPersonCopy(string copyName, string copyPassword)
        {
            return new Teacher(copyName, copyPassword, CoursesHeld);
        }
    }
}
