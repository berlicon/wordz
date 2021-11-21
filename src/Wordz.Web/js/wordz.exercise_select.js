var addAnswerBtnClick = function () {
    var rowCount = $('#answersTable').get(0).rows.length;
    var elementStr = '<tr><td><input type="hidden" name=answerId_' + (rowCount + 1)
                    + ' value="0" /><textarea cols=20 rows=3 name="text_'
                    + (rowCount + 1) + '"></textarea></td>';
    elementStr += '<td>';
    elementStr += '<div><label for="isNeedDeleteImage_' + (rowCount + 1) + 'Id">Удалить изображение</label>';
    elementStr += '<input type="checkbox" id="isNeedDeleteImage_' + (rowCount + 1) + 'Id" name="isNeedDeleteImage_' + (rowCount + 1) + '" value="1"/></div>';
    elementStr += '<input type="file" name="image_' + (rowCount + 1) + '"/></td>';
    elementStr += '<td><input type="checkbox" value="" name="isAnswer_' + (rowCount + 1) + '" /> Верный ответ</td>';
    elementStr += '<td><button style="color: red">X</button></td>';
    elementStr += '</tr>';
    var elementItem = $(elementStr);
    $('button', elementItem).click(removeCurrentRowFunction(elementItem));
    $('#answersTable').append(elementItem);
};

var deleteAnswerBtnClick = function () {
    var rowCount = $('#answersTable').get(0).rows.length;
    document.getElementById("answersTable").deleteRow(rowCount - 1);
};

function deletePictureBtnClick(id) {
    document.getElementById("changePicture_" + id).setAttribute("value", -1);
};

function setPicture(id) {
    document.getElementById("changePicture_" + id).setAttribute("value", +1);
};
function deleteQuestionPictureBtnClick() {
    document.getElementById("changeQuestionPicture").setAttribute("value", -1);
};
function setQuestionPicture() {
    document.getElementById("changeQuestionPicture").setAttribute("value", +1);
};

var SaveExerciseSelect = function () {
    $('#ExerciseSelectContrDiv').arcticmodal('close');
    $('#ExerciseSelectContrDiv').appendTo($('form'));
    $("form").ajaxSubmit({
        url: '/ExerciseSelectAddHandler.ashx',
        success: formExerciseSelectCallbackHandler,
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
};

var formExerciseSelectCallbackHandler = function (data) {
    alert('Ok!');
    ReLoadExercises();
};

var prepareAnswersTable = function () {
    $('#answersTable').html('<td width="30%">Текст</td><td width="30%">Изображение</td><td width="30%">Верный ответ</td><td></td>');
};

var OpenExerciseSelect = function (id) {
    //загружаем данные упражнения
    if (id != 0) {

        var str = "/ExerciseSelectGetHandler.ashx?id=" + id;
        $.getJSON(str, function (data) {
            if (data.status && data.status == 'ERR') { }
            else {
                SelectTextId.value = data.Id;
                SelectModuleId.value = data.ModuleId;
                questionNameId.value = data.Name;
                questionDescriptionId.value = data.Description;
                questionTextId.value = data.Text;

                prepareAnswersTable();

                //delTableRow($('#answersTable'));

                data.Answers.forEach(function (ans) {
                    var rowCount = $('#answersTable').get(0).rows.length;
                    var check = '';
                    var checkValue = '\"\"';
                    if (ans.IsRight == true) {

                        check = ' checked=checked ';
                        checkValue = 1;
                    }

                    var elementStr = '<tr><td><input type=hidden name=answerId_' + (rowCount + 1)
                    + ' value=' + ans.Id + ' /><textarea cols=20 rows=3 name=text_' + (rowCount + 1)
                    + '>' + ans.Text + '</textarea></td>';
                    elementStr += '<td>';

                    if (ans.PictureId) {
                        elementStr += '<div><img width="50px" src="/Handlers/LoadPictureHandler.ashx?id=' + ans.PictureId + '" /></div>';
                    }

                    elementStr += '<div><label for="isNeedDeleteImage_' + (rowCount + 1) + 'Id">Удалить изображение</label>';
                    elementStr += '<input type="checkbox" id="isNeedDeleteImage_' + (rowCount + 1) + 'Id" name="isNeedDeleteImage_' + (rowCount + 1) + '" value="1"/></div>';
                    elementStr += '<div><input type="file" name="image_' + (rowCount + 1) + '"/></div>';
                    elementStr += '</td>';
                    elementStr += '<td><input type=checkbox value=' + checkValue + ' name=isAnswer_'
                    + (rowCount + 1) + check + ' />Верный ответ</td>';
                    elementStr += '<td><button style="color: red">X</button></td>';
                    elementStr += '</tr>';
                    var elementItem = $(elementStr);
                    $('button', elementItem).click(removeCurrentRowFunction(elementItem));
                    $('#answersTable').append(elementItem);
                });
            }
        });
    };

    //открываем окно 
    $('#ExerciseSelectContrDiv').arcticmodal({
        closeOnEsc: true,
        closeOnOverlayClick: true,
        beforeOpen: function () {
            $('#ExerciseSelectContrDiv').show();
        },
        afterClose: function (data, el) {
            $('#ExerciseSelectContrDiv').hide();
        }
    });
}

function removeCurrentRowFunction(rowElement) {
    return function () {
        rowElement.remove();
    }
}

function delTableRow(jQtable) {
    jQtable.each(function () {
        while ($('tr', this).length > 1) {
            $('tr:last', this).remove();
        }
    });
}