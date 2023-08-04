using Api.Models;
using System;

namespace ApiTests.IntegrationTests
{
    public  class Helper
    {
        public static PLEmployee Employee1 => new()
        {
            Id = 3,
            FirstName = "Michael",
            LastName = "Jordan",
            Salary = 143211.12m,
            DateOfBirth = new DateTime(1963, 2, 17)
        };
        public static PLEmployee Employee2 => new()
        {
            Id = 2,
            FirstName = "Ja",
            LastName = "Morant",
            Salary = 92365.22m,
            DateOfBirth = new DateTime(1999, 8, 10)
        };
        public static PLEmployee Employee3 => new()
        {
            Id = 3,
            FirstName = "Michael",
            LastName = "Jordan",
            Salary = 143211.12m,
            DateOfBirth = new DateTime(1963, 2, 17)
        };
    }
}
