using System.Collections;

namespace Wordz.BC.Comparers.WordElement {
    public class AlphabeticallyComparer : IComparer {
        public int Compare(object x, object y) {
            return string.Compare(((BE.WordElement) x).Original, ((BE.WordElement) y).Original);
        }
    }
}