
TvPage = function () {
}

TvPage.prototype = {

    _accountId: 0,

    _isSomethingChanged: false,

    _addingIndex: -1,

    _internalCache: {},

    _baseTvImagePath: '/',

    initializePage: function () {
        var self = this;
        this.initializeTvEditForm();

        $('#openChannelsListBtn').click(function (e) { e.preventDefault(); self.openChannelsList(); });

        $('#addNewChannelBtn').click(function (e) {
            e.preventDefault();
            self._addingIndex--;
            self.openEditForm(self._addingIndex);
        });

        $("#myChannelsList").sortable({
            connectWith: ".connectedSortable3",
            dropOnEmpty: true,
            //stop: this.functionWrapper(this.myChannelsListItemDrop),
            receive: this.functionWrapper(this.myChannelsListItemDrop)
        }).disableSelection();

        $(" #otherChannelsList").sortable({
            connectWith: ".connectedSortable1",
            dropOnEmpty: true
        }).disableSelection();

        $("#hidedChannelslist").sortable({ dropOnEmpty: true, distance: 15, connectWith: ".connectedSortable1" })
		    .disableSelection();

        $("#channelsSearchText").keydown(function (e) {
            if (e.which == 13) {
                this.refreshOtherChannelListData();
            }
        });

        $('#channelListSaveBtn').click(function () {
            self.saveChanges();
        });

        $('#channelListCancelBtn').click(function () {
            self.closeChannelsList();
        });

        self._baseTvImagePath = $('#baseTvImagePath').html();
        self._accountId = $('#myAccountId').val();
    },

    initializeTvEditForm: function () {
        var self = this;
        $("#tvEditDiv div textarea")
                    .focus(function () { this.rows = 5; })
                    .blur(function () { this.rows = 2; })
                    .attr("rows", 2);

        // Настраиваем обработчик кнопки "Сохранить"
        $("#editYesBtn").click(function () {
            var channel = self.getObjectFromEditForm();
            var validationList = self.validateChannel(channel);
            if (validationList.length > 0) {
                var validationContainer = $('#validationListContainer');
                validationContainer.fadeOut('fast');
                $('ul', validationContainer).html('');
                for (var i = 0; i < validationList.length; i++) {
                    $('<li class="validationItem">' + validationList[i] + '</li>').appendTo($('ul', validationContainer));
                }
                validationContainer.fadeIn('fast');
                return;
            }

            if ($('#channelId').val() > 0) {
                self.editChannelInternal(channel);
            } else {
                self.addChannelInternal(channel);
            }

            self.updateChannelControl(channel);

            $('#tvEditDivContainer').arcticmodal('close');

        });
        // Настраиваем обработчик кнопки "Отмена"
        $("#editNoBtn").click(function () {
            $('#tvEditDivContainer').arcticmodal('close');
        });
    },

    functionWrapper: function (fn) {
        var self = this;
        return function () {
            fn.apply(self, arguments);
        };
    },

    getCallBackHandler: function () {
        var self = this;
        var fn = function (data) {
            self.formCallbackHandler(data);
        };
        return fn;
    },

    formCallbackHandler: function (data) {
        this.isInPostBack = false;
        $('#tvEditDiv div.editValue :only-child').css('background-color', 'white');

        data = eval('(' + data + ')');

        if (data.status) {
            if (data.status == 'OK') {
                //Все ок
                this.refreshAllLists();
            } else {
                if (data.returnObject != null) {
                    alert(data.msg);
                }
            }
        }
        else {
            //Неизвестная причина
        }
    },

    editTv: function () {

    },
    refreshMyChannelListData: function (afterMethod, onFail) {
        var self = this;
        var channelsQueryUrl = '/GetTvChannelsList.ashx';
        $.ajax({
            url: channelsQueryUrl,
            success: function (data) {
                var data = eval('(' + data + ')');
                if (data.status == 'OK') {
                    self.updateMyChannelList(data);
                    if (afterMethod) {
                        afterMethod.call(self);
                    }
                } else {
                    self.updateMyChannelList([]);
                }
            }
        })
        .fail(function (xhr, ajaxOptions, thrownError) {
            self.updateMyChannelList([]);
            if (onFail) onFail.call(self, xhr, ajaxOptions, thrownError);
        });
    },

    refreshHidedChannelListData: function (afterMethod, onFail) {
        var self = this;
        var channelsQueryUrl = '/GetHidedTvChannelsList.ashx';
        $.ajax({
            url: channelsQueryUrl,
            success: function (data) {
                var data = eval('(' + data + ')');
                if (data.status == 'OK') {
                    self.updateHidedChannelList(data);
                    if (afterMethod) {
                        afterMethod.call(self);
                    }
                } else {
                    self.updateHidedChannelList([]);
                }
            }
        })
        .fail(function (xhr, ajaxOptions, thrownError) {
            self.updateHidedChannelList([]);
            if (onFail) onFail.call(self, xhr, ajaxOptions, thrownError);
        });
    },

    refreshOtherChannelListData: function (afterMethod, onFail) {
        var self = this;
        var searchText = $('#channelsSearchText').attr('value');
        var channelsQueryUrl = '/GetTvChannelsFromOtherUsers.ashx?searchText=' + searchText;
        $.ajax({
            url: channelsQueryUrl,
            success: function (data) {
                var data = eval('(' + data + ')');
                if (data.status == 'OK') {
                    self.updateOtherChannelList(data);
                    if (afterMethod) {
                        afterMethod.call(self);
                    }
                } else {
                    self.updateOtherChannelList([]);
                }
            }
        })
        .fail(function (xhr, ajaxOptions, thrownError) {
            self.updateOtherChannelList([]);
            if (onFail) onFail.call(self, xhr, ajaxOptions, thrownError);
        });
    },

    refreshAllLists: function () {
        this.refreshMyChannelListData();
        this.refreshOtherChannelListData();
        this.refreshHidedChannelListData();
    },

    openChannelsList: function () {
        var height = $(window).height() - 250;

        $('#myChannelsList').css('height', height + 'px');
        $('#otherChannelsList').css('height', height + 'px');
        this._countOfUpdatedList = 0;
        this.refreshMyChannelListData(this._updateCheckingFunction, this._updateCheckingFunction);
        this.refreshOtherChannelListData(this._updateCheckingFunction, this._updateCheckingFunction);
        this.refreshHidedChannelListData(this._updateCheckingFunction, this._updateCheckingFunction);
    },

    closeChannelsList: function () {
        $("#tvListDivContainer").arcticmodal('close');
    },

    _countOfUpdatedList: 0,

    _updateCheckingFunction: function () {
        this._countOfUpdatedList++;
        if (this._countOfUpdatedList >= 3) {
            this.showChannelsListForm();
        }
    },

    updateHidedChannelList: function (data) {
        var myList = $("#hidedChannelsList");
        myList.html('');
        if (data.returnObject) {
            for (var i = 0; i < data.returnObject.length; i++) {
                var channel = data.returnObject[i];
                var elementText = '<li class="ui-state-default">';
                elementText += '<div><div class="sortableItemDivleft">';
                if (channel.ImageUrl) {
                    elementText += '<img src="/img/TvChannels/' + channel.ImageUrl + '" />';
                }
                elementText += '</div>';
                elementText += '<div class="sortableItemDivRight>"';
                elementText += '<img src="/img/TvChannels/' + channel.ImageUrl + '" />';
                elementText += '</div></div>';
                elementText += channel.Name;
                elementText += '</li>';
                myList.append(elementText);
            }
        }
    },

    updateOtherChannelList: function (data) {
        var myList = $("#otherChannelsList");
        myList.html('');
        if (data.returnObject) {
            for (var i = 0; i < data.returnObject.length; i++) {
                var channel = data.returnObject[i];
                var resultElement = this.createNewLiItemFromChannel(channel);
                resultElement.appendTo(myList);
                $('[name="edit"]', resultElement).hide();
                $('[name="delete"]', resultElement).hide();
            }
        }
    },

    updateMyChannelList: function (data) {
        var myList = $("#myChannelsList");
        myList.html('');
        if (data.returnObject) {
            for (var i = 0; i < data.returnObject.length; i++) {
                var channel = data.returnObject[i];
                var resultElement = this.createNewLiItemFromChannel(channel);
                resultElement.appendTo(myList);
            }
        }
    },

    createNewLiItemFromChannel: function (channel) {
        var self = this;
        var accountId = $('#myAccountId').val();
        var elementId = 'channel_' + channel.Id;
        var elementText = '<li class="ui-state-default" id="' + elementId + '">';
        elementText += '<div style="overflow: hidden; min-height:32px"><div class="sortableItemDivleft">';
        if (channel.ImageUrl) {
            var baseImagePath = !channel.AccountId ? self._baseTvImagePath : '';
            elementText += '<img src="' + baseImagePath + channel.ImageUrl + '" />';
        }
        elementText += '</div>';

        elementText += '<div class="sortableItemDivRight">';
        //elementText += '<img src="/img/button-edit.png" onmouseover="this.src=\"/img/button-edit.png\"" onmouseleave="this.src=\"this.src=\"/img/button-edit-h.png\"" />';
        if (channel.IsEditable) {
            elementText += '<a id="' + elementId + '_edit" name="edit" class="button" style="color: white" onclick="page.openEditForm(' + channel.Id + ');">Правка</a>&nbsp;';
        }
        elementText += '<a id="' + elementId + '_delete" name="delete" class="button" style="color: red">X</a>';
        elementText += '</div>';
        elementText += '<div id="' + elementId + '_content" name="content" style="width: auto; vertical-align: middle">';
        elementText += channel.Name;
        elementText += '</div></div>';
        elementText += '</li>';
        var resultElement = $(elementText);

        $('[name="delete"]', resultElement).click(function () { self.onDeleteChannelButtonClick(elementId, channel) });
        return resultElement;
    },

    showChannelsListForm: function () {
        var self = this;
        $("#tvListDivContainer").arcticmodal({
            closeOnEsc: true,
            closeOnOverlayClick: true,
            beforeOpen: function () {
                $('#tvListDivContainer').show();
                $('#divContent').hide();
            },
            afterClose: function (data, el) {
                $('#tvListDivContainer').hide();
                self._internalCache = {};
                self._addingIndex = -1;

                if (self._isSomethingChanged) {
                    // если что-то изменилось - перезагружаем страницу
                    self._isSomethingChanged = false;
                    //document.location = document.location;
                } else {
                    $('#divContent').show();
                }
            }
        });
    },

    loadDataForChannel: function (tvId) {
        var self = this;
        $.ajax({
            url: "/GetTvChannelById.ashx?tvId=" + tvId,
            success: function (data) {
                var data = eval('(' + data + ')');

                if (!data.status || data.status && data.status === 'ERR') {
                    self.closeEditForm();
                    //alert();
                } else {
                    var tv = data.returnObject;
                    self.updateEditFieldFromObject(tv);
                }
            }
        })
        .fail(function (xhr, ajaxOptions, thrownError) {
            self.closeEditForm(); /*alert('Fail!' + thrownError);*/
        })
    },

    updateEditFieldFromObject: function (tv) {
        $('#nameId').val(tv.Name);
        $('#descriptionId').val(tv.Description);
        $('#embededCodeId').val(tv.Url);
        $('#imageUrlId').val(tv.ImageUrl);
        $('#isEditableId').attr('checked', tv.IsEditable == true);
        $("#editYesBtn").show();
        this.setReadonlyMode(false);
    },

    loadDataOrGetFromCache: function (tvId) {
        var valueInCache = this._internalCache[tvId.toString()];
        if (valueInCache) {
            this.updateEditFieldFromObject(valueInCache.obj);
        } else {
            this.loadDataForChannel(tvId);
        }
    },

    fillViewFromCacheIfExists: function (tvId) {
        var valueInCache = this._internalCache[tvId.toString()];
        if (valueInCache) {
            this.updateEditFieldFromObject(valueInCache.obj);
        }
    },

    validateChannel: function (channel) {
        var validationList = [];
        if (!channel.Name || channel.Name == "") {
            validationList.push("Необходимо ввести название канала!");
        }
        if (channel.Name && channel.Name.length > 20) {
            validationList.push("Название канала должно быть не более 20 символов!");
        }
        if (channel.Description && channel.Description.length > 500) {
            validationList.push("Описание должно быть не более 500 символов!");
        }
        if (channel.ImageUrl && channel.ImageUrl.length > 50) {
            validationList.push("Путь к изображению должен быть меньше 50 символов!");
        }
        if (channel.Url && channel.Url.length > 2000) {
            validationList.push("Код плеера должен быть меньше 2000 символов!");
        }
        if (!channel.Url || channel.Url == "") {
            validationList.push("Необходимо указать код плеера!");
        }

        return validationList;
    },

    getObjectFromEditForm: function () {
        var retObj = {};
        retObj.Id = $('#channelId').val();
        retObj.Name = $('#nameId').val();
        retObj.Description = $('#descriptionId').val();
        retObj.Url = $('#embededCodeId').val();
        retObj.ImageUrl = $('#imageUrlId').val();
        retObj.IsEditable = true;
        return retObj;
    },

    resetFormFields: function () {
        $('#nameId').val('');
        $('#descriptionId').val('');
        $('#embededCodeId').val('');
        $('#imageUrlId').val('');
        $('#isEditableId').attr('checked', 'checked');
    },

    setReadonlyMode: function (isReadonly) {
        $('#nameId').attr('readOnly', isReadonly);
        $('#descriptionId').attr('readOnly', isReadonly);
        $('#embededCodeId').attr('readOnly', isReadonly);
        $('#imageUrlId').attr('readOnly', isReadonly);
        $('#isEditableId').attr('readOnly', isReadonly);
    },


    openEditForm: function (tvId) {
        //Прописываем tvid
        $('#channelId').attr('value', tvId);

        this.resetFormFields();

        if (tvId > 0) {
            this.setReadonlyMode(true);
            $("#editYesBtn").hide();
            this.loadDataOrGetFromCache(tvId);
        } else {
            this.setReadonlyMode(false);
            $("#editYesBtn").show();
            this.fillViewFromCacheIfExists(tvId);
        }

        $("#tvEditDivContainer").arcticmodal({
            closeOnEsc: true,
            closeOnOverlayClick: true,
            beforeOpen: function () {
                $('#tvEditDivContainer').show();
                $('#validationListContainer').hide();
            },
            afterClose: function (data, el) {
                $('#tvEditDivContainer').hide();
            }
        });
    },

    closeEditForm: function () {
        $("#tvEditDivContainer").arcticmodal('close');
    },

    onDeleteChannelButtonClick: function (liContainerId, channel) {
        if (confirm('Действительно удалить канал?')) {
            var item = $('#' + liContainerId);
            var valueInCache = this._internalCache[channel.Id.toString()];
            if (valueInCache && valueInCache.action == "add" || channel.IsEditable) {
                item.remove();
            } else {
                item.prependTo($("#otherChannelsList"));
                $('[name="edit"]', item).hide();
                $('[name="delete"]', item).hide();
            }
            //this.deleteChannel(channelId);
            //var dummyChannel = { Id: channelId };
            this.deleteChannelInternal(channel);
        }
    },

    myChannelsListOrderDescriptor: function () {
        var dataObj = new Object();
        $("#myChannelsList").children().each(function (index, value) {
            var keyName = value.id;
            $(dataObj).attr(keyName, index);
        });
        return dataObj;
    },

    myChannelsListItemDrop: function (event, ui) {
        var self = this;
        $('[name="edit"]', ui.item).show();
        $('[name="delete"]', ui.item).show();
    },

    updateChannelControl: function (channel) {
        $("#channel_" + channel.Id + "_content").html(channel.Name);
    },

    addChannelInternal: function (channel) {
        var valueInCache = this._internalCache[channel.Id.toString()];
        if (!valueInCache) {

            this._internalCache[channel.Id.toString()] = {
                action: "add",
                obj: channel
            };
            channel.AccountId = $('#myAccountId').val();
            var resultElement = this.createNewLiItemFromChannel(channel);
            resultElement.prependTo($("#myChannelsList"));
        } else {
            valueInCache.obj = channel;
        }
    },

    editChannelInternal: function (channel) {
        var valueInCache = this._internalCache[channel.Id.toString()];
        if (!valueInCache) {

            this._internalCache[channel.Id.toString()] = {
                action: "edit",
                obj: channel
            };
        } else {
            valueInCache.obj = channel;
        }
    },

    deleteChannelInternal: function (channel) {
        var valueInCache = this._internalCache[channel.Id.toString()];
        if (!valueInCache) {

            this._internalCache[channel.Id.toString()] = {
                action: "delete",
                obj: channel
            };
        } else {
            if (valueInCache.action == "add") {
                delete this._internalCache[channel.Id.toString()];
            } else {
                if (valueInCache.action == "edit") {
                    valueInCache.action = "delete";
                }
                valueInCache.obj = channel;
            }
        }
    },

    saveChanges: function () {
        var self = this;

        resultData = {};
        var sortingInfo = this.myChannelsListOrderDescriptor();
        for (var key in self._internalCache) {
            var item = self._internalCache[key];
            if (item.action == "add"
                || item.action == "edit"
                || item.action == "delete") {
                var channel = item.obj;
                resultData["action_" + item.action + '_' + channel.Id] = JSON.stringify(channel, null, 2);
            }
        }

        for (var item in sortingInfo) {
            resultData[item] = sortingInfo[item];
        }

        //var result = JSON.stringify(resultData, null, 2);

        var errorMsg = function () {
            alert('Не удалось применить изменения!');
        };

        $.ajax({
            url: '/UpdateTvChannelsOrder.ashx',
            data: resultData,
            type: 'POST',
            success: function (data) {
                var data = eval('(' + data + ')');
                if (data.status) {
                    if (data.status == 'OK') {
                        self.closeChannelsList();
                        document.location = document.location;
                    } else {
                        alert(data.msg);
                    }
                } else {
                    errorMsg();
                }
            }
        })
        .fail(function (xhr, ajaxOptions, thrownError) {
            errorMsg();
        });
    }
}

$(document).ready(function () {
    page = new TvPage();
    page.initializePage();
    $('#divContent')
        .html($('#divContent').html().replace(new RegExp("(^<!--|-->$)", 'g'), ''))
        .css('min-height', '400px')
        .css('height', ($(window).height() - 100) + 'px');
});