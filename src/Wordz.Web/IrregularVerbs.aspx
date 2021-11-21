<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="IrregularVerbs.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.IrregularVerbs" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>������������ �������</title>
	<meta name="keywords" content="������� ������������ ������� irregular verbs">
	<meta name="description" content="�������� � ������������ ������ ������������ ��������, �������� txt/mp3 ������ � �������� �������� ��� ����������.">
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	������������ �������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	�������� � ������������ ������ ������������ ��������, �������� txt/mp3 ������ �
	�������� �������� ��� ����������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<uc:Progress ID="ctlProgress" runat="server" />
	<table width="100%" height="100%">
		<tr>
			<td width="40%" height="1%">
				�� ������: <b>
					<asp:Label ID="lblInfo" runat="server" Text="???|??%|??%" title="��� �������� �������� �� ����������� ������: �����|% �� ����������|% �� ����" /></b>
			</td>
			<td>
				�������� ���������:
				<asp:CheckBox ID="chkCheckAll" runat="server" Text="���" title="�������� ��� ������� � ������" />
			</td>
		</tr>
		<tr>
			<td valign="top">
				<p>
					��������� �������:<br/>
					<asp:RadioButton ID="rbLoadPopular" runat="server" Text="����������-137" GroupName="LoadSource"
						Checked="True" title="��������� �������� ������������� �������" />
					<asp:RadioButton ID="rbLoadAll" runat="server" Text="���-481" GroupName="LoadSource"
						title="��������� ��� �������" /><br/>
					�������� �����:
					<asp:TextBox ID="txtWordCount" runat="server" Text="20" CssClass="numerictextbox" />
				</p>
				<p>
					�������� �������:<br/>
					<asp:RadioButton ID="rbShowForm1" runat="server" Text="1 �.��." GroupName="ShowValue"
						title="�������� �������� � ������� � 1-�� ������ �������" />
					<asp:RadioButton ID="rbShowForm2" runat="server" Text="2 �.��." GroupName="ShowValue"
						title="�������� �������� � ������� � 2-�� ������ �������" />
					<asp:RadioButton ID="rbShowForm3" runat="server" Text="3 �.��." GroupName="ShowValue"
						title="�������� �������� � ������� � 3-�� ������ �������" /><br/>
					<asp:RadioButton ID="rbShowTranslate" runat="server" Text="�������" GroupName="ShowValue"
						title="�������� �������� � ������� � ��������� �������" />
					<asp:RadioButton ID="rbShowRandom" runat="server" Text="��������" GroupName="ShowValue"
						title="�������� �������� � ��������� ������� �������" />
					<asp:RadioButton ID="rbShowAll" runat="server" Text="���" GroupName="ShowValue" Checked="True"
						title="�������� �������� �� ���� ��������" />
				</p>
				<p>
					����������:<br/>
					<asp:RadioButton ID="rbSortForm1" runat="server" Text="1 �.��." GroupName="Sort"
						Checked="True" title="����������� �� �������� � ������� � 1-�� ������ �������" />
					<asp:RadioButton ID="rbSortByType" runat="server" Text="��� �����������" GroupName="Sort"
						title="����������� �� ���� ����������� �������" /><br/>
					<asp:RadioButton ID="rbSortTranslate" runat="server" Text="�������" GroupName="Sort"
						title="����������� �� �������� � ������� � ��������� �������" />
					<asp:RadioButton ID="rbSortRandom" runat="server" Text="��������" GroupName="Sort"
						title="����������� ��������� ������� �������" />
				</p>
				<p>
					<asp:CheckBox ID="chkNotUseWellKnownVerbs" runat="server" Text="������ �������� �������"
						Checked="True" title="������ �������, ����� �������������� ��� ��������" />
				</p>
				<div align="center">
					<table class="noborderalignedtable">
						<tr>
							<td>
								<a id="btnLoad" runat="server" href="#" title="��������� ��������� ����� ��������"
									class="button">���������</a>
							</td>
							<td>
								<a id="btnCheck" runat="server" href="#" title="��������� ������ ��������" class="button">
									���������</a>
							</td>
						</tr>
						<tr>
							<td>
								<a id="btnUpdate" runat="server" href="#" title="�������� ��������� ������� ��� �������� � ������ �������"
									class="button">� �������</a>
							</td>
							<td>
								<a id="btnClear" runat="server" href="#" title="�������� ������ ������� �� ���������� ������ ��������"
									class="button">��������</a>
							</td>
						</tr>
						<tr>
							<td>
								<a id="btnSaveText" runat="server" href="SaveTextHandler.ashx" title="������� ��������� ���� (*.txt) � �������������� ������� ��������"
									class="button">������� �����</a>&nbsp;
							</td>
							<td>
								<a id="btnSaveAudio" runat="server" href="SaveSoundVerbsHandler.ashx" title="������� �������� ���� (*.mp3) � �������������� ������� ��������"
									class="button">������� ����</a>
							</td>
						</tr>
					</table>
				</div>
				<hr>
				������ � ��������:<br/>
				<a href="/Articles/39">������� �����������</a><br/>
				<a href="/Articles/40">�������-�������</a><br/>
				<a href="/Articles/41">�������-�����</a><br/>
				<a href="/Articles/42">�������-�������</a><br/>
				<a href="/Articles/43">TOP 135 ��������</a><br/>
				<a href="/res/IrregularVerbs/82-irregular-verbs-british-man.mp3">82 ���������� �������
					(mp3-2Mb)</a>
			</td>
			<td>
				<div id="cklWords" runat="server" class="divWords" title="������ � ������������� ��������� ��� ������������" />
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="4" />
</asp:Content>
