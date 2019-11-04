using ESAPrizes.Models;

namespace ESAPrizes.Services {
    public class SizeSelectionService {
        public string GetSize(Prize p) {
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