$(document).ready(function () {
    //обработчик кнопки "Отмена"
    $('#exerciseAnswerTextDoCancelBtn').click(function () {
        $('#ExerciseAnswerTextContrDiv').arcticmodal('close');
    });

    //обработчик кнопки "Отмена"
    $('#exerciseAnswerTextCancelBtn').click(function () {
        $('#ExerciseAnswerTextContrDiv').arcticmodal('close');
    });

    //обработчик кнопки "Сохранить"
    $('#exerciseAnswerTextSaveBtn').click(function () {
        SaveAnswerTextExercise();
        ReLoadExercises();
    });

    //обработчик кнопки "Проверить"
    $('#exerciseAnswerTextCheckBtn').click(function () {
        CheckAnswerTextExercise();
    });
});

var OpenExerciseAnswerText = function (id) {
    //загружаем данные упражнения
    if (id != 0) {
        var str = "/ExerciseAnswerTextHandler.ashx?id=" + id;
        $.getJSON(str, function (data) {
            if (data.status && data.status == 'ERR') { }
            else {
                editExerciseAnswerTextId.value = data.Id;
                editExerciseAnswerTextModuleId.value = data.ModuleId;
                exerciseAnswerTextNameId.value = data.Name;
                exerciseAnswerTextDescriptionId.value = data.Description;
                exerciseAnswerTextQuestionTextId.value = data.Text;
            }
        });
    };
    //открываем окно 
    $('#ExerciseAnswerTextContrDiv').arcticmodal({
        closeOnEsc: true,
        closeOnOverlayClick: true,
        beforeOpen: function () {
            $('#ExerciseAnswerTextContrDiv').show();
        },
        afterClose: function (data, el) {
            $('#ExerciseAnswerTextContrDiv').hide();
        }
    });
}


//сохраняем данные упражнения на сервер
var SaveAnswerTextExercise = function () {
    $('#ExerciseAnswerTextContrDiv').arcticmodal('close');
    $('#ExerciseAnswerTextContrDiv').appendTo($('form'));
    $("form").ajaxSubmit({
        url: '/ExerciseAnswerTextAddHandler.ashx',
        success: formAnswerTextCallbackHandler,
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
};

var formAnswerTextCallbackHandler = function (data) {

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