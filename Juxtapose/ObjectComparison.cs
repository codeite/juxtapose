using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Juxtapose
{
    public class ObjectComparison
    {
        private bool _ignoreSequenceOrder = false;
        private bool _throwException = false;
        private List<string> _properties = null;
        private readonly TextWriter _output = Console.Out;
        private bool _aproximatly;
        private Type _typeForComparision;

        public ObjectComparison()
        {
        }

        public ObjectComparison IgnoreSequenceOrder()
        {
           _ignoreSequenceOrder = true;
           return this;
        }

        public ObjectComparison ThrowException()
        {
            _throwException = true;
            return this;
        }

        public ObjectComparison UseActualTypes<T>()
        {
            _typeForComparision = typeof(T);
            return this;
        }

        public ObjectComparison Aproximatly()
        {
            throw new NotImplementedException();
            _aproximatly = true;
            return this;
        }

        public bool CompareSequence(IEnumerable<object> leftSequence, IEnumerable<object> rightSequence)
        {
            var leftList = leftSequence.ToList();
            var rightList = rightSequence.ToList();
            var match = true;

            if (leftList.Count != rightList.Count)
            {
                _output.WriteLine("Sequences are not the same length. Left: {0} Right: {1}", leftList.Count, rightList.Count);
                match = false;
            }

            if (_ignoreSequenceOrder)
            {
                var leftInRight = FindMissing(leftList, rightList, "Left", "Right");
                var rightInLeft = FindMissing(rightList, leftList, "Right", "Left");

                match = match && (leftInRight && rightInLeft);
            }
            else
            {
                var count = new[] { leftList.Count, rightList.Count }.Min();

                for (var index = 0; index < count; index++)
                {
                    var isSame = Compare("[" + index + "]", leftList[index], rightList[index]);
                    match = match && isSame;

                    if (!isSame)
                    {
                        _output.WriteLine("Different at element [{0}]. Left:'{1}' Right:'{2}' ", index, leftList[index], rightList[index]);
                    }
                }

            }

            if (!match && _throwException)
            {
                throw new Exception("Sequences do not match");
            }

            return match;
        }

        private bool FindMissing(List<object> masterList, List<object> compareList, string masterName, string compareName)
        {
            var areEqual = true;

            // Loop over master
            foreach (var mater in masterList.ToList())
            {
                // see if item is in compare
                var inCompare = compareList.Any(compare => Compare(null, mater, compare));

                if (!inCompare)
                {
                    _output.WriteLine("Item '{0}' is in {1} but not {2}", mater, masterName, compareName);
                }

                areEqual = areEqual && inCompare;
            }

            return areEqual;
        }

        public bool CompareObject(object left, object right)
        {
            return Compare("", left, right);
        }

        private bool Compare(string name, object leftObject, object rightObject)
        {
            if (leftObject == null && rightObject == null)
            {
                return true;
            }

            if (leftObject == null)
            {
                if (name != null)
                {
                    _output.WriteLine(name + " Left hand side is null");
                }

                return false;
            }

            if (rightObject == null)
            {
                if (name != null)
                {
                    _output.WriteLine(name + " Right hand side is null");
                }

                return false;
            }

            var leftType = leftObject.GetType();
            var rightType = rightObject.GetType();

            if (leftType != rightType)
            {
                // Is supplied type a super type?
                if (_typeForComparision != null && 
                    _typeForComparision.IsAssignableFrom(leftType) &&
                    _typeForComparision.IsAssignableFrom(rightType))
                {
                    // We're ok.
                }

                else
                {
                    if (name != null)
                    {
                        _output.WriteLine(name + " Types do not match. Left:{0} Right:{1}", leftType, rightType);
                    }

                    return false;
                }
            }

            var type = _typeForComparision ?? leftType;

            if (type.IsPrimitive)
            {
                if (_aproximatly && type == typeof(double))
                {

                }

                return leftObject.Equals(rightObject);
            }

            if (type == typeof(string))
            {
                return leftObject.Equals(rightObject);
            }

            List<string> properties;

            if (_typeForComparision == null)
            {
                properties = _properties ?? new List<string>();

                var leftProps = leftObject.GetType().GetProperties().Select(x => x.Name);
                var rightProps = rightObject.GetType().GetProperties().Select(x => x.Name);

                properties = properties
                    .Union(leftProps)
                    .Union(rightProps)
                    .ToList();
            }
            else
            {
                properties = _properties ?? type.GetProperties().Select(x => x.Name).ToList();
            }

            var objectsMatch = true;

            foreach (var propertyName in properties)
            {
                bool found;
                var leftValue = GetValue(leftObject, propertyName, out found);
                var hasProps = found;

                var rightValue = GetValue(rightObject, propertyName, out found);
                hasProps = hasProps && found;

                if (!hasProps)
                {
                    return false;
                }

                // todo:Should think about comparison here
                var equal = leftValue.Equals(rightValue);

                if (!equal && name != null)
                {
                    _output.WriteLine("{0} Property:{1} not equal. Left:'{2}' Right:'{3}'", name, propertyName, leftValue, rightValue);
                }

                objectsMatch = objectsMatch && equal;
            }

            return objectsMatch;
        }

        private object GetValue(object item, string propertyName, out bool found)
        {
            var property = item.GetType().GetProperty(propertyName);

            if (property == null)
            {
                _output.WriteLine("Item '{0}' does not have property '{1}'", item, propertyName);
                found = false;
                return null;
            }

            found = true;
            return property.GetValue(item, null);
        }
    }
}
