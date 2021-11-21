<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Articles.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Articles" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Статьи, грамматика английского языка</title>
	<meta name="keywords" content="английский язык статьи грамматика обучение эффективное">
	<meta name="description" content="Статьи, полезные при изучении английского языка (грамматика, методики, советы).">
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Статьи, грамматика английского языка
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Полезные статьи для самообразования
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<asp:Literal ID="content" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="7" />
</asp:Content>
