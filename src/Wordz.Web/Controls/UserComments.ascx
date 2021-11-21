<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserComments.ascx.cs" Inherits="Wordz.Web.Controls.UserComments" %>
<div class="comment_control">
    <div id="commentBlock" class="comment_block">
        
    </div>
    <div id="commentBlockPaging" ></div>
    <div>
        <div class="comment_textarea">
            <textarea name="theNewCommentText" id="theNewCommentTextId" 
                class="comment_textbox" cols="50" rows="1" 
                onfocus="this.rows = 3;"
                ></textarea>
        </div>
        <div>
            <div></div>
            <div></div>
            <div></div>
        </div>
        <div>
            
        </div>
        <table width="100%">
        <tr>
            <td class="comment_captcha"><img id="captchaImage" width="150" height="70" src="/GetCaptcha.ashx?width=150&height=70" alt="Капча!" /></td>
            <td class="comment_captcha_upd"><input type="button" id="updateCaptchaBtn"/></td>
            <td class="comment_captcha_text"><input type="text" id="captchaText" /></td>
            <td class="comment_buttons"><a id="addCommentBtn" class="button">Отправить</a></td>
        </tr>
        <tr>
            <td id="captchaError" class="comment_captcha_error"></td>
        </tr>
        </table>
    </div>
    <script type="text/javascript">
        prepareUserCommentTextBox($("#theNewCommentTextId"), $('#addCommentBtn'), $('#commentBlock'), '<%= TargetElement.ToString() %>');
        $(function () {
            loadUserComments($('#commentBlock'), $("#theNewCommentTextId"), '<%= TargetElement.ToString() %>', 1);
        });
    </script>
</div>