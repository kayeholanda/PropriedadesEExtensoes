using System;
using System.ComponentModel;

namespace Test
{
    static class PersonExtensions
    {
        public static string FullName(this Person value) => $"{value.LastName}, {value.FirstName}";
    }

    public class Person: INotifyPropertyChanged
    {
        public Person() { }

        [field:NonSerialized]
        public int Id { get; set; }

        // [SetsRequiredMembers]
        // public Person(string middleName) => MiddleName = middleName; 

        // public string? FirstName { get; set; } = string.Empty;
        private string? _firstName;
        public string? FirstName
        {
            get => _firstName;
            set 
            {
                _firstName = value;
                _formattedName = null;
            }
        }

        private string? _middleName;
        public string? MiddleName 
        { 
            get => _middleName; 
            set
            {
                _middleName = value;
                _formattedName = null;
            }
        }
        // public string? MiddleName { get; private set; }
        // public string MiddleName { get; }
        // public required string MiddleName { get; init; }

        private string? _lastName;
        public string? LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Last name must not be blank");
                if (value != _lastName)
                {
                    _lastName = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(LastName)));
                }
                _formattedName = null;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private string? _formattedName;
        public string FormattedName
        {
            get
            {
                if (_formattedName is null)
                    _formattedName = $"{FirstName} {MiddleName} {LastName}";
                return _formattedName;
            }
        }
    }

    public static class Program
    { 
        public static void Main()
        {
            Person aPerson = new() { FirstName = "Jack", LastName = "Johnson" };
            string FullName = aPerson.FullName();
            Console.WriteLine(FullName);

            Person person = new() { FirstName = "Kaye", LastName = "Holanda", MiddleName = "A."};
            Console.WriteLine(person.FormattedName);

            Console.WriteLine(person.LastName);
        }
    }
}
