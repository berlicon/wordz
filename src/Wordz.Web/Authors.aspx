﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="false"
    EnableViewState="false" CodeBehind="Authors.aspx.cs" Inherits="Wordz.Web.Authors" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
    <title>Авторам курсов</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
    Авторам курсов
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
    Предложение по размещению контента
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
    <p>
        Для авторов бесплатных курсов всё бесплатно. Аналогично википедии - то, что создается бескорыстно
		всеми участниками сообщества, бесплатно для всех.
    </p>
    <p>
        Авторы платных курсов имеют возможность разместить контент и зарабатывать на нём.
		Конкретные условия (какой процент от оплаты удерживается сайтом) обсуждается индивидуально.
		Предполагается следующая схема:<br>
		1. размещение материалов курса на хостинге: 1,5руб/мегабайт/мес<br>
		2. автор получает 90% от поступившей оплаты за его курс<br>
		3. автор получает 80% от стоимости рекламы, размещенной на страницах его курса<br>
    </p>
    <p>
        После создания курса он доступен для редактирования только автору (авторизованному пользователю сайта).
		Курс виден только ему до прохождения модерации администрацией сайта. После успешной модерации, курс
		становится доступным всем пользователям сайта. В случае изменения логотипа или описания курса, то до
		проверки этих данных администрацией, по-прежнему все пользователи видят "старый" логотип и описание курса.
		Это сделано в целях недопущения появления некорректных материалов в списке курсов на главной странице сайта.
    </p>
    <p>
        На данный момент доступны следующие типы упражнений для добавления в модули:<br>
		<b>1. Упражнение "Текст"</b>. Обычный текст для изучения.<br>
		<b>2. Упражнение "Текстовый ответ"</b> . Возможность пользователю сайта отправить заполненное текстовое задание
		для проверки администрации сайта/автором курса.<br>
		<b>3. Упражнение "Выбор"</b> . Пользователь выбирает ответ из заданного списка. Ответы могут быть текстом или картинкой.<br>
		<b>4. Упражнение "Выбор в тексте"</b> . В заданных местах в тексте пользователь выбирает ответ  из списка.<br>
		При создании упражнения, необходимо ввести текст, в таком формате:<br>
		<i>Вопрос <b>[вариант 1/вариант 2/*вариант 3]</b></i><br>
		Правильный ответ помечается звездочкой (*). В кавычках [...]  указывается место для вопросов.<br>
		<b>5. Упражнение "Пропуски в тексте"</b>.  В заданных местах в тексте пользователь вводит ответы в текстовые поля.<br>
		При создании упражнения, необходимо ввести текст, в таком формате:<br>
		<i>Вопрос <b>[ответ]</b>.</i><br>
		В кавычках [...]  указывается правильный ответ.<br><br>

		... и этот список будет расти! Запланировано еще как минимум 6 новых типов упражнений. Следите за прогрессом
		разработки в нашем твиттере: <a href="https://twitter.com/wordzpm">twitter.com/wordzpm</a>
    </p>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
    <uc:Advert runat="server" Ad="3" />
</asp:Content>
