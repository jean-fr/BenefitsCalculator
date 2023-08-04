using Api.Models;

namespace Api.Dtos.Dependent;

public class Dependent : ModelBase
{
    public int EmployeeId { get; set; }
    public Relationship Relationship { get; set; }
}
