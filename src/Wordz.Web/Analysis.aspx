<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Analysis.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Analysis" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Анализ текста</title>
	<meta name="keywords" content="английский обучение анализ текст обновление словарь">
	<meta name="description" content="Быстрый анализ английского текста и добавление слов в личный словарь.">
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Анализ текста
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Определение в тексте незнакомых слов и пополнение личного словаря
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<uc:Progress ID="ctlProgress" runat="server" />
	<table width="100%" height="100%">
		<tr>
			<td colspan="2" height="1%">
				Вставьте текст из файла <a href="http://en.wikipedia.org/wiki/UTF-8" class="smalltext"
					title="Чтобы были корректно распознаны слова с не-английскими буквами, файл должен быть в кодировке UTF-8">
					(UTF-8)</a>:
				<input id="ctlFile" runat="server" type="file" size="15" name="ctlFile">
				<asp:LinkButton ID="btnLoadFile" runat="server" Text="Загрузить" CssClass="button"
					title="Загрузить файл с текстом для анализа" />
			</td>
		</tr>
		<tr>
			<td width="70%">
				<div id="cklWords" runat="server" class="divWordsAll" title="Панель со словами для анализа" />
			</td>
			<td valign="top" align="center">
				<br/>
				Кликнув по слову, вы можете отметить его как <span id="spanUnknownWord" runat="server">
					незнакомое</span>&nbsp;или как&nbsp; <span id="spanKnownWord" runat="server">знакомое
						слово</span><br/>
				<br/>
				<hr>
				<a id="btnMakeAllUnknown" runat="server" href="#" class="button" title="Пометить все слова как незнакомые">
					Всё незнакомо!</a><br/>
				<br/>
				<a id="btnMakeAllKnown" runat="server" href="#" class="button" title="Пометить все слова как знакомые">
					Всё знакомо!</a>
				<br/>
				<br/>
				<a id="btnAnalysis" runat="server" href="#" class="button" title="Проверить слова в тексте, присутствуют ли они в Вашем словаре">
					Анализировать текст</a><br/>
				<br/>
				<a id="btnUpdateVocabulary" runat="server" href="#" class="button" title="Удалить из Вашего словаря незнакомые слова и добавить знакомые">
					Обновить словарь</a><br/>
				<br/>
				<a id="btnProcessUnknownWords" runat="server" href="#" class="button" title="Открыть страницу обработки текста с незнакомыми словами">
					Обработать слова</a>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
