<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="AddWords.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.AddWords" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>���������� ���� � ������ �������</title>
	<meta name="keywords" content="���������� �������� ���������� ���� ������ �������">
	<meta name="description" content="������� ��������� ���� � ���������� �� � ��� ������ �������.">
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	���������� ���� � ������ �������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	������� ��������� ���� � ���������� �� � ��� ������ �������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<uc:Progress ID="ctlProgress" runat="server" />
	<table width="100%" height="100%">
		<tr>
			<td width="40%" height="1%">
				&nbsp;
			</td>
			<td>
				�������� ��������� �����:&nbsp;
				<asp:CheckBox ID="chkCheckAll" runat="server" Text="���" title="�������� ��� ����� � ������" />&nbsp;&nbsp;
				<a id="btnAdd" runat="server" href="#" class="button" title="�������� ��������� ����� � ������ �������">
					�������� � �������</a>
			</td>
		</tr>
		<tr>
			<td width="40%" valign="top">
				<p>
					��������� �����:<br/>
					<asp:RadioButton ID="rbLoadFromGlobalDictionary" runat="server" Text="�� �������"
						GroupName="LoadSource" Checked="True" title="��������� ����� ���� �� ����������� �������" />
					<asp:RadioButton ID="rbLoadFromProcessPage" runat="server" Text="������������ �����"
						GroupName="LoadSource" title="��������� �������������� ������������ �� ������ ��������� ����� ����" />
				</p>
				<p>
					������� �����:<br/>
					<asp:RadioButton ID="rbSelectForDomain" runat="server" Text="�� ����:" GroupName="SelectSource"
						Checked="True" title="��������� ����� ���� �� �������� ����" />
					<asp:DropDownList ID="ddlDomain" runat="server" title="��������� ����� ���� �� �������� ����" />
					<br/>
					<asp:RadioButton ID="rbSelectRandom" runat="server" Text="��������� �������" GroupName="SelectSource"
						title="��������� ��������� ����� ���� �� ����������� �������" /><br/>
					<asp:RadioButton ID="rbSelectOrdered" runat="server" Text="������������� �� ��������"
						GroupName="SelectSource" title="��������� ������������� ����� ���� �� ����������� �������" />
				</p>
				<p>
					<table class="nobordertable">
						<tr>
							<td>
								���������� ����:
							</td>
							<td>
								<asp:TextBox ID="txtWordCount" runat="server" Text="10" CssClass="numerictextbox"
									title="���������� ����������� ����" />
							</td>
						</tr>
						<tr id="trNumber">
							<td>
								������� � ������:
							</td>
							<td>
								<asp:TextBox ID="txtWordStartIndex" runat="server" Text="1" CssClass="numerictextbox"
									title="����� �� �������, � �������� �������� ��������� �����" />
							</td>
						</tr>
					</table>
				</p>
				<br/>
				<hr>
				<div align="center">
					<a id="btnLoad" runat="server" href="#" class="button" title="��������� ��������� ����� ����">
						���������</a> <a id="btnBackToProcess" runat="server" class="button" title="��������� � �������� ��������� ������">
							��������� � ��������� ����</a>
				</div>
			</td>
			<td>
				<div id="cklWords" runat="server" class="divWords" title="������ �� ������� ���� ��� ���������� � ������ �������" />
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
