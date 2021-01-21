using HRmanagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRmanagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IDbRepo repo;
        public EmployeeController(IDbRepo repo)
        {
            this.repo = repo;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await repo.Employees.GetAllAsync();
            var recordbooks = await repo.Recordbooks.GetAllAsync();
            var positions = await repo.Positions.GetAllAsync();
            var dtos = new List<EmployeeForDpm>();
            foreach (var e in employees)
            {
                var rcbTemp = recordbooks.Find(rc => rc.Id == e.RecordbookId);
                var ps = positions.Find(p => p.Id == e.PositionId);
                var disc = await repo.Employees.GetAllDisc(e.Id);
                dtos.Add(new EmployeeForDpm(e, rcbTemp, ps, disc));
            }
            return Ok(dtos);
        }

        public class ChangePositionProps
        {
            public string EmployeeId { get; set; }
            public string PositionId { get; set; }
        }
        [HttpPost("change-pos")]
        public async Task<IActionResult> ChangePosition([FromBody]ChangePositionProps payload)
        {
            repo.Employees.ChangePosition(payload.EmployeeId, payload.PositionId);
            var res = await repo.Employees.SaveChangesAsync();
            if (res > 0) return Ok();
            else return BadRequest();
        }

        [HttpGet("getpositions")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPositions()
        {
            var pos = await repo.Positions.GetAllAsync();
            return Ok(pos);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        public class AddEmployeeProps
        {
            [Required(ErrorMessage = "Field is required")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Field is required")]
            public int PassportNumber { get; set; }

            [Required(ErrorMessage = "Field is required")]
            public int TaxNumber { get; set; }

            [Required(ErrorMessage = "Field is required")]
            public string DepartmentId { get; set; }

            [Required(ErrorMessage = "Field is required")]
            public string PositionId { get; set; }

            [Required(ErrorMessage = "Field is required")]
            public string ContractStart { get; set; }

            [Required(ErrorMessage = "Field is required")]
            public string ContractEnd { get; set; }
        }
        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddEmployeeProps payload)
        {
            var pp = new Passport();
            pp.Id = Guid.NewGuid().ToString();
            pp.Number = payload.PassportNumber;
            pp.Serie = "b";
            repo.Passports.Add(pp);
            //await repo.SaveChangesAsync();

            var rc = new Recordbook();
            rc.Id = Guid.NewGuid().ToString();
            rc.Workload = 0;
            rc.ContractStart = DateTime.Parse(payload.ContractStart);
            rc.ContractEnd = DateTime.Parse(payload.ContractEnd);
            repo.Recordbooks.Add(rc);
            //await repo.SaveChangesAsync();

            var emp = new Employee();
            emp.Id = Guid.NewGuid().ToString();
            emp.PassportId = pp.Id;
            emp.RecordbookId = rc.Id;
            emp.Name = payload.Name;
            emp.Degree = "aa";
            emp.DepartmentId = payload.DepartmentId;
            emp.PositionId = payload.PositionId;
            repo.Employees.Add(emp);
            var res = await repo.SaveChangesAsync();
            if (res > 0) return Ok("Added new employee");
            else return BadRequest();
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
