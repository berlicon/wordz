<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master"
    CodeBehind="ExerciseTextAnswer.aspx.cs" Inherits="Wordz.Web.ExerciseTextAnswer" %>

<%@ Import Namespace="Wordz.Web.Helpers" %>
<%@ Import Namespace="System.Net" %>
<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="chpHead" runat="server">
    <title>Упражнение "Текстовый ответ"</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphPageCaption" runat="server">
    "<%= WebUtility.HtmlEncode(ExerciseText != null ? ExerciseText.Name : "Название")%>"
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphPageDescription" runat="server">
    "<%= WebUtility.HtmlEncode(ExerciseText != null ? ExerciseText.Description : "Описание")%>"
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cphMiddle" ID="mainContent">
    <asp:HiddenField runat="server" ID="ModuleId" ClientIDMode="Static" />
    <script type="text/javascript" src="/js/wordz.exercises_text_answer_do.js"></script>

    <%--форма выполнения упражнения--%>
    <div id="exerciseDoDivContainer" class="box-modal" style="width: auto">
        <div id="exerciseDoDiv" style="width: 100;">
            <input type="hidden" name="doAnswerId" value="<%= Answer.Id %>" />
            <input type="hidden" name="doExerciseId" value="<%= ExerciseText.Id %>" />
            <input type="hidden" name="mark" value="<%= Answer.Mark %>" />
            Название: <%= ExerciseText.Name %>
            <br />
            Описание: <%= ExerciseText.Description %>
            <br />
            Вопрос: <%= ExerciseText.Text %>
            <br />
            <div class="editValue">
                Ответ:
                <br />
                <textarea class="editMemo" name="answer" id="answerId" cols="50" rows="5"><%= Answer.Text %></textarea>
            </div>
            <div style="padding: 40px auto; text-align: right">
                <%= RenderHelper.Button("exerciseCheckBtn", "Отправить на проверку", null, "display: inline")%>
                &nbsp;&nbsp;&nbsp;
                <%= RenderHelper.Button("exerciseDoCancelBtn", "Отмена", null, "display: inline")%>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
    <uc:Advert runat="server" Ad="7" />
</asp:Content>
