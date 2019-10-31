using ESAPrizes.Models;

namespace ESAPrizes.Services {
    public class SizeSelectionService {
        public string GetSize(Prize p) {
                if (p.Value % 100 == 0) {
                    return SizeClass.Large;
                }

                if (p.Value % 50 == 0) {
                    return SizeClass.Medium;
                }

                return SizeClass.Small;
        }
    }
}