<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Default.aspx.cs"
    AutoEventWireup="true" Inherits="Wordz.Web.Default" EnableViewState="false" %>

<%@ Import Namespace="System.Net" %>

<%@ Import Namespace="Wordz.BE.Dto" %>
<%@ Register Src="Controls/Satellites.ascx" TagName="Satellites" TagPrefix="uc" %>
<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
    <title>Образовательная wiki-платформа</title>
    <meta name="keywords" content="курсы вики wiki образование английский словарный запас обучение аудиокниги перевод текст словарь формирование речи языковые движки mp3 онлайн ТВ радио фильмы конкурсы" />
    <meta name="description" content="Образовательная wiki-платформа. Словарный запас, сервисы для изучения английского. Личный онлайн словарь. Создание из текста txt/mp3 файлов с незнакомыми словами." />
    <link rel="canonical" href="http://www.wordz.ru" />
    <script type="text/javascript" src="https://apis.google.com/js/plusone.js"></script>
	<script type="text/javascript" src="http://vk.com/js/api/openapi.js?100"></script>
	<script type="text/javascript">VK.init({ apiId: 3231247, onlyWidgets: true });</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
    Образовательная wiki-платформа
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
    <br />
    <ul class="menu headerMenu">
        <li><a href="/Goal">о проекте</a></li>
        <li><a href="/Authors">авторам</a></li>
        <li><a href="/Contacts">контакты</a></li>
    </ul>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
    <div id="fb-root">
    </div>
    <script>
        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
            fjs.parentNode.insertBefore(js, fjs);
        } (document, 'script', 'facebook-jssdk'));
    </script>
    <div class="clr">
    </div>
    <div id="filter">
        <table>
            <tr>
                <td>
                    <label for="popularity">
                        Сортировка</label>
                </td>
                <td class="value">
                    <select name="popularity" id="" data-placeholder="--Выберите--">
                        <option value="0">название</option>
                        <option value="1">популярность</option>
                        <option value="2">стоимость</option>
                        <option value="3">новизна</option>
                    </select>
                </td>
                <td>
                    <label for="category">
                        Категория</label>
                </td>
                <td class="value">
                    <select name="category" id="" multiple="multiple" data-placeholder="--Выберите--">
                        <option value="0">Все</option>
                        <option value="1">иностранные языки</option>
                        <option value="2">кулинария</option>
                        <option value="3">ПДД</option>
                        <option value="4">ремонт</option>
                        <option value="5">спорт</option>
                    </select>
                </td>
                <td>
                    <label for="geography">
                        География</label>
                </td>
                <td class="value">
                    <select name="geography" id="" multiple="multiple" data-placeholder="--Выберите--">
                        <option value="0">Все</option>
                        <option value="1">Россия и СНГ</option>
                        <option value="2">Развитые страны</option>
                        <option value="3">США и Канада</option>
                        <option value="4">Европа</option>
                        <option value="5">Южная Америка</option>
                        <option value="6">Азия</option>
                        <option value="7">Африка</option>
                    </select>
                </td>
                <td>
                    <label for="languages">
                        Языки</label>
                </td>
                <td class="value">
                    <select name="languages" id="" multiple="multiple" data-placeholder="--Выберите--">
                        <option value="0">Все</option>
                        <option value="1">Русский</option>
                        <option value="2">English</option>
                    </select>
                </td>
                <td>
                    <input type="submit" value="ok" class="button" onclick="javascript:alert('Не реализовано');return false;">
                </td>
            </tr>
        </table>
    </div>
    <div id="courses">
        <listbox>
        <%
            var courses = TheCourses ?? new CourseDetailsDto[] {};
            foreach (var course in courses)
            {
        %>
        <div class="course">
        <a href="/Course/<%= course.Id %>" >
            <img width="50px" height="50px" src="/LoadPictureHandler.ashx?id=<%= course.PictureId %>" alt="">
            <div class="courseTitle">
                <%= WebUtility.HtmlEncode(course.Name)%></div>
            <div class="courseDescription">
                <%= WebUtility.HtmlEncode(course.Description)%></div>
            <div class="clr">
            </div>
            <p>
                <span class="cost"><%= WebUtility.HtmlEncode(course.Price.ToString())%></span> 
                <span class="stat">23</span> 
                <span class="lang"><%= WebUtility.HtmlEncode(course.UILanguageName)%></span>
            </p>
            <p>
                <span class="group">35</span>
                <span class="place">Москва</span>
                <span class="topic">32</span>
            </p>
            </a>
        </div>
        <%
            }
        %>
        </listbox>
        <div class="clr">
        </div>
        <ul class="pagination">
            <li class="prev"><a href="javascript:void(0);">&#60;</a></li>
            <li><a class="active" href="javascript:void(0);">1</a></li>
            <li><a href="javascript:void(0);">2</a></li>
            <li><a href="javascript:void(0);">3</a></li>
            <li>...</li>
            <li><a href="javascript:void(0);">99</a></li>
            <li class="next"><a href="javascript:void(0);">&#62;</a></li>
        </ul>
        <a href="/Courses" class="allCourses">Список всех курсов</a>
    </div>
    <!--  end #courses -->
    <!--  end #header -->
    <div id="middle">
        <div id="center">
            <div id="search">
                <label for="search">
                    поиск курса:</label>
                <input type="text" name="search" placeholder="введите текст для поиска">
                <input type="submit" value="ok" class="button" onclick="javascript:alert('Не реализовано');return false;">
                <a href="/Course/new" class="buttonRed">создать новый курс</a>
            </div>
            <div id="currentCourses">
                <h3 class="sectionTitle">
                    продолжить начатые курсы:</h3>
                <div class="courses">
					<a href="#" onclick="javascript:alert('Не реализовано. Найдите нужный курс через полный список.');return false;" class="currentCourse"><img src="/img/courseICO1.png" alt=""></a>
					<a href="#" onclick="javascript:alert('Не реализовано. Найдите нужный курс через полный список.');return false;" class="currentCourse"><img src="/img/courseICO2.png" alt=""></a>
					<a href="#" onclick="javascript:alert('Не реализовано. Найдите нужный курс через полный список.');return false;" class="currentCourse"><img src="/img/courseICO3.png" alt=""></a>
				</div>
            </div>
            <table class="userData">
                <tr>
                    <td class="udTitle">
                        <h3 class="sectionTitle">
                            ваше географическое положение:</h3>
                    </td>
                    <td class="udValue">
                        Россия, москва
                    </td>
                    <td class="udChange">
                        <a href="#" onclick="javascript:alert('Не реализовано');return false;" class="buttonRed">изменить</a>
                    </td>
                </tr>
                <tr>
                    <td class="udTitle">
                        <h3 class="sectionTitle">
                            ваш язык:</h3>
                    </td>
                    <td class="udValue">
                        русский
                    </td>
                    <td class="udChange">
                        <a href="#" onclick="javascript:alert('Не реализовано');return false;" class="buttonRed">изменить</a>
                    </td>
                </tr>
            </table>
            <div class="hr">
            </div>
            <div id="servises">
                <h3 class="servisesTitle">
                    Сервисы</h3>
                <ul class="list1 list">
                    <li class="item1"><a href="/Analysis">Анализировать текст на незнакомые слова</a></li>
                    <li class="item2"><a href="/Process">Составить для текста txt и mp3 файлы с незнакомыми
                        словами</a></li>
                    <li class="item3"><a href="/AddWords">Добавить слова в личный словарь</a></li>
                    <li class="item4"><a href="/CheckWords">Проверить словарный запас</a></li>
                    <li class="item5"><a href="/Vocabulary">Получить информацию о Вашем словаре</a></li>
                </ul>
                <ul class="list2 list">
                    <li class="item6"><a href="/IrregularVerbs">Проверить знания неправильных глаголов</a></li>
                    <li class="item7"><a href="/DictionaryQuiz">Участвовать в конкурсе на знание словаря</a></li>
                    <li class="item8"><a href="/WordsOrderQuiz">Участвовать в конкурсе на порядок слов в
                        предложении</a></li>
                    <li class="item9"><a href="/LevelQuiz">Определить знание грамматики</a></li>
                </ul>
                <div class="clr">
                </div>
                <ul class="list3">
                    <li class="item10"><a href="/OnlineTV">Онлайн ТВ</a></li>
                    <li class="item11"><a href="/OnlineFM">Онлайн радио</a></li>
                    <li class="item12"><a href="/OnlineFilm">Фильмы онлайн</a></li>
                    <li class="item13"><a href="/Articles">Полезные статьи</a></li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
    <%--<uc:Satellites runat="server" />--%>
    <div class="fb-like" data-href="http://wordz.ru/" data-send="true" data-show-faces="false">
    </div>
	<div id="vk_like"></div>
	<script type="text/javascript">
		VK.Widgets.Like("vk_like", { type: "button" });
	</script>
    <g:plusone></g:plusone>
	<a href="https://twitter.com/share" class="twitter-share-button" data-lang="ru">Твитнуть</a>
    <script>    	!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } } (document, "script", "twitter-wjs");</script>
    <a target="_blank" class="mrc__plugin_uber_like_button" href="http://connect.mail.ru/share"
        data-mrc-config="{'cm' : '1', 'ck' : '1', 'sz' : '20', 'st' : '1', 'tp' : 'combo'}">
        Нравится</a>
    <script src="http://cdn.connect.mail.ru/js/loader.js" type="text/javascript" charset="UTF-8"></script>
    <hr />
    <uc:Advert runat="server" Ad="7" />
</asp:Content>
