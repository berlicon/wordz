<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="false"
    EnableViewState="false" CodeBehind="Goal.aspx.cs" Inherits="Wordz.Web.Goal" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
    <title>� �������</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
    � �������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
    ����� ����� ���� ����?
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
    <p>
        ���� ������������ ����� �������� ����-��������� ��� �������� ������, ���������� �������
        <strong>����������� ����� <a href="/Languages">(���������� � ������, ��. ������)</a></strong> � �� ������ ��.
		�������� �������� ������ ����� ��������: ���, �������� ��������, ���������, ������ � �.�.
		����� ����� ���� ������� �������� � �������� �� �����; ���������� ����� ���������
		����� ������������ ����� ������. ��� ����� ����� ������ ����� �������, ���������� � ���; ��������� ���������
		������� ������ � ����� �����.
    </p>
    <p>
		�����, �������� ���������� �������, ���������� ������� ����������� �����:
		�������� ������ ����� ���� ��� �������� �� ������ ������� ������; ����������� ���� ���� � ��������
		mp3 ������ � �� ��������� ���  �������������; �������� ��������� ����� � ��� ��������; ��������
		������������ �������� � ���������� �����; ������������ � ������� � ��������� �� ������ �������; 
		������������ (������ ��, ����� � ������); �������� ������ �� �������� ������.
    </p>
    <p>
		���� ��������� � ���������� ����������, ������� �� ���������� ����� ������� � ����� ��������:
		<a href="https://twitter.com/wordzpm">twitter.com/wordzpm</a>
    </p>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
    <uc:Advert runat="server" Ad="3" />
</asp:Content>
