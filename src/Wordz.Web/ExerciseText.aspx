<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master"
    CodeBehind="ExerciseText.aspx.cs" Inherits="Wordz.Web.ExerciseText" %>

<%@ Import Namespace="System.Net" %>
<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="chpHead" runat="server">
    <title>Упражнение "Текст"</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphPageCaption" runat="server">
    "<%= WebUtility.HtmlEncode(ExerciseT != null ? ExerciseT.Name : "Название")%>"
    <asp:HiddenField runat="server" ID="ModuleId" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphPageDescription" runat="server">
    "<%= WebUtility.HtmlEncode(ExerciseT != null ? ExerciseT.Description : "Описание")%>"
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
    <div class="box-modal">
        <asp:TextBox runat="server" ID="TextBlock" Width="100%" ReadOnly="true" Wrap="true"
            TextMode="MultiLine" Height="200"></asp:TextBox>
        <br />
        <asp:Panel runat="server" ID="NavigationActionBtns" HorizontalAlign="Center">
            <asp:Button runat="server" ID="OkExerciseBtn" ClientIDMode="Static" Text="Ok" OnClick="OnOkClick" />
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
    <uc:Advert runat="server" Ad="7" />
</asp:Content>