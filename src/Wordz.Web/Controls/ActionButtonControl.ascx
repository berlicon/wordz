<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActionButtonControl.ascx.cs" Inherits="Wordz.Web.Controls.PaymentButtonControl" %>
<%@ Import Namespace="Wordz.Web.Helpers" %>
<%
    var spanId = Guid.NewGuid().ToString();
%>
<%=RenderHelper.Button(spanId, Text, "", Style)%>

<%
    if (!string.IsNullOrEmpty(OnClickHandler))
    {
%>
<script type="text/javascript">
    $('#<%= spanId %>').click(function (evt) { if (confirm('<%= ConfirmText  %> ')) { <%= OnClickHandler %> } evt.stopPropagation(); });
</script>
<%
    }
%>
