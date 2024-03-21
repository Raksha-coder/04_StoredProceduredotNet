using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.context;
using WebApplication1.DTO;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
       

        //DI
        private readonly PersonContext _context;

        public PersonController(PersonContext context)
        {
            _context = context;
        }


        [HttpGet("getPerson/{id}")]
        public IActionResult GetPersonById(int id)
        {
            //var findId = await _context.Persons.Where(p => p.Id == id).FirstOrDefaultAsync();
            var storedid = _context.Persons.FromSqlRaw($"spGetPerson {id}").ToList();

            if(storedid != null)
            {
                return Ok(storedid);
            }
            else
            {
                return BadRequest();
            }
            
        }


        [HttpPost("PostPersonData")]

        public async Task<IActionResult> Post([FromBody] personDTO person)
        {
            try
            {
                    await _context.InsertDataAsync(person) ;
                    return Ok("added successfully");

            }catch(Exception ex)
            {
                    return BadRequest(ex.Message);
            }
        }

        [HttpPut("editPersonfirstname")]

        public async Task<IActionResult> put(int id,string firstname)
        {
                var updated = await _context.UpdateFirstnameAsync(id,firstname);
                if(updated != null)
                {
                    return Ok(updated);
                }
                else
                {
                    return BadRequest();
                }
        }

        [HttpDelete("DeleteById/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var storedid = await _context.DeleteDataAsync(id); 

            if(storedid != null )
            {
                return Ok("deleted successfully");
            }
            else
            {
                return BadRequest("error");
            }
        }





    }
}
