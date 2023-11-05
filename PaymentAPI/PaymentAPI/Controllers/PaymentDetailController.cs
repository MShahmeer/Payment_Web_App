using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")] // Here [controller] is PaymentDetailController
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        //We are not creating the instance of the PaymentDetailController because it is automatically created by the ASP.NET core when the HTTP request occur. 
        private readonly PaymentDetailsContext _context;

        public PaymentDetailController(PaymentDetailsContext context)
        {
            //The value of context comes from the Dependency Injection we used in the Program.cs file
            _context = context;
        }

        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetails>>> GetPaymentDetails()
        {
          if (_context.PaymentDetails == null)
          {
              return NotFound();
          }
            return await _context.PaymentDetails.ToListAsync();
        }

        // GET: api/PaymentDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetails>> GetPaymentDetails(int id)
        {
          if (_context.PaymentDetails == null)
          {
              return NotFound();
          }
            var paymentDetails = await _context.PaymentDetails.FindAsync(id);

            if (paymentDetails == null)
            {
                return NotFound();
            }

            return paymentDetails;
        }

        // PUT: api/PaymentDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDetails(int id, PaymentDetails paymentDetails)
        {
            if (id != paymentDetails.PaymentDetailId)
            {
                return BadRequest();
            }

            _context.Entry(paymentDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)// if multiple people are trying to modify the data it will be handeled in this exception
            {
                if (!PaymentDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(await _context.PaymentDetails.ToListAsync());
        }

        // POST: api/PaymentDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaymentDetails>> PostPaymentDetails(PaymentDetails paymentDetails)
        {
            //the object that we pass from the swaggerUI to hit this post api will be binded to paymentDetails
          if (_context.PaymentDetails == null)
          {
                //This condition is checking the PaymentDetails table is present or not?
              return Problem("Entity set 'PaymentDetailsContext.PaymentDetails'  is null.");
          }
            _context.PaymentDetails.Add(paymentDetails); // if the table is available then add the corresponding object into that table
            await _context.SaveChangesAsync();//wait for the changes to get stored

            //In all this process the EntityFrameworkCore will take care to execute the naked SQL scripts to insert the data in the database

            return Ok(await _context.PaymentDetails.ToListAsync());
            //return CreatedAtAction("GetPaymentDetails", new { id = paymentDetails.PaymentDetailId }, paymentDetails);

            //response is returned with the CreatedAtAction and we can see that in the swagger the object paymentDetails with the updated primary key

            //A part from the last parameters the first 2 parameters are used to generate the URL to retrive the newly inserted record, and this URL is used in the Get method to get the record with that ID and render in the swagger.
        }

        // DELETE: api/PaymentDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetails(int id)
        {
            if (_context.PaymentDetails == null)
            {
                return NotFound();
            }
            var paymentDetails = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetails == null)
            {
                return NotFound();
            }

            _context.PaymentDetails.Remove(paymentDetails);
            await _context.SaveChangesAsync();

            return Ok(await _context.PaymentDetails.ToListAsync());
        }

        private bool PaymentDetailsExists(int id)
        {
            return (_context.PaymentDetails?.Any(e => e.PaymentDetailId == id)).GetValueOrDefault();
        }
    }
}
