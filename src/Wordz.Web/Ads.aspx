<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Ads.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Ads" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>���������� �������</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	���������� �������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	��� ����� ���������� ���� ������� �� ���� �����
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<p>
		�� ������ ���������� ������� �� ���� �����.
	</p>
	<p>
		<b>��������� �����:</b> ������� ������ � ����� � ������ ����� �������.
	</p>
	�� �������, ��� ������������ ������ ����� �������� �� ������������ �������, �.�.
	��������� � ��������� �����:<br/>
	��������, �������� �����, ������� � �������� ����������, ������� �������� (��� �������������
	���������) � �.�.
	<p>
		����� ������������ �� �������� ���������� �������,
		<b><a href="/Contacts">��������� � ����</a></b>.
	</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="4" />
</asp:Content>
