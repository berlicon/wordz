jQuery(function($){


		if (jQuery.browser.webkit) $brouser = 'webkit'
		else if (jQuery.browser.opera) $brouser = 'opera'
		else if (jQuery.browser.mozilla) $brouser = 'mozilla'
		else if (jQuery.browser.msie) $brouser = 'ie'+jQuery.browser.version.charAt(0);

		$('body').addClass($brouser);        

	$(document).ready(function(){
		// $('#filter select, #translate select ').styler();
		$('#file').styler();
		$('#filter select, #translate select ').chosen();
	});

});