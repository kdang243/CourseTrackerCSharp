using System;
using System.Collections.Generic;

namespace _210ProjectRemake.Model
{
    public class Course
    {
        public string CourseName { get; set; }
        public List<Component> ListOfComponent { get; set; }
        public double FinalGrade { get; set; }
        public CourseGradeRefresher Refresher { get; set; }

        public Course(String name)
        {
            this.CourseName = name;
            ListOfComponent = new List<Component>();
            FinalGrade = 0;
        }

        public void AddComponent(Component component)
        {
            List<String> temp = new List<String>();

            foreach (var c in ListOfComponent)
            {
                temp.Add(c.ComponentName);

            }

            if (!temp.Contains(component.ComponentName))
            {
                ListOfComponent.Add(component);
            }
            else
            {
                throw new Exceptions.PreExistingCompException();
            }

            Refresher = new CourseGradeRefresher(ListOfComponent);
            FinalGrade = Refresher.RefreshFinal();
        }
    }
}
    

