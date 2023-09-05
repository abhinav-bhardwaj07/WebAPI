using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Cache;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        List<Employee> TempList = new List<Employee>();

        public HomeController()
        {
            Employee e1 = new Employee { Id = 1, Name = "Deepak", Age = 25 };
            Employee e2 = new Employee { Id = 2, Name = "Raj", Age = 24 };
            Employee e3 = new Employee { Id = 3, Name = "Naveen", Age = 26 };
            TempList.Add(e1);
            TempList.Add(e2);
            TempList.Add(e3);
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

        [HttpDelete("DeleteRecord")]
        public IActionResult DeleteDetail(int id)
        {
            if(TempList.Where(x=> x.Id == id).Any())
            {
                TempList.Remove(TempList.Where(x => x.Id == id).First());
            }
            return Ok(TempList);
        }

        [HttpPut("UpdateDeatils")]
        public IActionResult UpdateDetail(Employee emp)
        {
            var RecordFound = TempList.Where(x => x.Id == emp.Id).FirstOrDefault();
            if (RecordFound != null)
            {
                RecordFound.Age = emp.Age;
                RecordFound.Name = emp.Name;
                return Ok(TempList);
            }
            else
            {
                return NotFound("No record with Id = "+ emp.Id.ToString() + " exits." );
            }
            
        }
    }
}
