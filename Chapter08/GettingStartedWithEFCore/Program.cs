

namespace GettingStartedWithEFCore
{
    using System;
    using System.Linq;
    using GettingStartedWithEFCore.Models;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting started with EF Core");
            Console.WriteLine("We will do CRUD operations on Person class.");
            //// Lets create an instance of Person class.

            Person person = new Person()
            {
                Name = "Rishabh Verma",
                Gender = true,   //// For demo true= Male, false = Female. Prefer enum in general cases.
                DateOfBirth = new DateTime(2000, 10, 23)
            };

            using (var context = new Context())
            {
                //// Context has strongly typed property named Persons which referes to Persons table.
                //// It has methods Add, Find, Update, Remove to perform CRUD among many others.
                //// Use AddRange to add multiple persons in once.
                //// Complete set of APIs can be seen by using F12 on the Persons property below.
                var personData = context.Persons.Add(person);
                //// Though we have done Add, nothing has actually happened in database. All changes are in context only.
                //// We need to call save changes, to persist these changes in the database.
                context.SaveChanges();

                //// Notice that Id is Primary Key (PK) and hence has not been specified in the person object passed to context.
                //// So, to know the created Id, we can use the below Id
                int createdId = personData.Entity.Id;
                //// If all goes well, person data should be persisted in the database.
                //// Use proper exception handling to discover unhandled exception if any.

                //// READ BEGINS
                Person readData = context.Persons.Where(j => j.Id == createdId).FirstOrDefault();

                //// UPDATE BEGINS
                person.Name = "Neha Shrivastava";
                person.Gender = false;
                person.DateOfBirth = new DateTime(2000, 6, 15);
                person.Id = createdId;   //// For update cases, we need this to be specified.

                context.Persons.Update(person);
                context.SaveChanges();

                //// DELETE
                context.Remove(readData);
                context.SaveChanges();
            }

            Console.WriteLine("All done. Please press Enter key to exit...");
            Console.ReadLine();
        }
    }
}
