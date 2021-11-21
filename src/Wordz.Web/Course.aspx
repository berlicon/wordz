<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    EnableViewState="false" CodeBehind="Course.aspx.cs" Inherits="Wordz.Web.Course" %>

<%@ Import Namespace="Wordz.Web" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="Wordz.Web.Helpers" %>
<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<%@ Register Src="Controls/UserComments.ascx" TagName="UserComments" TagPrefix="uc" %>
<%@ Register Src="Controls/ActionButtonControl.ascx" TagName="ActionButton" TagPrefix="uc" %>

<asp:Content ContentPlaceHolderID="chpHead" runat="server">
    <title><%= WebUtility.HtmlEncode(TheCourse.Name) %></title>
    <link rel="Stylesheet" href="css/jquery.cleditor.css" />
    <script type="text/javascript" src="/js/wordz.course.js"></script>
    <script type="text/javascript" src="/js/jquery.cleditor.js"></script>
    <script type="text/javascript" src="/js/wordz.comments.js"></script>
    <script type="text/javascript" src="/js/tinymce/tinymce.min.js"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
    <%= WebUtility.HtmlEncode(TheCourse.Name) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
    <div class="descriptionValue" style="padding-top: 10px; text-align: justify">
        <%= WebUtility.HtmlEncode(TheCourse.Description) %>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">    
    <div id="mainContainer" runat="server">
        <div style="float: left">
            <div style="padding-left: 35px">
                <a class="button" id="backUrlBtn" runat="server" clientidmode="Static">Вернуться назад</a>
            </div>
    <%
                if (TheCourse.PictureId.HasValue)
                {
    %>
            <div style="padding: 3px; padding-left: 35px">
                <img width="100px" height="100px" src="Handlers/LoadPictureHandler.ashx?id=<%= TheCourse.PictureId.Value %>" alt=""/>
            </div>
    <%
                }
    %>
            <div style="padding: 10px 5px 10px 40px">
                <uc:ActionButton ID="payForCourseButton" runat="server" Text="Оплатить курс" Visible="false" />
            </div>
            <div style="padding: 10px 5px 10px 25px" runat="server" id="editBtnDiv" visible="false" />
            <div style="padding: 10px 5px 10px 25px" runat="server" id="approveBtnDiv" visible="false" />
            <div id="rating" style="float: left">
                    <input type="hidden" name="val" value="<%= WebUtility.HtmlEncode(TheCourse.GetRateByCurrentUserString()) %>" />
                    <input type="hidden" name="target_element" value="<%= WebUtility.HtmlEncode(TheCourse.Number.ToString()) %>" />
                    <input type="hidden" name="cat_id" value="2" />
            </div>
        
        </div>
        <div style="padding-left: 15px; width: 100%">
            <table class="descriptionTable">
                <tr>
                    <td class="descriptionItem">
                        Авторы:
                    </td>
                    <td class="descriptionValue">
                        <%= WebUtility.HtmlEncode(TheCourse.Authors) %>
                    </td>
                </tr>
                <tr>
                    <td class="descriptionItem">
                        Категория:
                    </td>
                    <td class="descriptionValue">
                        <%= WebUtility.HtmlEncode(TheCourse.CategoryName) %>
                    </td>
                </tr>
                <tr>
                    <td class="descriptionItem">
                        Язык:
                    </td>
                    <td class="descriptionValue">
                        <%= WebUtility.HtmlEncode(TheCourse.UILanguageName) %>
                    </td>
                </tr>
                <tr>
                    <td class="descriptionItem">
                        Контакты:
                    </td>
                    <td class="descriptionValue">
                        <%= WebUtility.HtmlEncode(TheCourse.Contacts) %>
                    </td>
                </tr>
                <tr>
                    <td class="descriptionItem">
                        Теги:
                    </td>
                    <td class="descriptionValue">
                        <%= WebUtility.HtmlEncode(TheCourse.Tags) %>
                    </td>
                </tr>
                <tr>
                    <td class="descriptionItem">
                        Ссылки:
                    </td>
                    <td class="descriptionValue">
                        <%= TextHelper.GetLinksFromString(WebUtility.HtmlEncode(TheCourse.Links)) %>
                    </td>
                </tr>
                <tr>
                    <td class="descriptionItem">
                        Цена: 
                    </td>
                    <td class="descriptionValue">
                        <%= CourceCalculatedPrice.GetDecimalCaptionJs().HtmlEncode() +  "(" + TheCourse.CurrencyName.HtmlEncode() + ")" %>
                    </td>
                </tr>
                <tr>
                    <td class="descriptionItem">
                        Url: 
                    </td>
                    <td class="descriptionValue">
                        <%= WebUtility.HtmlEncode(TheCourse.Url) %>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="detailedDescriptionValue">
                            <%= TheCourse.DetailedDescription.ReplaceScriptTags() %>
                        </div>
                    </td>
                </tr>
            </table>
        
            <div style="padding-left: 179px; padding-top: 8px; font-family: Tahoma;">
                <span>Модули данного курса:</span>
                <table>
                    <%
                        foreach (var module in TheModules)
                        {
                    %>
                    <tr>
                        <td class="descriptionValue" style="padding: 3px 0;">
                            <div class="listElementLink module_item_container" data-module-url="/Module/<%= module.Id %>" 
                                <%= !module.IsPayd && module.Price > 0 && !TheCourse.IsBuyedByCurrentUser ? "data-module-alert-msg='Модуль не оплачен! Вы сможете перейти в него только после оплаты!'" : ""  %>
                            >
                                <table class="module_item_container_table">
                                <tr>
                                    <td class="module_item_left_panel">
                                        <div>
                                            <img width="70px" src="/LoadPictureHandler.ashx?id=<%= module.PictureId %>" alt=""/>
                                        </div>
                                        <div class="module_item_rating" style="float: left">
                                                <input type="hidden" name="val" value="<%= module.GetTotalRateString() %>" />
                                        </div>
                                    </td>
                                    <td class="module_item_right_panel">
                                        <div class="module_item_name"><%= module.Name.HtmlEncode() %></div>
                                        <div class="module_item_description"><%= module.Description.HtmlEncode() %></div>
                                        <div class="module_item_buttons">
                            
                            <%
                                if (!IsNew && !module.IsPayd && module.Price > 0 && !TheCourse.IsBuyedByCurrentUser)
                                {
                                    %>
                                    <span class="warningText">(модуль не оплачен, стоимость <%= module.Price %> (<%= module.CurrencyName %>))&nbsp;</span>
                                    <%
                                    paymentModuleButton.ConfirmText = "Вы действительно хотите оплатить модуль стоимостью " + module.Price + "(" + module.CurrencyName + ")";
                                    paymentModuleButton.OnClickHandler = "payForModuleClick(" + module.Id + ", " + module.Price.GetDecimalCaptionJs() + ", " + module.CurrencyId  + ");";
                                    paymentModuleButton.Style = "display: inline;";
                                    %>
                                        <uc:ActionButton ID="paymentModuleButton" runat="server" Text="Оплатить модуль" />
                                    <%
                                }
                             %>
                                        </div>
                                    </td>
                                </tr>
                                </table>
                             </div>
                        </td>
                    </tr>
                    <%
                        }
                    %>
                </table>
                <uc:UserComments ID="UserComments1" runat="server" Visible="true" />
            
                <div id="courseEditDivContainer" class="box-modal" style="display: none; width:105%">
                    <div id="courseEditDiv" style="width:auto; overflow:auto" >
                        <div id="validationListContainer" class="validationContainer" style="display: none">
                            <ul></ul>
                        </div>
                        <div id="waitBlock" style="text-align: center; display: none"><img alt="" src="/img/ajax-loader-big.gif" /></div>
                        <div id="inputsBlockContainer">
                            <div id="inputsBlock">
                                <input type="hidden" name="courseId" value="<%= TheCourse.Id %>" />
                                <input type="hidden" name="moduleDeletedListId"/>
                                <div class="editItem">Название:</div>
                                <div class="editValue">
                                    <textarea class="editMemo required" name="name" id="nameId" cols="50" rows="5"></textarea>
                                </div>
                                <div class="editItem">Описание:</div>
                                <div class="editValue">
                                    <textarea class="editMemo" name="description" id="descriptionId" cols="50" rows="5"></textarea>
                                </div>
                                <div class="editItem">Подробное описание:</div>
                                <div class="editValue">
                                    <textarea class="editMemo" name="detailDescription" id="detailDescriptionId" cols="50" rows="5"></textarea>
                                </div>
                                <div class="editItem">Авторы:</div>
                                <div class="editValue">
                                    <textarea class="editMemo" name="authors" id="authorsId" cols="50" rows="5"></textarea>
                                </div>
                                <div class="editItem">Цена:</div>
                                <div class="editValue">
                                    <input class="editText required" type="text" name="price" id="priceId" value="0.0" />
                                </div>                
                                <div class="editItem">Валюта:</div>
                                <div class="editValue">
                                    <select name="currency" id="currencyId"></select>
                                </div>
                                <div class="editItem">Категория:</div>
                                <div class="editValue">
                                    <select name="category" id="categoryId"></select>
                                </div>
                                <div class="editItem">Язык:</div>
                                <div class="editValue">
                                    <select name="language" id="languageId"></select>
                                </div>
                                <div class="editItem">Контакты:</div>
                                <div class="editValue">
                                    <textarea class="editMemo" name="contacts" id="contactsId" cols="50" rows="5"></textarea>
                                </div>
                                <div class="editItem">Теги:</div>
                                <div class="editValue">
                                    <textarea class="editMemo" name="tags" id="tagsId" cols="50" rows="5"></textarea>
                                </div>
                                <div class="editItem">Ссылки:</div>
                                <div class="editValue">
                                    <textarea class="editMemo" name="links" id="linksId" cols="50" rows="5"></textarea>
                                </div>
                                <div class="editItem">Url:</div>
                                <div class="editValue">
                                    <input class="editText" type="text" name="url" id="urlId" />
                                </div>
                                <div class="editItem">
                                    <label for="isRemovePictureId">Удалить логотип:</label><input type="checkbox" name="isRemovePicture" id="isRemovePictureId" value="1" />
                                </div>
                                <div class="editItem">Логотип:</div>
                                <div class="editValue">
                                    <input type="file" name="picture" id="pictureId" value="Выберите изображение" accept="image/*" />
                                </div>
                                <div class="editItem">GoogleAdsense:</div>
                                <div class="editValue">
                                    <textarea class="editMemo" name="adsence" id="adsenceId" cols="50" rows="5"></textarea>
                                </div>
                                <div class="editItem">
                                    <label for="isPublicId">Публичный:</label><input type="checkbox" name="isPublic" id="isPublicId" checked="checked" value="1" />
                                </div>
                                <div class="editItem">
                                    <label for="isEditableId">Разрешено редактирование:</label><input type="checkbox" name="isEditable" id="isEditableId" value="1"/>
                                </div>
                                <div class="editItem">
                                    <label for="isCopyableId">Разрешено копирование:</label><input type="checkbox" name="isCopyable" id="isCopyableId" value="1" />
                                </div>
                                <div class="editItem">Пароль:</div>
                                <div class="editValue">
                                    <input type="text" name="firstPassword" id="firstPasswordId" />
                                </div>
                                <div class="editItem">Модули:</div>
                                <div class="editValue">
                                    <div id="moduleSortableContainerId">
                                        <ul id="moduleSortableList" class="connectedSortable1">
                                        </ul>
                                    </div>
                                    <div id="addNewModuleContainer"></div>
                        
                                </div>
                        
                            </div>
                        </div>
                        <div style="padding: 40px auto; text-align:right">
                                    <%--<%= RenderHelper.Button("courseEditYesBtn", "Сохранить", null, "display: inline") %>--%>
                                    <a class="button" id="courseEditYesBtn">Сохранить</a>
                                    &nbsp;&nbsp;&nbsp;
                                    <a class="button" id="courseEditNoBtn">Отмена</a>
                                    <%--<%= RenderHelper.Button("courseEditNoBtn", "Отмена", null, "display: inline") %>--%>
                        </div>
                    </div>
                </div>
                
            </div>
        </div>
        <script type="text/javascript">
            $(document).ready(function () {
            
                BaseCoursePageUrl = '/Course/';
                DefaultCourceLanguageId = <%= ConfigurationManager.AppSettings["DefaultCourceLanguageId"]  %>;

                initializeCourseEditForm();
                initializePage();
        
                <% if (TheCourse.Id <= 0)
                   {%>
                   editCourse(true);
                <%
                   }%>
            });
        </script>
    </div>
    <div id="errorUserMessageTextContainer" class="box-modal" style="display: none" runat="server" clientidmode="Static" visible="false">
        <div id="errorUserMessageText" class="errorUserMessageText" runat="server" clientidmode="Static" >
        </div>
        <div id="errorScript" runat="server">
            <script type="text/javascript">
                $(document).ready(errorUserMessageInitMethod);
            </script>
        </div>
    </div>

    <div id="errorIncorrectPasswordMessageContainer" class="box-modal" style="display: none; width: 300px;" runat="server" clientidmode="Static" visible="false">
        <div id="errorIncorrectPasswordMessageText" class="errorUserMessageText" runat="server" clientidmode="Static" >
        </div>
        <div id="incorrectPasswordRuntimeMessageText" class="errorUserMessageText">
        </div>
        <div><input type="text" id="storedPasswordTextBox" style="width: 100%" /></div>
        <div style="text-align: right;"><a class="button" id="storedPasswordOkBtn">Ok</a> <a class="button" id="storedPasswordCancelBtn">Отмена</a> </div>
        <div id="errorScriptIM" runat="server">
            <script type="text/javascript">
                $(document).ready(function () {
                    errorIncorrectPasswordMessageInitMethod(<%= TheCourse.Id %>);
                });
            </script>
        </div>
    </div>
    
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
    <uc:Advert runat="server" Ad="7" />
</asp:Content>
