using Api.Models;

namespace Api.Business.Interfaces
{
    public interface IEmployeeBusiness
    {
        IList<PLEmployee> GetAll();
        PLEmployee? GetById(int id);

        PLEmployeePaycheck GeneratePaycheck(int employeeId);
    }
}
