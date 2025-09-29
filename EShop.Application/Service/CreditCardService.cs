using System.Text.RegularExpressions;
using EShop.Domain.Enums;
using EShop.Domain.Exceptions;             

namespace EShop.Application.Service
{
    public class CreditCardService : ICreditCardService
    {
        public bool ValidateCard(string cardNumber)                  
        {
            if (string.IsNullOrEmpty(cardNumber))
                throw new CardNumberTooShortException("Card number is too short.");

            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");
            if (!cardNumber.All(char.IsDigit))
                throw new CardNumberInvalidException("Invalid card number- not all characters ale digit");

            if (cardNumber.Length > 19)
            {
                throw new CardNumberTooLongException("Card number is too long.");

            }
            else if (cardNumber.Length < 13)
            {
                throw new CardNumberTooShortException("Card number is too short.");
            }

            int sum = 0;
            bool alternate = false;                                      //flaga - identyfikuje co druga liczbe 

            for (int i = cardNumber.Length - 1; i >= 0; i--)             //algorytm Luhna
            {
                int digit = cardNumber[i] - '0';

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }

                sum += digit;
                alternate = !alternate;
            }

            if (sum % 10 == 0)
            {
                return true;
            }
            else
            {
                throw new CardNumberInvalidException("Card number is not valid with Luna algorithm.");
            }
        }

        public string GetCardType(string cardNumber)
        {
            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            if (Regex.IsMatch(cardNumber, @"^4(\d{12}|\d{15}|\d{18})$"))
                return CreditCardProvider.Visa.ToString();
            else if (Regex.IsMatch(cardNumber, @"^(5[1-5]\d{14}|2(2[2-9][1-9]|2[3-9]\d{2}|[3-6]\d{3}|7([01]\d{2}|20\d))\d{10})$"))
                return CreditCardProvider.MasterCard.ToString();

            else if (Regex.IsMatch(cardNumber, @"^3[47]\d{13}$"))
                return CreditCardProvider.AmericanExpress.ToString();

            else
                throw new CardNumberInvalidException("Not recognized card provider");
        }
    }
}
