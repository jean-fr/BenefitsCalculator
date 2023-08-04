using Api.Dtos.Dependent;
 

namespace Api.Daos.Interfaces
{
    public interface IDependentDao
    {
        IList<Dependent> GetAll();
        IList<Dependent> GetByEmployeeId(int employeeId);
        Dependent? GetById(int id);
    }
}
