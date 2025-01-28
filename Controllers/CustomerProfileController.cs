using CustomerProfileApp.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CustomerProfileApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomerProfileController : ControllerBase
    {
        private readonly CustomerDbContext _context;
        private static List<Customer> _customerList = new List<Customer>();
        private readonly ILogger<CustomerProfileController> _logger;
        public CustomerProfileController(CustomerDbContext context, ILogger<CustomerProfileController> logger) { 
            _context= context;
            _logger = logger;
        }
        
        [HttpGet]
        public ActionResult<Customer> GetAllCustomerProfiles()
        {
            try
            {
                return Ok(_context.Set<Customer>().ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                { message = "An Error Occured", details = ex.Message });
            }
        }

        [HttpGet("{CustomerId}")]
        public ActionResult<Customer> GetCustomersById(int? CustomerId)
        {
            try
            {
                var cust = _context.Set<Customer>()
                            .Where(c => c.CustomerId == CustomerId)
                            .FirstOrDefault();
                if (cust == null)
                {
                    return NotFound();
                }
                return Ok(cust);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                { message = "An Error Occured", details = ex.Message });
            }
        }

        [HttpPost("AddCustomerProfile")]
        public ActionResult<Customer> AddCustomer([FromBody]Customer customer)
        {
            try
            {
                _context.Set<Customer>().Add(customer);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetAllCustomerProfiles), new { id = customer.CustomerId }, customer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                { message = "An Error Occured", details = ex.Message });
            }
        }
        [HttpPut("UpdateCustomerProfile")]
        public ActionResult<Customer> UpdateCustomer(int CustomerId, Customer customer)
        {
            try
            {
                var cust = _context.Set<Customer>().Find(CustomerId);
                if (CustomerId != customer.CustomerId)
                {
                    return BadRequest();
                }
                if (cust != null && CustomerId != customer.CustomerId) {
                    cust.FirstName = customer.FirstName; 
                    cust.LastName = customer.LastName;
                    cust.Gender = customer.Gender;
                    cust.Age = customer.Age;
                    cust.PhoneNumber = customer.PhoneNumber;
                    cust.Location = customer.Location;
                    _context.SaveChanges();
                }
                return Ok(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                { message = "An Error Occured", details = ex.Message });
            }
        }

        [HttpDelete("DeleteCustomerProfile")]
        public ActionResult<Customer> DeleteCustomer(int CustomerId)
        {
            try
            {
                var cust = _context.Set<Customer>().Find(CustomerId);
                if (cust == null) { return NotFound(); }
                if (cust != null)
                {
                    _context.Set<Customer>().Remove(cust);
                }
                _context.SaveChanges();
                return Ok(StatusCodes.Status200OK); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                { message = "An Error Occured", details = ex.Message });
            }
        }
    }
}
