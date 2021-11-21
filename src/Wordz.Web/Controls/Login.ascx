<%@ Control Language="c#" AutoEventWireup="False" Codebehind="Login.ascx.cs" Inherits="Wordz.Web.Controls.Login" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="false"%>

<div id="tblLogin" runat="server" class="login">
    <div class="regWrapper">
        <span>���/������:</span>
        <asp:TextBox ID="txtNick" Runat="server" MaxLength="20" Width="50px" CssClass="nicktext"/>
        <asp:TextBox ID="txtPassword" Runat="server" TextMode="Password" MaxLength="20" Width="50px"/>
        
        <br/>
        <a id="btnLogin" Runat="server" href="#" title="������������� �����">����</a>
	    <span> | </span>
        <a href="/Login" class="registration" title="������������������ �� �����">�����������</a>
	    <a href="/FAQ#register" class="registration" title="����� ����������������?">?</a>
    </div>
        <span>����� � �������: </span> 
	    <a href="#" onclick="javascript:alert('�� �����������');return false;"><img src="/img/facebook-l.png" alt=""></a>
	    <a href="#" onclick="javascript:alert('�� �����������');return false;"><img src="/img/vk-l.png" alt=""></a>
	    <a href="#" onclick="javascript:alert('�� �����������');return false;"><img src="/img/twitter-l.png" alt=""></a>
</div>

<div id="tblLogout" runat="server" class="info">
    <asp:Label ID="lblNick" Runat="server" CssClass="nic"/>
	<span class="messages">4</span>
	<span class="cash"><asp:Label ID="cashLabel" Runat="server"/></span>

    <div class="login">
    <a id="btnLogout" Runat="server" href="#" title="����� ��������� ������������� �����">�����</a>
    <a href="/Login" title="�������� ��������� ������ ��������">�������</a>
    </div>
    <div id="redirectScriptId" runat="server" visible="false">
        <script>document.location = document.location</script>
    </div>
</div>
<script>
    var loginActionOnEnter = function(event){
            if ( event.which == 13 ) {
                event.preventDefault();
                $('#<%= btnLogin.ClientID %>').click();
            }
        };

    $('#<%= txtNick.ClientID %>')
        .keypress(loginActionOnEnter);

    $('#<%= txtPassword.ClientID %>')
        .keypress(loginActionOnEnter);
</script>
<%--
---
div class="info">
	<span class="nic">Lorem ipsum.</span>
	<span class="messages">4</span>
	<span class="cash">345$</span>
</div>
<div class="login"><a href="javascript:void(0);">�����</a></div

---

<div class="login">
<div class="regWrapper">
	<a href="javascript:void(0);">����</a>
	<span> | </span>
	<a class="registration" href="javascript:void(0);">�����������</a>
</div>
					
<div class="clr"></div>
<span>����� � �������: </span> 
	<a href="javascript:void(0);"><img src="images/facebook-l.png" alt=""></a>
	<a href="javascript:void(0);"><img src="images/vk-l.png" alt=""></a>
	<a href="javascript:void(0);"><img src="images/twitter-l.png" alt=""></a>
</div>--%>