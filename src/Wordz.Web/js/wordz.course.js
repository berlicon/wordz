
DocumentModeNew = {};
DocumentModeExisting = {};

DocumentMode = DocumentModeExisting;
BaseCoursePageUrl = '';

DefaultCourceLanguageId = 0;

IsEditFormWasOpened = false;

initializeCourseEditForm = function () {

    $("#courseEditDiv div textarea")
                    .focus(function () { this.rows = 5; })
                    .blur(function () { this.rows = 2; })
                    .attr("rows", 2);

    // Настраиваем обработчик кнопки "Сохранить"
    $("#courseEditYesBtn").click(function () {
        //hideFormForEdit();
        $('#waitBlock').show();
        $('#inputsBlock').appendTo($('form'));

        var afterSubmit = function () {
            $('#waitBlock').hide(); $('#inputsBlock').appendTo($('#inputsBlockContainer'));
        };

        setHeightToAuto();
        $('#validationListContainer').hide();

        tinymce.triggerSave();

        $("form").ajaxSubmit({
            //dataType: 'json',
            url: '/UpdateCourseById.ashx',
            success: function (data) {
                formCallbackHandler(data);
                afterSubmit()
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
                afterSubmit();
            }
        });
    });

    // Настраиваем обработчик кнопки "Отмена"
    $("#courseEditNoBtn").click(function () {
        $('#courseEditDivContainer').arcticmodal('close');
    });

    //Обработчи выбора чекбокса "Удалить логотип"
    $('#isRemovePictureId').click(function () {
        var isChecked = $('#isRemovePictureId').is(':checked');
        if (isChecked) {
            $('#pictureId').parent().hide();
            $('#pictureId').parent().prev().hide();
            var control = $("#pictureId");
            control.replaceWith(control = control.clone(true));
        } else {
            $('#pictureId').parent().show();
            $('#pictureId').parent().prev().show();
        }
    });
}

initializePage = function () {

    payForModuleClick = function (moduleId, price, currencyId) {
        var c = $('<div id="payForModuleDivId" style="text-align: center;" class="box-modal"><img id="payForModuleImg" src="/img/ajax-loader-big.gif" /><div id="payForModuleContainer"></div></div>');
        c.prepend('<div id="payForModuleCloseId" style="display: none;" class="box-modal_close arcticmodal-close">X</div>');

        $.arcticmodal({
            content: c,
            closeOnEsc: false,
            closeOnOverlayClick: false,
            afterClose: function (data, el) {
                document.location = document.location;
            }
        });

        $.ajax(
            {
                type: "GET",
                url: "/PaymentForModule.ashx?id=" + moduleId + "&price=" + price + "&currencyId=" + currencyId,
                success: function (data) {
                    var data = eval('(' + data + ')');
                    $('#payForModuleImg').hide();
                    $('#payForModuleContainer').html(data.msg);
                    if (data.status === 'OK') {
                        $('#payForModuleContainer').css('color', '#2AF00A');
                        //Зарефрешить страниц через 3 секунды
                        setTimeout(function () { document.location = document.location; }, 3000);
                    } else {
                        $('#payForModuleContainer').css('color', '#FFA17F');
                    }
                    $('#payForModuleCloseId').css('display', 'block');
                },
                error: function () {
                    $('#payForModuleImg').hide();
                    $('#payForModuleContainer').html("Не удалось получить данные с сервера! пожалуйста, обновите страницу и попробуйте еще раз.");
                    $('#payForModuleCloseId').css('display', 'block');
                }
            }
            );
    }

    payForWholeCourse = function (courseId, price, currencyId) {
        var c = $('<div id="payForModuleDivId" style="text-align: center;" class="box-modal"><img id="payForModuleImg" src="/img/ajax-loader-big.gif" /><div id="payForModuleContainer"></div></div>');
        c.prepend('<div id="payForModuleCloseId" style="display: none;" class="box-modal_close arcticmodal-close">X</div>');

        $.arcticmodal({
            content: c,
            closeOnEsc: false,
            closeOnOverlayClick: false,
            afterClose: function (data, el) {
                document.location = document.location;
            }
        });

        $.ajax(
            {
                type: "GET",
                url: "/PaymentForCourse.ashx?id=" + courseId + "&price=" + price + "&currencyId=" + currencyId,
                success: function (data) {
                    var data = eval('(' + data + ')');
                    $('#payForModuleImg').hide();
                    $('#payForModuleContainer').html(data.msg);
                    if (data.status === 'OK') {
                        $('#payForModuleContainer').css('color', '#2AF00A');
                        //Зарефрешить страниц через 3 секунды
                        setTimeout(function () { document.location = document.location; }, 3000);
                    } else {
                        $('#payForModuleContainer').css('color', '#FFA17F');
                    }
                    $('#payForModuleCloseId').css('display', 'block');
                },
                error: function () {
                    $('#payForModuleImg').hide();
                    $('#payForModuleContainer').html("Не удалось получить данные с сервера! пожалуйста, обновите страницу и попробуйте еще раз.");
                    $('#payForModuleCloseId').css('display', 'block');
                }
            }
            );
    }

    showBusyIndicator = function () {
        var c = $('<div id="waitDialog" style="text-align: center;" class="box-modal"><img id="waitDialogImg" src="/img/ajax-loader-big.gif" /></div>');
        $.arcticmodal({
            content: c,
            closeOnEsc: false,
            closeOnOverlayClick: false
        });
    }

    showFormForEdit = function () {
        $("#courseEditDivContainer").arcticmodal({
            closeOnEsc: true,
            closeOnOverlayClick: true,
            beforeOpen: function () {
                $('#validationListContainer').hide();
                $('#courseEditDivContainer').show();
                setHeightForShow();
            },
            afterOpen: function () {
                tinymce.init({ selector: 'textarea#detailDescriptionId',
                    plugins: [
                    "textcolor advlist autolink lists link image charmap print preview anchor textcolor",
                    "searchreplace visualblocks code fullscreen",
                    "insertdatetime media table contextmenu paste"
                    ],
                    width: $('#detailDescriptionId').width(),
                    resize: "both",
                    toolbar1: "bold italic underline forecolor backcolor table",
                    toolbar2: "alignleft aligncenter alignright alignjustify formatselect",
                    toolbar3: "fontselect fontsizeselect",
                    toolbar4: "cut copy paste undo redo removeformat"
                });
            },
            afterClose: function (data, el) {
                tinymce.remove('textarea#detailDescriptionId');

                $('#courseEditDivContainer').hide();
                // уходим на предыдущую страницу
                if (DocumentMode == DocumentModeNew) {
                    window.location.href = $('#backUrlBtn').attr('href');
                }
            }
        });
    }

    hideFormForEdit = function () {
        $("#courseEditDivContainer").arcticmodal('close');
    }

    editCourse = function (isNew, courseId) {

        if (isNew) {
            DocumentMode = DocumentModeNew;
        } else {
            DocumentMode = DocumentModeExisting;
        }

        // Запрашиваем словари
        $.getJSON("/GetCategories.ashx", function (data) {

            var categories = $('#categoryId');
            categories.html("");
            $.each(data, function (item) {
                categories.append($("<option />").val(this.Id).text(this.Name));
            });

            $.getJSON("/GetLanguages.ashx", function (data) {
                var lang = $('#languageId');
                lang.html('');
                $.each(data, function (item) {
                    lang.append($("<option />").val(this.Id).text(this.Name));
                });


                $.getJSON("/GetCurrencies.ashx", function (data) {
                    var currency = $('#currencyId');
                    currency.html('');
                    $.each(data, function (item) {
                        currency.append($("<option />").val(this.Id).text(this.Name + '(' + this.LetterCode + ')'));
                    });

                    if (isNew) {
                        showHideModuleList(false);

                        $('#languageId option[value=' + DefaultCourceLanguageId + ']').attr('selected', 'selected');

                        showFormForEdit();
                    }
                    else {
                        $.getJSON("/GetCourseById.ashx?id=" + courseId, function (data) {
                            if (data.status && data.status === 'ERR') {

                            }
                            else {
                                $('#nameId').val(data.Name);
                                $('#descriptionId').val(data.Description);
                                $('#detailDescriptionId').val(data.DetailedDescription);
                                $('#authorsId').val(data.Authors);
                                $('#priceId').val(data.Price);
                                $('#currencyId option[value=' + data.CurrencyId + ']').attr('selected', 'selected');
                                $('#languageId option[value=' + data.UILanguageId + ']').attr('selected', 'selected');
                                $('#categoryId option[value=' + data.CategoryId + ']').attr('selected', 'selected');
                                $('#contactsId').val(data.Contacts);
                                $('#tagsId').val(data.Tags);
                                $('#linksId').val(data.Links);
                                $('#urlId').val(data.Url);
                                $('#adsenceId').val(data.GoogleAdvertisment);
                                $('#isPublicId').attr('checked', data.IsPublic == true);
                                $('#isEditableId').attr('checked', data.IsEditable == true);
                                $('#isCopyableId').attr('checked', data.IsCopied == true);
                                $('#isRemovePictureId').attr('checked', false);
                                $('#firstPasswordId').val(data.Password);

                                showHideModuleList(true);
                                var myList = $("#moduleSortableList");
                                myList.html('');

                                for (var i = 0; i < data.Modules.length; i++) {
                                    var module = data.Modules[i];
                                    var elementId = 'module_' + module.Id;
                                    var elementText = '<li class="ui-state-default">';
                                    elementText += '<input type="hidden" name="' + elementId + '" value="' + i + '"/>';

                                    elementText += '<div style="overflow: auto;"><div class="sortableItemDivleft">';
                                    elementText += module.Name;
                                    elementText += '</div>';
                                    elementText += '<div class="sortableItemDivRight">';
                                    elementText += '<button style="color: red">X</button>';
                                    elementText += '</div>';
                                    elementText += '</div>';

                                    elementText += '</li>';
                                    var elementItem = $(elementText);
                                    $('button', elementItem).click(deleteModuleFunction(module.Id, elementItem));
                                    elementItem.appendTo(myList);
                                }

                                $('input[name=moduleDeletedListId]').val('');

                                $('#addNewModuleContainer').html('<a href="/Module/new/' + courseId + '">Добавить новый модуль</a>');

                                showFormForEdit();
                            }
                        });
                    }
                });
            });
        }
            )
    }

    showHideModuleList = function (isShow) {
        var list = $("#moduleSortableList").parent().parent();
        var selection = list
                        .add(list.prev());
        if (isShow) {
            selection.show();
        } else {
            selection.hide();
        }
    }

    updateModuleOrder = function () {
        $("#moduleSortableList").children().each(function (index, value) {
            $(':hidden', value).attr('value', index);
        });
    }

    formCallbackHandler = function (data) {
        //showFormForEdit();
        $('#courseEditDiv div.editValue').css('background-color', 'white');

        data = eval('(' + data + ')');

        if (data.status) {
            if (data.status == 'OK') {
                //Все ок

                if (DocumentMode == DocumentModeExisting) {
                    document.location = document.location;
                } else {
                    var createdCourceId = data.returnObject;
                    document.location.href = BaseCoursePageUrl + createdCourceId;
                }

                //hideFormForEdit();
            } else {
                if (data.returnObject != null) {
                    if (data.returnObject.length > 0) {
                        var validationContainer = $('#validationListContainer');
                        validationContainer.fadeOut('fast');
                        $('ul', validationContainer).html('');

                        $.each(data.returnObject, function () {
                            var elementId = this.PropertyName + "Id";
                            var message = this.Message;

                            $('<li> - ' + message + '</li>').appendTo($('ul', validationContainer));

                            //$('#' + elementId).parent().css('background-color', 'red');
                        });

                        validationContainer.fadeIn('fast');
                        return;
                    }
                }
            }
        }
        else {
            //Неизвестная причина
        }
    }

    setHeightForShow = function () {
        $('#courseEditDiv').css('height', ($(window).height() - 100) + 'px');
    }

    setHeightToAuto = function () {
        $('#courseEditDiv').css('height', 'auto');
    }

    approveCourse = function (courseId) {
        $.ajax(
            {
                type: "GET",
                url: "/ApproveCourse.ashx?courseId=" + courseId,
                success: function (data) {
                    var data = eval('(' + data + ')');
                    if (data.status === 'OK') {
                        alert(data.msg);
                    } else {
                        alert('Возникла сетевая ошибка! Обновите страницу!');
                    }
                    document.location = document.location;
                },
                error: function () {
                    alert('Возникла сетевая ошибка! Обновите страницу!');
                    document.location = document.location;
                }
            }
        )
    }

    deleteModuleFunction = function (moduleId, liElement) {
        return function () {
            if (confirm('Действительно удалить модуль?')) {
                liElement.append('<input type="hidden" name="module_delete_' + moduleId + '" value="1"/>');
                var delItems = $('input[name=moduleDeletedListId]').val();
                $('input[name=moduleDeletedListId]').val(delItems + ';' + moduleId);
                liElement.remove();
            }
        }
    }

    $('div.listElementLink').each(function (index) {
        var moduleUrl = $(this).attr('data-module-url');
        if (moduleUrl && moduleUrl != '') {
            var alertMessage = $(this).attr('data-module-alert-msg');
            if (alertMessage && alertMessage != "") {

                var alertMessage = $(this).attr('data-module-alert-msg');

                $(this).click(function () {
                    alert(alertMessage);
                });

            } else {

                $(this).click(function () {
                    window.location = moduleUrl;
                });

            }
        }
    });

    $('#rating').rating({
        fx: 'float',
        image: '/img/stars.png',
        loader: '/img/ajax-loader.gif',
        url: '/UpdateRateHandler.ashx',
        type: 'get',
        callback: function (responce) {
        }
    })

    $('.module_item_rating').rating({
        fx: 'float',
        image: '/img/stars.png',
        loader: '/img/ajax-loader.gif',
        readOnly: true,
        callback: function (responce) {
        }
    })

    $("#moduleSortableList").sortable({ stop: updateModuleOrder });
}

errorUserMessageInitMethod = function () {
    $("#errorUserMessageTextContainer").arcticmodal({
        closeOnEsc: true,
        closeOnOverlayClick: true,
        beforeOpen: function () {
            $("#errorUserMessageTextContainer").show();
        },
        afterClose: function (data, el) {
            $("#errorUserMessageTextContainer").hide();
            window.location.href = '/';
        }
    });
}

errorIncorrectPasswordMessageInitMethod = function (courceId) {
    var initMethod = function () {
        $('#incorrectPasswordRuntimeMessageText').hide();
        $.ajax({
            url: 'CheckCoursePassword.ashx',
            data: { courseId: courceId, password: $('#storedPasswordTextBox').val() }
        })
            .done(function (data) {
                var data = eval('( ' + data + ' )');
                if (data && data.status == 'OK') {
                    document.location = document.location;
                } else {
                    $('#storedPasswordTextBox').val('');
                    $('#incorrectPasswordRuntimeMessageText').text('Неверный пароль!').show('fast');
                }
            });
    }

    $('#storedPasswordOkBtn').click(function () {
        initMethod();
    });

    $('#storedPasswordCancelBtn').click(function () {
        $("#errorIncorrectPasswordMessageContainer").arcticmodal('close');
    });

    $('#storedPasswordTextBox').keypress(function (event) {
        if (event.which == 13) {
            initMethod();
            event.preventDefault();
        }
    });

    $("#errorIncorrectPasswordMessageContainer").arcticmodal({
        closeOnEsc: true,
        closeOnOverlayClick: true,
        beforeOpen: function () {
            $("#errorIncorrectPasswordMessageContainer").show();
        },
        afterClose: function (data, el) {
            $("#errorIncorrectPasswordMessageContainer").hide();
            window.location.href = '/';
        }
    });
}
