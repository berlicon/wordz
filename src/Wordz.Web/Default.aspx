<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Default.aspx.cs"
    AutoEventWireup="true" Inherits="Wordz.Web.Default" EnableViewState="false" %>

<%@ Import Namespace="System.Net" %>

<%@ Import Namespace="Wordz.BE.Dto" %>
<%@ Register Src="Controls/Satellites.ascx" TagName="Satellites" TagPrefix="uc" %>
<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
    <title>��������������� wiki-���������</title>
    <meta name="keywords" content="����� ���� wiki ����������� ���������� ��������� ����� �������� ���������� ������� ����� ������� ������������ ���� �������� ������ mp3 ������ �� ����� ������ ��������" />
    <meta name="description" content="��������������� wiki-���������. ��������� �����, ������� ��� �������� �����������. ������ ������ �������. �������� �� ������ txt/mp3 ������ � ����������� �������." />
    <link rel="canonical" href="http://www.wordz.ru" />
    <script type="text/javascript" src="https://apis.google.com/js/plusone.js"></script>
	<script type="text/javascript" src="http://vk.com/js/api/openapi.js?100"></script>
	<script type="text/javascript">VK.init({ apiId: 3231247, onlyWidgets: true });</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
    ��������������� wiki-���������
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
    <br />
    <ul class="menu headerMenu">
        <li><a href="/Goal">� �������</a></li>
        <li><a href="/Authors">�������</a></li>
        <li><a href="/Contacts">��������</a></li>
    </ul>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
    <div id="fb-root">
    </div>
    <script>
        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
            fjs.parentNode.insertBefore(js, fjs);
        } (document, 'script', 'facebook-jssdk'));
    </script>
    <div class="clr">
    </div>
    <div id="filter">
        <table>
            <tr>
                <td>
                    <label for="popularity">
                        ����������</label>
                </td>
                <td class="value">
                    <select name="popularity" id="" data-placeholder="--��������--">
                        <option value="0">��������</option>
                        <option value="1">������������</option>
                        <option value="2">���������</option>
                        <option value="3">�������</option>
                    </select>
                </td>
                <td>
                    <label for="category">
                        ���������</label>
                </td>
                <td class="value">
                    <select name="category" id="" multiple="multiple" data-placeholder="--��������--">
                        <option value="0">���</option>
                        <option value="1">����������� �����</option>
                        <option value="2">���������</option>
                        <option value="3">���</option>
                        <option value="4">������</option>
                        <option value="5">�����</option>
                    </select>
                </td>
                <td>
                    <label for="geography">
                        ���������</label>
                </td>
                <td class="value">
                    <select name="geography" id="" multiple="multiple" data-placeholder="--��������--">
                        <option value="0">���</option>
                        <option value="1">������ � ���</option>
                        <option value="2">�������� ������</option>
                        <option value="3">��� � ������</option>
                        <option value="4">������</option>
                        <option value="5">����� �������</option>
                        <option value="6">����</option>
                        <option value="7">������</option>
                    </select>
                </td>
                <td>
                    <label for="languages">
                        �����</label>
                </td>
                <td class="value">
                    <select name="languages" id="" multiple="multiple" data-placeholder="--��������--">
                        <option value="0">���</option>
                        <option value="1">�������</option>
                        <option value="2">English</option>
                    </select>
                </td>
                <td>
                    <input type="submit" value="ok" class="button" onclick="javascript:alert('�� �����������');return false;">
                </td>
            </tr>
        </table>
    </div>
    <div id="courses">
        <listbox>
        <%
            var courses = TheCourses ?? new CourseDetailsDto[] {};
            foreach (var course in courses)
            {
        %>
        <div class="course">
        <a href="/Course/<%= course.Id %>" >
            <img width="50px" height="50px" src="/LoadPictureHandler.ashx?id=<%= course.PictureId %>" alt="">
            <div class="courseTitle">
                <%= WebUtility.HtmlEncode(course.Name)%></div>
            <div class="courseDescription">
                <%= WebUtility.HtmlEncode(course.Description)%></div>
            <div class="clr">
            </div>
            <p>
                <span class="cost"><%= WebUtility.HtmlEncode(course.Price.ToString())%></span> 
                <span class="stat">23</span> 
                <span class="lang"><%= WebUtility.HtmlEncode(course.UILanguageName)%></span>
            </p>
            <p>
                <span class="group">35</span>
                <span class="place">������</span>
                <span class="topic">32</span>
            </p>
            </a>
        </div>
        <%
            }
        %>
        </listbox>
        <div class="clr">
        </div>
        <ul class="pagination">
            <li class="prev"><a href="javascript:void(0);">&#60;</a></li>
            <li><a class="active" href="javascript:void(0);">1</a></li>
            <li><a href="javascript:void(0);">2</a></li>
            <li><a href="javascript:void(0);">3</a></li>
            <li>...</li>
            <li><a href="javascript:void(0);">99</a></li>
            <li class="next"><a href="javascript:void(0);">&#62;</a></li>
        </ul>
        <a href="/Courses" class="allCourses">������ ���� ������</a>
    </div>
    <!--  end #courses -->
    <!--  end #header -->
    <div id="middle">
        <div id="center">
            <div id="search">
                <label for="search">
                    ����� �����:</label>
                <input type="text" name="search" placeholder="������� ����� ��� ������">
                <input type="submit" value="ok" class="button" onclick="javascript:alert('�� �����������');return false;">
                <a href="/Course/new" class="buttonRed">������� ����� ����</a>
            </div>
            <div id="currentCourses">
                <h3 class="sectionTitle">
                    ���������� ������� �����:</h3>
                <div class="courses">
					<a href="#" onclick="javascript:alert('�� �����������. ������� ������ ���� ����� ������ ������.');return false;" class="currentCourse"><img src="/img/courseICO1.png" alt=""></a>
					<a href="#" onclick="javascript:alert('�� �����������. ������� ������ ���� ����� ������ ������.');return false;" class="currentCourse"><img src="/img/courseICO2.png" alt=""></a>
					<a href="#" onclick="javascript:alert('�� �����������. ������� ������ ���� ����� ������ ������.');return false;" class="currentCourse"><img src="/img/courseICO3.png" alt=""></a>
				</div>
            </div>
            <table class="userData">
                <tr>
                    <td class="udTitle">
                        <h3 class="sectionTitle">
                            ���� �������������� ���������:</h3>
                    </td>
                    <td class="udValue">
                        ������, ������
                    </td>
                    <td class="udChange">
                        <a href="#" onclick="javascript:alert('�� �����������');return false;" class="buttonRed">��������</a>
                    </td>
                </tr>
                <tr>
                    <td class="udTitle">
                        <h3 class="sectionTitle">
                            ��� ����:</h3>
                    </td>
                    <td class="udValue">
                        �������
                    </td>
                    <td class="udChange">
                        <a href="#" onclick="javascript:alert('�� �����������');return false;" class="buttonRed">��������</a>
                    </td>
                </tr>
            </table>
            <div class="hr">
            </div>
            <div id="servises">
                <h3 class="servisesTitle">
                    �������</h3>
                <ul class="list1 list">
                    <li class="item1"><a href="/Analysis">������������� ����� �� ���������� �����</a></li>
                    <li class="item2"><a href="/Process">��������� ��� ������ txt � mp3 ����� � �����������
                        �������</a></li>
                    <li class="item3"><a href="/AddWords">�������� ����� � ������ �������</a></li>
                    <li class="item4"><a href="/CheckWords">��������� ��������� �����</a></li>
                    <li class="item5"><a href="/Vocabulary">�������� ���������� � ����� �������</a></li>
                </ul>
                <ul class="list2 list">
                    <li class="item6"><a href="/IrregularVerbs">��������� ������ ������������ ��������</a></li>
                    <li class="item7"><a href="/DictionaryQuiz">����������� � �������� �� ������ �������</a></li>
                    <li class="item8"><a href="/WordsOrderQuiz">����������� � �������� �� ������� ���� �
                        �����������</a></li>
                    <li class="item9"><a href="/LevelQuiz">���������� ������ ����������</a></li>
                </ul>
                <div class="clr">
                </div>
                <ul class="list3">
                    <li class="item10"><a href="/OnlineTV">������ ��</a></li>
                    <li class="item11"><a href="/OnlineFM">������ �����</a></li>
                    <li class="item12"><a href="/OnlineFilm">������ ������</a></li>
                    <li class="item13"><a href="/Articles">�������� ������</a></li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
    <%--<uc:Satellites runat="server" />--%>
    <div class="fb-like" data-href="http://wordz.ru/" data-send="true" data-show-faces="false">
    </div>
	<div id="vk_like"></div>
	<script type="text/javascript">
		VK.Widgets.Like("vk_like", { type: "button" });
	</script>
    <g:plusone></g:plusone>
	<a href="https://twitter.com/share" class="twitter-share-button" data-lang="ru">��������</a>
    <script>    	!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } } (document, "script", "twitter-wjs");</script>
    <a target="_blank" class="mrc__plugin_uber_like_button" href="http://connect.mail.ru/share"
        data-mrc-config="{'cm' : '1', 'ck' : '1', 'sz' : '20', 'st' : '1', 'tp' : 'combo'}">
        ��������</a>
    <script src="http://cdn.connect.mail.ru/js/loader.js" type="text/javascript" charset="UTF-8"></script>
    <hr />
    <uc:Advert runat="server" Ad="7" />
</asp:Content>
