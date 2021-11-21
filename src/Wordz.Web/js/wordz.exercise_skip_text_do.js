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
    var newNode = document.getElementById('userAnswerDiv').cloneNode(true);
    $('#userAnswerDiv input').each(function (index) {
        var oldElement = $(this);
        var newElement = $(this).attr('value');
        var oldText = oldElement[0];
        var newText = document.createTextNode(newElement);
        oldText.parentNode.replaceChild(newText, oldText);
    });
    var answer = $.trim(document.getElementById('answerDoId').value);
    var userAnswer = $.trim(document.getElementById('userAnswerDiv').innerHTML);
    if (answer == userAnswer) {
        alert('Правильно!');
    } else {
        alert('Неправильно!');
        var oldDiv = document.getElementById('userAnswerDiv');
        oldDiv.parentNode.replaceChild(newNode, oldDiv);
    }
};