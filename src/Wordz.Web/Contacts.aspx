<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Contacts.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Contacts" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>���������� ����������</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	���������� ����������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	��� � ���� ���������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<p>
		�� ����� �������� �� ������ ���������� �� ����� ������:
		<b>wordzPM<span>@</span>gmail.com</b>
	</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
