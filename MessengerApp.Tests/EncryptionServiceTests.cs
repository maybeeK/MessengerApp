using MessengerApp.Client.Sevices;
using MessengerApp.Server.Services;
using Xunit;

namespace MessengerApp.Tests
{
    public class EncryptionServiceTests
    {
        [Fact]
        public async void ToEndcryptEqualsDecrypted()
        {
            // Arrange
            string text = "Hello World!";
            string text2 = "Hello World!";
            string pass = "pass123";
            EncryptionService es = new();

            //Act
            var encrypted = await es.EncryptAsync(text, pass);
            var decrypted = await es.DecryptAsync(encrypted, pass);

            // Assert
            Assert.Equal(text2, decrypted);
        }

        [Fact]
        public async void EncryptDecryptSymbols()
        {
            // Arrange
            string text = "!@#$%^^&**()_++_-=-~~``:\".,?\\/|<>";
            string pass = "pass123";
            EncryptionService es = new();

            //Act
            var encrypted = await es.EncryptAsync(text, pass);
            var decrypted = await es.DecryptAsync(encrypted, pass);

            // Assert
            Assert.Equal(text, decrypted);
        }

        [Fact]
        public async void PassAsGuid()
        {
            // Arrange
            string text = "Hello World!";
            string pass = "c829cdbe-407e-4586-89b0-cfe151827148";
            EncryptionService es = new();

            //Act
            var encrypted = await es.EncryptAsync(text, pass);
            var decrypted = await es.DecryptAsync(encrypted, pass);

            // Assert
            Assert.Equal(text, decrypted);
        }
        
        [Fact]
        public async void EncryptDecryptSpaces()
        {
            // Arrange
            string text = "                                         ";
            string pass = "c829cdbe-407e-4586-89b0-cfe151827148";
            EncryptionService es = new();

            //Act
            var encrypted = await es.EncryptAsync(text, pass);
            var decrypted = await es.DecryptAsync(encrypted, pass);

            // Assert
            Assert.Equal(text, decrypted);
        }
        
        [Fact]
        public async void EncryptDecryptWithDifferentServices()
        {
            // Arrange
            string text = "Hello World!";
            string pass = "pass123";
            EncryptionService es1 = new();
            EncryptionService es2 = new();


            //Act
            var encrypted = await es1.EncryptAsync(text, pass);
            var decrypted = await es2.DecryptAsync(encrypted, pass);

            // Assert
            Assert.Equal(text, decrypted);
        }

        [Fact]
        public async void EncryptDecryptCyrillicSymbols()
        {
            // Arrange
            string text = "Привіт! Я - Роман! В мене є їжак, його звати Йю'ящ))";
            string pass = "pass123";
            EncryptionService es = new();

            //Act
            var encrypted = await es.EncryptAsync(text, pass);
            var decrypted = await es.DecryptAsync(encrypted, pass);

            // Assert
            Assert.Equal(text, decrypted);
        }
    }
}