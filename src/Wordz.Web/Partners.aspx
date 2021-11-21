<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Partners.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Partners" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Полезные ссылки</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Полезные ссылки
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Набор ссылок, связанных с тематикой сайта
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<div style="overflow: auto; width: 100%; height: 100%;">
		<p>
			Если Вы считаете, что Ваш ресурс должен находиться в этом списке,
			<b><a href="/Contacts">свяжитесь с нами</a></b>.
		</p>
		<a name="portals"></a>
		<h2>
			Языковые порталы</h2>
		<ul>
			<li><a href="http://catchenglish.ru/">CatchEnglish.ru - Сайт для изучения английского языка</a></li>
			<li><a href="http://www.learnathome.ru/help/how-it-works.html" title="Уроки английского языка">
				Learnathome.ru - видео уроки английского языка</a></li>
			<li><a href="http://www.english-thebest.ru/">english-thebest.ru - English: the best</a></li>
			<li><a href="http://www.homeenglish.ru">homeenglish.ru - Изучение, уроки английского
				языка</a></li>
			<li><a href="http://www.english4free.ru">english4free.ru - English 4 FREE</a></li>
			<li><a href="http://www.englishlearner.com">englishlearner.com - Learn English online
				free English lessons and tests</a></li>
			<li><a href="http://www.englishpage.com">englishpage.com - Free online English lessons
				& ESL / EFL resources</a></li>
			<li><a href="http://www.langinfo.ru">langinfo.ru - Лингвистический портал английского
				языка</a></li>
			<li><a href="http://www.efl.ru/">efl.ru - Английский язык из первых рук</a></li>
		</ul>
		<a name="audiobooks"></a>
		<h2>
			Аудиокниги</h2>
		<ul>
			<li><a href="http://ska4ka.com">ska4ka.com - Аудиокниги на английском и прочих языках
				мира</a></li>
			<li><a href="http://audio-class.ru/english.html">audio-class.ru - Audio-Class - языки
				со звуком!</a></li>
			<li><a href="http://www.oculture.com/2006/10/audio_book_podc.html">oculture.com - Free
				Audio Books: Great Books for Free</a></li>
			<li><a href="http://www.theaudiobookstore.com/servlet/StoreFront">theaudiobookstore.com
				- Audio Books for iPod, iPhone & MP3 Players</a></li>
			<li><a href="http://www.simplylisten.com">simplylisten.com - Audio Books | MP3 Books
				| iPod Books</a></li>
		</ul>
		<a name="subtitles"></a>
		<h2>
			Субтитры к фильмам</h2>
		<ul>
			<li><a href="http://www.opensubtitles.com/ru">opensubtitles.com - Movie DivX subtitles</a></li>
			<li><a href="http://www.subtitlesbox.com">subtitlesbox.com - Divx subtitles!</a></li>
			<li><a href="http://www.mysubtitles.org">mysubtitles.org - Download subtitles for any
				movie</a></li>
		</ul>
		<a name="translates"></a>
		<h2>
			Переводы онлайн</h2>
		<ul>
			<li><a href="http://multitran.ru">multitran.ru - Словарь Мультитран</a></li>
			<li><a href="http://translate.google.ru">google.ru - Переводчик Google</a></li>
			<li><a href="http://www.google.ru/dictionary">google.ru - Словарь Google</a></li>
			<li><a href="http://slovari.yandex.ru">yandex.ru - Яндекс.Словари</a></li>
			<li><a href="http://www.onelook.com">onelook.com - OneLook Dictionary Search</a></li>
			<li><a href="http://radugaslov.ru">radugaslov.ru - Онлайн Переводчик</a></li>
			<li><a href="http://www.yanglish.ru/online_translator.htm">yanglish.ru - Онлайн переводчик</a></li>
			<li><a href="http://www.transneed.com/on_line_free_translator.php">transneed.com - Онлайн
				переводчик</a></li>
		</ul>
		<a name="programs"></a>
		<h2>
			Программы - переводчики</h2>
		<ul>
			<li><a href="http://www.lingvo.ru">lingvo.ru - Словари ABBYY Lingvo</a></li>
			<li><a href="http://www.translate.ru">translate.ru - Online-переводчик текста ПРОМТ</a></li>
			<li><a href="http://www.medialingua.ru">medialingua.ru - МедиаЛингва</a></li>
			<li><a href="http://www.multilex.ru">multilex.ru - Словарь МультиЛекс. Электронные словари.</a></li>
			<li><a href="http://mueller-dic.chat.ru">mueller-dic.chat.ru - Англо-русский словарь
				Мюллера</a></li>
		</ul>
		<a name="voices"></a>
		<h2>
			Озвучивание текста</h2>
		<ul>
			<li><a href="http://mp3book2005.narod.ru">mp3book2005.narod.ru - MP3book2005</a></li>
			<li><a href="http://www.vector-ski.ru/vecs/">vector-ski.ru - Govorilka</a></li>
			<li><a href="http://www.cross-plus-a.ru/balabolka.html">www.cross-plus-a.ru - Балаболка</a></li>
			<li><a href="http://parovoz.com.ua/aboo/">parovoz.com.ua - ABoo</a></li>
			<li><a href="http://softsearch.ru/programs/87-149-spiker-download.shtml">softsearch.ru
				- Спикер</a></li>
			<li><a href="http://kom-pas.narod.ru/audiobook_net.htm">kom-pas.narod.ru - AUDIOBOOK</a></li>
			<li><a href="http://ice-graphics.com/ICEReader/IndexR.html">ice-graphics.com - ICE Book
				Reader Professional</a></li>
			<li><a href="http://tomreader.chat.ru">tomreader.chat.ru - ToM Reader</a></li>
			<li><a href="http://www.coolreader.org">coolreader.org - Cool Reader</a></li>
		</ul>
		<a name="communication"></a>
		<h2>
			Разговорные клубы</h2>
		<ul>
			<li><a href="http://www.encc.ru">encc.ru - English Communication Club is a place, where
				you can socialize, practice English and improve your communication skills, get new
				interesting friends, learn a lot of new things and simply have a good time.</a></li>
			<li><a href="http://www.livemocha.com">livemocha.com - Social Language Learning</a></li>
			<li><a href="http://www.english-test.net/pdf-worksheets/learn-english-via-skype.html">
				english-test.net - Learning English via Skype</a></li>
			<li><a href="http://www.skypeenglishclasses.com/">skypeenglishclasses.com - Learn English
				via Skype</a></li>
		</ul>
		<a name="TVchannels"></a>
		<h2>
			Онлайн ТВ</h2>
		<ul>
			<li><a href="http://www.freetvonline.com/">FreeTVonline.com - Watch free TV, movies,
				and videos</a></li>
			<li><a href="http://on-tv.ru/">on-tv.ru - смотреть телевидение через интернет</a></li>
			<li><a href="http://www.webantenne.com">webAntenne.com - Вебантенна: телевидение и Радио
				в интернете</a></li>
			<li><a href="http://www.findinternettv.com/">findinternettv.com - Find Internet TV</a></li>
		</ul>
		<a name="FMchannels"></a>
		<h2>
			Онлайн радио</h2>
		<ul>
			<li><a href="http://www.webantenne.com">webAntenne.com - Вебантенна: телевидение и Радио
				в интернете</a></li>
			<li><a href="http://guzei.com/online_radio/">guzei.com - Список Интернет Радио</a></li>
			<li><a href="http://www.radio.puler.ru/">radio.puler.ru - интернет радио онлайн</a></li>
			<li><a href="http://www.bbc.co.uk/radio/">bbc.co.uk - BBC Radio</a></li>
			<li><a href="http://www.radiotower.com/">radiotower.com - FIND & LISTEN TO 4963 STATIONS</a></li>
			<li><a href="http://www.zozanga.com/internetstuff/englishradiostations.htm">zozanga.com
				- RADIO STATIONS FOR ESL STUDENTS</a></li>
		</ul>
		<a name="films"></a>
		<h2>
			Фильмы онлайн</h2>
		<ul>
			<li><a href="http://www.film-online.us">film-online.us - Watch film online for free</a></li>
			<li><a href="http://www.xtubo.net">xtubo.net - Watch Anime, films Online</a></li>
			<li><a href="http://www.freemooviesonline.com">freemooviesonline.com - Watch Free Movies
				Online - The Ultimate Source for Free Movies, cinema, films, tv shows, tv series
				for free.</a></li>
			<li><a href="http://youku.com">youku.com - China’s leading Internet video website (если
				не знаете китайского :-) просто введите название фильма в строку поиска)</a></li>
			<li><a href="http://www.cinemawww.info">cinemawww.info - ОНЛАЙН КИНОТЕАТР - Смотреть
				бесплатно самые лучшие фильмы онлайн</a></li>
			<li><a href="http://engfilms.ru">engfilms.ru - фильмы на английском языке скачать (стоит
				1$ через СМС)</a></li>
			<li><a href="http://kinomix.net">kinomix.net - фильмы онлайн</a></li>
		</ul>
		<a name="courses"></a>
		<h2>
			Языковые курсы</h2>
		<ul>
			<li><a href="http://lingualeo.ru">LinguaLeo.ru - Английский язык онлайн</a></li>
			<li><a href="http://www.study.ru">study.ru - Английский язык на Study.ru и курсы английского
				онлайн</a></li>
			<li><a href="http://vinidiktov.ru">"Нескучный курс английского языка", "Сказочный курс
				английского языка", программа для запоминания слов Vocabilis и многое другое</a></li>
			<li><a href="http://englishinaction.ru/grammar/">"Английская грамматика в действии"
				- учитесь правильно говорить по-английски</a></li>
			<li><a href="http://www.runovschool.ru">runovschool.ru - КУРСЫ АНГЛИЙСКОГО ЯЗЫКА В МОСКВЕ,
				Санкт-Петербурге | Изучение английского языка Runov School</a></li>
			<li><a href="http://nekin.narod.ru/e47.htm">nekin.narod.ru - Английский язык для детей
				и взрослых</a></li>
			<li><a href="http://learn-english.ru">learn-english.ru - Разговорный онлайн курс английского
				языка</a></li>
			<li><a href="http://www.5english.com">5english.com - Изучение английского языка в интернет</a></li>
			<li><a href="http://www.englspace.com">englspace.com - Английский язык</a></li>
		</ul>
		<a name="subscribes"></a>
		<h2>
			Рассылки <a href="http://www.subscribe.ru">Subscribe.ru</a></h2>
		<ul>
			<li><a href="http://subscribe.ru/catalog/job.lang.englishlegko">English: The woman in
				white</a></li>
			<li><a href="http://subscribe.ru/catalog/job.lang.wonderland">Английский язык, каким
				его знают и любят во всем мире</a></li>
			<li><a href="http://subscribe.ru/catalog/job.lang.mp3english">Английский вслух</a></li>
			<li><a href="http://subscribe.ru/catalog/job.lang.good">Знать английский - выгодно!</a></li>
			<li><a href="http://subscribe.ru/catalog/job.lang.fluentenglish">Fluent English</a></li>
		</ul>
		<a name="vkontakte"></a>
		<h2>
			Группы <a href="http://www.vkontakte.ru">ВКонтакте</a></h2>
		<ul>
			<li><a href="http://vkontakte.ru/club714">English Speakers</a></li>
			<li><a href="http://vkontakte.ru/club37856">Иностранные языки в контакте</a></li>
			<li><a href="http://vkontakte.ru/club5041614">MOVIES IN ENGLISH</a></li>
			<li><a href="http://vkontakte.ru/club1402175">Изучаем английский язык</a></li>
			<li><a href="http://vkontakte.ru/club4586414">English</a></li>
			<li><a href="http://vkontakte.ru/club14330">You speak English, don't you?</a></li>
			<li><a href="http://vkontakte.ru/club1948211">Английский разговорный клуб Hubble-Bubble</a></li>
			<li><a href="http://vkontakte.ru/club5198528">English Speaking Club</a></li>
			<li><a href="http://vkontakte.ru/club7861584">English Communication Club</a></li>
			<li><a href="http://vkontakte.ru/club4758119">SKYPE international club (free communication)</a></li>
			<li><a href="http://vkontakte.ru/club9044">Клуб Англоговорящих</a></li>
			<li><a href="http://vkontakte.ru/club5539068">English is English</a></li>
			<li><a href="http://vkontakte.ru/club124161">Журнал COOL ENGLISH</a></li>
			<li><a href="http://vkontakte.ru/club611601">Журнал English4U + English Club - Украина</a></li>
			<li><a href="http://vkontakte.ru/club6777608">TOEFL iBT (+ МАТЕРИАЛЫ ДЛЯ СКАЧИВАНИЯ)</a></li>
			<li><a href="http://vkontakte.ru/club620">Курсы Английского Open English (Казань)</a></li>
			<li><a href="http://vkontakte.ru/club5009">Siberian English Club</a></li>
		</ul>
	</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="4" />
</asp:Content>
