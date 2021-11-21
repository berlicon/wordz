<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="WordsOrderQuiz.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.WordsOrderQuiz" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Конкурс на порядок слов в предложении</title>
	<meta name="keywords" content="английский конкурс викторина соревнование тест порядок слов предложении" />
	<meta name="description" content="Вы можете соревноваться кто лучше знает порядок слов в английском предложении" />
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Конкурс на порядок слов в предложении
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Вы можете соревноваться кто лучше знает порядок слов в английском предложении
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
					<asp:Literal ID="litTestsCount" runat="server" /></b> предложений с перемешанными
				словами.<br />
				<br />
				Вы должны выставить правильный порядок слов. Используйте кнопки &lt; и &gt; на словах
				для перемещения порядка очерёдности слова.<br />
				Вы можете принудительно закончить экзамен или начать его сначала.<br />
				Подробнее о порядке слов в английском предложении можно прочитать здесь:<br />
				<a href="/Articles/45" class="button">ссылка 1</a> <a href="/Articles/47" class="button">
					ссылка 2</a> <a href="/Articles/48" class="button">ссылка 3</a> <a href="/Articles/49"
						class="button">ссылка 4</a>
				<br />
				<br />
				Лучшие 100 участников викторины попадут в <a href="/Statistics#WordsOrderQuiz" class="button">
					таблицу рекордов</a>. Если участник конкурса зарегистирован на сайте, то в таблице
				будет показан его ник, для незарегистрированных участников потребуется ввести имя,
				или же остаться анонимом.
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
