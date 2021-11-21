using System.Collections;

namespace Wordz.BC.Comparers.WordElement {
    public class TranslateComparer : IComparer {
        public int Compare(object x, object y) {
            return string.Compare(((BE.VerbElement) x).Translation, ((BE.VerbElement) y).Translation);
        }
    }
}