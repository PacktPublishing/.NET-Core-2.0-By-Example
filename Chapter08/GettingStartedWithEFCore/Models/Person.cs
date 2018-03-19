using System;
using System.Collections.Generic;
using System.Text;

namespace GettingStartedWithEFCore.Models
{
    public class Person
    {
        public int Id { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool Gender { get; set; }

        public string Name { get; set; }

        public int Age
        {
            get
            {
                var age = DateTime.Now.Year - this.DateOfBirth.Year;

                if (DateTime.Now.DayOfYear < this.DateOfBirth.DayOfYear)
                {
                    age = age - 1;
                }

                return age;
            }
        }

    }
}
