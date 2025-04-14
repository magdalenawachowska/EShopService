using Microsoft.AspNetCore.Mvc;
using EShop.Domain.Exceptions;
using System.Net;
using EShop.Application.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {

        protected ICreditCardService _creditCardService;

        public CreditCardController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpGet]
        public IActionResult Get(string cardNumber)
        {
            try
            {
                _creditCardService.ValidateCard(cardNumber);
                return Ok(new { cardProvider = _creditCardService.GetCardType(cardNumber)});

            }
            catch(CardNumberTooLongException ex)
            {
                return StatusCode((int)HttpStatusCode.RequestUriTooLong, new { error = ex.Message, code = (int)HttpStatusCode.RequestUriTooLong });
            }
            catch (CardNumberTooShortException ex)
            {
                return BadRequest(new {error = ex.Message, code = (int)HttpStatusCode.BadRequest});
            }
            catch(CardNumberInvalidException ex)
            {
                return BadRequest(new { error = ex.Message, code = (int)HttpStatusCode.BadRequest });
            }
        }

        // GET api/<CreditCardCoontroller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CreditCardCoontroller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CreditCardCoontroller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CreditCardCoontroller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
