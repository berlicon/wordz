<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExerciseAnswerTextControl.ascx.cs"
    Inherits="Wordz.Web.Controls.ExerciseAnswerTextControl" %>
<%@ Import Namespace="Wordz.Web.Helpers" %>
<div id="exerciseAnswerTextEditDivContainer" class="box-modal" style="width: auto">
    <div id="exerciseAnswerTextEditDiv" style="width: auto">
        <div id="exerciseAnswerTextTitle" runat="server">
            Упражнение "Текстовый ответ"
        </div>
        <input type="hidden" id="editExerciseAnswerTextId" name="editExerciseAnswerText" />
        <input type="hidden" id="editExerciseAnswerTextModuleId" name="editExerciseAnswerTextModule" />
        <div class="editItem">
            Название:
        </div>
        <div class="editValue">
            <textarea class="editMemo" name="exerciseAnswerTextName" id="exerciseAnswerTextNameId"
                cols="50" rows="3"></textarea>
        </div>
        <div class="editItem">
            Описание:</div>
        <div class="editValue">
            <textarea class="editMemo" name="exerciseAnswerTextDescription" id="exerciseAnswerTextDescriptionId"
                cols="50" rows="3"></textarea>
        </div>
        <div class="editItem">
            Текст вопроса:</div>
        <div class="editValue">
            <textarea class="editMemo" name="exerciseAnswerTextQuestionText" id="exerciseAnswerTextQuestionTextId"
                cols="50" rows="5"></textarea>
        </div>
        <div style="padding: 40px auto; text-align: right">
            <%= RenderHelper.Button("exerciseAnswerTextSaveBtn", "Сохранить", null, "display: inline")%>
            &nbsp;&nbsp;&nbsp;
            <%= RenderHelper.Button("exerciseAnswerTextCancelBtn", "Отмена", null, "display: inline")%>
        </div>
    </div>
</div>
