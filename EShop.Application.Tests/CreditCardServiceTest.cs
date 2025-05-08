using EShop.Domain.Exceptions;
using EShop.Application.Service;

namespace EShop.Application.Tests
{
    public class CreditCardTest
    {
        [Theory]
        [InlineData("3497 7965 8312 797", true)]
        [InlineData("4532289052809181", true)]
        [InlineData("5551561443896215", true)]
        [InlineData("345-470-784-783-010", true)]
        [InlineData("4532 2080 2150 4434", true)]
        public void ValidateCard_CorrectValue_ReturnsTrue(string number, bool expected)
        {
            //Arrange
            var card_number = new CreditCardService();
            //Act
            var result = card_number.ValidateCard(number);          
            //Assert
            Assert.Equal(expected, result);
        }


        /* testy - za krotkie lub za dlugie numery kart
        [Theory]
        [InlineData("", false)]
        [InlineData("12345", false)]
        [InlineData("12345678901234567890123", false)]
        [InlineData("AB1234D901234", false)]
        public void ValidateCard_IncorrectValue_ReturnsFalse(string number, bool expected)
        {
            //Arrange
            var card_number = new CreditCardService();
            //Act
            var result = card_number.ValidateCard(number);
            //Assert
            Assert.Equal(expected, result);
        }
        */

        [Theory]
        [InlineData("")]                           
        [InlineData("12345")]
        [InlineData("123-456-789")]
        public void ValidateCard_TooShortNumber_ThrowsException(string number)  
        {
            //Arrange
            var card_number = new CreditCardService();

            //Act 
            //var result = card_number.ValidateCard(number);
            var action = () => card_number.ValidateCard(number);

            //Assert
            Assert.Throws<CardNumberTooShortException>(() => action);
        }

        [Theory]
        [InlineData("4024-0071-6540-1778--6540-1778")]                           
        [InlineData("4532289052809181289052809181")]
        [InlineData("1234 5678 9123 4567 8912 3456")]
        public void ValidateCard_TooLongNumber_ThrowsException(string number) 
        {
            //Arrange
            var card_number = new CreditCardService();

            //Act 
            var action = () => card_number.ValidateCard(number);

            //Assert
            Assert.Throws<CardNumberTooLongException>(() => action);
        }

        [Theory]
        [InlineData("AB1234D901234")]
        [InlineData("5551AC14438D6215")]
        [InlineData("45322890528091B1")]
        public void ValidateCard_NotAllDigits_ThrowsException(string number)
        {
            //Arrange
            var card_number = new CreditCardService();

            //Act 
            var action = () => card_number.ValidateCard(number);

            //Assert
            Assert.Throws<CardNumberInvalidException>(() => action);
        }

        [Fact]
        public void ValidateCard_NotAllDigits_ThrowsWithExpectedMessage()
        {
            //Arrange
            var card_number = new CreditCardService();

            //Act 
            var exception = Assert.Throws<CardNumberInvalidException>(() => card_number.ValidateCard("4024-0071-6540-I77B"));

            //Assert
            Assert.Equal("Invalid card number- not all characters ale digit", exception.Message);                  
        }

        [Fact]
        public void ValidateCard_NotValidWithLunaAlgorithm_ThrowsWithExpectedMessage()
        {
            //Arrange
            var cardNumber = new CreditCardService();
            //Act
            var exception = Assert.Throws<CardNumberInvalidException>(() => cardNumber.ValidateCard("453228905280918123"));
            //Assert
            Assert.Equal("Card number is not valid with Luna algorithm.", exception.Message);
        }


        [Theory]
        [InlineData("5131208517986691","MasterCard")]
        [InlineData("4532 2080 2150 4434", "Visa")]
        [InlineData("345-470-784-783-010", "American Express")]
        public void GetCardType_WhenGivenNumber_ReturnsCorrectType(string number, string expected_type)
        {

            //Arrange
            var card_number = new CreditCardService();
            //Act
            var result = card_number.GetCardType(number);
            //Assert
            Assert.Equal(expected_type, result);
        
        }

        [Fact]
        public void GetCardType_WhenGivenWrongNumber_ReturnsNotRecognized()
        {
            //Arrange
            var card_number = new CreditCardService();
            //Act
            var exception = Assert.Throws<CardNumberInvalidException>(() => card_number.GetCardType("3528145728933714"));
            //Assert
            Assert.Equal("Not recognized card provider", exception.Message);
        }

    }
}