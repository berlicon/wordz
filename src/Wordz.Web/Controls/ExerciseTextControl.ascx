<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExerciseTextControl.ascx.cs"
    Inherits="Wordz.Web.Controls.ExerciseTextControl" %>
<%@ Import Namespace="Wordz.Web.Helpers" %>
<div>
    <script type="text/javascript" src="../js/jHtmlArea-0.7.5.min.js"></script>

    <textarea runat="server" id="txtText" cols="50" rows="15"></textarea>
    <input type="submit" value='manual submit' />
    <br />

    <div id="exerciseTextTitle" runat="server">
        Упражнение "Текст"
    </div>
    <b />
    <input type="hidden" id="textExerciseId" name="textExercise" />
    <input type="hidden" id="textExerciseModuleId" name="textExerciseModule" />
    <div id="textExerciseNameDiv" runat="server">
        <textarea id="textExerciseNameId" name="textExerciseName" cols="50" rows="2"></textarea>
    </div>
    <div id="Content4" runat="server">
        <textarea id="textExerciseDescriptionId" name="textExerciseDescription" cols="50"
            rows="2"></textarea>
    </div>
    <div id="Content1" runat="server">
        <textarea id="textExerciseTextId" name="textExerciseText" rows="10" cols="50"></textarea>
    </div>
    <b />
    <div style="padding: 40px auto; text-align: right">
        <%= RenderHelper.Button("exerciseTextSaveBtn", "Сохранить", null, "display: inline")%>
        <%= RenderHelper.Button("exerciseTextCancelBtn", "Закрыть", null, "display: inline")%>
    </div>
</div>
