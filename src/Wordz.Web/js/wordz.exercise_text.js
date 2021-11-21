var OpenExerciseText = function (id) {
    //загружаем данные упражнения
    if (id != 0) {

        var str = "/ExerciseTextGetHandler.ashx?id=" + id;
        $.getJSON(str, function (data) {
            if (data.status && data.status == 'ERR') { }
            else {
                textExerciseId.value = data.Id;
                textExerciseModuleId.value = data.ModuleId;
                textExerciseNameId.value = data.Name;
                textExerciseDescriptionId.value = data.Description;
                textExerciseTextId.value = data.Text;
            }
        });
    };
    //открываем окно 
    $('#ExerciseTextContrDiv').arcticmodal({
        closeOnEsc: true,
        closeOnOverlayClick: true,
        beforeOpen: function () {
            $('#ExerciseTextContrDiv').show();
        },
        afterClose: function (data, el) {
            $('#ExerciseTextContrDiv').hide();
        }
    });
}

//обработчик кнопки создания/редактирования упражнения
var ExerciseTextEdit = function (id) {
    showFormExerciseTextForEdit(id);
};

var showFormExerciseTextForEdit = function (id) {
    //загружаем данные упражнения
    if (id != 0) {
        var str = "/ExerciseTextGetHandler.ashx?id=" + id;
        $.getJSON(str, function (data) {
            if (data.status && data.status == 'ERR') { }
            else {
                textExerciseId.value = data.Id;
                textExerciseModuleId.value = data.ModuleId;
                nameEditId.value = data.Name;
                descriptionEditId.value = data.Description;
                questionTextEditId.value = data.Text;
            }
        });
    };
    //открываем окно редактирования
    $("#ExerciseTextContrDiv").arcticmodal({
        closeOnEsc: true,
        closeOnOverlayClick: true,
        beforeOpen: function () {
            $('#ExerciseTextContrDiv').show();
        },
        afterClose: function (data, el) {
            $('#ExerciseTextContrDiv').hide();
        }
    });
};

//сохраняем данные упражнения на сервер
var SaveExerciseText = function () {
    $('#ExerciseTextContrDiv').arcticmodal('close');
    $('#ExerciseTextContrDiv').appendTo($('form'));
    $("form").ajaxSubmit({
        url: '/ExerciseTextAddHandler.ashx',
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
            //if (data.msg == "Save") {
               // window.location.href = window.location.href + '?Id=' + data.returnObject.Id
            //}
            ReLoadExercises();
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