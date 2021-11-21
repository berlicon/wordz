<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExerciseSelectText.aspx.cs"
    MasterPageFile="~/MasterPage.Master" Inherits="Wordz.Web.ExerciseSelectText" %>

<%@ Import Namespace="Wordz.Web.Helpers" %>
<%@ Import Namespace="System.Net" %>
<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="chpHead" runat="server">
    <title>Упражнение "Текстовый ответ"</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphPageCaption" runat="server">
    "<%= WebUtility.HtmlEncode(Exercise != null ? Exercise.Name : "Название")%>"
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphPageDescription" runat="server">
    "<%= WebUtility.HtmlEncode(Exercise != null ? Exercise.Description : "Описание")%>"
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cphMiddle" ID="mainContent">
    <script type="text/javascript" language="javascript" src="/js/wordz.exercises_select_text_do.js"></script>
    <asp:HiddenField runat="server" ID="ModuleId" ClientIDMode="Static" />
    <%--форма выполнения упражнения--%>
    <div id="exerciseDoDivContainer" class="box-modal" style="width: auto">
        <div id="exerciseDoDiv" style="width: 100;">
            <input type="hidden" name="doExerciseId" value="<%= Exercise.Id %>" />
            <%= ExerciseText %>
            <br />
            <br />
            <div style="padding: 40px auto; text-align: right">
                <%= RenderHelper.Button("exerciseCheckBtn", "Проверить", null, "display: inline")%>
                &nbsp;&nbsp;&nbsp;
                <%= RenderHelper.Button("exerciseDoCancelBtn", "ОК", null, "display: inline")%>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
    <uc:Advert runat="server" Ad="7" />
</asp:Content>
