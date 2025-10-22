using Grocery.Core.Helpers;
using Grocery.Core.Models;

namespace TestCore
{
    public class TestHelpers
    {
        [SetUp]
        public void Setup()
        {
        }


        //Happy flow
        [Test]
        public void TestPasswordHelperReturnsTrue()
        {
            string password = "user3";
            string passwordHash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            Assert.IsTrue(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        [TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        public void TestPasswordHelperReturnsTrue(string password, string passwordHash)
        {
            Assert.IsTrue(PasswordHelper.VerifyPassword(password, passwordHash));
        }


        //Unhappy flow
        [Test]
        public void TestPasswordHelperReturnsFalse()
        {
            string password = "user3";
            string passwordHash = "sxnIcZdYt8wC8MYWcQVQjQ";
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg")]
        [TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ")]
        public void TestPasswordHelperReturnsFalse(string password, string passwordHash)
        {
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [Test]
        public void TestProductSearchUnhappyPath()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product(1, "Appel", 5, new DateOnly(2025, 10, 21), 555),
                new Product(2, "Banaan", 3, new DateOnly(2025, 10, 21), 444),
                new Product(3, "Peer", 2, new DateOnly(2025, 10, 21), 777),
                new Product(4, "Ananas", 1, new DateOnly(2025, 10, 21), 555)
            };

            string searchTerm = "xyz";

            // Act
            var filtered = products.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            // Assert
            Assert.That(filtered.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestProductSearchHappyPath()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product(1, "Appel", 5),
                new Product(2, "Banaan", 3),
                new Product(3, "Peer", 2),
                new Product(4, "Ananas", 1)
            };
            string searchTerm = "an";

            // Act
            var filtered = products.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            // Assert
            Assert.That(filtered.Count, Is.EqualTo(2));
            Assert.That(filtered.Select(p => p.Name), Is.EqualTo(new[] { "Banaan", "Ananas" }));
        }
    }
}