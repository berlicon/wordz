using System.Collections;

namespace Wordz.BC.Comparers.VerbElement {
    public class VerbTypeComparer : IComparer {
        public int Compare(object x, object y) {
            int result = ((BE.VerbElement) x).VerbType - ((BE.VerbElement) y).VerbType;
            return result * 10 + string.Compare(((BE.VerbElement) x).Form1, ((BE.VerbElement) y).Form1);
        }
    }
}