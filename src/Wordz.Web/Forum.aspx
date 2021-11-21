<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Forum.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Forum" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Форум</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Форум
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Обсуждение сайта
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<p>
		<b>Форум находится в разработке.</b>
	</p>
	<p>
		Пока Вы можете послать письмо с вопросом: <b><a href="/Contacts">по этому адресу</a></b>
	</p>
	Пожалуйста, напишите в начале темы письма одно из ключевых слов:
	<div align="left">
		<ul>
			<li><b>Ошибка</b> - замечания об ошибках. Если не трудно, укажите страницу, последовательность
				Ваших действий, версию браузера </li>
			<li><b>Новое</b> - предложения о новой функции на сайте </li>
			<li><b>Совет</b> - советы по развитию ресурса, архитектуре, дизайну, повышению скорости
				работы, корректной работе под разными браузерами и т.п. </li>
			<li><b>Реклама</b> - предложения о рекламе своего ресурса на сайте или об обмене ссылками
				с Вашим сайтом смежной тематики </li>
			<li><b>Вопрос</b> - любые вопросы о сайте и т.д. </li>
		</ul>
	</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
