var ReLoadExercises = function () {
    //берем Id модуля, для которого делаем загрузку упражнений.
    var modId = (document.getElementById('moduleIdField')).value;
    if (modId == 0) {
        return;
    }
    //очищаем весь список упражнений
    //    var node = document.getElementById('lblModuleEditExerciseDiv');
    //    while (node.hasChildNodes()) {
    //        node.removeChild(node.firstChild);
    //    }
    $('#exerciseListId').html('');

    //получаем список упражнений
    $.ajax({
        url: '/ExercisesListForModuleGetHandler.ashx?moduleId=' + modId,
        success: function (data) {
            var data = eval('(' + data + ')');
            //заполняем таблицу с упражнениями
            //$('#lblModuleEditExerciseDiv').append('<ul id=\"exerciseListId\" class=\"connectedSortableExercises\">');
            for (var i in data) {
                switch (data[i].ExerciseType) {
                    case 1:
                        addRowForExerciseList('OpenExerciseText', data[i]);
                        break;
                    case 2:
                        addRowForExerciseList('OpenExerciseSelect', data[i]);
                        break;
                    case 3:
                        addRowForExerciseList('OpenExerciseAnswerText', data[i]);
                        break;
                    case 4:
                        addRowForExerciseList('OpenExerciseSelectText', data[i]);
                        break;
                    case 5:
                        addRowForExerciseList('OpenExerciseSkipText', data[i]);
                        break;
                    default:
                        break;
                }
            };
            //$('#lblModuleEditExerciseDiv').append('</ul>');
        }
    })
    .fail(function (xhr, ajaxOptions, thrownError) {
        alert(thrownError);
    });
}

var errorMsg = function () {
    alert('Не удалось применить изменения!');
};

var AddNewExercise = function (typeId) {
    //если модуль не сохранен - упражнения создавать нельзя
    if ((document.getElementById('moduleIdField')).value == 0) {
        alert("Нельзя создавать упражнения для несохраненного модуля.\nСначала выполните операцию сохранения модуля.");
    }
    else {
        //в зависимости от типа создаем новое упражнение
        if (typeId == 1) {
            $('#textExerciseId').val(0);
            $('#textExerciseModuleId').val((document.getElementById('moduleIdField')).value);

            $('#textExerciseNameId').val("Name");
            $('#textExerciseDescriptionId').val("Description");
            $('#textExerciseTextId').val("Text");

            $("form").ajaxSubmit({
                url: '/ExerciseTextAddHandler.ashx',
                success: addNewTextExerciseCallbackHandler,
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        }
        else {
            if (typeId == 2) {
                $('#SelectTextId').val(0);
                $('#SelectModuleId').val((document.getElementById('moduleIdField')).value);

                $('#questionNameId').val("Name");
                $('#questionDescriptionId').val("Description");
                $('#questionTextId').val("Text");

                $("form").ajaxSubmit({
                    url: '/ExerciseSelectAddHandler.ashx',
                    success: addNewSelectExerciseCallbackHandler,
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
            }
            else {
                if (typeId == 3) {
                    $('#editExerciseAnswerTextId').val(0);
                    $('#editExerciseAnswerTextModuleId').val((document.getElementById('moduleIdField')).value);

                    $('#exerciseAnswerTextNameId').val("Name");
                    $('#exerciseAnswerTextDescriptionId').val("Description");
                    $('#exerciseAnswerTextQuestionTextId').val("Text");

                    $("form").ajaxSubmit({
                        url: '/ExerciseAnswerTextAddHandler.ashx',
                        success: addNewAnswerTextExerciseCallbackHandler,
                        error: function (xhr, ajaxOptions, thrownError) {
                            alert(xhr.status);
                            alert(thrownError);
                        }
                    });
                }
                else {
                    if (typeId == 5) {
                        $('#exerciseSkipTextExerciseId').val(0);
                        $('#exerciseSkipTextExerciseModuleId').val((document.getElementById('moduleIdField')).value);

                        $('#exerciseSkipTextNameId').val("Name");
                        $('#exerciseSkipTextDescriptionId').val("Description");
                        $('#exerciseSkipTextQuestionTextId').val("Text");

                        $("form").ajaxSubmit({
                            url: '/ExerciseSkipTextAddHandler.ashx',
                            success: addNewSkipTextExerciseCallbackHandler,
                            error: function (xhr, ajaxOptions, thrownError) {
                                alert(xhr.status);
                                alert(thrownError);
                            }
                        });
                    }
                    else {
                        if (typeId == 4) {
                            $('#exerciseSelectTextExerciseId').val(0);
                            $('#exerciseSelectTextExerciseModuleId').val((document.getElementById('moduleIdField')).value);

                            $('#exerciseSelectTextNameId').val("Name");
                            $('#exerciseSelectTextDescriptionId').val("Description");
                            $('#exerciseSelectTextQuestionTextId').val("Text");

                            $("form").ajaxSubmit({
                                url: '/ExerciseSelectTextAddHandler.ashx',
                                success: addNewSelectTextExerciseCallbackHandler,
                                error: function (xhr, ajaxOptions, thrownError) {
                                    alert(xhr.status);
                                    alert(thrownError);
                                }
                            });
                        };
                    }
                }
            }
        }
    }
    //ReLoadExercises();
}

var addNewTextExerciseCallbackHandler = function (data) {
	ReLoadExercises();
	data = eval('(' + data + ')');
    if (data.status) {
        if (data.status == 'OK') {
            //Все ок
            if (data.msg == "Save") {
                alert('Упражнение добавлено!');
                //                var elementStr = '<tr><td><a onclick=OpenExerciseText(' + data.returnObject.Id + ');> ' + data.returnObject.Name +
                //                    ' </a></td><td>Текст</td><td>' + data.returnObject.Description + '</td></tr>';
                //                $('#exerciseListId').append(elementStr);

                //addRowForExerciseList('OpenExerciseText', data.returnObject);
            }
            if (data.msg == "Update") {
                alert('Упражнение изменено!');
            }
        } else {
            if (data.returnObject != null) {
                $.each(data.returnObject, function () {

                });
            }
        }
    }
    else {
        //Неизвестная причина
    }
};

var addNewSelectExerciseCallbackHandler = function (data) {
	ReLoadExercises();
	data = eval('(' + data + ')');
    if (data.status) {
        if (data.status == 'OK') {
            //Все ок
            if (data.msg == "Save") {
                alert('Упражнение добавлено!');
                //                var elementStr = '<tr><td><a onclick=OpenExerciseSelect(' + data.returnObject.Id + ');> ' + data.returnObject.Name +
                //                    ' </a></td><td>Выбор</td><td>' + data.returnObject.Description + '</td></tr>';
                //                $('#exerciseListId').append(elementStr);

                //addRowForExerciseList('OpenExerciseSelect', data.returnObject);
            }
            if (data.msg == "Update") {
                alert('Упражнение изменено!');
            }
        } else {
            if (data.returnObject != null) {
                $.each(data.returnObject, function () {

                });
            }
        }
    }
    else {
        //Неизвестная причина
    }
};

var addNewAnswerTextExerciseCallbackHandler = function (data) {
	ReLoadExercises();
	data = eval('(' + data + ')');
    if (data.status) {
        if (data.status == 'OK') {
            //Все ок
            if (data.msg == "Save") {
                alert('Упражнение добавлено!');
                //                var elementStr = '<tr><td><a onclick=OpenExerciseAnswerText(' + data.returnObject.Id + ');> ' + data.returnObject.Name +
                //                    ' </a></td><td>Текстовый ответ</td><td>' + data.returnObject.Description + '</td></tr>';
                //                $('#exerciseListId').append(elementStr);

                //addRowForExerciseList('OpenExerciseAnswerText', data.returnObject);
            }
            if (data.msg == "Update") {
                alert('Упражнение изменено!');
            }
        } else {
            if (data.returnObject != null) {
                $.each(data.returnObject, function () {

                });
            }
        }
    }
    else {
        //Неизвестная причина
    }
};

var addNewSkipTextExerciseCallbackHandler = function (data) {
	ReLoadExercises();
	data = eval('(' + data + ')');
    if (data.status) {
        if (data.status == 'OK') {
            //Все ок
            if (data.msg == "Save") {
                //                alert('Упражнение добавлено!');
                //                var elementStr = '<tr><td><a onclick=OpenExerciseSkipText(' + data.returnObject.Id + ');> ' + data.returnObject.Name +
                //                    ' </a></td><td>Пропуски в тексте</td><td>' + data.returnObject.Description + '</td></tr>';
                //                $('#exerciseListId').append(elementStr);

                // addRowForExerciseList('OpenExerciseSkipText', data.returnObject);
            }
            if (data.msg == "Update") {
                alert('Упражнение изменено!');
            }
        } else {
            if (data.returnObject != null) {
                $.each(data.returnObject, function () {

                });
            }
        }
    }
    else {
        //Неизвестная причина
    }
};

var addNewSelectTextExerciseCallbackHandler = function (data) {
	ReLoadExercises();
	data = eval('(' + data + ')');
    if (data.status) {
        if (data.status == 'OK') {
            //Все ок
            if (data.msg == "Save") {
                alert('Упражнение добавлено!');
                //                var elementStr = '<tr><td><a onclick=OpenExerciseSelectText(' + data.returnObject.Id + ');> ' + data.returnObject.Name +
                //                    ' </a></td><td>Выбор в тексте</td><td>' + data.returnObject.Description + '</td></tr>';
                //                $('#exerciseListId').append(elementStr);

                //addRowForExerciseList('OpenExerciseSelectText', data.returnObject);
            }
            if (data.msg == "Update") {
                alert('Упражнение изменено!');
            }
        } else {
            if (data.returnObject != null) {
                $.each(data.returnObject, function () {

                });
            }
        }
    }
    else {
        //Неизвестная причина
    }
};

var addRowForExerciseList = function (openMethodName, exercise) {
    var index = $('#exerciseListId').children().size();
    var ex_id = exercise.Id;
    var ex_type = exercise.ExerciseType;

    var elementText = '<li class="ui-state-default">';

    elementText += '<div style="overflow: auto;"><div class="sortableItemDivleft">';

    elementText += '<input type="hidden" value="' + index + '" name="exercise_' + ex_id + '_' + ex_type + '"/>';
    elementText += exercise.Name;
    elementText += '</div>';

    elementText += '<div class="sortableItemDivRight">';
    elementText += '<button onclick="' + openMethodName + '(' + ex_id + ')">Правка</button>';
    elementText += '</div>';
    elementText += '</div>';
    elementText += '</li>';

    $('#exerciseListId').append(elementText);
}
