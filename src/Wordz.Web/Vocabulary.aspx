<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Vocabulary.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Vocabulary" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Ваш словарный запас</title>
	<meta name="keywords" content="английский обучение личный словарь словарный запас" />
	<meta name="description" content="Получение информации о личном словаре и удаление незнакомых слов." />
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Ваш словарный запас
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Получение информации о личном словаре и удаление незнакомых слов
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<uc:Progress ID="ctlProgress" runat="server" />
	<table width="100%" height="100%">
		<tr>
			<td width="40%">
				<div id="cklWords" runat="server" class="divWordsAll" title="Панель со словами для удаления из личного словаря" />
			</td>
			<td valign="top">
				<p>
					В Вашем словаре слов:<br />
					<b>
						<asp:Label ID="lblCount" runat="server" Text="?" /></b>
				</p>
				<p>
					Вы можете загрузить слова из личного словаря, чтобы создать текстовый список из
					них (на всякий случай).
				</p>
				<p>
					Также, Вы можете отметить и удалить некоторые слова, если Вы считаете, что плохо
					их знаете.
				</p>
				<asp:CheckBox ID="chkCheckAll" runat="server" Text="Отметить все слова" title="Отметить все слова" />
				<br />
				<br />
				<hr>
				<div align="center">
					<a id="btnLoad" runat="server" href="#" class="button" title="Загрузить в панель все слова из личного словаря">
						Загрузить</a> <a id="btnDelete" runat="server" href="#" class="button" title="Удалить выбранные слова из личного словаря">
							Удалить</a> <a id="btnSaveText" runat="server" href="SaveTextHandler.ashx" class="button"
								title="Создать текстовый файл со списком слов из личного словаря">Создать текст</a>
				</div>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
