<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExerciseSelectEdit.aspx.cs"
    MasterPageFile="~/MasterPage.Master" Inherits="Wordz.Web.ExerciseSelectEdit" %>

<%@ Import Namespace="System.Net" %>

<asp:Content ID="Content2" ContentPlaceHolderID="chpHead" runat="server">
    <script type="text/javascript" language="javascript" src="js/exercises.js"></script>
    <title>Добавление/Изменение упражнения "Выбор"</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphPageCaption" runat="server">
    Добавление/Изменение упражнения "Выбор"
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMiddle" runat="server">
    <%--Скрытые поля с идентификаторами--%>
    <asp:HiddenField runat="server" ID="TextId" />
    <asp:HiddenField runat="server" ID="ModuleId" />
    <asp:HiddenField runat="server" ID="SelectedAnswer" ClientIDMode="AutoID" />
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
                <td>
                    Изображение вопроса
                </td>
            </tr>
            <tr>
                <td>
                    <textarea cols="20" rows="3" name="questionName"><%= WebUtility.HtmlEncode(ExerciseT.Name)%></textarea>
                </td>
                <td>
                    <textarea cols="20" rows="3" name="questionDescription"><%= WebUtility.HtmlEncode(ExerciseT.Description)%></textarea>
                </td>
                <td>
                    <textarea cols="20" rows="3" name="questionText"><%= WebUtility.HtmlEncode(ExerciseT.Text)%></textarea>
                    <img width="50px" height="50px" src="Handlers/LoadPictureHandler.ashx?id=<%= ExerciseT.PictureId %>"
                        alt="">
                    <input type="hidden" name="changeQuestionPicture" value="0" />
                    <input type="button" onclick="deleteQuestionPictureBtnClick();" name="deletePictureBtn"
                        value="Удалить изображение" />
                </td>
                <td>
                    <input type="file" name="questionFile" value="Выберите изображение" accept="image/*"
                        onclick="setQuestionPicture();" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table id="answersTable" style="border: 1px">
            <tr>
                <td>
                    Текст ответа
                </td>
                <td>
                    Изображение ответа
                </td>
                <td>
                    Верность ответа
                </td>
            </tr>
            <%
                for (var i = 0; i < ExerciseT.Answers.ToList().Count(); i++)
                {
            %>
            <tr>
                <td>
                    <input type="hidden" name="answerId_<%=i + 1%>" value="<%=ExerciseT.Answers.ToArray()[i].Id%>" />
                    <textarea cols="20" rows="3" name="text_<%=i + 1%>"><%= WebUtility.HtmlEncode(ExerciseT.Answers.ToArray()[i].Text)%></textarea>
                </td>
                <td>
                    <input type="file" name="myFile_<%=i + 1%>" value="Выберите изображение" accept="image/*"
                        onclick="setPicture(<%=i + 1%>);" />
                    <img width="50px" height="50px" src="Handlers/LoadPictureHandler.ashx?id=<%= ExerciseT.Answers.ToArray()[i].PictureId %>"
                        alt="">
                    <input type="hidden" name="changePicture_<%=i + 1%>" value="0" />
                    <input type="button" onclick="deletePictureBtnClick(<%=i + 1%>);" name="deletePictureBtn"
                        value="Удалить изображение" />
                </td>
                <td>
                    <input type="checkbox" value="Верный ответ" name="isAnswer_<%=i + 1%>" checked="<%=ExerciseT.Answers.ToArray()[i].IsRight%>" />
                    Верный ответ
                </td>
            </tr>
            <%
                }
            %>
        </table>
        <%--Кнопки действий над упражнением--%>
        <asp:Panel ID="ButtonPanel" runat="server" HorizontalAlign="Right">
            <input type="button" id="addAnswerBtn" value="Добавить новый ответ" />
            <input type="button" id="deleteAnswerBtn" value="Удалить ответ" />
            <asp:Button ID="submitToServerBtn" runat="server" Text="Сохранить" />
        </asp:Panel>
    </div>
</asp:Content>
