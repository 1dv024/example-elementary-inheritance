using System;

namespace ElementaryInheritance
{
    // Klassen ärver allt från Employee men
    // kommer inte åt privata medlemmar i 
    // Employee.
    class Manager : Employee, IEquatable<Manager>
    {
        // Privat fält för bonus.
        private decimal _bonus;

        // Konstruktor med tre parametrar vars värden används
        // till att initiera objektet via basklassens konstruktor
        // och egenskapen.
        public Manager(string name, decimal salary, DateTime hireDay, decimal bonus)
            : base(name, salary, hireDay)
        {
            Bonus = bonus;
        }

        // Publik egenskap som kapslar in fältet
        // _bonus och säkerställer att _bonus inte
        // tilldelas ett värde mindre än 0.
        public decimal Bonus
        {
            get { return _bonus; }
            set
            {
                if (value < 0m)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _bonus = value;
            }
        }

        // Publik "read-only" egenskap som ger
        // den totala årslönen, d.v.s summan av
        // årlönen (från basklassen) och bonusen.
        public decimal AnnualPay => Salary + Bonus;

        // Implementation av interfacet IEquatable<Manager> och indikerar om aktuellt
        // objekt är lika med specificerat objekt.
        public bool Equals(Manager other)
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

            return base.Equals(other) &&
                   Bonus.Equals(other.Bonus);
        }

        // Överskuggar metoden Equals i basklassen Employee och returnerar
        // ett booleskt värde indikerande om aktuellt objekt är lika med
        // specificerat objekt.
        public override bool Equals(object obj)
        {
            return Equals(obj as Manager);
        }

        // Överskuggar metoden GetHashCode i basklassen Employee och returnerar
        // ett numeriskt värde av aktuellt objekt.
        // http://stackoverflow.com/a/263416/377476
        public override int GetHashCode()
        {
            unchecked // "overflow" är ok
            {
                var hash = (int) 2166136261;

                hash = (hash * 16777619) ^ base.GetHashCode();
                hash = (hash * 16777619) ^ Bonus.GetHashCode();

                return hash;
            }
        }

        // Överskuggar metoden ToString i basklassen Employee och
        // returnerar en textbeskrivning av anropande objekt.
        public override string ToString() => $"{Name}, {AnnualPay:c0}, {HireDay.ToShortDateString()}";

        // Överlagring av operatorn ==
        public static bool operator ==(Manager m1, Manager m2)
        {
            // Om både m1 och m2 är null returnera true, eller om m1 är null 
            // men inte m2 returnera false, annars jämför de två objekten.
            return ReferenceEquals(m1, null) ? ReferenceEquals(m2, null) : m1.Equals(m2);
        }

        // Överlagring av operatorn !=
        public static bool operator !=(Manager m1, Manager m2)
        {
            return !(m1 == m2);
        }
    }
}