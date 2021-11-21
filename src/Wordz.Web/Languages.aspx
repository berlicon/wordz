<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Languages.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Languages" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Поддерживаемые языки</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Поддерживаемые языки
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Языки, поддерживамые сайтом, для обучения
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<p>
		<b>Список языков на сайте для обучения</b>
		<asp:Repeater ID="rptLanguage" runat="server" EnableViewState="False">
			<HeaderTemplate>
				<table class="bordertable">
					<tr bgcolor="LightGrey">
						<th width="5%">
							№
						</th>
						<th width="35%">
							Язык
						</th>
						<th width="30%">
							Словарь, слов
						</th>
						<th width="30%">
							Озвучено, слов
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
