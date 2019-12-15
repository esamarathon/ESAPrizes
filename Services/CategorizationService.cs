using System;
using System.Globalization;
using ESAPrizes.Models;

namespace ESAPrizes.Services
{
    public class CategorizationService {

        private readonly CurrencyFormatterService _formatterService;

        public CategorizationService(CurrencyFormatterService formatterService) {
            _formatterService = formatterService;
        }

        public Category GetCategory(Prize p) {
            if (p.Category == "Grand Prize") {
                return new Category(p.Category, p.Category, Int32.MaxValue);
            }

            if (p.MinimumBid != null) {
                decimal minBid = p.MinimumBid.Value;
                string currency = _formatterService.ToDollars(minBid);
                string category = $"Minimum donation: {currency}";
                return new Category(category, currency, Decimal.ToInt32(minBid));
            }

            return new Category("Other");
        }
    }
}