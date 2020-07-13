using System;
using System.Collections.Generic;

namespace _210ProjectRemake.Model
{
    public class ComponentGradeRefresher
    {
        List<Assignment> ListOfAssignments { get; set; }

        public ComponentGradeRefresher(List<Assignment> list)
        {
            this.ListOfAssignments = list;
        }

        public double RefreshGrade()
        {
            List<Double> temp = new List<Double>();

            foreach (var a in ListOfAssignments)
            {
                double score = a.Score;
                temp.Add(score);
            }

            int size = temp.Count;
            double sumOfScores = 0;

            foreach (var d in temp)
            {
                sumOfScores += d;
            }

            return sumOfScores / size;
        }
    }
}
