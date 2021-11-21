<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExerciseSkipTextControl.ascx.cs"
    Inherits="Wordz.Web.Controls.ExerciseSkipTextControl" %>
<%@ Import Namespace="Wordz.Web.Helpers" %>
<div id="exerciseSkipTextEditDivContainer" class="box-modal" style="width: auto">
    <div id="exerciseSkipTextEditDiv" style="width: auto">
        <input type="hidden" name="exerciseSkipTextExercise" id="exerciseSkipTextExerciseId" />
        <input type="hidden" name="exerciseSkipTextExerciseModule" id="exerciseSkipTextExerciseModuleId" />
        <div class="editItem">
            Название:
        </div>
        <div class="editValue">
            <textarea class="editMemo" name="exerciseSkipTextName" id="exerciseSkipTextNameId"
                cols="50" rows="3"></textarea>
        </div>
        <div class="editItem">
            Описание:</div>
        <div class="editValue">
            <textarea class="editMemo" name="exerciseSkipTextDescription" id="exerciseSkipTextDescriptionId"
                cols="50" rows="3"></textarea>
        </div>
        <div class="editItem">
            Текст:</div>
        <div class="editValue">
            <textarea class="editMemo" name="exerciseSkipTextQuestionText" id="exerciseSkipTextQuestionTextId"
                cols="50" rows="5"></textarea>
        </div>
        <div style="padding: 40px auto; text-align: right">
            <%= RenderHelper.Button("exerciseSkipTextSaveBtn", "Сохранить", null, "display: inline")%>
            &nbsp;&nbsp;&nbsp;
            <%= RenderHelper.Button("exerciseSkipTextCancelBtn", "Отмена", null, "display: inline")%>
        </div>
    </div>
</div>
