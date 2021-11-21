<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="CheckWords.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.CheckWords" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>�������� ���������� ������</title>
	<meta name="keywords" content="���������� �������� �������� ������ ���� ��������� �����">
	<meta name="description" content="�������� ������ ���� � ���������� �� � ������ �������.">
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	�������� ���������� ������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	�������� ������ ���� � ���������� �� � ������ �������. ����������� ���� �� ����
	(�������������).
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<uc:Progress ID="ctlProgress" runat="server" />
	<table width="100%" height="100%">
		<tr>
			<td width="40%" valign="top">
				�������� ����� �� ����� <a href="http://en.wikipedia.org/wiki/UTF-8" class="smalltext"
					title="����� ���� ��������� ���������� ����� � ��-����������� �������, ���� ������ ���� � ��������� UTF-8">
					(UTF-8)</a>:<br/>
				<input id="ctlFile" runat="server" type="file" size="15" name="ctlFile"/>
				<asp:LinkButton ID="btnLoadFile" runat="server" Text="���������" CssClass="button"
					title="��������� ���� �� ������� ���� ��� ��������" />
				<br/>
				<asp:CheckBox ID="chkNotUseWellKnownWords" runat="server" Text="�� ���������� �������� �����"
					Checked="True" title="�� ���������� ����� ��� ����������� � ������ �������" />
				<p>
					������� ��������� � �������� ����, ������� �� ���������. ����� �������� ����������
					����� ����� ������ � ������ �������. ��� ��������� ���� �� ����� ��������� ���������
					� ��� ���������.
				</p>
				<p>
					����� �� ������ ���� ����������� ������, �� ������� ����� ������ � ���������� �����
					(���� ��� �������� ��������, ����� ��������� <a href="http://www.adobe.com/products/flashplayer/">
						Adobe Flash Player</a>).
				</p>
				<asp:CheckBox ID="chkCheckAll" runat="server" Text="�������� ��� �����" title="�������� ��� ����� � ������" />
				<br/>
				���������� >=
				<asp:TextBox ID="txtMinPercent" runat="server" Text="70" CssClass="numerictextbox"
					title="����������� % ������������ �����" />
				% <a id="btnSelect" runat="server" href="#" class="button" title="�������� �����, ���������� ��� ������� �� �������� %">
					��������</a>
				<br/>
				<hr>
				<div align="center">
					<a id="btnChangePanel" runat="server" href="#" class="button" title="������� ���� ������ �� �������: �������/��������">
						������� ����</a> <a id="btnTest" runat="server" href="#" class="button" title="��������� ������������ ��������� ����">
							���������</a> <a id="btnAdd" runat="server" href="#" class="button" title="�������� ���������� ����� � ������ �������">
								� �������</a>
				</div>
			</td>
			<td colspan="2">
				<div id="cklWordsOriginal" runat="server" class="divWordsCheck" title="������ �� ������� ��� �������� (������� � ���� �������� ����)" />
				<div id="cklWordsTranslation" runat="server" class="divWordsCheck" title="������ �� ������� ��� �������� (������� � ���� �������� ����)" />
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="4" />
</asp:Content>
