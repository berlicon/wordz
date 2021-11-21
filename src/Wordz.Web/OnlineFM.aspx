<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="OnlineFM.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.OnlineFM" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Онлайн радио на английском</title>
	<meta name="keywords" content="слушать онлайн радио трансляция радиостанция английский"/>
	<meta name="description" content="Онлайн радио на английском: BBC CNN Sky.FM DI.FM Jazz BassDrive Club977 Fusion KRKT Rock XmasMelody WKCR Bandit Q-Music Virgin Video Gamers' Hit Music BBC Scotland Caroline BBC Ulster Solar Premier Christian DI.FM EuroDance DI.FM DeepHouse DI.FM Salsa BBC Asian BBC 1Xtra BBC Cymru BBC Oxford BBC Cambridge BBC Manchester"/>
    <script type="text/javascript" src="/js/wordz.onlinefm.js"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Онлайн радио на английском
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	<strong class="description">
		<asp:Literal ID="lblChannelInfo" runat="server" /></strong>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
    <input type="hidden" id="myAccountId" style="display: none"  runat="server" clientidmode="Static"/>
    <span id="baseFmImagePath" style="display: none"  runat="server" clientidmode="Static"/>
    <div id="editChannelsBtn" style="margin: 10px auto" runat="server" clientidmode="Static" >
                    <a class="button" id="openChannelsListBtn">Редактировать список каналов</a>
                </div>
	<table class="maintable">
		<tr>
			<td align="center" valign="middle" height="1%">
				<strong class="description">Выберите<br />
					радио<br />
					станцию:</strong>
			</td>
			<td align="center" valign="middle">
				<div id="divContent" runat="server" style="width: 100%; height: 100px; overflow:visible" clientidmode="Static"/>
			</td>
		</tr>
		<tr>
            
			<td colspan="2" align="center" valign="middle">
				<asp:DataList ID="dlChannels" runat="server" RepeatColumns="5" EnableViewState="False"
					ItemStyle-HorizontalAlign="Center" RepeatDirection="Horizontal" BorderWidth="0px"
					Width="100%" Height="100%" CellSpacing="10">
					<ItemTemplate>
						<a href="/OnlineFM/<%#DataBinder.Eval(Container.DataItem, "Id")%>" title="<%#DataBinder.Eval(Container.DataItem, "Description")%>"
							class="button">
							<img src="<%#DataBinder.Eval(Container.DataItem, "ImageUrl")%>" alt="<%#DataBinder.Eval(Container.DataItem, "Name")%>" /><br />
							<%#DataBinder.Eval(Container.DataItem, "Name")%>
					</ItemTemplate>
				</asp:DataList>
			</td>
		</tr>
	</table>
    <div id="fmListDivContainer" class="box-modal" style="display: none; width:auto">
        <div style="display: none;" class="box-modal_close arcticmodal-close">X</div>
        <div id="fmListDiv" style="width:100%;">
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
    <div id="fmEditDivContainer" class="box-modal" style="display: none; width:auto">
        <div id="fmEditDiv" style="width:auto">
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
            <div class="editItem">
                Использовать: 
                <input type="radio" id="useMediaPlayerId" name="useMediaPlayer" class="editValue" value="true" /><label for="useMediaPlayerId">MediaPlayer</label>
                <input type="radio" id="useRealPlayerId" name="useMediaPlayer" class="editValue" value="false" /><label for="useRealPlayerId">RealPlayer</label>
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
