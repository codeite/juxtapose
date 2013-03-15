using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace Juxtapose.Tests
{
    [TestFixture]
    public class TestArrayOfSimpleObjects
    {
        class SimpleObject
        {
            public string Name { get; set; }
        }

        [Test]
        public void TestArrayInOrderIsEqual()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison();
            var baseArray = new[] { "One", "Two", "Three" }.Select(x => new SimpleObject { Name = x }).ToArray();
            var same = new[] { "One", "Two", "Three" }.Select(x => new SimpleObject { Name = x }).ToArray();

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
            var baseArray = new[] { "One", "Two", "Three" }.Select(x => new SimpleObject { Name = x }).ToArray();
            var shorter = new[] { "One", "Two" }.Select(x => new SimpleObject { Name = x }).ToArray(); ;

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
            var baseArray = (new[] { "One", "Two", "Three" }).Select(x => new SimpleObject { Name = x }).ToArray();
            var longer = new[] { "One", "Two", "Three", "Four" }.Select(x => new SimpleObject { Name = x }).ToArray(); ;

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
            var baseArray = (new[] { "One", "Two", "Three" }).Select(x => new SimpleObject { Name = x }).ToArray();
            var diff = new [] { "One", "Two", "Baloon" }.Select(x => new SimpleObject { Name = x }).ToArray(); ;

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

            var baseArray = (new[] { "One", "Two", "Three" }).Select(x => new SimpleObject { Name = x }).ToArray();
            var diffOrder1 = new[] { "One", "Three", "Two" }.Select(x => new SimpleObject { Name = x }).ToArray(); ;
            var diffOrder2 = new[] { "Two", "Three", "One" }.Select(x => new SimpleObject { Name = x }).ToArray(); ;

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
            var baseArray = (new[] { "One", "Two", "Three" }).Select(x => new SimpleObject { Name = x }).ToArray();
            var diffOrder = new[] { "One", "Three", "Two" }.Select(x => new SimpleObject { Name = x }).ToArray(); ;

            // Act
            var isDifferent = compare.CompareSequence(baseArray, diffOrder);

            // Assert
            isDifferent.ShouldBe(false);
        }
    }
}