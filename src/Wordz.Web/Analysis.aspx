<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Analysis.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Analysis" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>������ ������</title>
	<meta name="keywords" content="���������� �������� ������ ����� ���������� �������">
	<meta name="description" content="������� ������ ����������� ������ � ���������� ���� � ������ �������.">
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	������ ������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	����������� � ������ ���������� ���� � ���������� ������� �������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<uc:Progress ID="ctlProgress" runat="server" />
	<table width="100%" height="100%">
		<tr>
			<td colspan="2" height="1%">
				�������� ����� �� ����� <a href="http://en.wikipedia.org/wiki/UTF-8" class="smalltext"
					title="����� ���� ��������� ���������� ����� � ��-����������� �������, ���� ������ ���� � ��������� UTF-8">
					(UTF-8)</a>:
				<input id="ctlFile" runat="server" type="file" size="15" name="ctlFile">
				<asp:LinkButton ID="btnLoadFile" runat="server" Text="���������" CssClass="button"
					title="��������� ���� � ������� ��� �������" />
			</td>
		</tr>
		<tr>
			<td width="70%">
				<div id="cklWords" runat="server" class="divWordsAll" title="������ �� ������� ��� �������" />
			</td>
			<td valign="top" align="center">
				<br/>
				������� �� �����, �� ������ �������� ��� ��� <span id="spanUnknownWord" runat="server">
					����������</span>&nbsp;��� ���&nbsp; <span id="spanKnownWord" runat="server">��������
						�����</span><br/>
				<br/>
				<hr>
				<a id="btnMakeAllUnknown" runat="server" href="#" class="button" title="�������� ��� ����� ��� ����������">
					�� ���������!</a><br/>
				<br/>
				<a id="btnMakeAllKnown" runat="server" href="#" class="button" title="�������� ��� ����� ��� ��������">
					�� �������!</a>
				<br/>
				<br/>
				<a id="btnAnalysis" runat="server" href="#" class="button" title="��������� ����� � ������, ������������ �� ��� � ����� �������">
					������������� �����</a><br/>
				<br/>
				<a id="btnUpdateVocabulary" runat="server" href="#" class="button" title="������� �� ������ ������� ���������� ����� � �������� ��������">
					�������� �������</a><br/>
				<br/>
				<a id="btnProcessUnknownWords" runat="server" href="#" class="button" title="������� �������� ��������� ������ � ����������� �������">
					���������� �����</a>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
