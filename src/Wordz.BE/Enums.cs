namespace Wordz.BE {
    public enum WordsOrder {
        Undefined = 0,
        Learn,
        Native,
    }

    public enum SortBy {
        Undefined = 0,
        Alphabetically,
        Frequency,
        WordsLength,
        MixedOrder,
        OriginalOrder,
    }

    public enum WordsSource {
        LoadFromProcessPage = 1,
        LoadFromGlobalDictionary,
    }

    public enum WordsSelector {
        ForDomain = 1,
        Random,
        Ordered,
    }

    public enum ShowColumn {
        Form1 = 1,
        Form2,
        Form3,
        Translation,
        Random,
        All,
    }

    public enum SortColumn {
        Form1 = 1,
        VerbType,
        Translate,
        Random,
    }

    public enum SoundingType {
        Learn = 0,
        Native,
        Both,
    }
}