using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Andrusenko_Lab4.Tools.Exceptions;

namespace Andrusenko_Lab4.Models
{
    [Serializable]
    public class Person
    {
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _dateOfBirth;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
            }
        }
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
            }
        }

        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age))
                    age--;
                return age;
            }
        }

        private bool _isAdult;
        private string _sunSign;
        private string _chineseSign;
        private bool _isBirthday;

        public bool IsAdult
        {
            get => _isAdult;
            set
            {
                _isAdult = value;
            }
        }
        public string SunSign {
            get => _sunSign;
            set
            {
                _sunSign = value;
            }
        }
        public string ChineseSign
        {
            get => _chineseSign;
            set
            {
                _chineseSign = value;
            }
        }
        public bool IsBirthday
        {
            get => _isBirthday;
            set
            {
                _isBirthday = value;
            }
        }


        [JsonConstructor]
        public Person(string name, string surname, string email, DateTime dateOfBirth)
        {
            Name = name;
            Surname = surname;
            Email = email;
            DateOfBirth = dateOfBirth;
            IsAdult = CalculateIsAdult(dateOfBirth);
            SunSign = CalculateSunSign(dateOfBirth);
            ChineseSign = CalculateChineseSign(dateOfBirth);
            IsBirthday = CalculateIsBirthday(dateOfBirth);

            if (!CheckEmailValidity())
            {
                throw new InvalidEmailAddressException("Email must be valid!");
            }
            if (!CheckDateIsNotFarPast())
            {
                throw new DateIsInFarPastException("Person must be younger than 135 years old!");
            }
            if (!CheckDateIsNotFuture())
            {
                throw new DateIsInFutureException("Person must be at least 0 years old!");
            }
        }
        public Person(string name, string surname, string email)
            : this(name, surname, email, DateTime.MinValue)
        {
        }
        public Person(string name, string surname, DateTime dateOfBirth)
            : this(name, surname, null, dateOfBirth)
        {
        }

        

        private static bool CalculateIsAdult(DateTime birthday)
        {
            var today = DateTime.Today;
            var age = today.Year - birthday.Year;
            if (birthday.Date > today.AddYears(-age)) age--;
            return age >= 18;
        }
        private static string CalculateSunSign(DateTime birthday)
        {
            if (DateTime.Compare(new DateTime(birthday.Year, 1, 21), birthday) <= 0 && DateTime.Compare(new DateTime(birthday.Year, 2, 19), birthday) >= 0)
            {
                return "Aquarius";
            }
            else if (DateTime.Compare(new DateTime(birthday.Year, 2, 20), birthday) <= 0 && DateTime.Compare(new DateTime(birthday.Year, 3, 20), birthday) >= 0)
            {
                return "Pisces";
            }
            else if (DateTime.Compare(new DateTime(birthday.Year, 3, 21), birthday) <= 0 && DateTime.Compare(new DateTime(birthday.Year, 4, 20), birthday) >= 0)
            {
                return "Aries";
            }
            else if (DateTime.Compare(new DateTime(birthday.Year, 4, 21), birthday) <= 0 && DateTime.Compare(new DateTime(birthday.Year, 5, 21), birthday) >= 0)
            {
                return "Taurus";
            }
            else if (DateTime.Compare(new DateTime(birthday.Year, 5, 22), birthday) <= 0 && DateTime.Compare(new DateTime(birthday.Year, 6, 21), birthday) >= 0)
            {
                return "Gemini";
            }
            else if (DateTime.Compare(new DateTime(birthday.Year, 6, 22), birthday) <= 0 && DateTime.Compare(new DateTime(birthday.Year, 7, 23), birthday) >= 0)
            {
                return "Cancer";
            }
            else if (DateTime.Compare(new DateTime(birthday.Year, 7, 24), birthday) <= 0 && DateTime.Compare(new DateTime(birthday.Year, 8, 23), birthday) >= 0)
            {
                return "Leo";
            }
            else if (DateTime.Compare(new DateTime(birthday.Year, 8, 24), birthday) <= 0 && DateTime.Compare(new DateTime(birthday.Year, 9, 23), birthday) >= 0)
            {
                return "Virgo";
            }
            else if (DateTime.Compare(new DateTime(birthday.Year, 9, 24), birthday) <= 0 && DateTime.Compare(new DateTime(birthday.Year, 10, 23), birthday) >= 0)
            {
                return "Libra";
            }
            else if (DateTime.Compare(new DateTime(birthday.Year, 10, 24), birthday) <= 0 && DateTime.Compare(new DateTime(birthday.Year, 11, 22), birthday) >= 0)
            {
                return "Scorpio";
            }
            else if (DateTime.Compare(new DateTime(birthday.Year, 11, 23), birthday) <= 0 && DateTime.Compare(new DateTime(birthday.Year, 12, 21), birthday) >= 0)
            {
                return "Sagittarius";
            }
            else
            {
                return "Capricorn";
            }
        }
        private static string CalculateChineseSign(DateTime birthday)
        {
            switch (birthday.Year % 12)
            {
                case 0:
                    return "Monkey";
                case 1:
                    return "Rooster";
                case 2:
                    return "Dog";
                case 3:
                    return "Pig";
                case 4:
                    return "Rat";
                case 5:
                    return "Ox";
                case 6:
                    return "Tiger";
                case 7:
                    return "Rabbit";
                case 8:
                    return "Dragon";
                case 9:
                    return "Snake";
                case 10:
                    return "Horse";
                default:
                    return "Goat";
            }
        }
        private bool CalculateIsBirthday(DateTime birthday)
        {
            return DateOfBirth.Month == DateTime.Today.Month && DateOfBirth.Day == DateTime.Today.Day;
        }


        public bool CheckEmailValidity()
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(Email);
        }

        public bool CheckDateIsNotFarPast()
        {
            return Age <= 135;
        }

        public bool CheckDateIsNotFuture()
        {
            return Age >= 0;
        }





    }
}
