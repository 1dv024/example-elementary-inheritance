﻿using System;

namespace ElementaryInheritance
{
    class Program
    {
        static void Main()
        {
            // Skapar nytt Manager-objekt.
            var manager = new Manager("Ellen Nu", 760000, new DateTime(1997, 9, 1), 150000);

            // Skapar nya Employee-objekt.
            Employee[] employees =
            {
                new Employee("Nisse Hult", 287000, new DateTime(2005, 3, 1)),
                new Employee("Adam Bertilsson", 315000, new DateTime(2008, 12, 1)),
                new Employee("Ceasar Davidsson", 269000, new DateTime(2011, 8, 1))
            };

            // Presentation av de olika objekten.
            Console.WriteLine(manager);

            foreach (var e in employees)
            {
                Console.WriteLine(e);
            }

            // Ökar lönerna.
            manager.RaiseSalary(12.5m);

            foreach (var e in employees)
            {
                e.RaiseSalary(3.2m);
            }

            // Presentation av de olika objekten.
            Console.WriteLine(manager);

            foreach (var e in employees)
            {
                Console.WriteLine(e);
            }
        }
    }
}