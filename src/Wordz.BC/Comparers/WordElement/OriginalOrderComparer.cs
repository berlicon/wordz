using System.Collections;

namespace Wordz.BC.Comparers.WordElement {
    public class OriginalOrderComparer : IComparer {
        public int Compare(object x, object y) {
            return ((BE.WordElement) x).OriginalOrder - ((BE.WordElement) y).OriginalOrder;
        }
    }
}