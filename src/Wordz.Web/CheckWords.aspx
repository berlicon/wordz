<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="CheckWords.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.CheckWords" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Проверка словарного запаса</title>
	<meta name="keywords" content="английский обучение проверка знание слов словарный запас">
	<meta name="description" content="Контроль знания слов и добавление их в личный словарь.">
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Проверка словарного запаса
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Контроль знания слов и добавление их в личный словарь. Определение слов на слух
	(орфоолимпиада).
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<uc:Progress ID="ctlProgress" runat="server" />
	<table width="100%" height="100%">
		<tr>
			<td width="40%" valign="top">
				Вставьте слова из файла <a href="http://en.wikipedia.org/wiki/UTF-8" class="smalltext"
					title="Чтобы были корректно распознаны слова с не-английскими буквами, файл должен быть в кодировке UTF-8">
					(UTF-8)</a>:<br/>
				<input id="ctlFile" runat="server" type="file" size="15" name="ctlFile"/>
				<asp:LinkButton ID="btnLoadFile" runat="server" Text="Загрузить" CssClass="button"
					title="Загрузить файл со списком слов для проверки" />
				<br/>
				<asp:CheckBox ID="chkNotUseWellKnownWords" runat="server" Text="не отображать знакомые слова"
					Checked="True" title="Не отображать слова уже добавленные в личный словарь" />
				<p>
					Впишите оригиналы и переводы слов, которые вы запомнили. После проверки отмеченные
					слова можно внести в личный словарь. При наведении мыши на слово выводится подсказка
					с его переводом.
				</p>
				<p>
					Рядом со словом есть специальный значок, на который можно нажать и прослушать слово
					(если оно озвучено диктором, также требуется <a href="http://www.adobe.com/products/flashplayer/">
						Adobe Flash Player</a>).
				</p>
				<asp:CheckBox ID="chkCheckAll" runat="server" Text="Отметить все слова" title="Отметить все слова в списке" />
				<br/>
				Правильные >=
				<asp:TextBox ID="txtMinPercent" runat="server" Text="70" CssClass="numerictextbox"
					title="минимальный % корректности слова" />
				% <a id="btnSelect" runat="server" href="#" class="button" title="Отметить слова, правильные как минимум на заданный %">
					Отметить</a>
				<br/>
				<hr>
				<div align="center">
					<a id="btnChangePanel" runat="server" href="#" class="button" title="Сменить язык панели со словами: перевод/оригинал">
						Сменить язык</a> <a id="btnTest" runat="server" href="#" class="button" title="Проверить корректность введенных слов">
							Проверить</a> <a id="btnAdd" runat="server" href="#" class="button" title="Добавить отмеченные слова в личный словарь">
								В словарь</a>
				</div>
			</td>
			<td colspan="2">
				<div id="cklWordsOriginal" runat="server" class="divWordsCheck" title="Панель со словами для проверки (введите в поля переводы слов)" />
				<div id="cklWordsTranslation" runat="server" class="divWordsCheck" title="Панель со словами для проверки (введите в поля переводы слов)" />
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="4" />
</asp:Content>
