<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="DictionaryQuiz.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.DictionaryQuiz" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Конкурс на знание словаря</title>
	<meta name="keywords" content="английский словарь словарный запас конкурс викторина соревнование тест">
	<meta name="description" content="Вы можете соревноваться кто лучше знает английский словарь">
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Конкурс на знание словаря
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Вы можете соревноваться кто лучше знает английский словарь
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<uc:Progress ID="ctlProgress" runat="server" />
	<input id="hdnStep" runat="server" type="hidden" value="0" />
	<input id="hdnSuccessCount" runat="server" type="hidden" value="0" />
	<table class="maintable">
		<tr>
			<td valign="middle" align="center" height="99%" id="tdExam" runat="server">
				Вам будет задан тест из <b>
					<asp:Literal ID="litTestsCount" runat="server" /></b> случайно выбранных слов
				из словаря.<br/>
				<br/>
				Вы должны выбрать из предложенных вариантов правильный перевод слова.<br/>
				Вы можете принудительно закончить экзамен или начать его сначала.
				<br/>
				<br/>
				Лучшие 100 участников викторины попадут в <a href="/Statistics#DictionaryQuiz" class="button">
					таблицу рекордов</a>. Если участник конкурса зарегистрирован на сайте, то в
				таблице будет показан его ник, для незарегистрированных участников потребуется ввести
				имя, или же остаться анонимом.
				<br/>
				<br/>
				<br/>
				<a id="btnStartExam" runat="server" href="#" class="button" title="Приступить к тестированию в конкурсе">
					Начать тест!</a>
			</td>
		</tr>
		<tr>
			<td>
				<table id="tblButtons" runat="server" class="nobordertable" width="100%" style="display: none">
					<tr>
						<td>
							<a id="btnNext" runat="server" href="#" class="button" title="Перейти к следующему вопросу">
								Следующий вопрос</a><br/>
							<br/>
						</td>
						<td align="right">
							<a id="btnAbortExam" runat="server" href="#" class="button" title="Принудительно прервать экзамен">
								Прервать экзамен</a> <a id="btnStartExamAgain" runat="server" href="#" class="button"
									title="Заново начать тест">Повторить экзамен</a><br/>
							<br/>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
