<%@ Control Language="c#" AutoEventWireup="False" CodeBehind="Language.ascx.cs" Inherits="Wordz.Web.Controls.Language"
	TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="false" %>
<div class="language">
	<span id="lblLearnText" runat="server">Я изучаю</span>:
	<asp:DropDownList ID="ddlLanguageLearn" runat="server" />
	<span id="pnlNative" runat="server" class="language">
	<br/>
	<span id="lblNativeText" runat="server">Я знаю</span>:
	<asp:DropDownList ID="ddlLanguageNative" runat="server" />
	</span>
</div>