using System;
using System.Collections.Generic;

namespace _210ProjectRemake.Model
{
    public class AcademicHistory
    {
        public List<Term> ListOfTerm { get; set; }

        public AcademicHistory()
        {
            ListOfTerm = new List<Term>();
        }

        public void AddTerm(Term term)
        {
            List<String> temp = new List<String>();

            foreach (var t in ListOfTerm)
            {
                String termName = t.TermName;
                temp.Add(termName);
            }

            String newTermName = term.TermName;

            if (!temp.Contains(newTermName))
            {
                this.ListOfTerm.Add(term);
            }
            else
            {
                throw new Exceptions.PreExisitingTermException();
            }
        }

        public void RemoveTerm(String name)
        {
            List<String> temp = new List<String>();

            foreach (var t in ListOfTerm)
            {
                String termName = t.TermName;
                temp.Add(termName);
            }

            int index = temp.IndexOf(name);
            Term term = ListOfTerm[index];
            ListOfTerm.Remove(term);
        }
    }
}
