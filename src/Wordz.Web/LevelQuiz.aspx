<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="LevelQuiz.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.LevelQuiz" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>������� �� ������� ������ ����������</title>
	<meta name="keywords" content="���������� ������� Beginner Pre Upper Intermediate Advanced ������� ��������� ������������ ����" />
	<meta name="description" content="�� ������ ������������� � ���� ���� ������� ������ ����������� �����" />
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	������� �� ������� ������ ����������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	�� ������ ������������� � ���� ���� ������� ������ ����������� �����
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<uc:Progress ID="ctlProgress" runat="server" />
	<input id="hdnStep" runat="server" type="hidden" value="0" name="hdnStep" />
	<input id="hdnSuccessCount" runat="server" type="hidden" value="0" name="hdnSuccessCount" />
	<input id="hdnErrorInfo" runat="server" type="hidden" value="" name="hdnErrorInfo" />
	<table class="maintable">
		<tr>
			<td valign="middle" align="center" height="99%" id="tdExam" runat="server">
				��� ����� ����� ���� �� <b>
					<asp:Literal ID="litTestsCount" runat="server" /></b> �������� ��������� �����������.<br />
				<br />
				�� ������ �������� ����������� �����/����� ������ [...].<br />
				�� ������ ������������� ��������� ������� ��� ������ ��� �������.
				<br />
				<br />
				������ 100 ���������� ��������� ������� � <a href="/Statistics#LevelQuiz" class="button">
					������� ��������</a>. ���� �������� �������� ��������������� �� �����, �� �
				������� ����� ������� ��� ���, ��� �������������������� ���������� ����������� ������
				���, ��� �� �������� ��������.
				<br />
				<br />
				<br />
				<a id="btnStartExam" runat="server" href="#" class="button" title="���������� � ������������ � ��������">
					������ ����!</a>
			</td>
		</tr>
		<tr>
			<td>
				<table id="tblButtons" runat="server" class="nobordertable" width="100%" style="display: none">
					<tr>
						<td>
							<a id="btnNext" runat="server" href="#" class="button" title="������� � ���������� �������">
								��������� ������</a><br />
							<br />
						</td>
						<td align="right">
							<a id="btnAbortExam" runat="server" href="#" class="button" title="������������� �������� �������">
								�������� �������</a> <a id="btnStartExamAgain" runat="server" href="#" class="button"
									title="������ ������ ����">��������� �������</a><br />
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
