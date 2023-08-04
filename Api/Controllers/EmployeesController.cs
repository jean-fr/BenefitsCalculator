using Api.Business.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeBusiness _employeeBusiness;

    public EmployeesController(IEmployeeBusiness employeeBusiness)
    {
        _employeeBusiness = employeeBusiness;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public ActionResult<ApiResponse<PLEmployee>> Get(int id)
    {
        PLEmployee? employee = _employeeBusiness.GetById(id);

        if (employee == null)
        {
            return NotFound();
        }

        return new ApiResponse<PLEmployee>
        {
            Data = employee,
            Success = true
        };

    }

    [SwaggerOperation(Summary = "Get employee paycheck by employee Id")]
    [HttpGet("paycheck/{employeeId}")]
    public ActionResult<ApiResponse<PLEmployeePaycheck>> GetPaycheckByEmployeeId(int employeeId)
    {

        try
        {
            PLEmployeePaycheck paycheck = _employeeBusiness.GeneratePaycheck(employeeId);

            return new ApiResponse<PLEmployeePaycheck>
            {
                Data = paycheck
            };
        }
        catch (Exception ex)
        {

            if (ex.Message == "NotFound")
            {
                return NotFound();
            }
            else
            {
                throw new Exception(ex.Message);
            }

        }


    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public ActionResult<ApiResponse<List<PLEmployee>>> GetAll()
    {
        IList<PLEmployee> employees = _employeeBusiness.GetAll();

        return new ApiResponse<List<PLEmployee>>
        {
            Data = (List<PLEmployee>)employees
        };

    }
}
