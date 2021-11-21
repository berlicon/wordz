<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="false"
    EnableViewState="false" CodeBehind="Goal.aspx.cs" Inherits="Wordz.Web.Goal" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
    <title>ќ проекте</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
    ќ проекте
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
    «ачем нужен этот сайт?
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
    <p>
        —айт представл€ет собой открытую вики-платформу дл€ создани€ курсов, помогающих изучать
        <strong>иностранные €зыки <a href="/Languages">(английский и другие, см. список)</a></strong> и не только их.
		¬озможно создание курсов любой тематики: ѕƒƒ, школьные предметы, кулинари€, дизайн и т.п.
		 урсы могут быть созданы авторами и доступны за плату; бесплатные курсы создаютс€
		всеми посетител€ми сайта сообща. ƒл€ курса можно задать набор модулей, упражнений к ним; выставить стоимость
		каждого модул€ и всего курса.
    </p>
    <p>
		“акже, доступны бесплатные сервисы, помогающие изучать иностранные €зыки:
		создание списка новых слов дл€ изучени€ на основе анализа текста; озвучивание этих слов и создание
		mp3 файлов с их переводом дл€  прослушивани€; проверка написани€ слова и его перевода; изучение
		неправильных глаголов и грамматики €зыка; тестирование и участие в конкурсах на знание словар€; 
		медиаконтент (онлайн “¬, радио и фильмы); полезные статьи об изучении €зыков.
    </p>
    <p>
		—айт находитс€ в посто€нной разработке, следите за по€влением новых функций в нашем твиттере:
		<a href="https://twitter.com/wordzpm">twitter.com/wordzpm</a>
    </p>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
    <uc:Advert runat="server" Ad="3" />
</asp:Content>
