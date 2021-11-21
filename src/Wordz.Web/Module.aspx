<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    EnableViewState="false" CodeBehind="Module.aspx.cs" Inherits="Wordz.Web.Module" %>

<%@ Import Namespace="Wordz.Web.Helpers" %>
<%@ Import Namespace="System.Net" %>
<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register TagPrefix="a" TagName="ExerciseTextControl" Src="~/Controls/ExerciseTextControl.ascx" %>
<%@ Register TagPrefix="a" TagName="ExerciseSelectControl" Src="~/Controls/ExerciseSelectControl.ascx" %>
<%@ Register TagPrefix="a" TagName="ExerciseAnswerTextControl" Src="~/Controls/ExerciseAnswerTextControl.ascx" %>
<%@ Register TagPrefix="a" TagName="ExerciseSkipTextControl" Src="~/Controls/ExerciseSkipTextControl.ascx" %>
<%@ Register TagPrefix="a" TagName="ExerciseSelectTextControl" Src="~/Controls/ExerciseSelectTextControl.ascx" %>
<%@ Register Src="Controls/UserComments.ascx" TagName="UserComments" TagPrefix="uc" %>

<asp:Content ContentPlaceHolderID="chpHead" runat="server">
    <title id="lblModuleTitle" runat="server" ></title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
    <asp:Label ID="lblModuleName" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
    <div class="descriptionValue" style="padding-top: 10px; text-align: justify">
        <asp:Label ID="lblModuleDescription" runat="server" />
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
    <script src="/js/wordz.module.js" type="text/javascript"> </script>
    <script src="/js/wordz.module.exercises.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/wordz.exercise_select.js"></script>
    <script type="text/javascript" src="/js/wordz.exercise_answer_text.js"></script>
    <script type="text/javascript" src="/js/wordz.exercise_skip_text.js"></script>
    <script type="text/javascript" src="/js/wordz.exercises_select_text.js"></script>
    <script type="text/javascript" src="/js/wordz.exercise_text.js"></script>
    <script type="text/javascript" src="/js/wordz.comments.js"></script>
    <script type="text/javascript" src="/js/tinymce/tinymce.min.js"></script>
    <div style="float: left">
        <div style="padding-left: 35px">
            <a class="button" id="backUrlBtn" runat="server" clientidmode="Static">Вернуться назад</a>
        </div>
        <div style="padding-left: 35px">
            <asp:Image ID="moduleImage" Width="100px" Height="100px" runat="server" />
        </div>
        <div id="editButtonPanel" style="padding: 10px 5px 10px 25px" runat="server">
        </div>
        <div id="rating" style="float: left">
            <input type="hidden" name="val" value="<%= TheModule != null ? WebUtility.HtmlEncode(TheModule.GetRateByCurrentUserString()) : "0" %>" />
            <input type="hidden" name="target_element" value="<%= TheModule != null ? WebUtility.HtmlEncode(TheModule.Number.ToString()) : "" %>" />
            <input type="hidden" name="cat_id" value="2" />
        </div>
    </div>
    <div style="padding-left: 15px; width: 100%">
        <table class="descriptionTable">
            <tr>
                <td class="descriptionItem">
                    Тэги:
                </td>
                <td class="descriptionValue">
                    <asp:Label ID="lblModuleTags" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="descriptionItem">
                    Ссылки:
                </td>
                <td class="descriptionValue">
                    <asp:Label ID="lblModuleLinks" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="descriptionItem">
                    Цена:
                </td>
                <td class="descriptionValue">
                    <asp:Label ID="lblModulePrice" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="descriptionItem">
                    URL:
                </td>
                <td class="descriptionValue">
                    <asp:Label ID="lblModuleUrl" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="detailedDescriptionValue">
                        <asp:Label ID="lblModuleDetailedDescription" runat="server" />
                    </div>
                </td>
            </tr>
        </table>
        <div style="padding-left: 179px; padding-top: 8px; font-family: Tahoma;">
            <span>Упражнения данного модуля:</span>
            <div id="lblModuleExerciseDiv" runat="server">
                <ul>
                    <asp:Repeater ID="moduleExerciseRepeater" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href="<%# DataBinder.Eval(Container.DataItem, "Link") %>"><%# DataBinder.Eval(Container.DataItem, "Name") %></a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <uc:UserComments ID="UserComments1" runat="server" Visible="true" />
        </div>
        
    </div>
    
    <div id="moduleEditDivContainer" class="box-modal" style="display: none; width: 105%">
        <div id="moduleEditDiv" style="width:auto; overflow:auto">
            <div id="validationListContainer" class="validationContainer" style="display: none">
                        <ul></ul>
            </div>
            <div id="waitBlock" style="text-align: center; display: none"><img alt="" src="/img/ajax-loader-big.gif" /></div>
            <div id="inputsBlockContainer">
                <div id="inputsBlock">
                    <input type="hidden" id="moduleIdField" name="moduleId" clientidmode="Static" runat="server" />
                    <input type="hidden" id="courseIdField" name="courseId" clientidmode="Static" runat="server" />
                    <div class="editItem">
                        Название:</div>
                    <div class="editValue">
                        <textarea class="editMemo required" name="name" id="nameId" cols="50" rows="5"></textarea>
                    </div>
                    <div class="editItem">
                        Описание:</div>
                    <div class="editValue">
                        <textarea class="editMemo" name="description" id="descriptionId" cols="50" rows="5"></textarea>
                    </div>
                    <div class="editItem">
                        Подробное описание:</div>
                    <div class="editValue">
                        <textarea class="editMemo" name="detailDescription" id="detailDescriptionId" cols="50"
                            rows="5"></textarea>
                    </div>
                    <div class="editItem">
                        Цена:</div>
                    <div class="editValue">
                        <input class="editText required" type="text" name="price" id="priceId" value="0.0" />
                    </div>
                    <div class="editItem">
                        Валюта:</div>
                    <div class="editValue">
                        <select name="currency" id="currencyId">
                        </select>
                    </div>
                    <div class="editItem">
                        Контакты:</div>
                    <div class="editValue">
                        <textarea class="editMemo" name="contacts" id="contactsId" cols="50" rows="5"></textarea>
                    </div>
                    <div class="editItem">
                        Теги:</div>
                    <div class="editValue">
                        <textarea class="editMemo" name="tags" id="tagsId" cols="50" rows="5"></textarea>
                    </div>
                    <div class="editItem">
                        Ссылки:</div>
                    <div class="editValue">
                        <textarea class="editMemo" name="links" id="linksId" cols="50" rows="5"></textarea>
                    </div>
                    <div class="editItem">
                        Url:</div>
                    <div class="editValue">
                        <input class="editText" type="text" name="url" id="urlId" />
                    </div>
                    <div class="editItem">
                        <label for="isRemovePictureId">Удалить логотип: </label>
                        <input type="checkbox" name="isRemovePicture" id="isRemovePictureId" value="1" />
                    </div>
                    <div class="editItem">
                        Логотип:</div>
                    <div class="editValue">
                        <input type="file" name="picture" id="pictureId" value="Выберите изображение" accept="image/*" />
                    </div>
                    <div class="editItem">
                        Упражнения:</div>
                    <div class="editValue" runat="server" id="lblModuleEditExerciseDiv" clientidmode="Static">
                    </div>
                    <br />
                    <div>
                        Добавить упражнение:
                        <select id="addNewExerciseSelect" onchange="AddNewExercise(this.selectedIndex);">
                            <option>Выбрать тип </option>
                            <option>Текст</option>
                            <option>Выбор</option>
                            <option>Текстовый ответ</option>
                            <option>Выбор в тексте</option>
                            <option>Пропуски в тексте</option>
                        </select>
                    </div>
                </div>
            </div>
            <br />
            <div style="padding: 40px auto; text-align: right" runat="server" id="editButtonsContainer">
            </div>
        </div>
    </div>
    <asp:Literal ID="isNewModule" runat="server" Visible="false" />
    <div id="ExerciseTextContrDiv" class="box-modal" style="display: none; width: auto">
        <a:ExerciseTextControl ID="ExerciseTextContr" name="exercise_text_control" ClientIDMode="Static"
            runat="server"></a:ExerciseTextControl>
    </div>
    <div id="ExerciseSelectContrDiv" class="box-modal" style="display: none; width: auto">    
       <a:ExerciseSelectControl ID="ExerciseSelectContr" name="exercise_select_control" ClientIDMode="Static"
            runat="server"></a:ExerciseSelectControl>
    </div>
    <div id="ExerciseAnswerTextContrDiv" class="box-modal" style="display: none; width: auto">    
       <a:ExerciseAnswerTextControl ID="ExerciseAnswerTextContriol" name="exercise_answer_text_control" ClientIDMode="Static"
            runat="server"></a:ExerciseAnswerTextControl>
    </div>
    <div id="ExerciseSkipTextContrDiv" class="box-modal" style="display: none; width: auto">    
       <a:ExerciseSkipTextControl ID="ExerciseSkipTextContriol" name="exercise_skip_text_control" ClientIDMode="Static"
            runat="server"></a:ExerciseSkipTextControl>
    </div>
    <div id="ExerciseSelectTextContrDiv" class="box-modal" style="display: none; width: auto">    
       <a:ExerciseSelectTextControl ID="ExerciseSelectTextContriol" name="exercise_select_text_control" ClientIDMode="Static"
            runat="server"></a:ExerciseSelectTextControl>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            
            BaseModulePageUrl = '/Module/';
            DefaultCourceLanguageId = <%= ConfigurationManager.AppSettings["DefaultCourceLanguageId"]  %>;
            
            initializeModuleEditForm();
            initializePage();
        });
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
    <uc:Advert runat="server" Ad="7" />
</asp:Content>
