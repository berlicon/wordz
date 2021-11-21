namespace Wordz.BE {
    public struct VerbElement {
        public string Form1;
        public string Form2;
        public string Form3;
        public string Translation;
        public int VerbType;
        public int MixedOrder;
        public int Id;

        public VerbElement(int id, string form1, string form2, string form3,
                           string translation, int verbType, int mixedOrder) {
            Form1 = form1;
            Form2 = form2;
            Form3 = form3;
            Translation = translation;

            VerbType = verbType;
            MixedOrder = mixedOrder;

            Id = id;
        }
    }
}