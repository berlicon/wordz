<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master"
    CodeBehind="ExerciseTextEdit.aspx.cs" Inherits="Wordz.Web.ExerciseTextEdit" %>
<%@ Import Namespace="System.Net" %>

<asp:Content ID="Content2" ContentPlaceHolderID="chpHead" runat="server">

    <title>Упражнение "Текст"</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphPageCaption" runat="server">
    "<%= WebUtility.HtmlEncode(ExerciseT != null ? ExerciseT.Name : "Название")%>"
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphPageDescription" runat="server">
    "<%= WebUtility.HtmlEncode(ExerciseT != null ? ExerciseT.Description : "Описание")%>"
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMiddle" runat="server">

<%--Скрытые поля с идентификаторами--%>
    <asp:HiddenField runat="server" ID="TextId" />
    <asp:HiddenField runat="server" ID="ModuleId" />

<%--Таблица с полями упражнения--%>
    <asp:Table runat="server" ID="ExerciseEditTable" Width="100%">
        <asp:TableRow>
            <asp:TableHeaderCell Width="100"> Название </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="NameText" MaxLength="200" Width="100%" TextMode="SingleLine">
                </asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell Width="100">Описание</asp:TableHeaderCell>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="DescriptionText" Width="100%" TextMode="MultiLine">
                </asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell Width="100"> Текст </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:TextBox runat="server" ID="TextText" Width="100%" TextMode="MultiLine">
                </asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

<%--Кнопки действий над упражнением--%>
    <asp:Panel runat="server" HorizontalAlign="Right">
        <asp:Button ID="SaveButton" Text="Сохранить" runat="server" OnClick="OnSaveClick" />
        <asp:Button ID="DeleteButton" Text="Удалить" runat="server" OnClick="OnDeleteClick" />
    </asp:Panel>

</asp:Content>
