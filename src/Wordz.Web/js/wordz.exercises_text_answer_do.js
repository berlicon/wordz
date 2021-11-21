$(document).ready(function () {
    //обработчик кнопки "Отмена"
    $('#exerciseDoCancelBtn').click(function () {
        var module = document.getElementById('ModuleId');
        window.location = "../Module/" + module.getAttribute('value');
    });

    //обработчик кнопки "Проверить"
    $('#exerciseCheckBtn').click(function () {
        CheckExercise();
    });
});

//сохраняем данные ответа на упражнениe на сервер
var CheckExercise = function () {
    $('#exerciseDoDivContainer').appendTo($('form'));
    $("form").ajaxSubmit({
        url: '/CheckExerciseTextAnswerHandler.ashx',
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
    var module = document.getElementById('ModuleId');
    window.location = "../Module/" + module.getAttribute('value');
};