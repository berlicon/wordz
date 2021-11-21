$(document).ready(function () {
    //обработчик кнопки "Отмена"
    $('#exerciseSelectTextCancelBtn').click(function () {
        $('#ExerciseSelectTextContrDiv').arcticmodal('close');
    });

    //обработчик кнопки "Сохранить"
    $('#exerciseSelectTextSaveBtn').click(function () {
        SaveExerciseSelectText();
        ReLoadExercises();
    });
});

var OpenExerciseSelectText = function (id) {
    //загружаем данные упражнения
    if (id != 0) {
        var str = "/ExerciseSelectTextGetHandler.ashx?id=" + id;
        $.getJSON(str, function (data) {
            if (data.status && data.status == 'ERR') { }
            else {
                exerciseSelectTextExerciseId.value = data.Id;
                exerciseSelectTextExerciseModuleId.value = data.ModuleId;
                exerciseSelectTextNameId.value = data.Name;
                exerciseSelectTextDescriptionId.value = data.Description;
                exerciseSelectTextQuestionTextId.value = data.Text;
            }
        });
    };
    //открываем окно 
    $('#ExerciseSelectTextContrDiv').arcticmodal({
        closeOnEsc: true,
        closeOnOverlayClick: true,
        beforeOpen: function () {
            $('#ExerciseSelectTextContrDiv').show();
        },
        afterClose: function (data, el) {
            $('#ExerciseSelectTextContrDiv').hide();
        }
    });
}


////обработчик кнопки выполнения упражнения
//var ExercixeSelectTextDo = function (id) {
//    $('#answerDoId').hide();
//    showFormForDo(id);
//};

//var showFormForDo = function (id) {
//    $("#exerciseDoDivContainer").arcticmodal({
//        closeOnEsc: true,
//        closeOnOverlayClick: true,
//        beforeOpen: function () {
//            $('#exerciseDoDivContainer').show();
//        },
//        afterClose: function (data, el) {
//            $('#exerciseDoDivContainer').hide();
//        }
//    });
//};

////обработчик кнопки создания/редактирования упражнения
//var ExercixeSelectTextEdit = function (id) {
//    showFormForEdit(id);
//};

//var showFormForEdit = function (id) {
//    //загружаем данные упражнения
//    if (id != 0) {
//        var str = "/ExerciseSelectTextGetHandler.ashx?id=" + id;
//        $.getJSON(str, function (data) {
//            if (data.status && data.status == 'ERR') { }
//            else {
//                nameEditId.value = data.Name;
//                descriptionEditId.value = data.Description;
//                questionTextEditId.value = data.Text;
//            }
//        });
//    };
//    //открываем окно редактирования
//    $("#exerciseEditDivContainer").arcticmodal({
//        closeOnEsc: true,
//        closeOnOverlayClick: true,
//        beforeOpen: function () {
//            $('#exerciseEditDivContainer').show();
//        },
//        afterClose: function (data, el) {
//            $('#exerciseEditDivContainer').hide();
//        }
//    });
//};

//сохраняем данные упражнения на сервер
var SaveExerciseSelectText = function () {
    $('#ExerciseSelectTextContrDiv').arcticmodal('close');
    $('#ExerciseSelectTextContrDiv').appendTo($('form'));
    $("form").ajaxSubmit({
        url: '/ExerciseSelectTextAddHandler.ashx',
        success: formSelectTextCallbackHandler,
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
};

var formSelectTextCallbackHandler = function (data) {

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

////проверка ответа
//var CheckExercise = function () {
//    $('#answerDoId').show();
//};
