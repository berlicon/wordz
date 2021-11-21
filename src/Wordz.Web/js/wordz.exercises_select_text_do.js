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

var CheckExercise = function () {
    var wrongAnswerCount = 0;
    $('#exerciseDoDiv select option:selected').each(function (index) {
        if ($(this).attr('value') == 0) {
            wrongAnswerCount++;
        }
    });
    if (wrongAnswerCount == 0) {
        alert('Правильно!');
    } else {
        alert('Неправильно!');
    }
};