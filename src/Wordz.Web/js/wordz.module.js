
isInPostBack = false;
DocumentMode = {};
DocumentModeNew = {};
DocumentModeExisting = {};
DefaultCourceLanguageId = 0;
BaseModulePageUrl = '';

formCallbackHandlerModule = function (data) {
    isInPostBack = false;
    
    $('#moduleEditDiv div.editValue :only-child').css('background-color', 'white');

    data = eval('(' + data + ')');

    if (data.status) {
        if (data.status == 'OK') {
            if (DocumentMode == DocumentModeExisting) {
                document.location = document.location;
            } else {
                var createdModuleId = data.returnObject;
                document.location.href = BaseModulePageUrl + createdModuleId;
            }
            //hideFormForEdit();
        } else {
            if (data.returnObject != null && data.returnObject.length > 0) {
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
    else {
        //Неизвестная причина
    }
}

initializePage = function () {

    // Инициализация контрола рейтингов
    $('#rating').rating({
        fx: 'float',
        image: '/img/stars.png',
        loader: '/img/ajax-loader.gif',
        url: '/UpdateRateHandler.ashx',
        type: 'get',
        callback: function (responce) {
        }
    });

    $("#lblModuleEditExerciseList").sortable({
        connectWith: ".connectedSortable1",
        dropOnEmpty: true
    }).disableSelection();

    if (document.isNewModule == true) {
        DocumentMode = DocumentModeNew;
    } else {
        DocumentMode = DocumentModeExisting;
    }



    //Если это новый модуль, то сразу открываем форму
    if (document.isNewModule == true) {
        editModule(true, 0);
    }
}

//Настраиваем форму редактирования
initializeModuleEditForm = function () {

    $('#moduleIdField').attr('name', 'moduleId');
    $('#courseIdField').attr('name', 'courseId');

    $("#moduleEditDiv div textarea")
                    .focus(function () { this.rows = 5; })
                    .blur(function () { this.rows = 2; })
                    .attr("rows", 2);


    var afterSubmit = function () {
        $('#waitBlock').hide();
        $('#inputsBlock').appendTo($('#inputsBlockContainer'));
        setHeightForShow();
    };

    // Настраиваем обработчик кнопки "Сохранить"
    $("#editYesBtn").click(function () {
        isInPostBack = true;
        //hideFormForEdit();
        $('#waitBlock').show();
        $('#inputsBlock').appendTo($('form'));
        $('#validationListContainer').hide();
        setHeightToAuto();
        tinymce.triggerSave();
        $("form").ajaxSubmit({
            //dataType: 'json',
            url: '/UpdateModuleByIdHandler.ashx',
            success: function (data) {
                formCallbackHandlerModule(data);
                afterSubmit();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
                isInPostBack = false;
                afterSubmit();
            }
        });
    });

    // Настраиваем обработчик кнопки "Отмена"
    $("#editNoBtn").click(function () {
        $('#moduleEditDivContainer').arcticmodal('close');
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

    $('#exerciseListId').sortable({ stop: updateExercisesOrder });
}

showFormForEdit = function () {
    $("#moduleEditDivContainer").arcticmodal({
        closeOnEsc: true,
        closeOnOverlayClick: true,
        beforeOpen: function () {
            $('#validationListContainer').hide();
            $('#moduleEditDivContainer').show();
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

            $('#moduleEditDivContainer').hide();
            // уходим на предыдущую страницу
            if (DocumentMode == DocumentModeNew) {
                window.location.href = $('#backUrlBtn').attr('href');
            }
            else {
                //Todo: Временный косталь - нужно будет убрать
                document.location = document.location;
            }
        }
    });
}
hideFormForEdit = function () {
    $("#moduleEditDivContainer").arcticmodal('close');
}

editModule = function (isNew, moduleId) {
    // Запрашиваем словари

    if (!isNew) {
        ReLoadExercises();
    }

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
            //$.getJSON("/GetModuleById.ashx?moduleId=" + moduleId, 

            $.ajax({
                url: "/GetModuleByIdHandler.ashx?moduleId=" + moduleId,
                success: function (data) {
                    var data = eval('(' + data + ')');

                    if (data.status && data.status === 'ERR') {

                    } else {
                        $('#nameId').val(data.returnObject.Name);
                        $('#descriptionId').val(data.returnObject.Description);
                        $('#detailDescriptionId').val(data.returnObject.DetailedDescription);
                        $('#tagsId').val(data.returnObject.Tags);
                        $('#linksId').val(data.returnObject.Links);
                        $('#urlId').val(data.returnObject.Url);
                        $('#priceId').val(data.returnObject.Price);
                        $('#currencyId option[value=' + data.returnObject.CurrencyId + ']').attr('selected', 'selected');
                        $('#isRemovePictureId').attr('checked', false);

                        showHideModuleList(true);

                        showFormForEdit();
                    }
                }
            })
            .fail(function (xhr, ajaxOptions, thrownError) { alert('Fail!' + thrownError); })
        }
    })

    //упражнения

    //Упражнение "выбор"

    // Настраиваем обработчик кнопки "Закрыть"
    $("#exerciseSelectCancelBtn").click(function () {
        $('#ExerciseSelectContrDiv').arcticmodal('close');
    });
    // Настраиваем обработчик кнопки "Сохранить"
    $("#exerciseSelectSaveBtn").click(function () {
        SaveExerciseSelect();
    });
    // Настраиваем обработчик кнопки "Добавить ответ"
    $("#addAnswerBtn").click(function () {
        addAnswerBtnClick();
    });
    // Настраиваем обработчик кнопки "Удалить ответ"
    $("#deleteAnswerBtn").click(function () {
        deleteAnswerBtnClick();
    });

    //Упражнение "Текст"

    // Настраиваем обработчик кнопки "Закрыть"
    $("#exerciseTextCancelBtn").click(function () {
        $('#ExerciseTextContrDiv').arcticmodal('close');
    });
    // Настраиваем обработчик кнопки "Сохранить"
    $("#exerciseTextSaveBtn").click(function () {
        SaveExerciseText();
    });
}

showHideModuleList = function (isShow) {
    var list = $("#lblModuleEditExerciseDiv");
    var selection = list
        .add(list.prev())
        .add(list.next())
        .add(list.next().next());
    if (isShow) {
        selection.show();
    } else {
        selection.hide();
    }
}

setHeightForShow = function () {
    $('#moduleEditDiv').css('height', ($(window).height() - 100) + 'px');
}

setHeightToAuto = function () {
    $('#moduleEditDiv').css('height', 'auto');
}

updateExercisesOrder = function () {
    $("#exerciseListId").children().each(function (index, value) {
        $(':hidden', value).attr('value', index);
    });
}

