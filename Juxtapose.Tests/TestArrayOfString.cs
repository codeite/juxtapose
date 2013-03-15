using NUnit.Framework;
using Shouldly;

namespace Juxtapose.Tests
{
    [TestFixture]
    public class TestArrayOfString
    {
        [Test]
        public void TestArrayInOrderIsEqual()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison();
            var baseArray = new string[] {"One", "Two", "Three"};
            var same = new string[] { "One", "Two", "Three" };

            // Act
            var areEqual = compare.CompareSequence(baseArray, same);

            // Assert
            areEqual.ShouldBe(true);
        } 
        
        [Test]
        public void TestShorterArray()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison();
            var baseArray = new string[] {"One", "Two", "Three"};
            var shorter = new string[] { "One", "Two" };

            // Act
            var isShorter = compare.CompareSequence(baseArray, shorter);

            // Assert
            isShorter.ShouldBe(false);
        }        
        
        [Test]
        public void TestLongerArray()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison();
            var baseArray = new string[] {"One", "Two", "Three"};
            var longer = new string[] { "One", "Two", "Three", "Four" };

            // Act
            var isLonger = compare.CompareSequence(baseArray, longer);

            // Assert
            isLonger.ShouldBe(false);
        }

        [Test]
        public void TestDifferentContent()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison();
            var baseArray = new string[] { "One", "Two", "Three" };
            var diff = new string[] { "One", "Two", "Baloon" };

            // Act
            var isDifferent = compare.CompareSequence(baseArray, diff);

            // Assert
            isDifferent.ShouldBe(false);
        }

        [Test]
        public void TestDifferentOrderAllowed()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison()
                .IgnoreSequenceOrder();
            
            var baseArray = new string[] { "One", "Two", "Three" };
            var diffOrder1 = new string[] { "One", "Three", "Two" };
            var diffOrder2 = new string[] { "Two", "Three", "One" };

            // Act
            var isDifferentOrder1 = compare.CompareSequence(baseArray, diffOrder1);
            var isDiffOrderOrder2 = compare.CompareSequence(baseArray, diffOrder1);

            // Assert
            isDifferentOrder1.ShouldBe(true);
            isDiffOrderOrder2.ShouldBe(true);
        }

        [Test]
        public void TestDifferentOrderDeny()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison();
            var baseArray = new string[] { "One", "Two", "Three" };
            var diffOrder = new string[] { "One", "Three", "Two" };

            // Act
            var isDifferent = compare.CompareSequence(baseArray, diffOrder);

            // Assert
            isDifferent.ShouldBe(false);
        }
    }
}