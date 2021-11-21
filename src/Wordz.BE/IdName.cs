using System.Runtime.Serialization;

namespace Wordz.BE {
    [DataContract]
    public class IdName {
        private int id;
        private string name;

        [DataMember]
        public int Id {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Name {
            get { return name; }
            set { name = value; }
        }

        public static string IdField = "Id";
        public static string NameField = "Name";

        public IdName(int id, string name) {
            Id = id;
            Name = name;
        }
    }
}