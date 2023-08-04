using Api.Business.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{

    private readonly IDependentBusiness _dependentBusiness;

    public DependentsController(IDependentBusiness dependentBusiness)
    {
       _dependentBusiness= dependentBusiness;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public ActionResult<ApiResponse<PLDependent>> Get(int id)
    {
        PLDependent? dependent = _dependentBusiness.GetById(id);

        if(dependent == null)
        {
            return NotFound();
        }

        return new ApiResponse<PLDependent>
        {
            Data = dependent
        };
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public ActionResult<ApiResponse<List<PLDependent>>> GetAll()
    {
        IList<PLDependent> employees = _dependentBusiness.GetAll();

        return new ApiResponse<List<PLDependent>>
        {
            Data = (List<PLDependent>)employees
        };
    }
}
