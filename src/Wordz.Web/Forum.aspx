<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Forum.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Forum" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>�����</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	�����
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	���������� �����
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<p>
		<b>����� ��������� � ����������.</b>
	</p>
	<p>
		���� �� ������ ������� ������ � ��������: <b><a href="/Contacts">�� ����� ������</a></b>
	</p>
	����������, �������� � ������ ���� ������ ���� �� �������� ����:
	<div align="left">
		<ul>
			<li><b>������</b> - ��������� �� �������. ���� �� ������, ������� ��������, ������������������
				����� ��������, ������ �������� </li>
			<li><b>�����</b> - ����������� � ����� ������� �� ����� </li>
			<li><b>�����</b> - ������ �� �������� �������, �����������, �������, ��������� ��������
				������, ���������� ������ ��� ������� ���������� � �.�. </li>
			<li><b>�������</b> - ����������� � ������� ������ ������� �� ����� ��� �� ������ ��������
				� ����� ������ ������� �������� </li>
			<li><b>������</b> - ����� ������� � ����� � �.�. </li>
		</ul>
	</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
