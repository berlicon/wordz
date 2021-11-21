<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Process.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Process" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>��������� ������</title>
	<meta name="keywords" content="���������� �������� mp3 txt ����� ����� ���������� �����">
	<meta name="description" content="��������� ������ ���������� ���� (txt � mp3 �����) ��� ��������.">
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	��������� ������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	��������� ������ ���������� ���� (txt � mp3 �����) ��� ��������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<uc:Progress ID="ctlProgress" runat="server" />
	<table width="100%" height="100%">
		<tr>
			<td width="40%">
				<table width="100%" height="100%">
					<tr>
						<td height="10%">
							�������� ����� �� ������ ��� ����� <a href="http://en.wikipedia.org/wiki/UTF-8" class="smalltext"
								title="����� ���� ��������� ���������� ����� � ��-����������� �������, ���� ������ ���� � ��������� UTF-8">
								(UTF-8)</a>:
							<asp:LinkButton ID="btnLoadFile" runat="server" Text="���������" title="��������� ���� � ������� ��� ���������"
								CssClass="button" /><br />
							<input id="ctlFile" runat="server" type="file" size="15" name="ctlFile" />
						</td>
					</tr>
					<tr>
						<td height="40%">
							<asp:TextBox ID="txtSrc" runat="server" CssClass="multilinetextbox" TextMode="MultiLine"
								Rows="6" title="������ � ������� ��� ��������� (����� ����� ������ �������)" />
						</td>
					</tr>
					<tr>
						<td height="5%">
							���������� ����:&nbsp;<asp:Label ID="lblWordCount" runat="server" Text="0" /><br />
							����. �������:&nbsp;<asp:Label ID="lblMaxFrequency" runat="server" Text="0" />
						</td>
					</tr>
					<tr>
						<td height="40%">
							<asp:TextBox ID="txtDest" runat="server" CssClass="multilinetextbox" TextMode="MultiLine"
								Rows="6" ReadOnly="True" title="������ �� ������� ������������ ����" />
						</td>
					</tr>
					<tr>
						<td height="5%" align="center">
							<a id="btnAddWords" runat="server" class="button">� �������</a>
						</td>
					</tr>
				</table>
			</td>
			<td width="5%" />
			<td>
				<p>
					������ �����:&nbsp;
					<asp:RadioButton ID="rbWordOrderEngRus" runat="server" Text="��������" GroupName="WordOrder"
						Checked="True" title="���������� ���� � �������������� ������" />
					<asp:RadioButton ID="rbWordOrderRusEng" runat="server" Text="�������" GroupName="WordOrder"
						title="���������� ���� � �������������� ������" />
				</p>
				���������� ������ �����:<br />
				<asp:CheckBox ID="chkNotUseWellKnownWords" runat="server" Text="����������" Checked="True"
					title="���������� ������ �����, �� �������������� � ����� �������" />
				<asp:CheckBox ID="chkNotUseNotKnownWords" runat="server" Text="� ���������" Checked="True"
					title="���������� ������ �����, ��� ������� ���� �������" />
				<asp:CheckBox ID="chkNotUseNotSoundedWords" runat="server" Text="��������� ��������"
					title="���������� ������ �����, ������� �������� ��������" />
				<p>
					����������:<br />
					<asp:RadioButton ID="rbSortAlphabet" runat="server" Text="����������" GroupName="Sort"
						Checked="True" title="���������� ���� � �������������� ������ �� ��������" /><br />
					<asp:RadioButton ID="rbSortFrequency" runat="server" Text="������������� �����" GroupName="Sort"
						title="���������� ���� � �������������� ������ �� ������� ������������� �����" /><br />
					<asp:RadioButton ID="rbSortWordsLength" runat="server" Text="����� �����" GroupName="Sort"
						title="���������� ���� � �������������� ������ �� ����� �����" /><br />
					<asp:RadioButton ID="rbSortMixedOrder" runat="server" Text="��������� �������" GroupName="Sort"
						title="���������� ���� � �������������� ������ � ��������� �������" /><br />
					<asp:RadioButton ID="rbSortOriginalOrder" runat="server" Text="������������ �������"
						GroupName="Sort" title="���������� ���� � �������������� ������ � �������, ��� ��� ���� � ������" />
				</p>
				<div align="right">
					���. �������&nbsp;&gt;=
					<asp:TextBox ID="txtMinFrequency" runat="server" Text="1" CssClass="numerictextbox"
						title="������� ��� ������ ����������� �����, ����� ������� � �������������� ������" /><br />
					�������� ����&nbsp;&lt;=
					<asp:TextBox ID="txtMaxSignedWords" runat="server" Text="2" CssClass="numerictextbox"
						title="������� ���� ���������� � �������� �����" />
				</div>
				<hr>
				<div align="center">
					<a id="btnProcess" runat="server" href="#" title="���������� �������� ����� � �������� �������������� ������"
						class="button">����������</a>&nbsp; <a id="btnSaveText" runat="server" href="SaveTextHandler.ashx"
							title="������� ��������� ���� (*.txt) � �������������� ������� ����" class="button">
							������� �����</a>&nbsp; <a id="btnSaveAudio" runat="server" href="SaveSoundHandler.ashx"
								title="������� �������� ���� (*.mp3) � �������������� ������� ����" class="button">
								������� ����</a>
				</div>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
