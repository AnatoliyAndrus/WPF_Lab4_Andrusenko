using Andrusenko_Lab4.Models;
using System.Collections.ObjectModel;

namespace Andrusenko_Lab4.Tools
{
    class PersonGenerator
    {
        public static ObservableCollection<Person> GeneratePeople(int count)
        {
            ObservableCollection<Person> people = new ObservableCollection<Person>();
            Random random = new Random();

            string[] names = { "John", "Alice", "Michael", "Emily", "David", "Sophia", "James", "Emma", "William", "Olivia" };
            string[] surnames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor" };

            for (int i = 0; i < count; i++)
            {
                string name = names[random.Next(names.Length)];
                string surname = surnames[random.Next(surnames.Length)];
                string email = $"{name.ToLower()}.{surname.ToLower()}@gmail.com";
                DateTime dateOfBirth = GenerateRandomDateOfBirth(random);

                people.Add(new Person(name, surname, email, dateOfBirth));
            }

            return people;
        }

        private static DateTime GenerateRandomDateOfBirth(Random random)
        {
            DateTime startDate = DateTime.Today.AddYears(-100);
            int range = (DateTime.Today - startDate).Days;
            return startDate.AddDays(random.Next(range));
        }
    }
}
