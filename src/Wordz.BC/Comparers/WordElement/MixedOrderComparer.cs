using System.Collections;

namespace Wordz.BC.Comparers.WordElement {
    public class MixedOrderComparer : IComparer {
        public int Compare(object x, object y) {
            return ((BE.WordElement) x).MixedOrder - ((BE.WordElement) y).MixedOrder;
        }
    }
}