<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="AddWords.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.AddWords" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Добавление слов в личный словарь</title>
	<meta name="keywords" content="английский обучение добавление слов личный словарь">
	<meta name="description" content="Отметка известных слов и добавление их в Ваш личный словарь.">
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Добавление слов в личный словарь
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Отметка известных слов и добавление их в Ваш личный словарь
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<uc:Progress ID="ctlProgress" runat="server" />
	<table width="100%" height="100%">
		<tr>
			<td width="40%" height="1%">
				&nbsp;
			</td>
			<td>
				Отметьте известные слова:&nbsp;
				<asp:CheckBox ID="chkCheckAll" runat="server" Text="Все" title="Отметить все слова в списке" />&nbsp;&nbsp;
				<a id="btnAdd" runat="server" href="#" class="button" title="Добавить выбранные слова в личный словарь">
					Добавить в словарь</a>
			</td>
		</tr>
		<tr>
			<td width="40%" valign="top">
				<p>
					Загрузить слова:<br/>
					<asp:RadioButton ID="rbLoadFromGlobalDictionary" runat="server" Text="из словаря"
						GroupName="LoadSource" Checked="True" title="Загрузить набор слов из глобального словаря" />
					<asp:RadioButton ID="rbLoadFromProcessPage" runat="server" Text="обработанные ранее"
						GroupName="LoadSource" title="Загрузить предварительно обработанный на других страницах набор слов" />
				</p>
				<p>
					Выбрать слова:<br/>
					<asp:RadioButton ID="rbSelectForDomain" runat="server" Text="по теме:" GroupName="SelectSource"
						Checked="True" title="Загрузить набор слов по заданной теме" />
					<asp:DropDownList ID="ddlDomain" runat="server" title="Загрузить набор слов по заданной теме" />
					<br/>
					<asp:RadioButton ID="rbSelectRandom" runat="server" Text="случайным образом" GroupName="SelectSource"
						title="Загрузить случайный набор слов из глобального словаря" /><br/>
					<asp:RadioButton ID="rbSelectOrdered" runat="server" Text="упорядоченные по алфавиту"
						GroupName="SelectSource" title="Загрузить упорядоченный набор слов из глобального словаря" />
				</p>
				<p>
					<table class="nobordertable">
						<tr>
							<td>
								Количество слов:
							</td>
							<td>
								<asp:TextBox ID="txtWordCount" runat="server" Text="10" CssClass="numerictextbox"
									title="Количество загружаемых слов" />
							</td>
						</tr>
						<tr id="trNumber">
							<td>
								Начиная с номера:
							</td>
							<td>
								<asp:TextBox ID="txtWordStartIndex" runat="server" Text="1" CssClass="numerictextbox"
									title="Номер по порядку, с которого начинать загружать слова" />
							</td>
						</tr>
					</table>
				</p>
				<br/>
				<hr>
				<div align="center">
					<a id="btnLoad" runat="server" href="#" class="button" title="Загрузить требуемый набор слов">
						Загрузить</a> <a id="btnBackToProcess" runat="server" class="button" title="Вернуться к странице обработки текста">
							Вернуться к обработке слов</a>
				</div>
			</td>
			<td>
				<div id="cklWords" runat="server" class="divWords" title="Панель со списком слов для добавления в личный словарь" />
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
