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
            }
            catch (CardNumberTooShortException ex)
            {
                return BadRequest(new {error = ex.Message, code = (int)HttpStatusCode.BadRequest});
            }
            catch(CardNumberInvalidException ex)
            {
                if (ex.Message == "Not recognized card provider")
                {
                    return StatusCode((int)HttpStatusCode.NotAcceptable, new { error = ex.Message, code = (int)HttpStatusCode.NotAcceptable });
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
            /*var card = _bazadanychkart.FirstOrDefault(c => c.Id == id);
            if (card == null)
                return NotFound(new { message = "Card doesn't exist" });

            card.CardNUmber = updatedCard.CardNumber;

            //to nizej to osobne rozwiazanie, mozliwe ze bledne xd

            if (card.Id == id && card != null)
            {
                card.CardNumber = updatedCard.CardNumber;
            }
            else
            {
               return NotFound(new { message = "Not found any credit card with given id" });
            }
            
            return NoContent();                         // 204 – aktualizacja zakończona bez zwracania danych
            */

            return Ok("Updated");
        }

        // DELETE api/<CreditCardCoontroller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            /*
            if (card.Id == id && card != null)
            {
                _bazadanychkart.Remove(card);
                return Ok(new { message = "Card removed" });
            }
            else
            {
                return NotFound(new { message = "Not found any credit card with given id" });
            }
            */
            return Ok("Removed");
        }
    }
}
