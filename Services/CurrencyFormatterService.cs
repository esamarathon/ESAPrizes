using System.Globalization;

namespace ESAPrizes.Services {

    public class CurrencyFormatterService {
        protected CultureInfo _usCultureInfo { get; set; }

        public CurrencyFormatterService() {
            _usCultureInfo = new CultureInfo("en-US");
        }

        public string ToDollars(decimal? value) {
            return value?.ToString("C", _usCultureInfo) ?? "";
        }
    }
}