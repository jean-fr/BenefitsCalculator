using Api.Models;

namespace Api.Business.Interfaces
{
    public interface IDependentBusiness
    {
        IList<PLDependent> GetAll();
        IList<PLDependent> GetByEmployeeId(int employeeId);
        PLDependent? GetById(int id);
    }
}
