using Assessment.API.Data;
using Assessment.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assessment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCallController : ControllerBase
    {
        private readonly ServiceCallDbContext _context;
        
        public ServiceCallController(ServiceCallDbContext context)
        {
            this._context = context;
        }

        // method to get all ServiceCalls
        [HttpGet]   
        public async Task<IEnumerable<ServiceCall>> GetServiceCalls()
        {
            return await _context.ServiceCalls.ToListAsync();
        }

        // GetById (id)
        [HttpGet("{id}")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            ServiceCall serviceCall = await _context.ServiceCalls.FindAsync(id);   
            if (serviceCall == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(serviceCall);
            }            
        }

        // Post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateServiceCall (ServiceCall serviceCall)
        {
            await _context.ServiceCalls.AddAsync(serviceCall);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = serviceCall.Id }, serviceCall);
        }

        // Update (id)
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateServiceCall (int id, ServiceCall serviceCall)
        {
            if (id != serviceCall.Id)
            {
                return BadRequest();
            }
            _context.Entry(serviceCall).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Delete (id)
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteServiceCall(int id)
        {
            ServiceCall serviceCallToDelete = await _context.ServiceCalls.FindAsync(id);
            if (serviceCallToDelete == null)
            {
                return NotFound();
            }

            _context.ServiceCalls.Remove(serviceCallToDelete);
            await _context.SaveChangesAsync();
            return Ok();            
        }
    }
}
