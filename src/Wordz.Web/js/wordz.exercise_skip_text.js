$(document).ready(function () {
    //обработчик кнопки "Отмена"
    $('#exerciseSkipTextCancelBtn').click(function () {
        $('#ExerciseSkipTextContrDiv').arcticmodal('close');
    });

    //обработчик кнопки "Сохранить"
    $('#exerciseSkipTextSaveBtn').click(function () {
        SaveExerciseSkipText();
        ReLoadExercises();
    });
});

var OpenExerciseSkipText = function (id) {
    //загружаем данные упражнения
    if (id != 0) {
        var str = "/ExerciseSkipTextGetHandler.ashx?id=" + id;
        $.getJSON(str, function (data) {
            if (data.status && data.status == 'ERR') { }
            else {
                exerciseSkipTextExerciseId.value = data.Id;
                exerciseSkipTextExerciseModuleId.value = data.ModuleId;
                exerciseSkipTextNameId.value = data.Name;
                exerciseSkipTextDescriptionId.value = data.Description;
                exerciseSkipTextQuestionTextId.value = data.Text;
            }
        });
    };
    //открываем окно 
    $('#ExerciseSkipTextContrDiv').arcticmodal({
        closeOnEsc: true,
        closeOnOverlayClick: true,
        beforeOpen: function () {
            $('#ExerciseSkipTextContrDiv').show();
        },
        afterClose: function (data, el) {
            $('#ExerciseSkipTextContrDiv').hide();
        }
    });
}

//var showFormForEdit = function (id) {
//    //загружаем данные упражнения
//    if (id != 0) {
//        var str = "/ExerciseSkipTextGetHandler.ashx?id=" + id;
//        $.getJSON(str, function (data) {
//            if (data.status && data.status == 'ERR') { }
//            else {
//                exerciseSkipTextNameId.value = data.Name;
//                exerciseSkipTextDescriptionId.value = data.Description;
//                exerciseSkipTextQuestionTextId.value = data.Text;
//            }
//        });
//    };
//    //открываем окно редактирования
//    $("#ExerciseSkipTextContrDiv").arcticmodal({
//        closeOnEsc: true,
//        closeOnOverlayClick: true,
//        beforeOpen: function () {
//            $('#ExerciseSkipTextContrDiv').show();
//        },
//        afterClose: function (data, el) {
//            $('#ExerciseSkipTextContrDiv').hide();
//        }
//    });
//};

//сохраняем данные упражнения на сервер
var SaveExerciseSkipText = function () {
    $('#ExerciseSkipTextContrDiv').arcticmodal('close');
    $('#ExerciseSkipTextContrDiv').appendTo($('form'));
    $("form").ajaxSubmit({
        url: '/ExerciseSkipTextAddHandler.ashx',
        success: formSkipTextCallbackHandler,
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
};

var formSkipTextCallbackHandler = function (data) {

    data = eval('(' + data + ')');

    if (data.status) {
        if (data.status == 'OK') {
            //Все ок
            if (data.msg == "Save") {
                window.location.href = window.location.href + '?Id=' + data.returnObject.Id
            }
        } else {
            if (data.returnObject != null) {
                $.each(data.returnObject, function () {
                    var elementId = this.PropertyName + "Id";
                    var message = this.Message + "Id";
                    $('#' + elementId).parent().css('background-color', 'red');
                });
            }
        }
    }
    else {
        //Неизвестная причина
    }
}
