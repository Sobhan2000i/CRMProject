using CRMProject.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace CRMProject.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomerController(ApplicationDbContext applicationDbContext , ApplicationIdentityDbContext applicationIdentityDbContext) : ControllerBase
    {
        //[HttpGet]
        //public async Task<ActionResult<>> GetAllCustomers()
        //{
        //    return string.Empty;
        //}
        //[HttpGet]
        //public async Task<ActionResult<>> GetCustomerById()
        //{
        //    return string.Empty;
        //}
    }
}
