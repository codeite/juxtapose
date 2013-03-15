using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shouldly;

namespace Juxtapose.Tests
{
    [TestFixture]
    public class TestPrimativeComparison
    {
        [Test]
        public void TestInt()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison();

            // Act
            var areEqual = compare.CompareObject(456, 456);
            var areNotEqual = compare.CompareObject(456, 100);

            // Assert
            areEqual.ShouldBe(true);
            areNotEqual.ShouldBe(false);
        }

        [Test]
        public void TestDouble()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison();

            // Act
            var areEqual = compare.CompareObject(3.33, 3.33);
            var areNotEqual = compare.CompareObject(3.33, 6.66);

            // Assert
            areEqual.ShouldBe(true);
            areNotEqual.ShouldBe(false);
        }

        [Ignore("Not implimented")]
        [Test]
        public void TestDoubleApprox()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison()
                .Aproximatly();

            // Act
            var areEqual = compare.CompareObject(3.33, 10d/3d);
            var areNotEqual = compare.CompareObject(3.33, 3);

            // Assert
            areEqual.ShouldBe(true);
            areNotEqual.ShouldBe(false);
        }

        [Test]
        public void TestBasicString()
        {
            // Arrange
            var compare = new Juxtapose.ObjectComparison();

            // Act
            var areEqual = compare.CompareObject("my string", "my string");
            var areNotEqual = compare.CompareObject("my string", "a different string");

            // Assert
            areEqual.ShouldBe(true);
            areNotEqual.ShouldBe(false);
        }
    }
}
