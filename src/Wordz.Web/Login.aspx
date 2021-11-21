<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Login.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Login" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Аккаунт пользователя</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Аккаунт пользователя
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Данные вашего профиля
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<table width="75%" class="nobordertable">
		<tr>
			<td class="smalltext" align="right" width="50%">
				<b>Ник:&nbsp;&nbsp;</b>
			</td>
			<td>
				<asp:TextBox ID="txtNick" runat="server" MaxLength="20" Width="200px" CssClass="nicktext" />
			</td>
		</tr>
		<tr>
			<td class="smalltext" align="right">
				<b>Пароль:&nbsp;&nbsp;</b>
			</td>
			<td>
				<asp:TextBox ID="txtPassword" runat="server" MaxLength="20" Width="200px" />
			</td>
		</tr>
		<tr>
			<td class="smalltext" align="right">
				<b>Емайл:&nbsp;&nbsp;</b>
			</td>
			<td>
				<asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="200px" />
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a id="btnSave" runat="server" href="#" class="button"
					title="Сохранить сделанные изменения Вашего аккаунта">Сохранить</a> <a href="/" class="button"
						title="Вернуться на главную страницу без изменений">Отмена</a>
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
					<li class="smalltext"><b>Ник</b> - уникален, обязательное поле. </li>
					<li class="smalltext"><b>Пароль</b> - необязательное поле. Оставьте пустым, есть хотите.
						В этом случае любой, кто знает ваш ник сможет его использовать.</li>
					<li class="smalltext"><b>Емайл</b> - необязательное поле. Оставьте пустым, если хотите.
						Тогда некоторые функции будут недоступны. Мы никогда не пошлём вам спам и не 
						передадим ваши данные кому бы то ни было.</li>
				</ul>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
