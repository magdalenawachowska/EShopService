using Microsoft.AspNetCore.Mvc;
using EShop.Domain.Exceptions;
using EShop.Domain.Models;
using System.Net;
using EShop.Application.Service;

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
               // return StatusCode(414, new { message = ex.Message, code = 414 });
            }
            catch (CardNumberTooShortException ex)
            {
                return BadRequest(new {error = ex.Message, code = (int)HttpStatusCode.BadRequest});
                //return StatusCode(400, new { message = ex.Message, code = 400 });
            }
            catch(CardNumberInvalidException ex)
            {
                if (ex.Message == "Not recognized card provider")
                {
                    return StatusCode((int)HttpStatusCode.NotAcceptable, new { error = ex.Message, code = (int)HttpStatusCode.NotAcceptable });
                    //return StatusCode(406, new { message = ex.Message, code = 406 });
                }
                return BadRequest(new { error = ex.Message, code = (int)HttpStatusCode.BadRequest });
            }
        }


        // POST api/<CreditCardCoontroller>
        [HttpPost]
        public IActionResult Post([FromBody] Card card)
        {
            return Ok(card);
        }

        // PUT api/<CreditCardCoontroller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Card updatedCard)
        {
            return Ok("Updated");
        }

        // DELETE api/<CreditCardCoontroller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok("Removed");
        }
    }
}
