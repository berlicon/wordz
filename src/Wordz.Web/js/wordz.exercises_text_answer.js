//---------------------------------------------"Текстовый ответ"-------------------------------------------------//

$(document).ready(function () {
    //обработчик кнопки "Отмена"
    $('#exerciseDoCancelBtn').click(function () {
        $('#exerciseDoDivContainer').arcticmodal('close');
    });

    //обработчик кнопки "Отмена"
    $('#exerciseEditCancelBtn').click(function () {
        $('#exerciseEditDivContainer').arcticmodal('close');
    });

    //обработчик кнопки "Сохранить"
    $('#exerciseSaveBtn').click(function () {
        SaveExercise();
        ReLoadExercises();
    });

    //обработчик кнопки "Проверить"
    $('#exerciseCheckBtn').click(function () {
        CheckExercise();
    });
});

//обработчик кнопки выполнения упражнения
var ExercixeTextAnswerDo = function (id) {
    showFormForDo(id);
};

var showFormForDo = function (id) {
    $("#exerciseDoDivContainer").arcticmodal({
        closeOnEsc: true,
        closeOnOverlayClick: true,
        beforeOpen: function () {
            $('#exerciseDoDivContainer').show();
        },
        afterClose: function (data, el) {
            $('#exerciseDoDivContainer').hide();
        }
    });
};

//сохраняем данные ответа на упражнениe на сервер
var CheckExercise = function () {
    $('#exerciseDoDivContainer').arcticmodal('close');
    $('#exerciseDoDivContainer').appendTo($('form'));
    $("form").ajaxSubmit({
        url: '/CheckExerciseTextAnswerHandler.ashx',        
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
};

//обработчик кнопки создания/редактирования упражнения
var ExercixeTextAnswerEdit = function (id) {
    showFormForEdit(id);
};

var showFormForEdit = function (id) {
    //загружаем данные упражнения
    if (id != 0) {
    var str = "/ExerciseAnswerTextHandler.ashx?id=" + id;
    $.getJSON(str, function (data) {
            if (data.status && data.status == 'ERR') {}
            else{
                nameId.value = data.Name;
                descriptionId.value = data.Description;
                questionTextId.value = data.Text;
            }
        });
    };
    //открываем окно редактирования
    $("#exerciseEditDivContainer").arcticmodal({
        closeOnEsc: true,
        closeOnOverlayClick: true,
        beforeOpen: function () {
            $('#exerciseEditDivContainer').show();
        },
        afterClose: function (data, el) {
            $('#exerciseEditDivContainer').hide();
        }
    });
};

//сохраняем данные упражнения на сервер
var SaveExercise = function () {
    $('#exerciseEditDivContainer').arcticmodal('close');
    $('#exerciseEditDivContainer').appendTo($('form'));
    $("form").ajaxSubmit({
        url: '/UpdateExerciseAnswerTextHandler.ashx',
        success: formCallbackHandler,
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
};

var formCallbackHandler = function (data) {

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

//----------------------------------------------------------------------------------------------//