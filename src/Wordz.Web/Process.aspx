<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Process.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Process" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Обработка текста</title>
	<meta name="keywords" content="английский обучение mp3 txt аудио текст незнакомые слова">
	<meta name="description" content="Получение списка незнакомых слов (txt и mp3 файлы) для изучения.">
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Обработка текста
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Получение списка незнакомых слов (txt и mp3 файлы) для изучения
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<uc:Progress ID="ctlProgress" runat="server" />
	<table width="100%" height="100%">
		<tr>
			<td width="40%">
				<table width="100%" height="100%">
					<tr>
						<td height="10%">
							Вставьте текст из буфера или файла <a href="http://en.wikipedia.org/wiki/UTF-8" class="smalltext"
								title="Чтобы были корректно распознаны слова с не-английскими буквами, файл должен быть в кодировке UTF-8">
								(UTF-8)</a>:
							<asp:LinkButton ID="btnLoadFile" runat="server" Text="Загрузить" title="Загрузить файл с текстом для обработки"
								CssClass="button" /><br />
							<input id="ctlFile" runat="server" type="file" size="15" name="ctlFile" />
						</td>
					</tr>
					<tr>
						<td height="40%">
							<asp:TextBox ID="txtSrc" runat="server" CssClass="multilinetextbox" TextMode="MultiLine"
								Rows="6" title="Панель с текстом для обработки (текст можно ввести вручную)" />
						</td>
					</tr>
					<tr>
						<td height="5%">
							Уникальных слов:&nbsp;<asp:Label ID="lblWordCount" runat="server" Text="0" /><br />
							Макс. частота:&nbsp;<asp:Label ID="lblMaxFrequency" runat="server" Text="0" />
						</td>
					</tr>
					<tr>
						<td height="40%">
							<asp:TextBox ID="txtDest" runat="server" CssClass="multilinetextbox" TextMode="MultiLine"
								Rows="6" ReadOnly="True" title="Панель со списком обработанных слов" />
						</td>
					</tr>
					<tr>
						<td height="5%" align="center">
							<a id="btnAddWords" runat="server" class="button">В словарь</a>
						</td>
					</tr>
				</table>
			</td>
			<td width="5%" />
			<td>
				<p>
					Первое слово:&nbsp;
					<asp:RadioButton ID="rbWordOrderEngRus" runat="server" Text="оригинал" GroupName="WordOrder"
						Checked="True" title="Сортировка слов в результирующем тексте" />
					<asp:RadioButton ID="rbWordOrderRusEng" runat="server" Text="перевод" GroupName="WordOrder"
						title="Сортировка слов в результирующем тексте" />
				</p>
				Показывать только слова:<br />
				<asp:CheckBox ID="chkNotUseWellKnownWords" runat="server" Text="незнакомые" Checked="True"
					title="Показывать только слова, не присутствующие в Вашем словаре" />
				<asp:CheckBox ID="chkNotUseNotKnownWords" runat="server" Text="с переводом" Checked="True"
					title="Показывать только слова, для которых есть перевод" />
				<asp:CheckBox ID="chkNotUseNotSoundedWords" runat="server" Text="озвученые диктором"
					title="Показывать только слова, которые озвучены диктором" />
				<p>
					Сортировка:<br />
					<asp:RadioButton ID="rbSortAlphabet" runat="server" Text="алфавитная" GroupName="Sort"
						Checked="True" title="Сортировка слов в результирующем тексте по алфавиту" /><br />
					<asp:RadioButton ID="rbSortFrequency" runat="server" Text="встречаемость слова" GroupName="Sort"
						title="Сортировка слов в результирующем тексте по частоте встречаемости слова" /><br />
					<asp:RadioButton ID="rbSortWordsLength" runat="server" Text="длина слова" GroupName="Sort"
						title="Сортировка слов в результирующем тексте по длине слова" /><br />
					<asp:RadioButton ID="rbSortMixedOrder" runat="server" Text="смешанный порядок" GroupName="Sort"
						title="Сортировка слов в результирующем тексте в случайном порядке" /><br />
					<asp:RadioButton ID="rbSortOriginalOrder" runat="server" Text="оригинальный порядок"
						GroupName="Sort" title="Сортировка слов в результирующем тексте в порядке, как они были в тексте" />
				</p>
				<div align="right">
					Мин. частота&nbsp;&gt;=
					<asp:TextBox ID="txtMinFrequency" runat="server" Text="1" CssClass="numerictextbox"
						title="Сколько раз должно встретиться слово, чтобы попасть в результирующий список" /><br />
					Значащих слов&nbsp;&lt;=
					<asp:TextBox ID="txtMaxSignedWords" runat="server" Text="2" CssClass="numerictextbox"
						title="Сколько слов отображать в переводе слова" />
				</div>
				<hr>
				<div align="center">
					<a id="btnProcess" runat="server" href="#" title="Обработать исходный текст и получить результирующий список"
						class="button">Обработать</a>&nbsp; <a id="btnSaveText" runat="server" href="SaveTextHandler.ashx"
							title="Создать текстовый файл (*.txt) с результирующим списком слов" class="button">
							Создать текст</a>&nbsp; <a id="btnSaveAudio" runat="server" href="SaveSoundHandler.ashx"
								title="Создать звуковой файл (*.mp3) с результирующим списком слов" class="button">
								Создать звук</a>
				</div>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
