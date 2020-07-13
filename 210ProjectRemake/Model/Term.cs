using System;
using System.Collections.Generic;

namespace _210ProjectRemake.Model
{
    public class Term
    {
        public List<Course> ListOfCourse { get; set; }
        public string TermName { get; set; }

        public Term(string name)
        {
            this.TermName = name;
            ListOfCourse = new List<Course>();
        }

        public void AddCourse(Course course)
        {
            List<String> temp = new List<String>();

            foreach (var c in ListOfCourse)
            {
                String courseName = c.CourseName;
                temp.Add(courseName);
            }

            String newCourseName = course.CourseName;

            if(!temp.Contains(newCourseName))
            {
                this.ListOfCourse.Add(course);
            }
            else
            {
                throw new Exceptions.PreExistingCourseException();
            }
        }

       
    }
}
