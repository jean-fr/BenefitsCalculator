using Api.Daos.Interfaces;
using Api.Dtos.Dependent;
using Api.Models;

namespace Api.Daos
{
    public class DependentDao : IDependentDao
    {
        // for the sake of this test, considering this as a datasource/DB
        private readonly List<Dependent> _dependents = new()
        {
           new()
            {
                Id = 1,
                FirstName = "Spouse",
                LastName = "Morant",
                Relationship = Relationship.Spouse,
                DateOfBirth = new DateTime(1998, 3, 3),  
                EmployeeId=2
            },
            new()
            {
                Id = 2,
                FirstName = "Child1",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2020, 6, 23),  
                EmployeeId=2
            },
            new()
            {
                Id = 3,
                FirstName = "Child2",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2021, 5, 18),  
                EmployeeId=2
            },
            new()
            {
                Id = 4,
                FirstName = "DP",
                LastName = "Jordan",
                Relationship = Relationship.DomesticPartner,
                DateOfBirth = new DateTime(1974, 1, 2),
                EmployeeId=3
            }
        };
        public IList<Dependent> GetAll()
        {
            return _dependents;
        }

        public IList<Dependent> GetByEmployeeId(int employeeId)
        {
            return _dependents.FindAll(d => d.EmployeeId == employeeId);
        }

        public Dependent? GetById(int id)
        {
            return _dependents.FirstOrDefault(e => e.Id == id);
        }
    }
}
