<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Statistics.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Statistics" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Статистика</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Статистика
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Ключевые параметры сайта
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<p>
		<br/>
		Зарегистрированных пользователей сайта:&nbsp;
		<asp:Label ID="lblSiteVersion" runat="server" Font-Bold="True" /><br/>
		Версия сайта:&nbsp; <b>2.156</b><br/>
		<br/>
		<a href="/res/Statistics/Statistics_www_wordz_ru_on_18_08_09.pdf">Статистика сайта www.wordz.ru
			на 18.08.09 (pdf-375Kb)</a><br/>
		<a href="/Languages">Список поддерживаемых языков</a><br/>
	</p>
	<p>
		<b>ТОП 20 пользователей сайта<br/>
			<a href="/CheckWords">по объёму словарного запаса</a> </b>
		<asp:Repeater ID="rptUsersByVocabulary" runat="server" EnableViewState="False">
			<HeaderTemplate>
				<table class="bordertable">
					<tr bgcolor="LightGrey">
						<th width="5%">
							№
						</th>
						<th width="65%">
							Пользователь
						</th>
						<th width="30%">
							Знает слов
						</th>
					</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td align="center">
						<%#DataBinder.Eval(Container.DataItem, "Id")%>
					</td>
					<td>
						&nbsp;<%#DataBinder.Eval(Container.DataItem, "Name")%>
					</td>
					<td align="center">
						<%#DataBinder.Eval(Container.DataItem, "Name2")%>
					</td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
				</table>
			</FooterTemplate>
		</asp:Repeater>
	</p>
	<p>
		<b>ТОП 20 пользователей сайта<br/>
			<a href="/IrregularVerbs">по знанию неправильных глаголов</a> </b>
		<asp:Repeater ID="rptUsersByVerbs" runat="server" EnableViewState="False">
			<HeaderTemplate>
				<table class="bordertable">
					<tr bgcolor="LightGrey">
						<th width="5%">
							№
						</th>
						<th width="65%">
							Пользователь
						</th>
						<th width="30%">
							Знает глаголов
						</th>
					</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td align="center">
						<%#DataBinder.Eval(Container.DataItem, "Id")%>
					</td>
					<td>
						&nbsp;<%#DataBinder.Eval(Container.DataItem, "Name")%>
					</td>
					<td align="center">
						<%#DataBinder.Eval(Container.DataItem, "Name2")%>
					</td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
				</table>
			</FooterTemplate>
		</asp:Repeater>
	</p>
	<p>
		<a name="DictionaryQuiz"></a><b>ТОП 100 пользователей и гостей сайта<br/>
			<a href="/DictionaryQuiz">по результатам конкурса на знание словаря</a> </b>
		<asp:Repeater ID="rptUsersByDictionaryQuiz" runat="server" EnableViewState="False">
			<HeaderTemplate>
				<table class="bordertable">
					<tr bgcolor="LightGrey">
						<th width="5%">
							№
						</th>
						<th width="65%">
							Участник
						</th>
						<th width="30%">
							Результат, %
						</th>
					</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td align="center">
						<%#DataBinder.Eval(Container.DataItem, "Id")%>
					</td>
					<td>
						&nbsp;<%#DataBinder.Eval(Container.DataItem, "Name")%>
					</td>
					<td align="center">
						<%#DataBinder.Eval(Container.DataItem, "Name2")%>
					</td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
				</table>
			</FooterTemplate>
		</asp:Repeater>
	</p>
	<p>
		<a name="WordsOrderQuiz"></a><b>ТОП 100 пользователей и гостей сайта<br/>
			<a href="/WordsOrderQuiz">по результатам конкурса на порядок слов в предложении</a>
		</b>
		<asp:Repeater ID="rptUsersByWordsOrderQuiz" runat="server" EnableViewState="False">
			<HeaderTemplate>
				<table class="bordertable">
					<tr bgcolor="LightGrey">
						<th width="5%">
							№
						</th>
						<th width="65%">
							Участник
						</th>
						<th width="30%">
							Результат, %
						</th>
					</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td align="center">
						<%#DataBinder.Eval(Container.DataItem, "Id")%>
					</td>
					<td>
						&nbsp;<%#DataBinder.Eval(Container.DataItem, "Name")%>
					</td>
					<td align="center">
						<%#DataBinder.Eval(Container.DataItem, "Name2")%>
					</td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
				</table>
			</FooterTemplate>
		</asp:Repeater>
	</p>
	<p>
		<a name="LevelQuiz"></a><b>ТОП 100 пользователей и гостей сайта<br/>
			<a href="/LevelQuiz">по результатам конкурса на знание грамматики</a> </b>
		<asp:Repeater ID="rptUsersByLevelQuiz" runat="server" EnableViewState="False">
			<HeaderTemplate>
				<table class="bordertable">
					<tr bgcolor="LightGrey">
						<th width="5%">
							№
						</th>
						<th width="65%">
							Участник
						</th>
						<th width="30%">
							Правильных ответов
						</th>
					</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td align="center">
						<%#DataBinder.Eval(Container.DataItem, "Id")%>
					</td>
					<td>
						&nbsp;<%#DataBinder.Eval(Container.DataItem, "Name")%>
					</td>
					<td align="center">
						<%#DataBinder.Eval(Container.DataItem, "Name2")%>
					</td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
				</table>
			</FooterTemplate>
		</asp:Repeater>
	</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="10" />
</asp:Content>
