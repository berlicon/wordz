<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Languages.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Languages" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>�������������� �����</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	�������������� �����
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	�����, ������������� ������, ��� ��������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<p>
		<b>������ ������ �� ����� ��� ��������</b>
		<asp:Repeater ID="rptLanguage" runat="server" EnableViewState="False">
			<HeaderTemplate>
				<table class="bordertable">
					<tr bgcolor="LightGrey">
						<th width="5%">
							�
						</th>
						<th width="35%">
							����
						</th>
						<th width="30%">
							�������, ����
						</th>
						<th width="30%">
							��������, ����
						</th>
					</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td align="center">
						<%#DataBinder.Eval(Container.DataItem, "Id")%>
					</td>
					<td>
						&nbsp;<%#DataBinder.Eval(Container.DataItem, "Name")%>
					</td>
					<td align="right">
						<%#DataBinder.Eval(Container.DataItem, "Name2")%>
					</td>
					<td align="right">
						<%#DataBinder.Eval(Container.DataItem, "Name3")%>
					</td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
				</table>
			</FooterTemplate>
		</asp:Repeater>
	</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="4" />
</asp:Content>
