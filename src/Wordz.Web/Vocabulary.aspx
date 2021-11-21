<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Vocabulary.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Vocabulary" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>��� ��������� �����</title>
	<meta name="keywords" content="���������� �������� ������ ������� ��������� �����" />
	<meta name="description" content="��������� ���������� � ������ ������� � �������� ���������� ����." />
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	��� ��������� �����
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	��������� ���������� � ������ ������� � �������� ���������� ����
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<uc:Progress ID="ctlProgress" runat="server" />
	<table width="100%" height="100%">
		<tr>
			<td width="40%">
				<div id="cklWords" runat="server" class="divWordsAll" title="������ �� ������� ��� �������� �� ������� �������" />
			</td>
			<td valign="top">
				<p>
					� ����� ������� ����:<br />
					<b>
						<asp:Label ID="lblCount" runat="server" Text="?" /></b>
				</p>
				<p>
					�� ������ ��������� ����� �� ������� �������, ����� ������� ��������� ������ ��
					��� (�� ������ ������).
				</p>
				<p>
					�����, �� ������ �������� � ������� ��������� �����, ���� �� ��������, ��� �����
					�� ������.
				</p>
				<asp:CheckBox ID="chkCheckAll" runat="server" Text="�������� ��� �����" title="�������� ��� �����" />
				<br />
				<br />
				<hr>
				<div align="center">
					<a id="btnLoad" runat="server" href="#" class="button" title="��������� � ������ ��� ����� �� ������� �������">
						���������</a> <a id="btnDelete" runat="server" href="#" class="button" title="������� ��������� ����� �� ������� �������">
							�������</a> <a id="btnSaveText" runat="server" href="SaveTextHandler.ashx" class="button"
								title="������� ��������� ���� �� ������� ���� �� ������� �������">������� �����</a>
				</div>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
