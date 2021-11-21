<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Login.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Login" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>������� ������������</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	������� ������������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	������ ������ �������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<table width="75%" class="nobordertable">
		<tr>
			<td class="smalltext" align="right" width="50%">
				<b>���:&nbsp;&nbsp;</b>
			</td>
			<td>
				<asp:TextBox ID="txtNick" runat="server" MaxLength="20" Width="200px" CssClass="nicktext" />
			</td>
		</tr>
		<tr>
			<td class="smalltext" align="right">
				<b>������:&nbsp;&nbsp;</b>
			</td>
			<td>
				<asp:TextBox ID="txtPassword" runat="server" MaxLength="20" Width="200px" />
			</td>
		</tr>
		<tr>
			<td class="smalltext" align="right">
				<b>�����:&nbsp;&nbsp;</b>
			</td>
			<td>
				<asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="200px" />
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a id="btnSave" runat="server" href="#" class="button"
					title="��������� ��������� ��������� ������ ��������">���������</a> <a href="/" class="button"
						title="��������� �� ������� �������� ��� ���������">������</a>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<br/>
				<br/>
				<br/>
				<hr>
			</td>
		</tr>
		<tr>
			<td colspan="2" class="smalltext">
				<ul>
					<li class="smalltext"><b>���</b> - ��������, ������������ ����. </li>
					<li class="smalltext"><b>������</b> - �������������� ����. �������� ������, ���� ������.
						� ���� ������ �����, ��� ����� ��� ��� ������ ��� ������������.</li>
					<li class="smalltext"><b>�����</b> - �������������� ����. �������� ������, ���� ������.
						����� ��������� ������� ����� ����������. �� ������� �� ����� ��� ���� � �� 
						��������� ���� ������ ���� �� �� �� ����.</li>
				</ul>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
