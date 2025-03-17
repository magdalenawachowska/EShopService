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


        [Theory]
        [InlineData("5131208517986691","MasterCard")]
        [InlineData("4532 2080 2150 4434", "Visa")]
        [InlineData("345-470-784-783-010", "American Express")]
        [InlineData("3528770266930341", "JCB")]
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
            var result = card_number.GetCardType("1ABC2345601E09");
            //Assert
            Assert.Equal("Not recognized", result);
        }


    }
}