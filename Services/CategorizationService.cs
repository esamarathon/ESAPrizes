using System;
using System.Globalization;
using ESAPrizes.Models;

namespace ESAPrizes.Services {
    public class CategorizationService {
        public Tuple<int,string> GetCategory(Prize p) {
            if (p.Category == "Grand Prize") {
                return new Tuple<int, string>(Int32.MaxValue, p.Category);
            }

            if (p.MinimumBid != null) {
                decimal minBid = p.MinimumBid.Value;
                string currency = minBid.ToString("C", new CultureInfo("en-US"));
                string category = $"Minimum donation: {currency}";
                return new Tuple<int, string>(Decimal.ToInt32(minBid), category);
            }

            return new Tuple<int, string>(Int32.MinValue, "Other");
        }
    }
}