using NUnit.Framework;
using Shouldly;

namespace Juxtapose.Tests
{
    [TestFixture]
    public class TestSimpleObjects
    {
        [Test]
        public void TestObjectWithOneProperty()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison();
            var alpha = new ObjectWithOneProperty() {Name = "Alice"};
            var beta = new ObjectWithOneProperty() { Name = "Alice" };
            var gamma = new ObjectWithOneProperty() { Name = "Bob" };

            // Act
            var areEqual = compare.CompareObject(alpha, beta);
            var areNotEqual = compare.CompareObject(alpha, gamma);

            // Assert
            areEqual.ShouldBe(true);
            areNotEqual.ShouldBe(false);
        }

        class ObjectWithOneProperty
        {
            public string Name { get; set; }
        }

        [Test]
        public void TestObjectWithTwoProperties()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison();
            var baseObject = new ObjectWithTwoProperties() { Name = "Alice", Value = 1};
            var same = new ObjectWithTwoProperties() { Name = "Alice", Value = 1};
            var diffName = new ObjectWithTwoProperties() { Name = "Bob", Value = 1};
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
    }
}