<%@ Page Language="c#" MasterPageFile="~/MasterPage.Master" CodeBehind="Donate.aspx.cs"
	AutoEventWireup="False" Inherits="Wordz.Web.Donate" EnableViewState="false" %>

<%@ Register Src="Controls/Advert.ascx" TagName="Advert" TagPrefix="uc" %>
<asp:Content ContentPlaceHolderID="chpHead" runat="server">
	<title>Как помочь сайту</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageCaption" runat="server">
	Как помочь сайту
</asp:Content>
<asp:Content ContentPlaceHolderID="cphPageDescription" runat="server">
	Способы, как вы можете помочь улучшить этот сайт
</asp:Content>
<asp:Content ContentPlaceHolderID="cphMiddle" runat="server">
	<p>
		Если Вам понравился этот сайт, Вы можете содействовать его развитию, поделившись
		с Вашими друзьями <a href="/">ссылкой на сайт</a> и/или разместив её в Вашем блоге,
		рассылке, социальных закладках или в другом месте Сети.
	</p>
	<p align="left">
		<b><u>Описание сайта:</u></b><br/>
		<a href="/">Английский язык: увеличение словарного запаса</a><br/>
		Сайт <a href="http://wordz.ru">http://wordz.ru</a> предоставляет сервисы по изучению
		английского языка. Основные возможности: составление для текста txt и mp3 файлов
		с незнакомыми словами, проверка выученного, онлайн ТВ/радио/фильмы, изучение неправильных
		глаголов, конкурсы на знание словаря/порядок слов в предложении, грамматика.
	</p>
	<p style="font-size: 75%; background-color: white" align="left">
		<b>HTML-код для размещения кнопки/баннера <a href="/">wordz.ru</a>:</b><br/>
		<a href="/">
			<img src="/img/Wordz_ru_Logo_86_40.PNG" alt="Логотип сайта www.wordz.ru" /></a>
		&lt;a href=&quot;http://wordz.ru&quot; target=&quot;_blank&quot;&gt;&lt;img src=&quot;http://wordz.ru/img/Wordz_ru_Logo_86_40.PNG&quot;
		width=&quot;86&quot; height=&quot;40&quot; border=&quot;0&quot; alt=&quot;Английский
		язык: увеличение словарного запаса&quot;&gt;&lt;/a&gt;
		<br/>
		<br/>
		<a href="/">
			<img src="/img/Wordz_ru_Banner_88_31.gif" alt="Логотип сайта www.wordz.ru" /></a>
		&lt;a href=&quot;http://wordz.ru&quot; target=&quot;_blank&quot;&gt;&lt;img src=&quot;http://wordz.ru/img/Wordz_ru_Banner_88_31.gif&quot;
		width=&quot;88&quot; height=&quot;31&quot; border=&quot;0&quot; alt=&quot;Английский
		язык: увеличение словарного запаса&quot;&gt;&lt;/a&gt;
	</p>
	<p style="font-size: 75%; background-color: #90EE90" align="left">
		<b>Или Вы можете помочь проекту материально:</b><br/>
		<br/>
		<a href="http://webmoney.ru/">WebMoney</a> кошельки<br/>
		WMR (RUR) <b>R144637697315</b><br/>
		WMZ (USD) <b>Z200656311256</b><br/>
		WME (EUR) <b>E121519487680</b><br/>
		<br/>
		<a href="http://money.yandex.ru/">Яндекс.Деньги</a><br/>
		№ счёта (RUR): <b>41001438705426</b><br/>
		<br/>
		<a href="https://www.paypal.com/us/">PayPal</a><br/>
		Перевести деньги на емайл, указанный <a href="/Contacts">здесь</a><br/>
		<br/>
		Через систему перевода денег <a href="http://westernunion.com">Western Union</a>,
		<a href="http://contact-sys.com">Contact</a>, <a href="http://unistream.ru/transfers/">
			Unistream</a>, <a href="http://leadermt.ru/">Лидер</a> и т.п.<br/>
		Персональные данные для перевода денег высылаются по запросу на наш <a href="/Contacts">
			контактный емайл</a><br/>
		<br/>
		<b>P.S.</b> Желательно сообщить о факте перевода денег <a href="/Contacts">отдельным
			письмом</a>
	</p>
	<p>
		Если у Вас есть словарь в электронном виде и Вы не против для его использования
		на сайте, либо любая другая полезная информация, Вы можете <a href="/Contacts">связаться
			с нами</a> для обсуждения деталей сотрудничества.
	</p>
	<p>
		Также, будем рады обменяться ссылками с <a href="/Partners">ресурсами смежной тематики</a>
	</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphAd" runat="server">
	<uc:Advert runat="server" Ad="7" />
</asp:Content>
