namespace Wordz.BE {
    public class Account {
        private int id;
        private string nick;
        private string email;
        private string password;

        public static int EmptyId = 0;

        public int Id {
            get { return id; }
            set { id = value; }
        }

        public string Nick {
            get { return nick; }
            set { nick = value; }
        }

        public string Email {
            get { return email; }
            set { email = value; }
        }

        public string Password {
            get { return password; }
            set { password = value; }
        }

        public bool IsAdmin { get; set; }

        public Account() {}

        public Account(int id, string nick, string email, string password) {
            this.id = id;
            this.nick = nick;
            this.email = email;
            this.password = password;
        }

        public Account(Account account) {
            this.id = account.Id;
            this.nick = account.Nick;
            this.email = account.Email;
            this.password = account.Password;
        }

        public bool IsEmpty {
            get { return Id == EmptyId; }
        }
    }
}