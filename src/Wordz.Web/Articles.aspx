<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Articles.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Articles" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>������, ���������� ����������� �����</title>
	<meta name="keywords" content="���������� ���� ������ ���������� �������� �����������">
	<meta name="description" content="������, �������� ��� �������� ����������� ����� (����������, ��������, ������).">
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	������, ���������� ����������� �����
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	�������� ������ ��� ���������������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<asp:Literal ID="content" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="7" />
</asp:Content>
