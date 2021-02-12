using ESAPrizes.Models;
#nullable enable

namespace ESAPrizes.Services {
    public class SizeSelectionService {
        public string GetSize(Prize p, int? categorySize) {
            var defaultSize = GetDefaultSize(p);

            if (categorySize != null && 
                categorySize < 2 && 
                defaultSize == SizeClass.Small) {
                return SizeClass.Medium;
            }

            return defaultSize;
        }

        public string GetDefaultSize(Prize p) {
            if (p.MinimumBid % 50 == 0) {
                return SizeClass.Large;
            }

            if (p.MinimumBid % 25 == 0) {
                return SizeClass.Medium;
            }

            return SizeClass.Small;
        }
    }
}