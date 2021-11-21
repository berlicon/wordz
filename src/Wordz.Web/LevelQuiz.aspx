<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="LevelQuiz.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.LevelQuiz" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Конкурс на уровень знаний грамматики</title>
	<meta name="keywords" content="английский уровень Beginner Pre Upper Intermediate Advanced конкурс викторина соревнование тест" />
	<meta name="description" content="Вы можете соревноваться у кого выше уровень знания английского языка" />
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Конкурс на уровень знаний грамматики
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Вы можете соревноваться у кого выше уровень знания английского языка
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<uc:Progress ID="ctlProgress" runat="server" />
	<input id="hdnStep" runat="server" type="hidden" value="0" name="hdnStep" />
	<input id="hdnSuccessCount" runat="server" type="hidden" value="0" name="hdnSuccessCount" />
	<input id="hdnErrorInfo" runat="server" type="hidden" value="" name="hdnErrorInfo" />
	<table class="maintable">
		<tr>
			<td valign="middle" align="center" height="99%" id="tdExam" runat="server">
				Вам будет задан тест из <b>
					<asp:Literal ID="litTestsCount" runat="server" /></b> случайно выбранных предложений.<br />
				<br />
				Вы должны вставить пропущенное слово/фразу вместо [...].<br />
				Вы можете принудительно закончить экзамен или начать его сначала.
				<br />
				<br />
				Лучшие 100 участников викторины попадут в <a href="/Statistics#LevelQuiz" class="button">
					таблицу рекордов</a>. Если участник конкурса зарегистрирован на сайте, то в
				таблице будет показан его ник, для незарегистрированных участников потребуется ввести
				имя, или же остаться анонимом.
				<br />
				<br />
				<br />
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
								Следующий вопрос</a><br />
							<br />
						</td>
						<td align="right">
							<a id="btnAbortExam" runat="server" href="#" class="button" title="Принудительно прервать экзамен">
								Прервать экзамен</a> <a id="btnStartExamAgain" runat="server" href="#" class="button"
									title="Заново начать тест">Повторить экзамен</a><br />
							<br />
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
