using System.Collections;

namespace Wordz.BC.Comparers.WordElement {
    public class Form1Comparer : IComparer {
        public int Compare(object x, object y) {
            return string.Compare(((BE.VerbElement) x).Form1, ((BE.VerbElement) y).Form1);
        }
    }
}