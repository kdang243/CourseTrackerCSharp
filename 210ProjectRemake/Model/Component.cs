using System;
using System.Collections.Generic;

namespace _210ProjectRemake.Model
{
    public class Component
    {

        public List<Assignment> ListOfAssignments { get; set; }
        public double Weight { get; set; }
        public double GradeOfComponent { get; set; }
        public string ComponentName { get; set; }
        public ComponentGradeRefresher Refresher { get; set; }


        public Component(double weight, string name)
        {
            this.ComponentName = name;
            this.Weight = weight;
            ListOfAssignments = new List<Assignment>();
            GradeOfComponent = 0;
        }

        public void AddAssignment(Assignment assignment)
        {
            List<String> temp = new List<String>();

            foreach (var assign in ListOfAssignments)
            {
                temp.Add(assign.Name);
            }

            if (!temp.Contains(assignment.Name))
            {
                this.ListOfAssignments.Add(assignment);
            }
            else
            {
                throw new Exceptions.PreExistingAssignException();
            }

            Refresher = new ComponentGradeRefresher(ListOfAssignments);
            GradeOfComponent = Refresher.RefreshGrade();
        }
    }
}
