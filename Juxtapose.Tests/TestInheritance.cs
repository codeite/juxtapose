using NUnit.Framework;
using Shouldly;

namespace Juxtapose.Tests
{
    [TestFixture]
    public class TestInheritance
    {
        [Test]
        public void TestSuperClass()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison()
                .UseActualTypes<BaseClass>();
            var alpha = new BaseClass() { Name = "Alice" };
            var beta = new SuperClass() { Name = "Alice", Value = "Ignored"};

            // Act
            var areEqual = compare.CompareObject(alpha, beta);

            // Assert
            areEqual.ShouldBe(true);
        }

        [Test]
        public void TestSuperClassUseActualTypes()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison();
            var alpha = new BaseClass() { Name = "Alice" };
            var beta = new SuperClass() { Name = "Alice", Value = "Ignored" };

            // Act
            var areEqual = compare.CompareObject(alpha, beta);

            // Assert
            areEqual.ShouldBe(false);
        }


        class BaseClass
        {
            public string Name { get; set; }
        }

        class SuperClass : BaseClass
        {
            public string Value { get; set; }
        }
    }
}