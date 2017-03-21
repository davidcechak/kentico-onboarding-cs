using System;
using System.Collections.Generic;
using ItemList.Api.Models;
using NUnit.Framework.Constraints;

namespace ItemList.Api.Tests
{
    internal static class EqualContraintExtensions
    {
        private sealed class ItemEqualityComparer : IEqualityComparer<Item>
        {
            public bool Equals(Item x, Item y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id) && string.Equals(x.Ueid, y.Ueid) && string.Equals(x.Value, y.Value);
            }

            public int GetHashCode(Item obj)
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