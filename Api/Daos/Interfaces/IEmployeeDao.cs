using Api.Dtos.Employee;

namespace Api.Daos.Interfaces
{
    public interface IEmployeeDao
    {
        IList<Employee> GetAll();
        Employee? GetById(int id);

    }
}
