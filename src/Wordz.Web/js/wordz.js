var xmlhttp;
var separator = '\f';
var separator2 = '\t';
var unknownWordClass = 'unknown';
var knownWordClass = 'known';

function ActivateXMLRequest() {
	if(window.XMLHttpRequest) {
		xmlhttp = new XMLHttpRequest();
	} else if(window.ActiveXObject) {
		try {
			xmlhttp = new ActiveXObject('Msxml2.XMLHTTP');
		} catch (e) {
			try
			{
				xmlhttp = new ActiveXObject('Microsoft.XMLHTTP');
			} catch (E) {
				xmlhttp = false;
			}
		}
	} else {
		return false;
	}
	return true;
}

function gt(objectId) {
	return document.getElementById(objectId);
}

function sleep(msec) {
	now = Date.parse( Date() );
	limit = now + msec;
	while(now < limit){
		now = Date.parse( Date() ) ;
	}
}

function Login(tblLoginId, tblLogoutId, txtNickId, txtPasswordId, lblNickId, messageWrongNickOrPassword) {
	var tblLogin = gt(tblLoginId);
	var tblLogout = gt(tblLogoutId);
	var txtNick = gt(txtNickId);
	var txtPassword = gt(txtPasswordId);
	var lblNick = gt(lblNickId);
	
	if (ActivateXMLRequest()) {
		var args = "Nick=" + txtNick.value + "&Password=" + txtPassword.value;
		xmlhttp.open('POST', "/LoginHandler.ashx" + '?' + args, true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.send('');
		
		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var response = xmlhttp.responseText;
				var success = response.length > 0;
				
				tblLogin.style.display = !success ? 'inline' : 'none';
				tblLogout.style.display = success ? 'inline' : 'none';
				lblNick.innerHTML = response;
				
				if (!success) {
					alert(messageWrongNickOrPassword);
				}
			}
		}
	}
}

function Logout(tblLoginId, tblLogoutId) {
	var tblLogin = gt(tblLoginId);
	var tblLogout = gt(tblLogoutId);
	
	if (ActivateXMLRequest()) {
		xmlhttp.open('POST', "/LogoutHandler.ashx", true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.send('');
		
		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				tblLogin.style.display = 'inline';
				tblLogout.style.display = 'none';
			}
		}
	}
}

function AccountSave(txtNickId, txtPasswordId, txtEmailId, messageNickRequired) {
	var txtNick = gt(txtNickId);
	var txtPassword = gt(txtPasswordId);
	var txtEmail = gt(txtEmailId);
	
	if (ActivateXMLRequest()) {
		var args = "Nick=" + txtNick.value + "&Password=" + txtPassword.value + "&Email=" + txtEmail.value;
		xmlhttp.open('POST', "/AccountHandler.ashx" + '?' + args, true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.send('');
		
		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var success = xmlhttp.responseText.length > 0;

				if (success) {
					location.href = '/';
				} else {
					alert(messageNickRequired);
				}
			}
		}
	}
}

function Process(txtDestId, rbWordOrderEngRusId, rbWordOrderRusEngId,
				chkNotUseWellKnownWordsId, chkNotUseNotKnownWordsId, chkNotUseNotSoundedWordsId,
				rbSortAlphabetId, rbSortFrequencyId, rbSortWordsLengthId,
				rbSortMixedOrderId, rbSortOriginalOrderId, txtMinFrequencyId,
				txtMaxSignedWordsId, lblWordCountId, lblMaxFrequencyId, divProgressId
				) {
	var txtDest = gt(txtDestId);
	var rbWordOrderEngRus = gt(rbWordOrderEngRusId);
	var rbWordOrderRusEng = gt(rbWordOrderRusEngId);
	var chkNotUseWellKnownWords = gt(chkNotUseWellKnownWordsId);
	var chkNotUseNotKnownWords = gt(chkNotUseNotKnownWordsId);
	var chkNotUseNotSoundedWords = gt(chkNotUseNotSoundedWordsId);
	var rbSortAlphabet = gt(rbSortAlphabetId);
	var rbSortFrequency = gt(rbSortFrequencyId);
	var rbSortWordsLength = gt(rbSortWordsLengthId);
	var rbSortMixedOrder = gt(rbSortMixedOrderId);
	var rbSortOriginalOrder = gt(rbSortOriginalOrderId);
	var txtMinFrequency = gt(txtMinFrequencyId);
	var txtMaxSignedWords = gt(txtMaxSignedWordsId);
	var lblWordCount = gt(lblWordCountId);
	var lblMaxFrequency = gt(lblMaxFrequencyId);
	var divProgress = gt(divProgressId);
	
	divProgress.style.display = '';
	
	var wordOrder = (rbWordOrderEngRus.checked) ? 1 : 2;
	var sort;
	if (rbSortAlphabet.checked) {
		sort = 1;
	} else if (rbSortFrequency.checked) {
			sort = 2;
		} else if (rbSortWordsLength.checked) {
				sort = 3;
			} else if (rbSortMixedOrder.checked) {
					sort = 4;
				} else if (rbSortOriginalOrder.checked) {
						sort = 5;
					}
	
	if (ActivateXMLRequest()) {
		var args = "WordOrder=" + wordOrder +
		"&NotUseWellKnownWords=" + chkNotUseWellKnownWords.checked +
		"&NotUseNotKnownWords=" + chkNotUseNotKnownWords.checked +
		"&NotUseNotSoundedWords=" + chkNotUseNotSoundedWords.checked +
		"&Sort=" + sort + "&MinFrequency=" + txtMinFrequency.value +
		"&MaxSignedWords=" + txtMaxSignedWords.value;
		xmlhttp.open('POST', "/ProcessHandler.ashx" + '?' + args, true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.send('');
		
		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var response = xmlhttp.responseText;
				
				var spacePos = response.indexOf(separator);
				lblWordCount.innerHTML = response.substr(0, spacePos);
				
				var spacePos2 = response.indexOf(separator, spacePos + 1);
				lblMaxFrequency.innerHTML = response.substr(spacePos + 1, spacePos2 - spacePos - 1);
				
				txtDest.value = (response.length > (spacePos2 + 1)) ? response.substr(spacePos2 + 1) : '';
				sleep(200);
				divProgress.style.display = 'none';
			}
		}
	}
}

function CheckAll(chkCheckAllId, cklWordsId) {
	var flag = gt(chkCheckAllId).checked;
	var checks = gt(cklWordsId).getElementsByTagName("input");
	
	for(var i = 0, len = checks.length; i < len; i++) {
		checks[i].checked = flag;
	}
}

function CheckAllByParam(txtMinPercentId, cklWordsId, controlFirstLetter) {
	var minPercent = gt(txtMinPercentId).value;
	minPercent = (isNaN(parseInt(minPercent))) ? 0 : parseInt(minPercent);
	
	var checks = gt(cklWordsId).getElementsByTagName("input");	
	for(var i = 0, len = checks.length; i < len; i++) {
		if (checks[i].type == 'checkbox') {
			var txtPercentId = controlFirstLetter + checks[i].id.substr(1);
			var txtPercent = gt(txtPercentId);
			checks[i].checked = (txtPercent.value >= minPercent);
		}
	}
}

function AddWords(cklWordsId, chkCheckAllId, messageRegisteringRequiredForAddWords, divProgressId) {
	var cklWords = gt(cklWordsId);
	var chkCheckAll = gt(chkCheckAllId);
	var divProgress = gt(divProgressId);
	
	chkCheckAll.checked = false;
	divProgress.style.display = '';
	
	var checks = cklWords.getElementsByTagName("input");
	var ids = "";
	
	for(var i = 0, len = checks.length; i < len; i++) {
		if (checks[i].checked) {
			ids += checks[i].id;
		}
	}
	
	if (ActivateXMLRequest()) {
		var args = ids;
		xmlhttp.open('POST', "/AddWordsHandler.ashx", true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.setRequestHeader("Content-length", args.length);
		xmlhttp.setRequestHeader("Connection", "close");
		xmlhttp.send(args);
		
		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var success = xmlhttp.responseText.length > 0;

				sleep(200);
				divProgress.style.display = 'none';

				if (!success) {
					alert(messageRegisteringRequiredForAddWords);
				}
			}
		}
	}
}

function LoadWords(cklWordsId, rbLoadFromGlobalDictionaryId,
		rbSelectForDomainId, ddlDomainId, rbSelectRandomId, rbSelectOrderedId,
		txtWordCountId, txtWordStartIndexId, chkCheckAllId, divProgressId) {
	var cklWords = gt(cklWordsId);
	var rbLoadFromGlobalDictionary = gt(rbLoadFromGlobalDictionaryId);
	var rbSelectForDomain = gt(rbSelectForDomainId);
	var ddlDomain = gt(ddlDomainId);
	var rbSelectRandom = gt(rbSelectRandomId);
	var rbSelectOrdered = gt(rbSelectOrderedId);
	var txtWordCount = gt(txtWordCountId);
	var txtWordStartIndex = gt(txtWordStartIndexId);
	var chkCheckAll = gt(chkCheckAllId);
	var divProgress = gt(divProgressId);
	
	var wordsSource = (rbLoadFromGlobalDictionary.checked) ? 2 : 1;
	var wordsSelector;
	if (rbSelectForDomain.checked) {
		wordsSelector = 1;
	} else if (rbSelectRandom.checked) {
			wordsSelector = 2;
		} else if (rbSelectOrdered.checked) {
				wordsSelector = 3;
			}
	
	chkCheckAll.checked = false;
	divProgress.style.display = '';
	
	if (ActivateXMLRequest()) {
		var args = "WordsSource=" + wordsSource + "&WordsSelector=" + wordsSelector +
		"&DomainId=" + ddlDomain.value + "&WordCount=" + txtWordCount.value +
		"&WordStartIndex=" + txtWordStartIndex.value;
		xmlhttp.open('POST', "/LoadWordsHandler.ashx" + '?' + args, true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.send('');
		
		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var response = xmlhttp.responseText;
				
				cklWords.innerHTML = response;
				sleep(200);
				divProgress.style.display = 'none';
			}
		}
	}
}

function LoadWordsInfo(cklWordsOriginalId, cklWordsTranslationId,
	chkNotUseWellKnownWordsId, divProgressId) {
	var cklWordsOriginal = gt(cklWordsOriginalId);
	var cklWordsTranslation = gt(cklWordsTranslationId);
	var chkNotUseWellKnownWords = gt(chkNotUseWellKnownWordsId);
	var divProgress = gt(divProgressId);
	
	divProgress.style.display = '';
	
	if (ActivateXMLRequest()) {
		var args = "NotUseWellKnownWords=" + chkNotUseWellKnownWords.checked;
		xmlhttp.open('POST', "/LoadWordsInfoHandler.ashx" + '?' + args, true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.send('');
		
		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var response = xmlhttp.responseText;
				
				var htmls = response.split(separator);
				cklWordsOriginal.innerHTML = htmls[0];
				cklWordsTranslation.innerHTML = htmls[1];

				sleep(200);
				divProgress.style.display = 'none';
			}
		}
	}
}

function TestWords(cklWordsOriginalId, cklWordsTranslationId) {
	var cklWordsOriginal = gt(cklWordsOriginalId);
	var cklWordsTranslation = gt(cklWordsTranslationId);
	
	var labels = cklWordsOriginal.getElementsByTagName("label");
	var labels2 = cklWordsTranslation.getElementsByTagName("label");
	
	for(var i = 0, len = labels.length; i < len; i++) {
		var id = labels[i].htmlFor.substr(1);
		var original = labels[i].innerHTML;
		var translation = labels2[i].innerHTML;
		var originalTry = gt('x' + id).value;
		var translationTry = gt('t' + id).value;

		gt('r' + id).value = GetWordEqualPercent(original, originalTry);
		gt('p' + id).value = GetWordEqualPercent(translation, translationTry);
	}
}

function GetWordEqualPercent(word1, word2) {
	word1 = word1.toLowerCase();
	word2 = word2.toLowerCase();
	
	if (word1 == word2) {
		return 100;
	} else if (word1.length == 0 || word2.length == 0) {
			return 0;
			} else if (word1.indexOf(word2) >= 0 || word2.indexOf(word1) >= 0) {
					return 90;
					} else {
						var w1 = word1;
						var w2 = word2;
						if (word1.length > word2.length) {
							w1 = word2;
							w2 = word1;
						}

						for(var i = 0, len = w1.length; i < len; i++) {
							w2 = w2.replace(w1.charAt(i), '');
						}

						var percent = 100 * (1 - w2.length / w1.length);
						return (percent > 0) ? Math.round(percent) : 0;
						}
}

function AddTestedWords(cklWordsOriginalId, cklWordsTranslationId, chkCheckAllId,
		messageRegisteringRequiredForAddWords, divProgressId) {
	var cklWordsOriginal = gt(cklWordsOriginalId);
	var cklWordsTranslation = gt(cklWordsTranslationId);
	var chkCheckAll = gt(chkCheckAllId);
	var divProgress = gt(divProgressId);
	
	chkCheckAll.checked = false;
	divProgress.style.display = '';
	
	var ids = "";
	var checks = cklWordsOriginal.getElementsByTagName("input");
	for(var i = 0, len = checks.length; i < len; i++) {
		if (checks[i].type == 'checkbox' && checks[i].checked) {
			ids += checks[i].id;
		}
	}
	checks = cklWordsTranslation.getElementsByTagName("input");
	for(var i = 0, len = checks.length; i < len; i++) {
		if (checks[i].type == 'checkbox' && checks[i].checked) {
			ids += checks[i].id;
		}
	}

	if (ActivateXMLRequest()) {
		var args = ids;
		xmlhttp.open('POST', "/AddTestedWordsHandler.ashx", true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.setRequestHeader("Content-length", args.length);
		xmlhttp.setRequestHeader("Connection", "close");
		xmlhttp.send(args);

		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var success = xmlhttp.responseText.length > 0;

				sleep(200);
				divProgress.style.display = 'none';

				if (!success) {
					alert(messageRegisteringRequiredForAddWords);
				}
			}
		}
	}
}

function ChangePanel(cklWordsOriginalId, cklWordsTranslationId) {
	var cklWordsOriginal = gt(cklWordsOriginalId);
	var cklWordsTranslation = gt(cklWordsTranslationId);
	
	cklWordsOriginal.style.display = (cklWordsOriginal.style.display == 'none') ? '' : 'none';
	cklWordsTranslation.style.display = (cklWordsTranslation.style.display == 'none') ? '' : 'none';
}

function LoadMyWords(cklWordsId, lblCountId, divProgressId) {
	var cklWords = gt(cklWordsId);
	var lblCount = gt(lblCountId);
	var divProgress = gt(divProgressId);
	
	divProgress.style.display = '';
	
	if (ActivateXMLRequest()) {
		var args = "";
		xmlhttp.open('POST', "/LoadMyWordsHandler.ashx" + '?' + args, true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.send('');
		
		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var response = xmlhttp.responseText;

				var separatorPos = response.indexOf(separator);
				lblCount.innerHTML = response.substr(0, separatorPos);

				if (response.length > separatorPos + 1) {
					cklWords.innerHTML = response.substr(separatorPos + 1);
				}

				sleep(200);
				divProgress.style.display = 'none';
			}
		}
	}
}

function DeleteWords(cklWordsId, chkCheckAllId, messageRegisteringRequiredForDeleteWords,
		divProgressId) {
	var cklWords = gt(cklWordsId);
	var chkCheckAll = gt(chkCheckAllId);
	var divProgress = gt(divProgressId);
	
	chkCheckAll.checked = false;
	divProgress.style.display = '';
	
	var checks = gt(cklWordsId).getElementsByTagName("input");
	var ids = "";
	
	for(var i = 0, len = checks.length; i < len; i++) {
		if (checks[i].checked) {
			ids += checks[i].id;
		}
	}

	if (ActivateXMLRequest()) {
		var args = ids;
		xmlhttp.open('POST', "/DeleteWordsHandler.ashx", true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.setRequestHeader("Content-length", args.length);
		xmlhttp.setRequestHeader("Connection", "close");
		xmlhttp.send(args);

		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var success = xmlhttp.responseText.length > 0;

				sleep(200);
				divProgress.style.display = 'none';

				if (!success) {
					alert(messageRegisteringRequiredForDeleteWords);
				}
			}
		}
	}
}

function AddSourceText(txtSrcId) {
	var txtSrc = gt(txtSrcId);

	if (ActivateXMLRequest()) {
		var args = txtSrc.value;
		xmlhttp.open('POST', "/AddSourceTextHandler.ashx", true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.setRequestHeader("Content-length", args.length);
		xmlhttp.setRequestHeader("Connection", "close");
		xmlhttp.send(args);
	}
}

function ChangeControlsState(rbLoadFromProcessPageId,
	rbSelectForDomainId, ddlDomainId, rbSelectRandomId,
	rbSelectOrderedId, txtWordCountId, txtWordStartIndexId) {
	var rbLoadFromProcessPage = gt(rbLoadFromProcessPageId);
	var rbSelectForDomain = gt(rbSelectForDomainId);
	var ddlDomain = gt(ddlDomainId);
	var rbSelectRandom = gt(rbSelectRandomId);
	var rbSelectOrdered = gt(rbSelectOrderedId);
	var txtWordCount = gt(txtWordCountId);
	var txtWordStartIndex = gt(txtWordStartIndexId);

	var state = rbLoadFromProcessPage.checked;

	rbSelectForDomain.disabled = 
	ddlDomain.disabled = 
	rbSelectRandom.disabled = 
	rbSelectOrdered.disabled = 
	txtWordCount.disabled = 
	txtWordStartIndex.disabled = state;
}

function ChangeWordState(srcElement, toggleState, isMakeKnownWords) {
	if (toggleState) {
		srcElement.className = 
			(srcElement.className == knownWordClass)
			? unknownWordClass : knownWordClass;
	} else {
		srcElement.className = 
			(isMakeKnownWords.toLowerCase() == 'true')
			? knownWordClass : unknownWordClass;
	}
}

function ChangeState(id) {
	var spans = document.getElementsByTagName('span');
	
	for(var i = 0, len = spans.length; i < len; i++) {
		if (spans[i].attributes.w != null && 
			spans[i].attributes.w.value == id) {
			ChangeWordState(spans[i], true);
		}
	}
}

function MakeAllWords(cklWordsId, isMakeKnownWords) {
	var words = gt(cklWordsId).getElementsByTagName("span");
	
	for(var i = 0, len = words.length; i < len; i++) {
		ChangeWordState(words[i], false, isMakeKnownWords);
	}
}

function Analysis(cklWordsId, divProgressId) {
	var cklWords = gt(cklWordsId);
	var divProgress = gt(divProgressId);
	
	divProgress.style.display = '';

	var words = cklWords.getElementsByTagName("span");
	var wordsList = "";
	
	for(var i = 0, len = words.length; i < len; i++) {
		wordsList += (words[i].innerHTML + separator);
	}
	
	if (wordsList.length > 0) {
		wordsList = wordsList.substr(0, wordsList.length - 1);
	}

	if (ActivateXMLRequest()) {
		var args = wordsList;
		xmlhttp.open('POST', "/AnalysisHandler.ashx", true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.setRequestHeader("Content-length", args.length);
		xmlhttp.setRequestHeader("Connection", "close");
		xmlhttp.send(args);

		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var response = xmlhttp.responseText;

				for(var i = 0, len = words.length; i < len; i++) {
					words[i].className = 
						(response.charAt(i) == knownWordClass.charAt(0))
						? knownWordClass : unknownWordClass;
				}

				sleep(200);
				divProgress.style.display = 'none';
			}
		}
	}
}

function ProcessUnknownWords(cklWordsId) {
	var cklWords = gt(cklWordsId);

	var words = cklWords.getElementsByTagName("span");
	var wordsList = "";

	for(var i = 0, len = words.length; i < len; i++) {
		if (words[i].className == unknownWordClass) {
			wordsList += (words[i].innerHTML + separator2);
		}
	}
	
	if (wordsList.length > 0) {
		wordsList = wordsList.substr(0, wordsList.length - 1);
	}

	if (ActivateXMLRequest()) {
		var args = wordsList;
		xmlhttp.open('POST', "/ProcessUnknownWordsHandler.ashx", true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.setRequestHeader("Content-length", args.length);
		xmlhttp.setRequestHeader("Connection", "close");
		xmlhttp.send(args);

		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var success = xmlhttp.responseText.length > 0;

				if (success) {
					location.href = xmlhttp.responseText;
				}
			}
		}
	}
}

function UpdateVocabulary(cklWordsId, messageRegisteringRequiredForUpdateVocabulary, divProgressId) {
	var cklWords = gt(cklWordsId);
	var divProgress = gt(divProgressId);

	var words = cklWords.getElementsByTagName("span");
	var wordsKnownList = "";
	var wordsUnknownList = "";
	
	divProgress.style.display = '';

	for(var i = 0, len = words.length; i < len; i++) {
		var info = words[i].attributes.w.value + separator;
		if (words[i].className == knownWordClass) {
			wordsKnownList += info;
		} else {
			wordsUnknownList += info;
		}
	}

	if (wordsKnownList.length > 0) {
		wordsKnownList = wordsKnownList.substr(0, wordsKnownList.length - 1);
	}

	if (wordsUnknownList.length > 0) {
		wordsUnknownList = wordsUnknownList.substr(0, wordsUnknownList.length - 1);
	}

	if (ActivateXMLRequest()) {
		var args = wordsKnownList + separator2 + wordsUnknownList;
		xmlhttp.open('POST', "/UpdateVocabularyHandler.ashx", true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.setRequestHeader("Content-length", args.length);
		xmlhttp.setRequestHeader("Connection", "close");
		xmlhttp.send(args);

		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var success = xmlhttp.responseText.length > 0;

				sleep(200);
				divProgress.style.display = 'none';

				if (!success) {
					alert(messageRegisteringRequiredForUpdateVocabulary);
				}
			}
		}
	}
}

function WordCheck(id) {
	var elem = gt(id);
	var id = elem.id.substr(1);

	var labelOur = (elem.id.charAt(0) == 't')
		? gt('l' + id): gt('a' + id);

	var percentOur = (elem.id.charAt(0) == 't')
		? gt('p' + id): gt('r' + id);

	var checkOur = (elem.id.charAt(0) == 't')
		? gt('w' + id): gt('d' + id);

	if (elem.value == labelOur.title) {
		percentOur.value = 100;
		checkOur.checked = true;
		
		if (elem.nextSibling != null &&
			elem.nextSibling.nextSibling != null &&
			elem.nextSibling.nextSibling.nextSibling != null &&
			elem.nextSibling.nextSibling.nextSibling.nextSibling != null &&
			elem.nextSibling.nextSibling.nextSibling.nextSibling.nextSibling != null &&
			elem.nextSibling.nextSibling.nextSibling.nextSibling.nextSibling.nextSibling &&
			elem.nextSibling.nextSibling.nextSibling.nextSibling.nextSibling.nextSibling.nextSibling &&
			elem.nextSibling.nextSibling.nextSibling.nextSibling.nextSibling.nextSibling.nextSibling.nextSibling) {
			elem.nextSibling.nextSibling.nextSibling.nextSibling.nextSibling.nextSibling.nextSibling.nextSibling.focus();
		}
	}
}

function LoadVerbs(cklWordsId, txtWordCountId, rbLoadPopularId, rbShowForm1Id, rbShowForm2Id,
	rbShowForm3Id, rbShowTranslateId, rbShowRandomId, rbShowAllId, rbSortForm1Id,
	rbSortByTypeId, rbSortTranslateId, rbSortRandomId, chkNotUseWellKnownVerbsId,
	chkCheckAllId, divProgressId) {
	var cklWords = gt(cklWordsId);
	var rbLoadPopular = gt(rbLoadPopularId);
	var txtWordCount = gt(txtWordCountId);
	var rbShowForm1 = gt(rbShowForm1Id);
	var rbShowForm2 = gt(rbShowForm2Id);
	var rbShowForm3 = gt(rbShowForm3Id);
	var rbShowTranslate = gt(rbShowTranslateId);
	var rbShowRandom = gt(rbShowRandomId);
	var rbShowAll = gt(rbShowAllId);
	var rbSortForm1 = gt(rbSortForm1Id);
	var rbSortByType = gt(rbSortByTypeId);
	var rbSortTranslate = gt(rbSortTranslateId);
	var rbSortRandom = gt(rbSortRandomId);
	var chkNotUseWellKnownVerbs = gt(chkNotUseWellKnownVerbsId);
	var chkCheckAll = gt(chkCheckAllId);
	var divProgress = gt(divProgressId);
	
	var showColumn;
	if (rbShowForm1.checked) {
		showColumn = 1;
	} else if (rbShowForm2.checked) {
			showColumn = 2;
		} else if (rbShowForm3.checked) {
				showColumn = 3;
			} else if (rbShowTranslate.checked) {
						showColumn = 4;
					} else if (rbShowRandom.checked) {
								showColumn = 5;
							} else if (rbShowAll.checked) {
									showColumn = 6;
							}
	var sortColumn;
	if (rbSortForm1.checked) {
		sortColumn = 1;
	} else if (rbSortByType.checked) {
			sortColumn = 2;
		} else if (rbSortTranslate.checked) {
				sortColumn = 3;
			} else if (rbSortRandom.checked) {
						sortColumn = 4;
					}
	
	chkCheckAll.checked = false;
	divProgress.style.display = '';
	
	if (ActivateXMLRequest()) {
		var args = "LoadPopular=" + rbLoadPopular.checked +
		"&WordCount=" + txtWordCount.value + "&ShowColumn=" + showColumn +
		"&SortColumn=" + sortColumn + "&NotUseWellKnownVerbs=" + chkNotUseWellKnownVerbs.checked;
		xmlhttp.open('POST', "/LoadVerbsHandler.ashx" + '?' + args, true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.send('');
		
		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var response = xmlhttp.responseText;
				
				cklWords.innerHTML = response;
				sleep(200);
				divProgress.style.display = 'none';
			}
		}
	}
}

function DeleteVerbs(cklWordsId, lblInfoId,
		messageRegisteringRequiredForClearIrregularVerbs, divProgressId) {
	var cklWords = gt(cklWordsId);
	var lblInfo = gt(lblInfoId);
	var divProgress = gt(divProgressId);

	divProgress.style.display = '';

	if (ActivateXMLRequest()) {
		var args = '';
		xmlhttp.open('POST', "/DeleteVerbsHandler.ashx" + '?' + args, true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.send('');

		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var success = xmlhttp.responseText.length > 0;

				sleep(200);
				divProgress.style.display = 'none';
				lblInfo.innerHTML = '0|0%|0%';

				if (!success) {
					alert(messageRegisteringRequiredForClearIrregularVerbs);
				}
			}
		}
	}
}

function AddTestedVerbs(cklWordsId, chkCheckAllId, lblInfoId,
		messageRegisteringRequiredForAddIrregularVerbs, divProgressId) {
	var cklWords = gt(cklWordsId);
	var chkCheckAll = gt(chkCheckAllId);
	var lblInfo = gt(lblInfoId);
	var divProgress = gt(divProgressId);

	chkCheckAll.checked = false;
	divProgress.style.display = '';

	var ids = "";
	var checks = cklWords.getElementsByTagName("input");
	for(var i = 0, len = checks.length; i < len; i++) {
		if (checks[i].type == 'checkbox' && checks[i].checked) {
			ids += checks[i].id;
		}
	}

	if (ActivateXMLRequest()) {
		var args = ids;
		xmlhttp.open('POST', "/AddTestedVerbsHandler.ashx", true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.setRequestHeader("Content-length", args.length);
		xmlhttp.setRequestHeader("Connection", "close");
		xmlhttp.send(args);

		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var success = xmlhttp.responseText.length > 0;

				sleep(200);
				divProgress.style.display = 'none';

				if (!success) {
					alert(messageRegisteringRequiredForAddIrregularVerbs);
				} else {
					lblInfo.innerHTML = xmlhttp.responseText;
				}
			}
		}
	}
}

function TestVerbs(cklWordsId, chkCheckAllId) {
	var cklWords = gt(cklWordsId);
	var chkCheckAll = gt(chkCheckAllId);
	
	chkCheckAll.checked = false;
	var checks = cklWords.getElementsByTagName("input");
	
	for(var i = 0, len = checks.length; i < len; i++) {
		if (checks[i].type == 'checkbox') {
			var id = checks[i].id.substr(1);
			var inputA = gt('a' + id);
			var inputB = gt('b' + id);
			var inputC = gt('c' + id);
			var inputD = gt('d' + id);
			checks[i].checked = (
				(inputA.readOnly || inputA.value == inputA.attributes.v.value) &&
				(inputB.readOnly || inputB.value == inputB.attributes.v.value) &&
				(inputC.readOnly || inputC.value == inputC.attributes.v.value) &&
				(inputD.readOnly || inputD.value == inputD.attributes.v.value));
		}
	}
}

function VerbCheck(id) {
	var elem = gt(id);
	var id = elem.id.substr(1);

	var chk = gt('w' + id);
	var inputA = gt('a' + id);
	var inputB = gt('b' + id);
	var inputC = gt('c' + id);
	var inputD = gt('d' + id);
	
	chk.checked = (
		(inputA.readOnly || inputA.value == inputA.attributes.v.value) &&
		(inputB.readOnly || inputB.value == inputB.attributes.v.value) &&
		(inputC.readOnly || inputC.value == inputC.attributes.v.value) &&
		(inputD.readOnly || inputD.value == inputD.attributes.v.value));
		
	if (elem.readOnly || elem.value == elem.attributes.v.value) {
		var next = GetNextVerbControl(elem);
		if (next != null) next.focus();
	}
}

function GetNextVerbControl(elem) {
	if (elem.nextSibling == null) {
		return null;
	}
	if (!elem.nextSibling.readOnly &&
		elem.nextSibling.type == 'text') {
		return elem.nextSibling;
	} else return GetNextVerbControl(elem.nextSibling);
}

function LoadDictionaryQuiz(tdExamId, hdnStepId, divProgressId) {
	var tdExam = gt(tdExamId);
	var hdnStep = gt(hdnStepId);
	var divProgress = gt(divProgressId);

	divProgress.style.display = '';

	if (ActivateXMLRequest()) {
		var args = "Step=" + hdnStep.value;
		xmlhttp.open('POST', "/LoadDictionaryQuizHandler.ashx" + '?' + args, true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.send('');
		
		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var response = xmlhttp.responseText;
				
				tdExam.innerHTML = response;
				hdnStep.value = parseInt(hdnStep.value) + 1;
				sleep(200);
				divProgress.style.display = 'none';
			}
		}
	}
}

function TryFinishDictionaryQuiz(tdExamId, tblButtonsId, hdnStepId, hdnSuccessCountId,
divProgressId, dictionaryQuizTestsCount, userLogined, messageEnterNick) {
	var tdExam = gt(tdExamId);
	var tblButtons = gt(tblButtonsId);
	var hdnStep = gt(hdnStepId);
	var hdnSuccessCount = gt(hdnSuccessCountId);
	var divProgress = gt(divProgressId);
	
	var checks = tdExam.getElementsByTagName("input");
	for(var i = 0, len = checks.length; i < len; i++) {
		if (checks[i].checked && checks[i].attributes.correct.value == 1) {
			hdnSuccessCount.value = parseInt(hdnSuccessCount.value) + 1;
			break;
		}
	}

	var nick;
	if (parseInt(hdnStep.value) == dictionaryQuizTestsCount) {
		if (userLogined.toLowerCase() == 'false') {
			nick = window.prompt(messageEnterNick, '');
		}
		if (nick == null || nick == undefined || nick.length == 0) {
			nick = 'Anonymous-' + new Date().toTimeString();
		}

		divProgress.style.display = '';

		if (ActivateXMLRequest()) {
			var args = "Nick=" + nick + "&SuccessCount=" + hdnSuccessCount.value;
			xmlhttp.open('POST', "/CompleteDictionaryQuizHandler.ashx" + '?' + args, true);
			xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
			xmlhttp.send('');
			
			xmlhttp.onreadystatechange = function() {
				if (xmlhttp.readyState == 4) {
					var response = xmlhttp.responseText;
					
					tblButtons.style.display='none';
					tdExam.innerHTML = response;
					sleep(200);
					divProgress.style.display = 'none';
				}
			}
		}
	} else {
		LoadDictionaryQuiz(tdExamId, hdnStepId, divProgressId);
	}
}

function LoadWordsOrderQuiz(tdExamId, hdnStepId, divProgressId) {
	var tdExam = gt(tdExamId);
	var hdnStep = gt(hdnStepId);
	var divProgress = gt(divProgressId);

	divProgress.style.display = '';

	if (ActivateXMLRequest()) {
		var args = "Step=" + hdnStep.value;
		xmlhttp.open('POST', "/LoadWordsOrderQuizHandler.ashx" + '?' + args, true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.send('');
		
		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var response = xmlhttp.responseText;
				
				tdExam.innerHTML = response;
				hdnStep.value = parseInt(hdnStep.value) + 1;
				sleep(200);
				divProgress.style.display = 'none';
			}
		}
	}
}

function TryFinishWordsOrderQuiz(tdExamId, mainDivId, tblButtonsId, hdnStepId, hdnSuccessCountId,
divProgressId, WordsOrderQuizTestsCount, userLogined, hdnErrorInfoId, messageEnterNick) {
	var tdExam = gt(tdExamId);
	var mainDiv = gt(mainDivId);
	var tblButtons = gt(tblButtonsId);
	var hdnStep = gt(hdnStepId);
	var hdnSuccessCount = gt(hdnSuccessCountId);
	var divProgress = gt(divProgressId);
	var hdnErrorInfo = gt(hdnErrorInfoId);
	
	var text = mainDiv.attributes.text.value;
	var lastSymbol = text.substr(text.length - 1, 1);
	text = text.substr(0, text.length - 1);
	var words = text.split(' ');
	var spans = mainDiv.getElementsByTagName("span");
	var score = 0;
	
	var equal;
	var allEqual = true;
	var newText = '';
	for(var i = 0, len = words.length; i < len; i++) {
		var newTextData = spans[i].childNodes[1].data;
		newText = newText + ' ' + newTextData;
		
		equal = (newTextData == words[i]);
		if (!equal) {
			allEqual = false;
		}

		if ((i == 0 || i == 1 || i == len - 1 || i == len - 2) && equal) {
			score++;
		}
	}

	if (allEqual) {
		score++;
	}
	
	if (score != 5) {
		hdnErrorInfo.value = hdnErrorInfo.value + ' ' + text + lastSymbol
		+ '<br>' + newText + lastSymbol + '<br><br>';
	}

	hdnSuccessCount.value = parseInt(hdnSuccessCount.value) + score;

	var nick;
	if (parseInt(hdnStep.value) == WordsOrderQuizTestsCount) {
		if (userLogined.toLowerCase() == 'false') {
			nick = window.prompt(messageEnterNick, '');
		}
		if (nick == null || nick == undefined || nick.length == 0) {
			nick = 'Anonymous-' + new Date().toTimeString();
		}

		divProgress.style.display = '';

		if (ActivateXMLRequest()) {
			var args = "Nick=" + nick + "&SuccessCount=" + hdnSuccessCount.value;
			xmlhttp.open('POST', "/CompleteWordsOrderQuizHandler.ashx" + '?' + args, true);
			xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
			xmlhttp.send('');
			
			xmlhttp.onreadystatechange = function() {
				if (xmlhttp.readyState == 4) {
					var response = xmlhttp.responseText;
					
					tblButtons.style.display='none';
					
					var errorNote = (hdnErrorInfo.value.length > 0)
					? '<div class=mainDivWordsOrderQuiz>' + hdnErrorInfo.value + '</div>'
					: '--||--';
					tdExam.innerHTML = response + errorNote;
					sleep(200);
					divProgress.style.display = 'none';
				}
			}
		}
	} else {
		LoadWordsOrderQuiz(tdExamId, hdnStepId, divProgressId);
	}
}

function LoadLevelQuiz(tdExamId, hdnStepId, divProgressId) {
	var tdExam = gt(tdExamId);
	var hdnStep = gt(hdnStepId);
	var divProgress = gt(divProgressId);

	divProgress.style.display = '';

	if (ActivateXMLRequest()) {
		var args = "Step=" + hdnStep.value;
		xmlhttp.open('POST', "/LoadLevelQuizHandler.ashx" + '?' + args, true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.send('');
		
		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var response = xmlhttp.responseText;
				
				tdExam.innerHTML = response;
				hdnStep.value = parseInt(hdnStep.value) + 1;
				sleep(200);
				divProgress.style.display = 'none';
			}
		}
	}
}

function TryFinishLevelQuiz(tdExamId, mainDivId, tblButtonsId, hdnStepId, hdnSuccessCountId,
divProgressId, LevelQuizTestsCount, userLogined, hdnErrorInfoId, messageEnterNick) {
	var tdExam = gt(tdExamId);
	var mainDiv = gt(mainDivId);
	var tblButtons = gt(tblButtonsId);
	var hdnStep = gt(hdnStepId);
	var hdnSuccessCount = gt(hdnSuccessCountId);
	var divProgress = gt(divProgressId);
	var hdnErrorInfo = gt(hdnErrorInfoId);
	
	var text = mainDiv.innerHTML;
	var yourAnswer = '';
	var correctAnswer;
	var success = false;

	var checks = tdExam.getElementsByTagName("input");
	for(var i = 0, len = checks.length; i < len; i++) {
		if (checks[i].checked) yourAnswer = checks[i].nextSibling.innerHTML;
		if (checks[i].attributes.correct.value == 1) correctAnswer = checks[i].nextSibling.innerHTML;
		if (checks[i].checked && checks[i].attributes.correct.value == 1) {
			hdnSuccessCount.value = parseInt(hdnSuccessCount.value) + 1;
			success = true;
			break;
		}
	}

	if (!success) {
		hdnErrorInfo.value = hdnErrorInfo.value + text + ' / ' + correctAnswer + ' / ' + yourAnswer + '<br>';
	}

	var nick;
	if (parseInt(hdnStep.value) == LevelQuizTestsCount) {
		if (userLogined.toLowerCase() == 'false') {
			nick = window.prompt(messageEnterNick, '');
		}
		if (nick == null || nick == undefined || nick.length == 0) {
			nick = 'Anonymous-' + new Date().toTimeString();
		}

		divProgress.style.display = '';

		if (ActivateXMLRequest()) {
			var args = "Nick=" + nick + "&SuccessCount=" + hdnSuccessCount.value;
			xmlhttp.open('POST', "/CompleteLevelQuizHandler.ashx" + '?' + args, true);
			xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
			xmlhttp.send('');
			
			xmlhttp.onreadystatechange = function() {
				if (xmlhttp.readyState == 4) {
					var response = xmlhttp.responseText;
					
					tblButtons.style.display='none';
					
					var errorNote = (hdnErrorInfo.value.length > 0)
					? '<div class=mainDivWordsOrderQuiz>' + hdnErrorInfo.value + '</div>'
					: '--||--';
					tdExam.innerHTML = response + errorNote;
					sleep(200);
					divProgress.style.display = 'none';
				}
			}
		}
	} else {
		LoadLevelQuiz(tdExamId, hdnStepId, divProgressId);
	}
}

function SwapNode(left, nodeId, parentId) {
	var parent = gt(parentId);
	var node = gt(nodeId);
	var prevNode = node.previousSibling;
	var nextNode = node.nextSibling;
		if (left) {
			if (prevNode != null) parent.insertBefore(node, prevNode);
		} else {
			if (nextNode != null) parent.insertBefore(nextNode, node);
		}
}

function SearchFilm(categoryId, txtSearchId, divContentId, lblInfoId, divProgressId) {
	var txtSearch = gt(txtSearchId);
	var divContent = gt(divContentId);
	var lblInfo = gt(lblInfoId);
	var divProgress = gt(divProgressId);

	lblInfo.innerHTML = '';
	divProgress.style.display = '';


	if (ActivateXMLRequest()) {
	    var args = "Category=" + categoryId + "&Search=" + txtSearch.value;
	    xmlhttp.open('POST', "/SearchFilmHandler.ashx" + '?' + args, true);
	    xmlhttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
	    xmlhttp.send('');

	    xmlhttp.onreadystatechange = function () {
	        if (xmlhttp.readyState == 4) {
            	try {

	            var response = xmlhttp.responseText;

	            divContent.innerHTML = response;
	            sleep(200);
	            divProgress.style.display = 'none';

	        }
	        catch (err) {
	            var x = err.Message;
	        }
	        }
	    }
	}
}

function SetFilmMode(isFilmIconMode, divContentId, lblInfoId, divProgressId) {
	var divContent = gt(divContentId);
	var lblInfo = gt(lblInfoId);
	var divProgress = gt(divProgressId);

	lblInfo.innerHTML = '';
	divProgress.style.display = '';

	if (ActivateXMLRequest()) {
		var args = "IsFilmIconMode=" + isFilmIconMode;
		xmlhttp.open('POST', "/SetFilmModeHandler.ashx" + '?' + args, true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.send('');
		
		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
				var response = xmlhttp.responseText;
				
				divContent.innerHTML = response;
				sleep(200);
				divProgress.style.display = 'none';
			}
		}
	}
}

function ChangeLanguage(ddlLanguageNativeId, ddlLanguageLearnId) {
	var ddlLanguageNative = gt(ddlLanguageNativeId);
	var ddlLanguageLearn = gt(ddlLanguageLearnId);
	
	if (ActivateXMLRequest()) {
		var args = "LanguageNativeId=" + ddlLanguageNative.value + 
		"&LanguageLearnId=" + ddlLanguageLearn.value;
		xmlhttp.open('POST', "/ChangeLanguageHandler.ashx" + '?' + args, true);
		xmlhttp.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		xmlhttp.send('');
		
		xmlhttp.onreadystatechange = function() {
			if (xmlhttp.readyState == 4) {
			}
		}
	}
}

ShowModalWindowWithLoadind = function () {

    var c = $('<div id="modalWndDivId" style="text-align: center;" class="box-modal"><img id="modalWndImg" src="/img/ajax-loader-big.gif" /><div id="modalWndContainer"></div></div>');
    c.prepend('<div id="modalWndCloseId"style="display: none;" class="box-modal_close arcticmodal-close">X</div>');

    $.arcticmodal({
        content: c,
        closeOnEsc: false,
        closeOnOverlayClick: false,
        afterClose: function (data, el) {
            document.location = document.location;
        }
    });
}