using Api.Business.Interfaces;
using Api.Daos.Interfaces;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

namespace Api.Business
{
    public class DependentBusiness : IDependentBusiness
    {
        private readonly IEmployeeDao _employeeDao;
        private readonly IDependentDao _dependentDao;
        private readonly IMapper _mapper;
        public DependentBusiness(IEmployeeDao employeeDao, IDependentDao dependentDao, IMapper mapper)
        {
            _employeeDao = employeeDao;
            _dependentDao = dependentDao;
            _mapper = mapper;
        }
        public IList<PLDependent> GetAll()
        {
            IList<Dependent> dependents = _dependentDao.GetAll();

            return dependents.Select(d => BuildPLDependent(d)).ToList();
        }

        public IList<PLDependent> GetByEmployeeId(int employeeId)
        {
            IList<Dependent> dependents = _dependentDao.GetByEmployeeId(employeeId);

            return dependents.Select(d => BuildPLDependent(d)).ToList();

        }

        public PLDependent? GetById(int id)
        {
            Dependent? dependent = _dependentDao.GetById(id);
            if (dependent == null) return null;

            return BuildPLDependent(dependent);
        }

        private PLDependent BuildPLDependent(Dependent dependent)
        {
            Employee? employee = _employeeDao.GetById(dependent.EmployeeId);

            PLDependent d = _mapper.Map<PLDependent>(dependent);

            if (employee != null)
            {
                PLEmployee pLEmployee = _mapper.Map<PLEmployee>(employee);
                d.Employee = pLEmployee;
            }
            return d;
        }
    }
}
