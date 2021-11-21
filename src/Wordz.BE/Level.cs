namespace Wordz.BE
{
	public class Level {
		private string sentence;
		private string answer1;
		private string answer2;
		private string answer3;
		private byte correct;

		public string Sentence {
			get { return sentence; }
			set { sentence = value; }
		}

		public string Answer1 {
			get { return answer1; }
			set { answer1 = value; }
		}

		public string Answer2 {
			get { return answer2; }
			set { answer2 = value; }
		}

		public string Answer3 {
			get { return answer3; }
			set { answer3 = value; }
		}

		public byte Correct {
			get { return correct; }
			set { correct = value; }
		}

		public Level() {}

		public Level(string sentence, string answer1, string answer2, string answer3, byte correct) {
			this.sentence = sentence;
			this.answer1 = answer1;
			this.answer2 = answer2;
			this.answer3 = answer3;
			this.correct = correct;
		}
	}
}