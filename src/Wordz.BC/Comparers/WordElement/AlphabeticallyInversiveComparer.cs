using System.Collections;

namespace Wordz.BC.Comparers.WordElement {
    public class AlphabeticallyInversiveComparer : IComparer {
        public int Compare(object x, object y) {
            return string.Compare(((BE.WordElement) x).Translation, ((BE.WordElement) y).Translation);
        }
    }
}