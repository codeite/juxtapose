using NUnit.Framework;
using Shouldly;

namespace Juxtapose.Tests
{
    [TestFixture]
    public class TestAnonymouseObjectWithOneProperty
    {
        [Test]
        public void TestAnonymouseObjectWithOnePropertyAreEqual()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison();
            var baseObejct = new { Name = "Alice" };
            var same = new { Name = "Alice" };

            // Act
            var areEqual = compare.CompareObject(baseObejct, same);

            // Assert
            areEqual.ShouldBe(true);
        }

        [Test]
        public void TestAnonymouseObjectWithOnePropertyAreNotEqual()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison();
            var baseObejct = new { Name = "Alice" };
            var diff = new { Name = "Bob" };

            // Act
            var areNotEqual = compare.CompareObject(baseObejct, diff);

            // Assert
            areNotEqual.ShouldBe(false);
        }

        /*
        [Test]
        public void TestObjectWithTwoProperties()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison<ObjectWithTwoProperties>();
            var baseObject = new ObjectWithTwoProperties() { Name = "Alice", Value = 1 };
            var same = new ObjectWithTwoProperties() { Name = "Alice", Value = 1 };
            var diffName = new ObjectWithTwoProperties() { Name = "Bob", Value = 1 };
            var diffValue = new ObjectWithTwoProperties() { Name = "Alice", Value = 2 };

            // Act
            var areEqual = compare.CompareObject(baseObject, same);
            var hasDiffName = compare.CompareObject(baseObject, diffName);
            var hasDiffValue = compare.CompareObject(baseObject, diffValue);

            // Assert
            areEqual.ShouldBe(true);
            hasDiffName.ShouldBe(false);
            hasDiffValue.ShouldBe(false);
        }

        class ObjectWithTwoProperties
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }
        */
    }
}