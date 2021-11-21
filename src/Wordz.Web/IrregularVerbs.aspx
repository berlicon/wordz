<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="IrregularVerbs.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.IrregularVerbs" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Неправильные глаголы</title>
	<meta name="keywords" content="таблица неправильные глаголы irregular verbs">
	<meta name="description" content="Изучение и тестирование знания неправильных глаголов, создание txt/mp3 файлов с таблицей глаголов для заучивания.">
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Неправильные глаголы
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Изучение и тестирование знания неправильных глаголов, создание txt/mp3 файлов с
	таблицей глаголов для заучивания
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<uc:Progress ID="ctlProgress" runat="server" />
	<table width="100%" height="100%">
		<tr>
			<td width="40%" height="1%">
				Вы знаете: <b>
					<asp:Label ID="lblInfo" runat="server" Text="???|??%|??%" title="Для скольких глаголов Вы подтвердили знания: всего|% от популярных|% от всех" /></b>
			</td>
			<td>
				Отметьте известные:
				<asp:CheckBox ID="chkCheckAll" runat="server" Text="Все" title="Отметить все глаголы в списке" />
			</td>
		</tr>
		<tr>
			<td valign="top">
				<p>
					Загрузить глаголы:<br/>
					<asp:RadioButton ID="rbLoadPopular" runat="server" Text="популярные-137" GroupName="LoadSource"
						Checked="True" title="Загрузить наиболее употребляемые глаголы" />
					<asp:RadioButton ID="rbLoadAll" runat="server" Text="все-481" GroupName="LoadSource"
						title="Загрузить все глаголы" /><br/>
					Заданное число:
					<asp:TextBox ID="txtWordCount" runat="server" Text="20" CssClass="numerictextbox" />
				</p>
				<p>
					Показать колонку:<br/>
					<asp:RadioButton ID="rbShowForm1" runat="server" Text="1 ф.гл." GroupName="ShowValue"
						title="Показать значение в колонке с 1-ой формой глагола" />
					<asp:RadioButton ID="rbShowForm2" runat="server" Text="2 ф.гл." GroupName="ShowValue"
						title="Показать значение в колонке с 2-ой формой глагола" />
					<asp:RadioButton ID="rbShowForm3" runat="server" Text="3 ф.гл." GroupName="ShowValue"
						title="Показать значение в колонке с 3-ей формой глагола" /><br/>
					<asp:RadioButton ID="rbShowTranslate" runat="server" Text="перевод" GroupName="ShowValue"
						title="Показать значение в колонке с переводом глагола" />
					<asp:RadioButton ID="rbShowRandom" runat="server" Text="случайно" GroupName="ShowValue"
						title="Показать значение в случайной колонке глагола" />
					<asp:RadioButton ID="rbShowAll" runat="server" Text="все" GroupName="ShowValue" Checked="True"
						title="Показать значение во всех колонках" />
				</p>
				<p>
					Сортировка:<br/>
					<asp:RadioButton ID="rbSortForm1" runat="server" Text="1 ф.гл." GroupName="Sort"
						Checked="True" title="Сортировать по значению в колонке с 1-ой формой глагола" />
					<asp:RadioButton ID="rbSortByType" runat="server" Text="тип образования" GroupName="Sort"
						title="Сортировать по типу образования глагола" /><br/>
					<asp:RadioButton ID="rbSortTranslate" runat="server" Text="перевод" GroupName="Sort"
						title="Сортировать по значению в колонке с переводом глагола" />
					<asp:RadioButton ID="rbSortRandom" runat="server" Text="случайно" GroupName="Sort"
						title="Сортировать случайным образом глаголы" />
				</p>
				<p>
					<asp:CheckBox ID="chkNotUseWellKnownVerbs" runat="server" Text="скрыть знакомые глаголы"
						Checked="True" title="Скрыть глаголы, ранее подтвержденные как знакомые" />
				</p>
				<div align="center">
					<table class="noborderalignedtable">
						<tr>
							<td>
								<a id="btnLoad" runat="server" href="#" title="Загрузить требуемый набор глаголов"
									class="button">Загрузить</a>
							</td>
							<td>
								<a id="btnCheck" runat="server" href="#" title="Проверить знание глаголов" class="button">
									Проверить</a>
							</td>
						</tr>
						<tr>
							<td>
								<a id="btnUpdate" runat="server" href="#" title="Добавить выбранные глаголы как знакомые в личный словарь"
									class="button">В словарь</a>
							</td>
							<td>
								<a id="btnClear" runat="server" href="#" title="Очистить личный словарь от статистики знания глаголов"
									class="button">Очистить</a>
							</td>
						</tr>
						<tr>
							<td>
								<a id="btnSaveText" runat="server" href="SaveTextHandler.ashx" title="Создать текстовый файл (*.txt) с результирующим списком глаголов"
									class="button">Создать текст</a>&nbsp;
							</td>
							<td>
								<a id="btnSaveAudio" runat="server" href="SaveSoundVerbsHandler.ashx" title="Создать звуковой файл (*.mp3) с результирующим списком глаголов"
									class="button">Создать звук</a>
							</td>
						</tr>
					</table>
				</div>
				<hr>
				Статьи о глаголах:<br/>
				<a href="/Articles/39">Правила образования</a><br/>
				<a href="/Articles/40">Таблица-простая</a><br/>
				<a href="/Articles/41">Таблица-малая</a><br/>
				<a href="/Articles/42">Таблица-большая</a><br/>
				<a href="/Articles/43">TOP 135 глаголов</a><br/>
				<a href="/res/IrregularVerbs/82-irregular-verbs-british-man.mp3">82 озвученных глагола
					(mp3-2Mb)</a>
			</td>
			<td>
				<div id="cklWords" runat="server" class="divWords" title="Панель с неправильными глаголами для тестирования" />
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="4" />
</asp:Content>
