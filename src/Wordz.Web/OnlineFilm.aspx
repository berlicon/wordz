<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="OnlineFilm.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.OnlineFilm" EnableViewState="false" %>

<%--<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>--%>
<%@ Register Src="Controls/Progress.ascx" TagName="Progress" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
    <script type="text/javascript" src="/js/wordz.onlinefilm.js"></script>
	<title>Фильмы онлайн на английском</title>
	<meta name="keywords" content="смотреть онлайн фильм кино трансляция английский">
	<meta name="description" content="Фильмы онлайн на английском: Много фильмов на английском языке! БОЕВИК, ВОЕННОЕ КИНО, ДРАМА, КОМЕДИЯ, МУЛЬТФИЛЬМЫ, ПРИКЛЮЧЕНИЯ, ТРЕЙЛЕРЫ, ТРИЛЛЕР, УЖАСЫ, ФАНТАСТИКА, ФЭНТАЗИ">
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Фильмы онлайн на английском
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	<table width="100%">
		<tr>
			<td>
				<strong class="description">
					<asp:Label ID="lblInfo" runat="server" /></strong>
			</td>
			<td align="right">
				<asp:TextBox ID="txtSearch" runat="server" Width="140px" />
				<a id="btnSearch" runat="server" href="#" class="button" title="Найти фильм по названию">
					Поиск</a>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
    <input type="hidden" id="myAccountId" style="display: none"  runat="server" clientidmode="Static"/>
    <span id="baseFilmImagePath" style="display: none"  runat="server" clientidmode="Static"/>
	<uc:Progress ID="ctlProgress" runat="server" />
	<table class="maintable">
		<tr height="400px">
			<td align="center" valign="top" width="10%">
				<a id="btnSetFilmListMode" runat="server" href="#" class="button" title="Показать найденные фильмы списком">
					<img src="/img/Films/list.gif" alt="=" title="Показать найденные фильмы списком" /></a>
				<a id="btnSetFilmIconMode" runat="server" href="#" class="button" title="Показать найденные фильмы иконками">
					<img src="/img/Films/icon.gif" alt="::" title="Показать найденные фильмы иконками" /></a>
				<br />
				<br />
                <div id="editChannelsBtn" style="margin: 10px auto" runat="server" clientidmode="Static" >
                    <a class="button" id="openChannelsListBtn">Редактировать список фильмов</a>
                </div>
				<%--<asp:Button ID="btnOK" runat="server" Text="GOOD" />
						<asp:Button ID="btnBad" runat="server" Text="BAD" />--%>
				<asp:Repeater ID="rptCategories" runat="server">
					<HeaderTemplate>
						<strong class="description">Категория</strong><br />
					</HeaderTemplate>
					<ItemTemplate>
						<a id="hypFilm" runat="server" href="#" class="button" />
						<br />
					</ItemTemplate>
				</asp:Repeater>
			</td>
			<td colspan="2">
				<div id="divContent" runat="server" class="divWordsAll" clientidmode="Static" />
			</td>
		</tr>
	</table>
    <div id="filmListDivContainer" class="box-modal" style="display: none; width:auto">
        <div style="display: none;" class="box-modal_close arcticmodal-close">X</div>
        <div id="filmListDiv" style="width:100%;">
            <div style="padding: 5px">
                <a class="button" id="addNewChannelBtn">Добавить новый фильм</a>
            </div>
            <table>
            <tr>
                <td>
                    <div style="">
                        <div style="text-align: center; ">
	                        Другие фильмы
                        </div>
                        <ul id="otherChannelsList" class="connectedSortable2" style="overflow: auto; width:103%">
                          <li class="ui-state-highlight">Item 1</li>
                        </ul>
                    </div>
                </td>
                <td>
                    <div style="">
                        <div style="text-align: center; ">
	                        Мои фильмы
                        </div>
                        <ul id="myChannelsList" class="connectedSortable1" style="overflow: auto; width:103%">
                          <li class="ui-state-default">Item 1<br/>asdas</li>
                        </ul>
                    </div>
                </td>
                <td style="display: none">
                    <div>
                        <div style="text-align: center; ">
	                        Скрытые фильмы
                        </div>
                        <ul id="hidedChannelsList" class="connectedSortable3">
                          <li class="ui-state-highlight">Item 1</li>
                        </ul>
                    </div>
                </td>
            </tr>
            </table>
            <div style="margin: 10px auto auto auto; text-align:right">
                <a class="button" id="channelListSaveBtn">Ok</a>
                &nbsp;&nbsp;&nbsp;
                <a class="button" id="channelListCancelBtn">Отмена</a>
            </div>
        </div>
    </div>
    <div id="filmEditDivContainer" class="box-modal" style="display: none; width:auto">
        <div id="filmEditDiv" style="width:auto">
            <input type="hidden" name="channelId" id="channelId" value="0" />
            <div id="validationListContainer" class="validationContainer" style="display: none;">
                <ul></ul>
            </div>
            <div class="editItem">Название:</div>
            <div class="editValue">
                <input class="editValue required" name="name" id="nameId"/>
            </div>
            <div class="editItem">Категория:</div>
            <div class="editValue">
                <select class="editValue required" name="category" id="categoryId"></select>
            </div>
            <div class="editItem">Описание:</div>
            <div class="editValue">
                <textarea class="editMemo" name="description" id="descriptionId" cols="50" rows="5"></textarea>
            </div>
            <div class="editItem">Код плеера:</div>
            <div class="editValue">
                <textarea class="editMemo required" name="embededCode" id="embededCodeId" cols="50" rows="5"></textarea>
            </div>
            <div class="editItem">Ссылка на изображение:</div>
            <div class="editValue">
                <input class="editValue" name="imageUrl" id="imageUrlId" />
            </div>
            <div style="padding: 40px auto; text-align:right" runat="server" id="editButtonsContainer">
                <a class="button" id="editYesBtn">Ок</a>&nbsp;&nbsp;&nbsp;
                <a class="button" id="editNoBtn">Отмена</a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<%--<uc:Advert runat="server" Ad="3" />--%>
</asp:Content>
