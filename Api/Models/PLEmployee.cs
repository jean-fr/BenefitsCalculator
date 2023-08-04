namespace Api.Models;

public class PLEmployee : PLModelBase
{
    public decimal Salary { get; set; }
    public List<PLDependent> Dependents { get; set; } = new List<PLDependent>();
}
