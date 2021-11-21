<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExerciseSelect.aspx.cs"
    MasterPageFile="~/MasterPage.Master" Inherits="Wordz.Web.ExerciseSelect" %>

<%@ Import Namespace="System.Net" %>
<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="chpHead" runat="server">
    <title>Упражнение "Выбор"</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphPageCaption" runat="server">
    Упражнение "<%= WebUtility.HtmlEncode(ExerciseT != null ? ExerciseT.Name : "Название")%>"
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphPageDescription" runat="server">
    "<%= WebUtility.HtmlEncode(ExerciseT != null ? ExerciseT.Description : "Описание")%>"
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMiddle" runat="server">
    <%--Скрытые поля с идентификаторами--%>
    <asp:HiddenField runat="server" ID="TextId" />
    <asp:HiddenField runat="server" ID="ModuleId" />
    <asp:HiddenField runat="server" ID="SelectedAnswer" ClientIDMode="AutoID" />
    <div class="box-modal">
        <table>
            <tr>
                <td>
                    Вопрос:
                </td>
                <td>
                    <%= WebUtility.HtmlEncode(ExerciseT.Text)%>
                </td>
                <td>
                    <img src="Handlers/LoadPictureHandler.ashx?id=<%= ExerciseT.PictureId %>" alt="">
                </td>
            </tr>
        </table>
        Ответ:
        <br />
        <%
            foreach (var answer in ExerciseT.Answers)
            {
        %>
        <input type="radio" onclick="Applealert(<%=answer.Id%>);" id="radioSelect" name="group1"><%=WebUtility.HtmlEncode(answer.Text)%><img
            width="100px" src="Handlers/LoadPictureHandler.ashx?id=<%= answer.PictureId %>" alt=""><br>
        <%
            }
        %>
        <%--Кнопки действий над упражнением--%>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Right">
            <asp:Button runat="server" ID="OkExerciseBtn" ClientIDMode="Static" Text="ОК" OnClick="OnOkClick" />
            <asp:Button ID="CheckButton" Text="Проверить" runat="server" OnClick="OnCheckButtonClick" />
        </asp:Panel>
    </div>
    <script type="text/jscript" language="jscript">
        function Applealert(id) {
            $("#<%=SelectedAnswer.ClientID%>").val(id);
        }
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
    <uc:Advert runat="server" Ad="7" />
</asp:Content>