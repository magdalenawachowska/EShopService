using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application
{
    public interface ICreditCardService
    {
        public bool ValidateCard(string cardNumber);

        public string GetCardType(string cardNumber);

    }
}
