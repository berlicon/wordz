namespace Wordz.BE {
    public class IdNamePair {
        private int id;
        private string name;
        private string name2;
        private string name3;
        private bool flag;

        public int Id {
            get { return id; }
            set { id = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public string Name2 {
            get { return name2; }
            set { name2 = value; }
        }

        public string Name3 {
            get { return name3; }
            set { name3 = value; }
        }

        public bool Flag {
            get { return flag; }
            set { flag = value; }
        }

        public string Value {
            get { return Name + " " + Name2 + " " + Name3; }
        }

        public static string IdField = "Id";
        public static string ValueField = "Value";

        public IdNamePair(int id, string name) {
            Id = id;
            Name = name;
        }

        public IdNamePair(int id, string name, string name2): this(id, name) {
            Name2 = name2;
        }

        public IdNamePair(int id, string name, string name2, string name3)
            : this(id, name, name2)
        {
            Name3 = name3;
        }

        public IdNamePair(int id, string name, string name2, bool flag): this(id, name, name2) {
            Flag = flag;
        }
    }
}