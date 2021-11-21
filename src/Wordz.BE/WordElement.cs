namespace Wordz.BE {
    public struct WordElement {
        public string Original;
        public string Translation;
        public int Frequency;
        public int OriginalOrder;
        public int MixedOrder;
        public bool WellKnown;
        public bool Sounded;
        public int Id;

        public WordElement(string original) {
            Original = original;

            Translation = "";
            Frequency = 0;
            OriginalOrder = 0;
            MixedOrder = 0;

            Id = int.MinValue;
            WellKnown = false;
            Sounded = false;
        }

        public WordElement(string original, string translation)
            : this(original) {
            Translation = translation;
        }

        public WordElement(string original, string translation, int frequency, int originalOrder, int mixedOrder)
            : this(original, translation) {
            Frequency = frequency;
            OriginalOrder = originalOrder;
            MixedOrder = mixedOrder;
        }

        public bool ExistsInDB {
            get { return Id > 0; }
        }
    }
}