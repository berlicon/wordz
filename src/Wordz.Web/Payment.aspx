<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="false"
	EnableViewState="false" CodeBehind="Payment.aspx.cs" Inherits="Wordz.Web.Payment" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Оплата</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Оплата
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Способы пополнения баланса аккаунта
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<a href="http://webmoney.ru/">WebMoney</a> кошельки<br />
	WMR (RUR) <b>R144637697315</b><br />
	WMZ (USD) <b>Z200656311256</b><br />
	WME (EUR) <b>E121519487680</b><br />
	<br />
	<a href="http://money.yandex.ru/">Яндекс.Деньги</a><br />
	№ счёта (RUR): <b>41001438705426</b><br />
	<br />
	<a href="https://www.paypal.com/us/">PayPal</a><br />
	Перевести деньги на емайл, указанный <a href="/Contacts">здесь</a><br />
	<br />
	Через систему перевода денег <a href="http://westernunion.com">Western Union</a>,
	<a href="http://contact-sys.com">Contact</a>, <a href="http://unistream.ru/transfers/">
		Unistream</a>, <a href="http://leadermt.ru/">Лидер</a> и т.п.<br />
	Персональные данные для перевода денег высылаются по запросу на наш контактный емайл<br />
	<br />
	<b>P.S.</b> Желательно сообщить о факте перевода денег <a href="/Contacts">отдельным
		письмом</a>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
