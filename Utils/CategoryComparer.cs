using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ESAPrizes.Models;

namespace ESAPrizes.Utils
{
    public class CategoryComparer : IEqualityComparer<Category>
    {
        public bool Equals([AllowNull] Category x, [AllowNull] Category y) => x.Name == y.Name;

        public int GetHashCode([DisallowNull] Category obj) => obj.Name.GetHashCode();
    }

}