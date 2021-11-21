using System.Collections;

namespace Wordz.BC.Comparers.WordElement {
    public class WordsLengthComparer : IComparer {
        public int Compare(object x, object y) {
            return ((BE.WordElement) x).Original.Length - ((BE.WordElement) y).Original.Length;
        }
    }
}