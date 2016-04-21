using System;

namespace ElementaryInheritance
{
    // Klassen ärver automatiskt från Object även om det 
    // uttrycklingen inte står så.
    class Employee : IEquatable<Employee>
    {
        // Privata fält för namn och årslön.
        private string _name;
        private decimal _salary;

        // Konstruktor med tre parametrar vars värden används till att initiera 
        // objektet via egenskaperna.
        public Employee(string name, decimal salary, DateTime hireDay)
        {
            Name = name;
            Salary = salary;
            HireDay = hireDay;
        }

        // Publik autoimplementerad egenskap för antällningsdagen.
        public DateTime HireDay { get; set; }

        // Publik egenskap som kapslar in fältet _name och säkerställer
        // att _name inte tilldelas null eller en sträng med vita tecken.
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(value));
                }
                _name = value;
            }
        }

        // Publik egenskap som kapslar in fältet _salary och säkerställer 
        // att _salary inte tilldelas ett värde mindre än 0.
        public decimal Salary
        {
            get { return _salary; }
            set
            {
                if (value < 0m)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _salary = value;
            }
        }

        // Implementation av interfacet IEquatable<Employee> och indikerar om aktuellt
        // objekt är lika med specificerat objekt.
        public bool Equals(Employee other)
        {
            if (other == null || GetType() != other.GetType())
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetHashCode() != other.GetHashCode())
            {
                return false;
            }

            return string.Equals(Name, other.Name) &&
                   Salary.Equals(other.Salary) &&
                   HireDay.Equals(other.HireDay);
        }

        // Publik metod som ökar lönen med angiven procentsats.
        public void RaiseSalary(decimal percent) => Salary *= 1 + percent / 100m;

        // Överskuggar metoden Equals i basklassen Object och returnerar
        // ett booleskt värde indikerande om aktuellt objekt är lika med
        // specificerat objekt.
        public override bool Equals(object obj)
        {
            return Equals(obj as Employee);
        }

        // Överskuggar metoden GetHashCode i basklassen Object och returnerar
        // ett numeriskt värde av aktuellt objekt.
        // http://stackoverflow.com/a/263416/377476
        public override int GetHashCode()
        {
            unchecked // "overflow" är ok
            {
                var hash = (int) 2166136261;

                hash = (hash * 16777619) ^ Name?.GetHashCode() ?? 0;
                hash = (hash * 16777619) ^ Salary.GetHashCode();
                hash = (hash * 16777619) ^ HireDay.GetHashCode();

                return hash;
            }
        }

        // Överskuggar metoden ToString i basklassen Object och returnerar 
        // en textbeskrivning av aktuellt objekt.
        public override string ToString() => $"{Name}, {Salary:c0}, {HireDay.ToShortDateString()}";

        // Överlagring av operatorn ==
        public static bool operator ==(Employee e1, Employee e2)
        {
            // Om både e1 och e2 är null returnera true, eller om e1 är null 
            // men inte e2 returnera false, annars jämför de två objekten.
            return e1?.Equals(e2) ?? ReferenceEquals(e2, null);
        }

        // Överlagring av operatorn !=
        public static bool operator !=(Employee e1, Employee e2)
        {
            return !(e1 == e2);
        }
    }
}