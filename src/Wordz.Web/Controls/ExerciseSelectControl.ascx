<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExerciseSelectControl.ascx.cs"
    Inherits="Wordz.Web.Controls.ExerciseSelectControl" %>
<%@ Import Namespace="Wordz.Web.Helpers" %>
<%@ Import Namespace="System.Net" %>
<%--<div>--%>
    <div id="exerciseSelectTitle" runat="server">
        Упражнение "Выбор"
    </div>
 <%--   <b />
    <input type="hidden" id="selectExerciseId" name="selectExercise" />
    <input type="hidden" id="selectExerciseModuleId" name="selectExerciseModule" />
    <input type="hidden" id="selectedAnswerId" name="selectExerciseModule" clientidmode="AutoID" />
    <div id="selectExerciseNameDiv" runat="server">
        Название <b />
        <textarea id="selectExerciseQuestionNameId" name="selectExerciseuestionName" cols="50" rows="2"></textarea>
    </div>
    <div id="Content4" runat="server">
        Описание <b />
        <textarea id="selectExerciseQuestionDescriptionId" name="selectExerciseQuestionDescription" cols="50"
            rows="2"></textarea>
    </div>
    <div id="Content1" runat="server">
        Текст вопроса <b />
        <textarea id="selectExerciseQuestionTextId" name="selectExerciseQuestionText" rows="10" cols="50"></textarea>
    </div>
    <div id="Div1" runat="server">
        Изображение вопроса <b />
        <textarea id="selectExerciseQuestionPictureId" name="selectExerciseQuestionPicture" rows="10" cols="50"></textarea>
    </div>
    <b />
    <div style="padding: 40px auto; text-align: right">
        <%= RenderHelper.Button("exerciseSelectSaveBtn", "Сохранить", null, "display: inline")%>
        <%= RenderHelper.Button("exerciseSelectCancelBtn", "Закрыть", null, "display: inline")%>
    </div>
</div>

    <%--Скрытые поля с идентификаторами--%>
<%--    <asp:HiddenField runat="server" ID="SelectTextId" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="SelectModuleId" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="SelectedAnswer" ClientIDMode="Static" />--%>

    <input type="hidden" id="SelectTextId" name="SelectText" />
    <input type="hidden" id="SelectModuleId" name="SelectModule"/>

    <div>
        <table id="questionTable" style="border: 1px">
            <tr>
                <td>
                    Название
                </td>
                <td>
                    Описание
                </td>
                <td>
                    Текст вопроса
                </td>
<%--                <td>
                    Изображение вопроса
                </td>--%>
            </tr>
            <tr>
                <td>
                    <textarea cols="20" rows="3" name="questionName" id="questionNameId"></textarea>
                </td>
                <td>
                    <textarea cols="20" rows="3" name="questionDescription" id="questionDescriptionId"></textarea>
                </td>
                <td>
                    <textarea cols="20" rows="3" name="questionText" id="questionTextId"></textarea>
<%--                    <img width="50px" height="50px" src="Handlers/LoadPictureHandler.ashx?id=<%= ExerciseT.PictureId %>"
                        alt="">
                    <input type="hidden" name="changeQuestionPicture" value="0" />
                    <input type="button" onclick="deleteQuestionPictureBtnClick();" name="deletePictureBtn"
                        value="Удалить изображение" />--%>
                </td>
<%--                <td>
                    <input type="file" name="questionFile" value="Выберите изображение" accept="image/*"
                        onclick="setQuestionPicture();" />
                </td>--%>
            </tr>
        </table>
    </div>
    <div>
        <table id="answersTable" name="answersTable" style="border: 1px">
            <tr>
                <td>
                    Текст ответа
                </td>
<%--                <td>
                    Изображение ответа
                </td>--%>
                <td>
                    Верность ответа
                </td>
            </tr>
<%--            <%
                for (var i = 0; i < ExerciseT.Answers.ToList().Count(); i++)
                {
            %>--%>
<%--            <tr>
                <td>
                    <input type="hidden" name="answerId_<%=i + 1%>" value="<%=ExerciseT.Answers.ToArray()[i].Id%>" />
                    <textarea cols="20" rows="3" name="text_<%=i + 1%>"><%= WebUtility.HtmlEncode(ExerciseT.Answers.ToArray()[i].Text)%></textarea>
                </td>--%>
<%--                <td>
                    <input type="file" name="myFile_<%=i + 1%>" value="Выберите изображение" accept="image/*"
                        onclick="setPicture(<%=i + 1%>);" />
                    <img width="50px" height="50px" src="Handlers/LoadPictureHandler.ashx?id=<%= ExerciseT.Answers.ToArray()[i].PictureId %>"
                        alt="">
                    <input type="hidden" name="changePicture_<%=i + 1%>" value="0" />
                    <input type="button" onclick="deletePictureBtnClick(<%=i + 1%>);" name="deletePictureBtn"
                        value="Удалить изображение" />
                </td>--%>
<%--                <td>
                    <input type="checkbox" value="Верный ответ" name="isAnswer_<%=i + 1%>" checked="<%=ExerciseT.Answers.ToArray()[i].IsRight%>" />
                    Верный ответ
                </td>
            </tr>--%>
<%--            <%
                }
            %>--%>
        </table>
        <%--Кнопки действий над упражнением--%>
       <asp:Panel ID="SelectButtonPanel" runat="server" HorizontalAlign="Right">
            <input type="button" id="addAnswerBtn"  value="Добавить новый ответ" />
            <input type="button" id="deleteAnswerBtn" value="Удалить ответ" />
            <input type="button" id="exerciseSelectSaveBtn" value="Сохранить" />
<%--        <asp:Button ID="submitExerciseSelectToServerBtn" runat="server" Text="Сохранить" />--%>
        </asp:Panel>
    </div>
