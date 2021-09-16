using System.Collections.Generic;
using System.Linq;

namespace Revealyan.CleanArchitecture.Domain
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();
        public ValueObject GetCopy() => MemberwiseClone() as ValueObject;
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (left is not null &&
                right is not null)
                return left.Equals(right);
            else
                return false;
        }
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is not ValueObject other || obj.GetType() != GetType())
                return false;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
