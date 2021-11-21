prepareUserCommentTextBox = function (jqSelector, jqAddBtn, jqTargetDiv, targetElement) {
    jqSelector.keydown(function (e) {
        if ($(this).val().length > 500) {
            $(this).val($(this).val().substring(0, 49));
            e.preventDefault();
            return;
        }

        if (e.keyCode === 9) { // tab was pressed
            // get caret position/selection
            var start = this.selectionStart;
            var end = this.selectionEnd;

            var $this = $(this);
            var value = $this.val();

            // set textarea value to: text before caret + tab + text after caret
            $this.val(value.substring(0, start)
                                + "\t"
                                + value.substring(end));

            // put caret at right position again (add one for the tab)
            this.selectionStart = this.selectionEnd = start + 1;

            // prevent the focus lose
            e.preventDefault();
        }
        if (e.ctrlKey && e.keyCode === 13) {
            var valueToSend = $(this).val();
            alert('Working! TargetElement: <%= TargetElement %>; ValueToSend: ' + valueToSend);
            // prevent the focus lose
            e.preventDefault();
        }
    });

    var textBoxId = jqSelector.attr('id');
    jqAddBtn.click(function () {
        addComment(jqTargetDiv, textBoxId, null, targetElement, $('#captchaText').val());
    });

    $('#updateCaptchaBtn').click(function () {
    	var jqImg = $('#captchaImage');
    	jqImg.attr('src', '/GetCaptcha.ashx?width=150&height=70&r=' + Math.random());
        /*var oldSrc = jqImg.attr('src');
        jqImg.attr('src', '');
        jqImg.attr('src', oldSrc);*/
    });
}

loadUserComments = function (jqTargetDiv, jqTextBox, targetElement, pageNumber) {
    var targetDivId = jqTargetDiv.attr('id');
    var data = {
        targetElement: targetElement,
        pageNumber: pageNumber
    };
    $.ajax({
        url: '/GetUserComments.ashx',
        data: data,
        type: 'POST'
    })
    .done(function (data) {
        data = eval('( ' + data + ' )');
        var comments = data.returnObject.Comments;
        $('#' + targetDivId).html('<div class="comment_block_title">Комментарии (' + data.returnObject.Count + '):</div>');
        for (var i = 0; i < comments.length; i++) {
            var comment = comments[i];
            var commentDiv = $(getCommentItem(comment, data.returnObject.CurrentAccountId));
            if (data.returnObject.CurrentAccountId != comment.AuthorAccountId) {
                setupCommentItem(commentDiv, comment.Id);
            }
            commentDiv.appendTo(jqTargetDiv).show('normal');
        }
        $('#commentBlockPaging').html(getPagingNumbers(data.returnObject.Pages, pageNumber, targetElement));
        $('.comment_item_header_rating', jqTargetDiv).each(function () {
            setStyleForRatingTd(this);
        });
    })
    .fail(function () {
        // Bad things happens :(
    });
}

setStyleForRatingTd = function (ratingTdElement) {
    if (ratingTdElement.innerText >= 0) {
        $(ratingTdElement)
            .removeClass('comment_item_header_rating_negative')
            .addClass('comment_item_header_rating_positive');
    } else {
        $(ratingTdElement)
            .removeClass('comment_item_header_rating_positive')
            .addClass('comment_item_header_rating_negative');
    }
}

setupCommentItem = function (jqCommentDiv, commentId) {
    var upSpan = $('.comment_item_header_up', jqCommentDiv);
    var downSpan = $('.comment_item_header_down', jqCommentDiv);
    var claimSpan = $('.comment_item_header_claim', jqCommentDiv);
    var ratingSpan = $('.comment_item_header_rating', jqCommentDiv).get(0);

    upSpan.click(function () {
        $.ajax({
            url: '/RateOrClaimComment.ashx',
            type: 'POST',
            data: { actionType: 'ratePositive', commentId: commentId }
        })
        .done(function (data) {
            data = eval(' ( ' + data + ' ) ');
            ratingSpan.innerText = data.returnObject;
            setStyleForRatingTd(ratingSpan);
        })
    });

    downSpan.click(function () {
        $.ajax({
            url: '/RateOrClaimComment.ashx',
            type: 'POST',
            data: { 'actionType': 'rateNegative', commentId: commentId }
        })
        .done(function (data) {
            data = eval(' ( ' + data + ' ) ');
            ratingSpan.innerText = data.returnObject;
            setStyleForRatingTd(ratingSpan);
        });
    });

    claimSpan.click(function () {
        $.ajax({
            url: '/RateOrClaimComment.ashx',
            type: 'POST',
            data: { 'actionType': 'claim', commentId: commentId }
        })
        .done(function (data) {
            //ratingSpan.innerText = data;
        });
    });
}

getCommentItem = function (comment, currentAccountId) {
    var resultStr = '<div class="comment_item comment_level_' + comment.AnswerLevel + '" comment_id="' + comment.Id + '">' +
        '<div class="comment_item_header">' +
        '<table width="100%"><tr>' +
        '<td class="comment_item_header_user">' + comment.UserName + '</td>' +
        '<td class="comment_item_header_date">' + comment.CreateDateString + '</td>' +
        '<td class="comment_item_header_rating">' + comment.Rating + '</td>';

    resultStr += '<td class="comment_item_header_buttons">' +
        '<span class="comment_item_header_up"><img src="/img/up.png"/></span>' +
        '<span class="comment_item_header_down"><img src="/img/down.png"/></span>' +
        '<span class="comment_item_header_claim"><img src="/img/claim.png"/></span>' +
        '</td>';

    resultStr += '</tr></table>' +
        '</div>' +
        '<div class="comment_item_body">' + comment.Text + '</div>' +
        '</div>';
    
    return resultStr;
}

getPagingNumbers = function (pages, currentPage, targetElement) {
    var htmltext = '';
    while (pages > 0) {
        var additionalStyle = currentPage == pages ? 'style="color: red"' : '';
        htmltext = '<a class="button" ' + additionalStyle + ' onclick="selectPage(' + pages + ', \'' + targetElement + '\')">' + pages + '</a>' + '&nbsp;' + htmltext;
        pages--;
    }
    return htmltext;
}

selectPage = function (pageNumber, targetElement) {
    loadUserComments($('#commentBlock'), $('#theNewCommentTextId'), targetElement, pageNumber);
}

addComment = function (jqTargetDiv, textBoxId, parentId, targetElement, captchaText) {
    var data = {
        text: $('#' + textBoxId).val(),
        parentId: parentId,
        targetElement: targetElement,
        captchaText: captchaText
    };
    $.ajax({
        url: '/AddUserComment.ashx',
        data: data,
        type: 'POST'
    })
    .done(function (data) {
        data = eval('( ' + data + ' )');
        if (data.status == 'OK') {
            loadUserComments(jqTargetDiv, $('#' + textBoxId), targetElement, 1);
            $('#' + textBoxId).val('');
        } else if (data.returnObject == 'WrongCaptcha') {
            $('#captchaError').text(data.msg);
        }
        var jqImg = $('#captchaImage');
        jqImg.attr('src', jqImg.attr('src'));
    })
    .fail(function () {
    });

    $('#captchaError').text('');
    $('#captchaText').val('');
}