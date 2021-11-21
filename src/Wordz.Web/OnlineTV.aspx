<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="OnlineTV.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.OnlineTV" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title id="onlineTvTitle" runat="server" clientidmode="Static">Онлайн ТВ на английском</title>
    <script type="text/javascript" src="/js/wordz.onlinetv.js"></script>
	<meta name="keywords" content="смотреть слушать онлайн ТВ трансляция телеканал английский" />
	<meta name="description" content="Онлайн ТВ на английском: Russia Today CNN Sky CSPAN3 Pentagon Weather Bloomberg CSPAN1 ESPAN Islam JC-TV Kulak MLB CNN IBN Byu CPAC tSc SHOP Strip" />
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Онлайн ТВ на английском
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	<strong class="description">
		<asp:Literal ID="lblChannelInfo" runat="server" /></strong>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
    <input type="hidden" id="myAccountId" style="display: none"  runat="server" clientidmode="Static"/>
    <span id="baseTvImagePath" style="display: none"  runat="server" clientidmode="Static"/>
	<table class="maintable">
		<tr height="400px">
			<td align="center" width="20%">
                <div id="editChannelsBtn" style="margin: 10px auto" runat="server" clientidmode="Static" >
                    <a class="button" id="openChannelsListBtn">Редактировать список каналов</a>
                </div>
				<asp:DataList ID="dlChannels" runat="server" RepeatColumns="2" EnableViewState="False"
					ItemStyle-HorizontalAlign="Center" RepeatDirection="Horizontal" BorderWidth="0px"
					Width="100%" Height="100%" CellSpacing="10">
					<ItemTemplate>
                    <div style="text-align:center">
						<a href="/OnlineTV/<%#DataBinder.Eval(Container.DataItem, "Id")%>" title="<%#DataBinder.Eval(Container.DataItem, "Description")%>"
							class="button">
							<img src="<%#DataBinder.Eval(Container.DataItem, "ImageUrl")%>" <%--alt="<%#DataBinder.Eval(Container.DataItem, "Name")%>"--%> /><br />
							<%#DataBinder.Eval(Container.DataItem, "Name")%> </a>
                    </div>
					</ItemTemplate>
				</asp:DataList>
			</td>
			<td width="80%">
				 <div id="divContent" runat="server" clientidmode="Static" />
			</td>
		</tr>
	</table>
    <div id="tvListDivContainer" class="box-modal" style="display: none; width:auto">
        <div style="display: none;" class="box-modal_close arcticmodal-close">X</div>
        <div id="tvListDiv" style="width:100%;">
            <div style="padding: 5px">
                <a class="button" id="addNewChannelBtn">Добавить новый канал</a>
            </div>
            <table>
            <tr>
                <td>
                    <div style="">
                        <div style="text-align: center; ">
	                        Другие каналы
                        </div>
                        <ul id="otherChannelsList" class="connectedSortable2" style="overflow: auto; width:103%">
                          <li class="ui-state-highlight">Item 1</li>
                        </ul>
                    </div>
                </td>
                <td>
                    <div style="">
                        <div style="text-align: center; ">
	                        Мои каналы
                        </div>
                        <ul id="myChannelsList" class="connectedSortable1" style="overflow: auto; width:103%">
                          <li class="ui-state-default">Item 1<br/>asdas</li>
                        </ul>
                    </div>
                </td>
                <td style="display: none">
                    <div>
                        <div style="text-align: center; ">
	                        Скрытые каналы
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
    <div id="tvEditDivContainer" class="box-modal" style="display: none; width:auto">
        <div id="tvEditDiv" style="width:auto">
            <input type="hidden" name="channelId" id="channelId" value="0" />
            <div id="validationListContainer" class="validationContainer" style="display: none;">
                <ul></ul>
            </div>
            <div class="editItem">Название:</div>
            <div class="editValue">
                <input class="editValue required" name="name" id="nameId"/>
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
	<uc:Advert runat="server" Ad="3" />
</asp:Content>
