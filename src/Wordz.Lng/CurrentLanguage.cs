namespace Wordz.Lng {
    public class CurrentLanguage {
        public CurrentLanguage() {}

        /******** Russian - English *******/
        public static int NativeId {
            get { return 1; }
        }

        public static int LearnId {
            get { return 2; }
        }

        public static string MessageALotFilms = "Много фильмов на английском языке!";
        public static string MessageUnknown = "неизвестно";
        public static string MessageSource = "Источник";
        public static string MessageBackToArticles = "Вернуться к списку статей";
        public static string DictionaryQuizSuccessFormatString = @"
<b>Поздравляем!</b><br>
Вы успешно прошли тест и заняли <b>{0} место</b> в сотне лучших.<br>
Мы потрясены Вашими знаниями, и надеемся, что Вы 
поделитесь ссылкой на наш сайт со своими друзьями, 
чтобы и они хоть немного смогли приблизиться к Вашему 
интеллектуальному уровню!<br>
PS: Еще большее удовольствие Вы получите, наблюдая за 
бесплодными попытками Ваших друзей достигнуть Вашего результата. 
Так Вы им ненавязчиво объясните их место в пищевой цепочке :-)";
        public static string DictionaryQuizFailFormatString = @"
<b>Не расстраиваетесь!</b><br>
К сожалению, Вы не смогли занять место в сотне лучших.<br>
Может быть, Вы устали? Не смогли сконцентрироваться? 
К Вашему столу подошел начальник и грубо прервал процесс Вашего самообразования?<br>
В любом случае, мы верим, что Вам хватит сил в 
следующий раз пройти этот тест успешнее!<br>
PS: поделитесь ссылкой на наш сайт со своими друзьями и 
ненавязчиво заставьте их пройти этот тест. На основании 
результатов теста Вы сможете как выявить английских 
шпионов в Вашем окружении, так и прекрасно посмеяться над 
чужими неудачами :-)";
        public static string MessageBackToRecords = "Перейти к таблице рекордов";
        public static string MessageTestAgain = "Заново начать тест";
        public static string MessageReplyTest = "Повторить экзамен";
        public static string MessageQuestionFormatString = "ВОПРОС: <b>{0}</b> из <b>{1}</b><br>";
        public static string MessageFromFormatString = "{0} ({1}% от {2})";
        public static string MessageOrderWords = "Расположите слова в правильном порядке";
        public static string WordsOrderQuizSuccessFormatString = @"
<b>Поздравляем!</b><br>
Вы успешно прошли тест и заняли <b>{0} место</b> в сотне лучших.";
        public static string WordsOrderQuizFailFormatString = @"
<b>Не расстраиваетесь!</b>";
        public static string MessageErrors = "Вы сделали ошибки: (далее правильный и Ваши варианты предложений)<br>";
        public static string MessageNoFilms = "Фильмов не найдено.";
        public static string MessageDifferent = "Разные";
        public static string MessageCategory = "Категория";
        public static string MessageFilms = "Фильмов";
        public static string MessageAskTranslation = "Укажите правильный перевод слова:<br>";
        public static string MessageWrongNickOrPassword = "Неверный ник или пароль.";
        public static string MessageNickRequired = "Ник является уникальным и обязательным для ввода полем.";
        public static string MessageRegisteringRequiredForAddWords = "Вы должны зарегистрироваться на сайте, чтобы добавить слова в Ваш персональный словарь.";
        public static string MessageRegisteringRequiredForDeleteWords = "Вы должны зарегистрироваться на сайте, чтобы удалить слова из Вашего персонального словаря.";
        public static string MessageRegisteringRequiredForUpdateVocabulary = "Вы должны зарегистрироваться на сайте, чтобы обновить Ваш персональный словарь.";
        public static string MessageRegisteringRequiredForClearIrregularVerbs = "Вы должны зарегистрироваться на сайте, чтобы удалить всю информацию о знании неправильных глаголов из Вашего персонального словаря.";
        public static string MessageRegisteringRequiredForAddIrregularVerbs = "Вы должны зарегистрироваться на сайте, чтобы добавить неправильные глаголы в Ваш персональный словарь.";
        public static string MessageEnterNick = "Введите Ваш ник, пожалуйста.";
        public static string MessageAreYouSureWantDeleteWords = "Вы уверены, что хотите удалить выбранные слова из Вашего персонального словаря?";
        public static string MessageAreYouSureWantDeleteAndAddWords = "Вы уверены? Слова, помеченные как незнакомые, будут удалены из Вашего персонального словаря. А слова, помеченные как знакомые, будут добавлены в Ваш персональный словарь.";
        public static string MessageAreYouSureWantDeleteIrregularVerbs = "Вы уверены, что хотите очистить всю информацию о знании неправильных глаголов из Вашего персонального словаря?";
        public static string MessageNoSoundedWords = "Нет озвученных слов";
        public static string MessageNoSoundedVerbs = "Нет озвученных глаголов";
        public static string MessageNoSentences = "Нет предложений.";
        public static string MessageCutDataForAnalysisFormatString = "Для снижения нагрузки на сайт обработаны только первые {0} символов.";
		public static string MessageLevel1 = "[1/5] Beginner";
		public static string MessageLevel2 = "[2/5] Pre-Intermediate";
		public static string MessageLevel3 = "[3/5] Intermediate";
		public static string MessageLevel4 = "[4/5] Upper Intermediate";
		public static string MessageLevel5 = "[5/5] Advanced";
		public static string LevelQuizFormatString = @"
<br>Вы правильно ответили на <b>{0}</b> вопросов. Ваш уровень: <b>{1}</b><br><br>";
		public static string LevelQuizSuccessFormatString = @"
<b>Поздравляем!</b><br>
Вы успешно прошли тест и заняли <b>{0} место</b> в сотне лучших.";
		public static string LevelQuizFailFormatString = @"
<b>Не расстраиваетесь!</b>";

		public static string ApproveChanges = "Утвердить изменения";
		public static string EditCourse = "Редактировать курс";
		public static string RegisterToCreateCourses = "Зарегистрируйтесь на сайте, чтобы создавать курсы!";
		public static string RegisterToViewCourses = "Зарегистрируйтесь на сайте, чтобы просматривать закрытые курсы!";
		public static string EnterCoursePassword = "Введите пароль для этого курса!";
		public static string AreYouSureYouWantToPayCourse = "Вы действительно хотите оплатить весь курс стоимостью ";
		public static string Save = "Сохранить";
		public static string Cancel = "Отмена";
		public static string EditModule = "Редактировать модуль";
		public static string WrongCaptcha = "Неверная каптча!";
		public static string EmptyComment = "Пустой комментарий!";	
		public static string IncorrectParams = "Неверные параметры!";
		public static string CantAddComment = "Не удалось добавить комент!";
		public static string Correct = "Правильно!";
		public static string Incorrect = "Неправильно!";
		public static string AnswerNotChosen = "Ответ не выбран!";
		public static string ModuleNotDefined = "Не определен модуль!";
		public static string CantLoadExercise = "Не удалось загрузить упражнение";
		public static string WrongWidthAndHeight = "Ширина и высота заданы неверно!";
		public static string CantAproveCourse = "Не удалось утвердить изменения курса!";
		public static string AproveCourseSuccessful = "Успешно применены изменения курса!";
		public static string WrongPassword = "Неверный пароль!";
		public static string CantGetCourseById = "Ошибка. Не удалось получить курс по заданному Id.";
		public static string CantGetExerciseById = "Ошибка. Не удалось получить упражнение по заданному Id.";
		public static string CantGetExerciseListForModule = "Ошибка. Не удалось получить список упражнений для данного модуля.";
		public static string CantGetObject = "Не удалось получить объект!";
		public static string GetCourseSuccessful = "Успешно получен курс";
		public static string Success = "Успешно";
		public static string CantDeleteChannel = "Не удалось удалить канал!";
		public static string UpdateSuccessful = "Успешно обновлено!";
		public static string CantAddOrUpdateTvChannel = "Не удалось обновить или создать Tv канал.";
		public static string CantAddOrUpdateFmChannel = "Не удалось обновить или создать Fm канал.";
		public static string CantUpdateModule = "Не удалось обновить модуль!";
		public static string CantUpdate = "Не удалось обновить данные!";
		public static string CantSave = "Не удалось сохранить!";
		public static string YouAreNotLoggedIn = "Ошибка. Вы не вошли в систему!";
		public static string YourVoteWritten = "Спасибо. Ваш голос учтен";
		public static string Errors = "Ошибки!";
		public static string ValidationErrors = "Ошибки валидации!";
		public static string CantPayCourse = "Ошибка. Не удалось оплатить курс! Попробуйте еще раз.";
		public static string CantPayModule = "Ошибка. Не удалось оплатить модуль! Попробуйте еще раз.";
		public static string PayCourseSuccessful = "Оплата курса успешно осуществлена! Сейчас эта страница будет обновлена.";
		public static string PayModuleSuccessful = "Оплата модуля успешно осуществлена! Сейчас эта страница будет обновлена.";
		public static string AuthorsShouldBeLess1000 = "Поле 'авторы' должно быть меньше 1000 символов!";
		public static string NoCategory = "Не указана категория!";
		public static string ContactsShouldBeLess1000 = "Поле 'контакты' должно быть меньше 1000 символов!";
		public static string NoCurrency = "Не указана валюта!";
		public static string DescriptionShouldBeLess300 = "Поле 'описание' должно быть меньше 300 символов!";
		public static string DetailedDescriptionShouldBeLess15000 = "Поле 'детальное описание' должно быть меньше 15000 символов!";
		public static string LinksShouldBeLess4000 = "Поле 'ссылки' должно быть меньше 4000 символов!";
		public static string NameShouldBeLess100 = "Поле 'название' должно быть меньше 100 символов!";
		public static string NameShouldBeNotEmpty = "Поле 'название' должно быть непустым!";
		public static string PriceShouldBePositive = "Неверно указана цена. Число должно быть больше или равным нулю.";
		public static string TagsShouldBeLess1000 = "Поле 'тэги' должно быть меньше 1000 символов!";
		public static string NoLanguage = "Не указан язык курса.";
		public static string UrlShouldBeLess100 = "Поле 'url' должно быть меньше 100 символов!";
		public static string PasswordShouldBeLess100 = "Поле 'пароль' должно быть меньше 100 символов!";
		public static string NoCourse = "Не найден указанный курс. Пожалуйста, обновите страницу.";
		public static string NoModule = "Не найден указанный модуль. Пожалуйста, обновите страницу.";
	}
}