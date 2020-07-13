using System;
using System.Collections.Generic;

namespace _210ProjectRemake.Model
{
    public class CourseGradeRefresher
    {
        public List<Component> ListOfComponent { get; set; }
        
        public CourseGradeRefresher(List<Component> listofcomponent)
        {
            this.ListOfComponent = listofcomponent;
        }

        public double RefreshFinal()
        {
            List<Double> temp = new List<Double>();

            foreach (var c in ListOfComponent)
            {
                double weight = c.Weight / 100;
                double grade = c.GradeOfComponent;
                double part = weight * grade;
                temp.Add(part);
            }

            double answer = 0;

            foreach (var d in temp)
            {
                answer += d;
            }

            return answer;
        }
    }
}
