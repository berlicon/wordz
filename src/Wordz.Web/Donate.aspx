<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Donate.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Donate" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>��� ������ �����</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	��� ������ �����
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	�������, ��� �� ������ ������ �������� ���� ����
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<p>
		���� ��� ���������� ���� ����, �� ������ ������������� ��� ��������, �����������
		� ������ �������� <a href="/">������� �� ����</a> �/��� ��������� � � ����� �����,
		��������, ���������� ��������� ��� � ������ ����� ����.
	</p>
	<p align="left">
		<b><u>�������� �����:</u></b><br/>
		<a href="/">���������� ����: ���������� ���������� ������</a><br/>
		���� <a href="http://wordz.ru">http://wordz.ru</a> ������������� ������� �� ��������
		����������� �����. �������� �����������: ����������� ��� ������ txt � mp3 ������
		� ����������� �������, �������� ����������, ������ ��/�����/������, �������� ������������
		��������, �������� �� ������ �������/������� ���� � �����������, ����������.
	</p>
	<p style="font-size: 75%; background-color: white" align="left">
		<b>HTML-��� ��� ���������� ������/������� <a href="/">wordz.ru</a>:</b><br/>
		<a href="/">
			<img src="/img/Wordz_ru_Logo_86_40.PNG" alt="������� ����� www.wordz.ru" /></a>
		&lt;a href=&quot;http://wordz.ru&quot; target=&quot;_blank&quot;&gt;&lt;img src=&quot;http://wordz.ru/img/Wordz_ru_Logo_86_40.PNG&quot;
		width=&quot;86&quot; height=&quot;40&quot; border=&quot;0&quot; alt=&quot;����������
		����: ���������� ���������� ������&quot;&gt;&lt;/a&gt;
		<br/>
		<br/>
		<a href="/">
			<img src="/img/Wordz_ru_Banner_88_31.gif" alt="������� ����� www.wordz.ru" /></a>
		&lt;a href=&quot;http://wordz.ru&quot; target=&quot;_blank&quot;&gt;&lt;img src=&quot;http://wordz.ru/img/Wordz_ru_Banner_88_31.gif&quot;
		width=&quot;88&quot; height=&quot;31&quot; border=&quot;0&quot; alt=&quot;����������
		����: ���������� ���������� ������&quot;&gt;&lt;/a&gt;
	</p>
	<p style="font-size: 75%; background-color: #90EE90" align="left">
		<b>��� �� ������ ������ ������� �����������:</b><br/>
		<br/>
		<a href="http://webmoney.ru/">WebMoney</a> ��������<br/>
		WMR (RUR) <b>R144637697315</b><br/>
		WMZ (USD) <b>Z200656311256</b><br/>
		WME (EUR) <b>E121519487680</b><br/>
		<br/>
		<a href="http://money.yandex.ru/">������.������</a><br/>
		� ����� (RUR): <b>41001438705426</b><br/>
		<br/>
		<a href="https://www.paypal.com/us/">PayPal</a><br/>
		��������� ������ �� �����, ��������� <a href="/Contacts">�����</a><br/>
		<br/>
		����� ������� �������� ����� <a href="http://westernunion.com">Western Union</a>,
		<a href="http://contact-sys.com">Contact</a>, <a href="http://unistream.ru/transfers/">
			Unistream</a>, <a href="http://leadermt.ru/">�����</a> � �.�.<br/>
		������������ ������ ��� �������� ����� ���������� �� ������� �� ��� <a href="/Contacts">
			���������� �����</a><br/>
		<br/>
		<b>P.S.</b> ���������� �������� � ����� �������� ����� <a href="/Contacts">���������
			�������</a>
	</p>
	<p>
		���� � ��� ���� ������� � ����������� ���� � �� �� ������ ��� ��� �������������
		�� �����, ���� ����� ������ �������� ����������, �� ������ <a href="/Contacts">���������
			� ����</a> ��� ���������� ������� ��������������.
	</p>
	<p>
		�����, ����� ���� ���������� �������� � <a href="/Partners">��������� ������� ��������</a>
	</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="7" />
</asp:Content>
