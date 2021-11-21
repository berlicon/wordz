<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Ads.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Ads" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Размещение рекламы</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Размещение рекламы
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Как можно разместить вашу рекламу на этом сайте
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<p>
		Вы можете разместить рекламу на этом сайте.
	</p>
	<p>
		<b>Рекламные места:</b> верхний баннер и блоки в правой части страниц.
	</p>
	Мы считаем, что максимальную отдачу можно получить от тематической рекламы, т.е.
	связанной с тематикой сайта:<br/>
	учебники, языковые курсы, поездки и обучение заграницей, продажа гаджетов (для прослушивания
	аудиокниг) и т.п.
	<p>
		Чтобы договориться по вопросам размещения рекламы,
		<b><a href="/Contacts">свяжитесь с нами</a></b>.
	</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="4" />
</asp:Content>
