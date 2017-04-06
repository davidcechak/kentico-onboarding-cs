using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

namespace Contracts.Tests.Base.Item
{
    internal static class EqualContraintExtensions
    {
        private sealed class ItemEqualityComparer : IEqualityComparer<ItemList.Contracts.Models.Item>
        {
            public bool Equals(ItemList.Contracts.Models.Item x, ItemList.Contracts.Models.Item y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id) && string.Equals((string) x.Ueid, (string) y.Ueid) && string.Equals((string) x.Value, (string) y.Value);
            }

            public int GetHashCode(ItemList.Contracts.Models.Item obj)
            {
                unchecked
                {
                    var hashCode = obj.Id.GetHashCode();
                    hashCode = (hashCode * 397) ^ (obj.Ueid?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (obj.Value?.GetHashCode() ?? 0);
                    return hashCode;
                }
            }
        }

        private static readonly Lazy<ItemEqualityComparer> ItemEquilityComparer = new Lazy<ItemEqualityComparer>();

        public static EqualConstraint UsingItemComparer(this EqualConstraint constraint) => constraint.Using(ItemEquilityComparer.Value);
    }
}