<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Resources.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Resources" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Ресурсы для скачивания</title>
	<meta name="keywords" content="скачать аудиокниги текст English download audio books MP3 text">
	<meta name="description" content="Аудиокниги для прослушивания, тексты на английском языке.">
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Ресурсы для скачивания
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Аудиокниги для прослушивания, тексты на английском языке
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<p>
		Вы можете скачать эти файлы для постановки правильного произношения или для заучивания
		новых слов. Также, много других подобных ресурсов Вы можете <a href="/Partners">скачать
			здесь</a>.
	</p>
	<p>
		<table width="100%" class="bordertable">
			<tr bgcolor="LightGrey">
				<th width="5%">
					№
				</th>
				<th width="65%">
					Название
				</th>
				<th width="30%">
					Файлы
				</th>
			</tr>
			<tr>
				<td align="center">
					1
				</td>
				<td>
					Dan Brown - Angels And Demons
				</td>
				<td>
					<a href="/res/AD/Dan Brown - Angels And Demons (txt).rar">txt-English-473Kb</a><br/>
					<a href="/res/AD/Dan Brown - Angels And Demons (mp3).rar">mp3-English-272Mb</a>
				</td>
			</tr>
			<tr>
				<td align="center">
					2
				</td>
				<td>
					George Orwell - Animal Farm
				</td>
				<td>
					<a href="/res/AF/George_Orwell_-_Animal_Farm (txt EN).rar">txt-English-46Kb</a><br/>
					<a href="/res/AF/George_Orwell_-_Animal_Farm (txt RU).rar">txt-Russian-48Kb</a><br/>
					<a href="/res/AF/George_Orwell_-_Animal_Farm (mp3).rar">mp3-English-83Mb</a>
				</td>
			</tr>
			<tr>
				<td align="center">
					3
				</td>
				<td>
					The Economist - 2007-12-01
				</td>
				<td>
					<a href="/res/EC/The_Economist_2007-12-01 (pdf).rar">pdf-English-2Mb</a><br/>
					<a href="/res/EC/The_Economist_2007-12-01 (mp3).rar">mp3-English-184Mb</a>
				</td>
			</tr>
			<%--<tr>
				<td align="center">
					4
				</td>
				<td>
					Roald Dahl - A collection of short stories
				</td>
				<td>
					<a href="/res/SS/Roald Dahl - A collection of short stories (mp3).rar">mp3-English-154Mb</a>
				</td>
			</tr>
			<tr>
				<td align="center">
					5
				</td>
				<td>
					E.H. Gombrich - A Little History of the World
				</td>
				<td>
					<a href="/res/LH/E.H._Gombrich_-_A_Little_History_of_the_World (mp3).rar">mp3-English-535Mb</a>
				</td>
			</tr>--%>
			<tr>
				<td align="center">
					4
				</td>
				<td>
					Constantine Berlinsky - Silver Bullets Toolkit (Reference book of Successful Project
					Solutions in the Process of Software Development)
				</td>
				<td>
					<a href="/res/NSP/NSP_Book_English.pdf">pdf-English-0.4Mb</a><br/>
					<a href="/res/NSP/NSP_Book_Russian.pdf">pdf-Russian-0.5Mb</a><br/>
				</td>
			</tr>
		</table>
	</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="4" />
</asp:Content>
