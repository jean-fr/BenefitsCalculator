namespace Api.Models;

public class PLDependent: PLModelBase
{
    public Relationship Relationship { get; set; }    
    public PLEmployee? Employee { get; set; }
}
