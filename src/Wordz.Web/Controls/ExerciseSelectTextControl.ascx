<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExerciseSelectTextControl.ascx.cs"
    Inherits="Wordz.Web.Controls.ExerciseSelectTextControl" %>
<%@ Import Namespace="Wordz.Web.Helpers" %>

<div id="exerciseSelectTextEditDivContainer" class="box-modal" style="width: auto">
    <div id="exerciseSelectTextEditDiv" style="width: auto">
        <input type="hidden" name="exerciseSelectTextExercise" id="exerciseSelectTextExerciseId" />
        <input type="hidden" name="exerciseSelectTextExerciseModule" id="exerciseSelectTextExerciseModuleId" />
        <div class="editItem">
            Название:
        </div>
        <div class="editValue">
            <textarea class="editMemo" name="exerciseSelectTextName" id="exerciseSelectTextNameId"
                cols="50" rows="3"></textarea>
        </div>
        <div class="editItem">
            Описание:</div>
        <div class="editValue">
            <textarea class="editMemo" name="exerciseSelectTextDescription" id="exerciseSelectTextDescriptionId"
                cols="50" rows="3"></textarea>
        </div>
        <div class="editItem">
            Текст:</div>
        <div class="editValue">
            <textarea class="editMemo" name="exerciseSelectTextQuestionText" id="exerciseSelectTextQuestionTextId"
                cols="50" rows="5"></textarea>
        </div>
        <div style="padding: 40px auto; text-align: right">
            <%= RenderHelper.Button("exerciseSelectTextSaveBtn", "Сохранить", null, "display: inline")%>
            &nbsp;&nbsp;&nbsp;
            <%= RenderHelper.Button("exerciseSelectTextCancelBtn", "Отмена", null, "display: inline")%>
        </div>
    </div>
</div>
