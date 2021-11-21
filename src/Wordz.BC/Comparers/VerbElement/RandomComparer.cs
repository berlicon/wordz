using System.Collections;

namespace Wordz.BC.Comparers.WordElement {
    public class RandomComparer : IComparer {
        public int Compare(object x, object y) {
            return ((BE.VerbElement) x).MixedOrder - ((BE.VerbElement) y).MixedOrder;
        }
    }
}