
FilmPage = function () {
}

FilmPage.prototype = {

    _accountId: 0,

    _isSomethingChanged: false,

    _addingIndex: -1,

    _internalCache: {},

    _baseFilmImagePath: '/',

    initializePage: function () {
        var self = this;
        this.initializeFilmEditForm();

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

        self._baseFilmImagePath = $('#baseFilmImagePath').html();
        self._accountId = $('#myAccountId').val();
    },

    initializeFilmEditForm: function () {
        var self = this;
        $("#filmEditDiv div textarea")
                    .focus(function () { this.rows = 5; })
                    .blur(function () { this.rows = 2; })
                    .attr("rows", 2);

        var channelsCategoriesQueryUrl = '/GetFilmCategories.ashx';

        $.ajax({
            url: channelsCategoriesQueryUrl,
            success: function (data) {
                var data = eval('(' + data + ')');
                if (data.status == 'OK') {
                    var optionHtml = '';
                    for (var i = 0; i < data.returnObject.length; i++) {
                        var category = data.returnObject[i];
                        optionHtml += '<option value=' + category.Id + '>' + category.Name + '</option>';
                    }
                    $('#categoryId').html(optionHtml);

                } else {
                    alert('Не удалось получить сведения о категориях!');
                }
            }
        });

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

            $('#filmEditDivContainer').arcticmodal('close');

        });
        // Настраиваем обработчик кнопки "Отмена"
        $("#editNoBtn").click(function () {
            $('#filmEditDivContainer').arcticmodal('close');
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
        $('#filmEditDiv div.editValue :only-child').css('background-color', 'white');

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

    editFilm: function () {

    },
    refreshMyChannelListData: function (afterMethod, onFail) {
        var self = this;
        var channelsQueryUrl = '/GetFilmChannelsList.ashx';
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
        var channelsQueryUrl = '/GetHidedFilmChannelsList.ashx';
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
        var channelsQueryUrl = '/GetFilmChannelsFromOtherUsers.ashx?searchText=' + searchText;
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
        $("#filmListDivContainer").arcticmodal('close');
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
                    elementText += '<img src="/img/FilmChannels/' + channel.ImageUrl + '" />';
                }
                elementText += '</div>';
                elementText += '<div class="sortableItemDivRight>"';
                elementText += '<img src="/img/FilmChannels/' + channel.ImageUrl + '" />';
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
//        if (channel.ImageUrl) {
//            var baseImagePath = !channel.AccountId ? self._baseFilmImagePath : '';
//            elementText += '<img src="' + baseImagePath + channel.ImageUrl + '" />';
//        }
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
        elementText += '<div class="smallItemText">' + channel.CategoryName + '</div>'
        elementText += '</div></div>';
        elementText += '</li>';
        var resultElement = $(elementText);

        $('[name="delete"]', resultElement).click(function () { self.onDeleteChannelButtonClick(elementId, channel) });
        return resultElement;
    },

    showChannelsListForm: function () {
        var self = this;
        $("#filmListDivContainer").arcticmodal({
            closeOnEsc: true,
            closeOnOverlayClick: true,
            beforeOpen: function () {
                $('#filmListDivContainer').show();
                $('#divContent').hide();
            },
            afterClose: function (data, el) {
                $('#filmListDivContainer').hide();
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

    loadDataForChannel: function (filmId) {
        var self = this;
        $.ajax({
            url: "/GetFilmChannelById.ashx?filmId=" + filmId,
            success: function (data) {
                var data = eval('(' + data + ')');

                if (!data.status || data.status && data.status === 'ERR') {
                    self.closeEditForm();
                    //alert();
                } else {
                    var film = data.returnObject;
                    self.updateEditFieldFromObject(film);
                }
            }
        })
        .fail(function (xhr, ajaxOptions, thrownError) {
            self.closeEditForm(); /*alert('Fail!' + thrownError);*/
        })
    },

    updateEditFieldFromObject: function (film) {
        $('#nameId').val(film.Name);
        $('#descriptionId').val(film.Description);
        $('#embededCodeId').val(film.PlayerCode);
        $('#imageUrlId').val(film.ImageUrl);
        $('#isEditableId').attr('checked', film.IsEditable == true);
        $('#categoryId option[value=' + film.CategoryId + ']').attr('selected', 'selected');
        $("#editYesBtn").show();
        this.setReadonlyMode(false);
    },

    loadDataOrGetFromCache: function (filmId) {
        var valueInCache = this._internalCache[filmId.toString()];
        if (valueInCache) {
            this.updateEditFieldFromObject(valueInCache.obj);
        } else {
            this.loadDataForChannel(filmId);
        }
    },

    fillViewFromCacheIfExists: function (filmId) {
        var valueInCache = this._internalCache[filmId.toString()];
        if (valueInCache) {
            this.updateEditFieldFromObject(valueInCache.obj);
        }
    },

    validateChannel: function (channel) {
        var validationList = [];
        if (!channel.Name || channel.Name == "") {
            validationList.push("Необходимо ввести название фильма!");
        }
        if (channel.Name && channel.Name.length > 20) {
            validationList.push("Название фильма должно быть не более 20 символов!");
        }
        if (channel.Description && channel.Description.length > 500) {
            validationList.push("Описание должно быть не более 500 символов!");
        }
        if (channel.ImageUrl && channel.ImageUrl.length > 50) {
            validationList.push("Путь к изображению должен быть меньше 50 символов!");
        }
        if (channel.PlayerCode && channel.PlayerCode.length > 2000) {
            validationList.push("Код плеера должен быть меньше 2000 символов!");
        }
        if (!channel.PlayerCode || channel.PlayerCode == "") {
            validationList.push("Необходимо указать код плеера!");
        }

        return validationList;
    },

    getObjectFromEditForm: function () {
        var retObj = {};
        retObj.Id = $('#channelId').val();
        retObj.Name = $('#nameId').val();
        retObj.Description = $('#descriptionId').val();
        retObj.PlayerCode = $('#embededCodeId').val();
        retObj.ImageUrl = $('#imageUrlId').val();
        retObj.IsEditable = true;
        retObj.CategoryId = $('#categoryId option:selected').val();
        return retObj;
    },

    resetFormFields: function () {
        $('#nameId').val('');
        $('#descriptionId').val('');
        $('#embededCodeId').val('');
        $('#imageUrlId').val('');
        $('#isEditableId').attr('checked', 'checked');
        $('#categoryId option:first').attr('selected', 'selected');
    },

    setReadonlyMode: function (isReadonly) {
        $('#nameId').attr('readOnly', isReadonly);
        $('#descriptionId').attr('readOnly', isReadonly);
        $('#embededCodeId').attr('readOnly', isReadonly);
        $('#imageUrlId').attr('readOnly', isReadonly);
        $('#isEditableId').attr('readOnly', isReadonly);
        $('#categoryId').attr('readOnly', isReadonly);
    },


    openEditForm: function (filmId) {
        //Прописываем filmid
        $('#channelId').attr('value', filmId);

        this.resetFormFields();

        if (filmId > 0) {
            this.setReadonlyMode(true);
            $("#editYesBtn").hide();
            this.loadDataOrGetFromCache(filmId);
        } else {
            this.setReadonlyMode(false);
            $("#editYesBtn").show();
            this.fillViewFromCacheIfExists(filmId);
        }

        $("#filmEditDivContainer").arcticmodal({
            closeOnEsc: true,
            closeOnOverlayClick: true,
            beforeOpen: function () {
                $('#filmEditDivContainer').show();
                $('#validationListContainer').hide();
            },
            afterClose: function (data, el) {
                $('#filmEditDivContainer').hide();
            }
        });
    },

    closeEditForm: function () {
        $("#filmEditDivContainer").arcticmodal('close');
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
            url: '/UpdateFilmChannels.ashx',
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
    page = new FilmPage();
    page.initializePage();
    $('#divContent')
        .html($('#divContent').html().replace(new RegExp("(^<!--|-->$)", 'g'), ''))
        .css('min-height', '400px')
        .css('height', ($(window).height() - 100) + 'px');
});