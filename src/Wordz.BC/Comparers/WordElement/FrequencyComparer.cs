using System.Collections;

namespace Wordz.BC.Comparers.WordElement {
    public class FrequencyComparer : IComparer {
        public int Compare(object x, object y) {
            return ((BE.WordElement) y).Frequency - ((BE.WordElement) x).Frequency;
        }
    }
}