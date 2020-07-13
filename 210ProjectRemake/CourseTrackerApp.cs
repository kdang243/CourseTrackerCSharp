using System;
using System.IO;
using _210ProjectRemake.Model;
using _210ProjectRemake.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace _210ProjectRemake
{
    public class CourseTrackerApp
    {

        public string SAVE_PATH = "/Users/kevindang/Projects/210ProjectRemake/210ProjectRemake/data/saved.json";
        public AcademicHistory AcademicHistory1 { get; set; }
        public AcademicHistory AcademicHistory2 { get; set; }
        public Term Term1 { get; set; }
        public Course Course1 { get; set; }
        public Component Component1 { get; set; }


        public CourseTrackerApp()
        {
            RunTracker();
        }

        public void RunTracker()
        {
            bool keepGoing = true;
            String key = null;
            AcademicHistory1 = LoadData<AcademicHistory>(SAVE_PATH);

            while (keepGoing)
            {
                DisplayMenuOptions();
                key = Console.ReadLine();
                key.ToUpper();

                if (key.Equals("B"))
                {
                    keepGoing = false;
                    AutoSave<AcademicHistory>(SAVE_PATH, AcademicHistory1,false);
                }
                else
                {
                    Process(key);
                }

                Console.WriteLine("\nBye! Hope to see you again soon!");
            }
        }

        private void Process(String key)
        {
            switch (key)
            {
                case "A":
                    AddTerm();
                    break;
                case "C":
                    AddCourse();
                    break;
                case "E":
                    AddComponent();
                    break;
                case "G":
                    AddAssignment();
                    break;
                case "I":
                    AddCourseToTerm();
                    break;
                case "J":
                    AddComponentToCourse();
                    break;
                case "K":
                    AddAssignmentToComp();
                    break;
                default:
                    Console.WriteLine("That's not a valid option! Please try again...");
                    break;
            }
        }

        private void AddTerm()
        {
            Console.WriteLine("What would you like to call this new term?");
            String input = Console.ReadLine();
            Term newUserTerm = new Term(input);
            try
            {
                AcademicHistory1.AddTerm(newUserTerm);
            }
            catch (PreExisitingTermException)
            {
                Console.WriteLine("This term already exists! Please try again...");
            }

            Console.WriteLine("A new term has been added!");
            DisplayTermOptions();

            String key = Console.ReadLine();
            key.ToUpper();
            Term1 = newUserTerm;

            if (key.Equals("I"))
            {
                Process(key);
            }
            
        }



        private void AddCourse()
        {
            Console.WriteLine("Which term would you like to add this course to?");

            String termName = Console.ReadLine();
            Term tempTerm = GetTerm(termName);

            Console.WriteLine("What's the name of this course?");
            String newCourseName = Console.ReadLine();
            Course newUserCourse = new Course(newCourseName);

            try
            {
                tempTerm.AddCourse(newUserCourse);
            }
            catch (PreExistingCourseException)
            {
                Console.WriteLine("This course already exists in this term! Please try again...");
            }

            Console.WriteLine("The course has been added!");
            DisplayCourseOptions();

            String key = Console.ReadLine();
            key.ToUpper();

            if (key.Equals("J"))
            {
                Process(key);
            }
        }

        private void AddComponent()
        {
            Console.WriteLine("In which term is this does this component belong to?");
            String temp = Console.ReadLine();

            Term term = GetTerm(temp);
            List<Course> courses = term.ListOfCourse;

            Console.WriteLine("Which course is this component for?");
            String courseName = Console.ReadLine().ToLower();

            Course course = GetCourse(courseName, courses);

            Console.WriteLine("What's the name of this component?");
            String componentName = Console.ReadLine();

            Console.WriteLine("What's the weight of this component? (In percentage)");
            String weightString = Console.ReadLine();
            Double weight = Convert.ToDouble(weightString);

            Component tempComponent = new Component(weight, componentName);
            try
            {
                course.AddComponent(tempComponent);
            }
            catch (PreExistingCompException)
            {
                Console.WriteLine("This component already exists in this course! Please try again...");
            }
            Console.WriteLine("Component has been added!");
            DisplayAssignmentOptions();
            String key = Console.ReadLine().ToUpper();
            Component1 = tempComponent;

            if (key.Equals("K"))
            {
                Process(key);
            }
        }


        private void AddAssignment()
        {
            Console.WriteLine("In which term is this does this assignment belong to?");
            String temp = Console.ReadLine();

            Term term = GetTerm(temp);
            List<Course> courses = term.ListOfCourse;

            Console.WriteLine("Which course is this assignment for?");
            String courseName = Console.ReadLine().ToLower();

            Course course = GetCourse(courseName, courses);
            List<Component> components = course.ListOfComponent;

            Console.WriteLine("What component does this assignment fall under?");
            String componentName = Console.ReadLine();

            Component component = GetComponent(componentName, components);

            Console.WriteLine("What's the title of this assignment?");
            String assignName = Console.ReadLine();

            Console.WriteLine("What'd you score on this assignment?");
            Double score = Convert.ToDouble(Console.ReadLine());

            Assignment tempAssign = new Assignment(assignName, score);
            try
            {
                component.AddAssignment(tempAssign);
            }
            catch (PreExistingAssignException)
            {
                Console.WriteLine("This assignment already exists for this component! Please try again");
            }

            Console.WriteLine("Assignment has been added!\nReturning back to your Academic History");
        }

        private void AddCourseToTerm()
        {
            Console.WriteLine("What's the name of this course?");
            String newCourseName = Console.ReadLine();
            newCourseName.ToLower();
            Course newUserCourse = new Course(newCourseName);

            try
            {
                Term1.AddCourse(newUserCourse);
            }
            catch (PreExistingCourseException)
            {
                Console.WriteLine("This course already exists in this term! Please try again...");
            }

            Console.WriteLine("The course has been added!");
            DisplayCourseOptions();

            String key = Console.ReadLine();
            key.ToUpper();
            Course1 = newUserCourse;

            if (key.Equals("J"))
            {
                Process(key);
            }
        }

        private void AddComponentToCourse()
        {
            Console.WriteLine("What's the name of this component?");
            String componentName = Console.ReadLine();

            Console.WriteLine("What's the weight of this component? (In percentage)");
            Double weight = Convert.ToDouble(Console.ReadLine());

            Component tempComponent = new Component(weight, componentName);
            try
            {
                Course1.AddComponent(tempComponent);
            }
            catch (PreExistingCompException)
            {
                Console.WriteLine("This component already exists in this course! Please try again...");
            }
            Console.WriteLine("Component has been added!");
            DisplayAssignmentOptions();
            String key = Console.ReadLine().ToUpper();

            Component1 = tempComponent;

            if (key.Equals("K"))
            {
                Process(key);
            }

        }

        private void AddAssignmentToComp()
        {
            Console.WriteLine("What's the title of this assignment?");
            String assignName = Console.ReadLine();

            Console.WriteLine("What'd you score on this assignment?");
            Double score = Convert.ToDouble(Console.ReadLine());

            Assignment tempAssign = new Assignment(assignName, score);
            try
            {
                Component1.AddAssignment(tempAssign);
            }
            catch (PreExistingAssignException)
            {
                Console.WriteLine("This assignment already exists for this component! Please try again");
            }
            Console.WriteLine("Assignment has been added!\nReturning back to your Academic History");
        }

        private Term GetTerm(String termName)
        {
            List<Term> listOfTerm = AcademicHistory1.ListOfTerm;
            List<String> tempList = new List<String>();

            foreach (var t in listOfTerm)
            {
                tempList.Add(t.TermName);
            }

            int index = tempList.IndexOf(termName);
            Term[] answerList = listOfTerm.ToArray();
            return answerList[index];
        }

        private Course GetCourse(string courseName, List<Course> courses)
        {
            List<String> temp = new List<String>();

            foreach (var c in courses)
            {
                String name = c.CourseName;
                temp.Add(name);
            }

            int index = temp.IndexOf(courseName);
            Course[] answer = courses.ToArray();
            return answer[index];
        }

        private Component GetComponent(string componentName, List<Component> components)
        {
            List<String> temp = new List<string>();

            foreach (var c in components)
            {
                String name = c.ComponentName;
                temp.Add(name);
            }

            int index = temp.IndexOf(componentName);
            Component[] answer = components.ToArray();
            return answer[index];
        }

        public void DisplayMenuOptions()
        {
            Console.WriteLine("\nChoose an option!");
            Console.WriteLine("\tA -> Add a new term!");
            Console.WriteLine("\tC -> Add a new course");
            Console.WriteLine("\tE -> Add a component");
            Console.WriteLine("\tG -> Add an assignment");
            Console.WriteLine("\tB -> Quit");
        }

        private void DisplayTermOptions()
        {
            Console.WriteLine("\nWould you like to add a course this term?");
            Console.WriteLine("\tI -> Add a course");
            Console.WriteLine("\tM -> Go back to main menu");
        }

        private void DisplayCourseOptions()
        {
            Console.WriteLine("\nWould you like to add a component to this course?");
            Console.WriteLine("\tJ -> Add a component");
            Console.WriteLine("\tM -> Go back to main menu");
        }

        private void DisplayAssignmentOptions()
        {
            Console.WriteLine("\nWould you like to add an assignment to this component?");
            Console.WriteLine("\tK -> Add an assignment");
            Console.WriteLine("\tM -> Go back to main menu");
        }

        public static void AutoSave<AcademicHistory>(string filePath, AcademicHistory objectToWrite, bool append = false) where AcademicHistory : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public static AcademicHistory LoadData<AcademicHistory>(string filePath) where AcademicHistory : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<AcademicHistory>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }


    }
}
