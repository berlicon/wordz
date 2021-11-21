<%@ Control Language="c#" AutoEventWireup="False" Codebehind="Footer.ascx.cs" Inherits="Wordz.Web.Controls.Footer" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="false"%>
<%@ Register Src="NewSign.ascx" TagName="NewSign" TagPrefix="uc" %>
<%@ Register Src="UpdSign.ascx" TagName="UpdSign" TagPrefix="uc" %>
<%@ Register Src="Counters.ascx" TagName="Counters" TagPrefix="uc" %>
<%@ Register Src="Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<div id="footer">
    <div id="footer-l">
	    <div class="infoBlock">
		    <h3 class="infoBlockTitle">�������� ����������</h3>
		    <ul>
			    <li><a href="/Goal">� �������</a></li>
			    <li><a href="/FAQ">������ �� �������</a></li>
			    <li><a href="/News">�������</a></li>
			    <li><a href="/Forum">�����</a></li>
			    <li><a href="/Statistics">����������</a></li>
		    </ul>
	    </div>
	    <div class="infoBlock">
		    <h3 class="infoBlockTitle">������������� �������</h3>
		    <ul>
			    <li><a href="/OnlineTV">������ ��</a></li>
			    <li><a href="/OnlineFM">������ �����</a></li>
			    <li><a href="/OnlineFilm">������ ������</a></li>
			    <li><a href="/Articles">�������� ������</a></li>
			    <li><a href="/Resources">�������</a></li>
			    <li><a href="/Partners">������</a></li>
		    </ul>
	    </div>
	    <div class="infoBlock">
		    <h3 class="infoBlockTitle">� ��������</h3>
		    <ul>
			    <li><a href="/Payment">������</a></li>
			    <li><a href="/Donate">����������</a></li>
			    <li><a href="/Ads">�������</a></li>
			    <li><a href="/Authors">������� ������</a></li>
			    <li><a href="/Contacts">��������</a></li>
		    </ul>
	    </div>
	    <div class="infoBlock">
		    <%--<h3 class="infoBlockTitle">�� ��������� � ������</h3>
		    <a href="javascript:void(0);"><img src="/img/paySystems.jpg" alt=""></a>--%>
	    </div>
	    <div class="infoBlock">
		    <%--<h3 class="infoBlockTitle">�� ���������</h3>
		    <a href="javascript:void(0);"><img src="/img/twitter.png" alt=""></a>
		    <a href="javascript:void(0);"><img src="/img/facebook.png" alt=""></a>--%>
	    </div>
    </div>
    <div id="footer-r">
        <h3 class="infoBlockTitle">�� ���������</h3>
		<a href="https://twitter.com/wordzpm"><img src="/img/twitter.png" alt=""></a>
		<a href="https://www.facebook.com/wordzPM"><img src="/img/facebook.png" alt=""></a>
        <uc:Counters runat="server"/>
	    <%--<h3 class="infoBlockTitle">������������������ � �������������� ���������</h3>
	    <p>����������� ����� �������� ����� ��������� ������� � ���������� � ������ �����</p>
	    <div class="clr"></div>
		    <input type="text" placeholder="��� e-mail"> <input type="submit" class="buttonRed" value="Ok" id="email">				--%>
    </div>
    <div class="clr"></div>
    <div id="copyright">
<%--	    <p>
		    ������������� ����� PropertyRoom.com ��� ��� �������� �������� �������� � <a href="javascript:void(0);">���������������� �����������</a> <br/>
		    Copyright 1999 -2013 PropertyRoom.com, Inc. ��� ����� ��������
	    </p>--%>
        <uc:Advert runat="server" Ad="1"/>
    </div>
</div>