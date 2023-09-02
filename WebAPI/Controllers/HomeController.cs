using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Cache;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        List<Employee> TempList = new List<Employee>();

        public HomeController()
        {
            Employee e1 = new Employee { Id = 1, Name = "Deepak", Age = 25 };
            Employee e2 = new Employee { Id = 2, Name = "Raj", Age = 24 };
            TempList.Add(e1);
            TempList.Add(e2);
        }

        [HttpGet("GetDetails")]
        public List<Employee> GetInitialDetail()
        {
            return  TempList;
        }


        [HttpPost("PostDetail")]
        public IActionResult PostEmployeeDetail(Employee emp)
        {
            TempList.Add(emp);

            return Ok(TempList);
        }
    }
}
