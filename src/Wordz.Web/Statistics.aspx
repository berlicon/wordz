<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Statistics.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Statistics" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>����������</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	����������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	�������� ��������� �����
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<p>
		<br/>
		������������������ ������������� �����:&nbsp;
		<asp:Label ID="lblSiteVersion" runat="server" Font-Bold="True" /><br/>
		������ �����:&nbsp; <b>2.156</b><br/>
		<br/>
		<a href="/res/Statistics/Statistics_www_wordz_ru_on_18_08_09.pdf">���������� ����� www.wordz.ru
			�� 18.08.09 (pdf-375Kb)</a><br/>
		<a href="/Languages">������ �������������� ������</a><br/>
	</p>
	<p>
		<b>��� 20 ������������� �����<br/>
			<a href="/CheckWords">�� ������ ���������� ������</a> </b>
		<asp:Repeater ID="rptUsersByVocabulary" runat="server" EnableViewState="False">
			<HeaderTemplate>
				<table class="bordertable">
					<tr bgcolor="LightGrey">
						<th width="5%">
							�
						</th>
						<th width="65%">
							������������
						</th>
						<th width="30%">
							����� ����
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
		<b>��� 20 ������������� �����<br/>
			<a href="/IrregularVerbs">�� ������ ������������ ��������</a> </b>
		<asp:Repeater ID="rptUsersByVerbs" runat="server" EnableViewState="False">
			<HeaderTemplate>
				<table class="bordertable">
					<tr bgcolor="LightGrey">
						<th width="5%">
							�
						</th>
						<th width="65%">
							������������
						</th>
						<th width="30%">
							����� ��������
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
		<a name="DictionaryQuiz"></a><b>��� 100 ������������� � ������ �����<br/>
			<a href="/DictionaryQuiz">�� ����������� �������� �� ������ �������</a> </b>
		<asp:Repeater ID="rptUsersByDictionaryQuiz" runat="server" EnableViewState="False">
			<HeaderTemplate>
				<table class="bordertable">
					<tr bgcolor="LightGrey">
						<th width="5%">
							�
						</th>
						<th width="65%">
							��������
						</th>
						<th width="30%">
							���������, %
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
		<a name="WordsOrderQuiz"></a><b>��� 100 ������������� � ������ �����<br/>
			<a href="/WordsOrderQuiz">�� ����������� �������� �� ������� ���� � �����������</a>
		</b>
		<asp:Repeater ID="rptUsersByWordsOrderQuiz" runat="server" EnableViewState="False">
			<HeaderTemplate>
				<table class="bordertable">
					<tr bgcolor="LightGrey">
						<th width="5%">
							�
						</th>
						<th width="65%">
							��������
						</th>
						<th width="30%">
							���������, %
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
		<a name="LevelQuiz"></a><b>��� 100 ������������� � ������ �����<br/>
			<a href="/LevelQuiz">�� ����������� �������� �� ������ ����������</a> </b>
		<asp:Repeater ID="rptUsersByLevelQuiz" runat="server" EnableViewState="False">
			<HeaderTemplate>
				<table class="bordertable">
					<tr bgcolor="LightGrey">
						<th width="5%">
							�
						</th>
						<th width="65%">
							��������
						</th>
						<th width="30%">
							���������� �������
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
