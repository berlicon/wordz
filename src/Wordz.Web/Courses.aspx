<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    EnableViewState="false" CodeBehind="Courses.aspx.cs" Inherits="Wordz.Web.Courses" %>

<%@ Import Namespace="System.Net" %>
<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
    <title>Список курсов</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
    Список курсов
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
    Список всех курсов
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
    Список всех курcов
    <%--    <asp:CheckBox OnCheckedChanged="onCheckedChanged" runat="server" Text="Группировать по категориям" />--%>
    <br />
    <table id="courses">
        <tr class="headListItem">
            <th class="courseTd">
                <p>
                </p>
            </th>
            <th class="courseTd">
                <p>
                    Наименование</p>
            </th>
            <th class="courseTd">
                <p>
                    Категория</p>
            </th>
            <th class="courseTd">
                <p>
                    Описание</p>
            </th>
            <th class="courseTd">
                <p>
                    Цена</p>
            </th>
            <th class="courseTd">
                <p>
                    Язык</p>
            </th>
            <th class="courseTd">
            </th>
        </tr>
        <%
            foreach (var course in TheCourses)
            {
        %>
        <tr class="courseListItem">
            <td class="courseTd">
                <img width="50px" height="50px" src="Handlers/LoadPictureHandler.ashx?id=<%= course.PictureId %>" alt="">
            </td>
            <td class="courseTd">
                <%= WebUtility.HtmlEncode(course.Name)%>
            </td>
            <td class="courseTd">
                <%= WebUtility.HtmlEncode(course.CategoryName)%>
            </td>
            <td class="courseTd">
                <%= WebUtility.HtmlEncode(course.Description)%>
            </td>
            <td class="courseTd">
                <%= WebUtility.HtmlEncode(course.Price.ToString())%>
            </td>
            <td class="courseTd">
                <%= WebUtility.HtmlEncode(course.UILanguageName)%>
            </td>
            <td class="courseTd">
                <a href="/Course/<%= course.Id %>" class="readmore">&#8594;</a>
            </td>
        </tr>
        <%
            }
        %>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
    <uc:Advert runat="server" Ad="7" />
</asp:Content>
