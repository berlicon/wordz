<%@ Control Language="c#" AutoEventWireup="False" Codebehind="Footer.ascx.cs" Inherits="Wordz.Web.Controls.Footer" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="false"%>
<%@ Register Src="NewSign.ascx" TagName="NewSign" TagPrefix="uc" %>
<%@ Register Src="UpdSign.ascx" TagName="UpdSign" TagPrefix="uc" %>
<%@ Register Src="Counters.ascx" TagName="Counters" TagPrefix="uc" %>
<%@ Register Src="Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<div id="footer">
    <div id="footer-l">
	    <div class="infoBlock">
		    <h3 class="infoBlockTitle">Полезная информация</h3>
		    <ul>
			    <li><a href="/Goal">О проекте</a></li>
			    <li><a href="/FAQ">Ответы на вопросы</a></li>
			    <li><a href="/News">Новости</a></li>
			    <li><a href="/Forum">Форум</a></li>
			    <li><a href="/Statistics">Статистика</a></li>
		    </ul>
	    </div>
	    <div class="infoBlock">
		    <h3 class="infoBlockTitle">Интерактивный контент</h3>
		    <ul>
			    <li><a href="/OnlineTV">Онлайн ТВ</a></li>
			    <li><a href="/OnlineFM">Онлайн радио</a></li>
			    <li><a href="/OnlineFilm">Фильмы онлайн</a></li>
			    <li><a href="/Articles">Полезные статьи</a></li>
			    <li><a href="/Resources">Ресурсы</a></li>
			    <li><a href="/Partners">Ссылки</a></li>
		    </ul>
	    </div>
	    <div class="infoBlock">
		    <h3 class="infoBlockTitle">О компании</h3>
		    <ul>
			    <li><a href="/Payment">Оплата</a></li>
			    <li><a href="/Donate">Содействие</a></li>
			    <li><a href="/Ads">Реклама</a></li>
			    <li><a href="/Authors">Авторам курсов</a></li>
			    <li><a href="/Contacts">Контакты</a></li>
		    </ul>
	    </div>
	    <div class="infoBlock">
		    <%--<h3 class="infoBlockTitle">Мы принимаем к оплате</h3>
		    <a href="javascript:void(0);"><img src="/img/paySystems.jpg" alt=""></a>--%>
	    </div>
	    <div class="infoBlock">
		    <%--<h3 class="infoBlockTitle">Мы социальны</h3>
		    <a href="javascript:void(0);"><img src="/img/twitter.png" alt=""></a>
		    <a href="javascript:void(0);"><img src="/img/facebook.png" alt=""></a>--%>
	    </div>
    </div>
    <div id="footer-r">
        <h3 class="infoBlockTitle">Мы социальны</h3>
		<a href="https://twitter.com/wordzpm"><img src="/img/twitter.png" alt=""></a>
		<a href="https://www.facebook.com/wordzPM"><img src="/img/facebook.png" alt=""></a>
        <uc:Counters runat="server"/>
	    <%--<h3 class="infoBlockTitle">ЗАРЕГЕСТРИРОВАТЬСЯ В ИНФОРМАЦИОННОМ БЮЛЛЕТЕНЕ</h3>
	    <p>Подпишитесь чтобы получать самые последние новости и объявления с нашего сайта</p>
	    <div class="clr"></div>
		    <input type="text" placeholder="Ваш e-mail"> <input type="submit" class="buttonRed" value="Ok" id="email">				--%>
    </div>
    <div class="clr"></div>
    <div id="copyright">
<%--	    <p>
		    Использование сайта PropertyRoom.com или его филиалов означает согласие с <a href="javascript:void(0);">Пользовательским соглашением</a> <br/>
		    Copyright 1999 -2013 PropertyRoom.com, Inc. Все права защищены
	    </p>--%>
        <uc:Advert runat="server" Ad="1"/>
    </div>
</div>