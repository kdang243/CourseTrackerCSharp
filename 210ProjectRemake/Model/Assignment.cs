using System;
namespace _210ProjectRemake.Model
{
    public class Assignment
    {

        public string Name { get; set; }
        public double Score { get; set; }

        public Assignment(string title, double percentage)
        {
            this.Name = title;
            this.Score = percentage;
        }
    }
}
