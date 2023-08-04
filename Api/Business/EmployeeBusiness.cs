using Api.Business.Interfaces;
using Api.Daos.Interfaces;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

namespace Api.Business
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        private readonly IEmployeeDao _employeeDao;
        private readonly IDependentDao _dependentDao;
        private readonly IMapper _mapper;
        
        decimal monthlyBaseCost = 1000m;
        decimal dependentMonthlyCost = 600m;
        decimal dependentOver50MonthlyCost = 200m;
        decimal employeeMaximumSalaryToBeExempted = 80000m;
        int numberOfPaycheckPerYear = 26;

        public EmployeeBusiness(IEmployeeDao employeeDao, IDependentDao dependentDao, IMapper mapper)
        {
            _employeeDao = employeeDao;
            _dependentDao = dependentDao;
            _mapper = mapper;
        }

        public PLEmployeePaycheck GeneratePaycheck(int employeeId)
        {
            PLEmployee? employee = this.GetById(employeeId);

            if(employee == null)
            {
                throw new Exception("NotFound");
            }

            // monthlyTotalCost * 12 to get yearly one
            // 365 days / 14 days (2 weeks) = 26 paychecks
            // a paycheck per 14 days

            // calculate monthly total cost
            decimal monthlyTotalCost = CalculateTotalMonthlyCost(employee);

            decimal yearlyTotalCost = monthlyTotalCost * 12;// 12 months
            decimal yearlyNetSalary = employee.Salary - yearlyTotalCost;

            return new PLEmployeePaycheck()
            {
                Name = $"{employee.FirstName} {employee.LastName}",
                Amount =Math.Round(yearlyNetSalary / numberOfPaycheckPerYear, 2),
                Cost = Math.Round(yearlyTotalCost / numberOfPaycheckPerYear, 2)
            };

        }

        public IList<PLEmployee> GetAll()
        {
            IList<PLEmployee> result = new List<PLEmployee>();

            IList<Employee> employees = _employeeDao.GetAll();

            foreach (Employee employee in employees)
            {
                result.Add(BuildPLEmployee(employee));
            }

            return result;
        }

        public PLEmployee? GetById(int id)
        {
            Employee? employee = _employeeDao.GetById(id);

            if (employee != null) return BuildPLEmployee(employee);

            return null;
        }

        private PLEmployee BuildPLEmployee(Employee employee)
        {

            PLEmployee pLEmployee = _mapper.Map<PLEmployee>(employee);

            IList<Dependent> dependents = _dependentDao.GetByEmployeeId(employee.Id);

            pLEmployee.Dependents.AddRange(dependents.Select(d => _mapper.Map<PLDependent>(d)));

            return pLEmployee;
        }

       
        private decimal CalculateTotalMonthlyCost(PLEmployee employee)
        {
            // employees have a base cost of $1,000 per month(for benefits)
            decimal monthlyTotalCost = monthlyBaseCost;


            // employees that make more than $80,000 per year will incur an additional 2% of their yearly salary in benefits costs
            if (employee.Salary > employeeMaximumSalaryToBeExempted)
            {
                // 2% of salary
                decimal yearlyCostOver80 = employee.Salary * 0.02m;
                decimal monthlyCostOver80 = yearlyCostOver80 / 12; // 12 months

                monthlyTotalCost += monthlyCostOver80;
            }

            // each dependent represents an additional $600 cost per month(for benefits)
            if (employee.Dependents.Any())
            {
                monthlyTotalCost += dependentMonthlyCost * employee.Dependents.Count;

                // dependents that are over 50 years old will incur an additional $200 per month

                foreach(PLDependent dependent in employee.Dependents)
                {
                    int age = (int)((DateTime.Now - dependent.DateOfBirth).TotalDays / 365.242199);

                    if (age > 50)
                    {
                        monthlyTotalCost += dependentOver50MonthlyCost;
                    }
                }
            }

            return monthlyTotalCost;

        }
    }
}
