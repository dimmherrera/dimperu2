// Archivo JScript
/* Funciones para ocultar las filas */

		function oculta_capa (capa) {
		if (document.getElementById(capa).style.visibility == "visible") {
			document.getElementById(capa).style.visibility = "hidden";
		}
		else if (document.getElementById(capa).style.visibility == "hidden") {
			document.getElementById(capa).style.visibility = "visible";
		}
		else {
			document.getElementById(capa).style.visibility = "hidden";
		}
		}

		function oculta_capa_display (capa) {
		if (document.getElementById(capa).style.display == "block") {
			document.getElementById(capa).style.display = "none";
		}
		else if (document.getElementById(capa).style.display == "none") {
			document.getElementById(capa).style.display = "block";
		}
		else {
			document.getElementById(capa).style.display = "none";
		}
		}
		
	//TAB
       /*
        function PanelClick(sender, e) {
            var Messages = $get('<%=Messages.ClientID%>');
            Highlight(Messages);
        }

        */
        
        function ActiveTabChanged(sender, e) {
        /*
            var CurrentTab = $get('<%=CurrentTab.ClientID%>');
            CurrentTab.innerHTML = sender.get_activeTab().get_headerText();
            Highlight(CurrentTab);
            */
        }
        

        var HighlightAnimations = {};
        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = AjaxControlToolkit.Animation.createAnimation({
                    AnimationName : "color",
                    duration : 0.5,
                    property : "style",
                    propertyKey : "backgroundColor",
                    startValue : "#FFFF90",
                    endValue : "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }
        
        function ToggleHidden(value) {
            $find('<%=Tabs.ClientID%>').get_tabs()[2].set_enabled(value);
        }
//******************************************************************





function sack(file) {
this.xmlhttp = null;

this.resetData = function() {
this.method = "POST";
this.queryStringSeparator = "?";
this.argumentSeparator = "&";
this.URLString = "";
this.encodeURIString = true;
this.execute = false;
this.element = null;
this.elementObj = null;
this.requestFile = file;
this.vars = new Object();
this.responseStatus = new Array(2);
};

this.resetFunctions = function() {
this.onLoading = function() { };
this.onLoaded = function() { };
this.onInteractive = function() { };
this.onCompletion = function() { };
this.onError = function() { };
this.onFail = function() { };
};

this.reset = function() {
this.resetFunctions();
this.resetData();
};

this.createAJAX = function() {
try {
this.xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
} catch (e1) {
try {
this.xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
} catch (e2) {
this.xmlhttp = null;
}
}

if (! this.xmlhttp) {
if (typeof XMLHttpRequest != "undefined") {
this.xmlhttp = new XMLHttpRequest();
} else {
this.failed = true;
}
}
};

this.setVar = function(name, value){
this.vars[name] = Array(value, false);
};

this.encVar = function(name, value, returnvars) {
if (true == returnvars) {
return Array(encodeURIComponent(name), encodeURIComponent(value));
} else {
this.vars[encodeURIComponent(name)] = Array(encodeURIComponent(value), true);
}
}

this.processURLString = function(string, encode) {
encoded = encodeURIComponent(this.argumentSeparator);
regexp = new RegExp(this.argumentSeparator + "|" + encoded);
varArray = string.split(regexp);
for (i = 0; i < varArray.length; i++){
urlVars = varArray[i].split("=");
if (true == encode){
this.encVar(urlVars[0], urlVars[1]);
} else {
this.setVar(urlVars[0], urlVars[1]);
}
}
}

this.createURLString = function(urlstring) {
if (this.encodeURIString && this.URLString.length) {
this.processURLString(this.URLString, true);
}

if (urlstring) {
if (this.URLString.length) {
this.URLString += this.argumentSeparator + urlstring;
} else {
this.URLString = urlstring;
}
}

// prevents caching of URLString
this.setVar("rndval", new Date().getTime());

urlstringtemp = new Array();
for (key in this.vars) {
if (false == this.vars[key][1] && true == this.encodeURIString) {
encoded = this.encVar(key, this.vars[key][0], true);
delete this.vars[key];
this.vars[encoded[0]] = Array(encoded[1], true);
key = encoded[0];
}

urlstringtemp[urlstringtemp.length] = key + "=" + this.vars[key][0];
}
if (urlstring){
this.URLString += this.argumentSeparator + urlstringtemp.join(this.argumentSeparator);
} else {
this.URLString += urlstringtemp.join(this.argumentSeparator);
}
}

this.runResponse = function() {
eval(this.response);
}

this.runAJAX = function(urlstring) {
if (this.failed) {
this.onFail();
} else {
this.createURLString(urlstring);
if (this.element) {
this.elementObj = document.getElementById(this.element);
}
if (this.xmlhttp) {
var self = this;
if (this.method == "GET") {
totalurlstring = this.requestFile + this.queryStringSeparator + this.URLString;
this.xmlhttp.open(this.method, totalurlstring, true);
} else {
this.xmlhttp.open(this.method, this.requestFile, true);
try {
this.xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded")
} catch (e) { }
}

this.xmlhttp.onreadystatechange = function() {
switch (self.xmlhttp.readyState) {
case 1:
self.onLoading();
break;
case 2:
self.onLoaded();
break;
case 3:
self.onInteractive();
break;
case 4:
self.response = self.xmlhttp.responseText;
self.responseXML = self.xmlhttp.responseXML;
self.responseStatus[0] = self.xmlhttp.status;
self.responseStatus[1] = self.xmlhttp.statusText;

if (self.execute) {
self.runResponse();
}

if (self.elementObj) {
elemNodeName = self.elementObj.nodeName;
elemNodeName.toLowerCase();
if (elemNodeName == "input"
|| elemNodeName == "select"
|| elemNodeName == "option"
|| elemNodeName == "textarea") {
self.elementObj.value = self.response;
} else {
self.elementObj.innerHTML = self.response;
}
}
if (self.responseStatus[0] == "200") {
self.onCompletion();
} else {
self.onError();
}

self.URLString = "";
break;
}
};

this.xmlhttp.send(this.URLString);
}
}
};

this.reset();
this.createAJAX();
}



var enableCache = true;
var jsCache = new Array();

var dynamicContent_ajaxObjects = new Array();

function ajax_showContent(divId,ajaxIndex,url)
{
var targetObj = document.getElementById(divId);
targetObj.innerHTML = dynamicContent_ajaxObjects[ajaxIndex].response;
if(enableCache){
jsCache[url] = dynamicContent_ajaxObjects[ajaxIndex].response;
}
dynamicContent_ajaxObjects[ajaxIndex] = false;

ajax_parseJs(targetObj)
}

function ajax_parseJs(inputObj)
{
var jsTags = inputObj.getElementsByTagName('SCRIPT');
for(var no=0;no<jsTags.length;no++){
eval(jsTags[no].innerHTML);
}
}

function ajax_loadContent(divId,url)
{
    if(enableCache && jsCache[url])
    {
    document.getElementById(divId).innerHTML = jsCache[url];
    }
    return;

var ajaxIndex = dynamicContent_ajaxObjects.length;
document.getElementById(divId).innerHTML = jsCache[url]; //'Cargando Página - <a href="http://tuarroberos.blogcindario.com" target="_blank">Visita Blog Tuarroba</a>';
dynamicContent_ajaxObjects[ajaxIndex] = new sack();
dynamicContent_ajaxObjects[ajaxIndex].requestFile = url; // Specifying which file to get
dynamicContent_ajaxObjects[ajaxIndex].onCompletion = function(){ ajax_showContent(divId,ajaxIndex,url); }; // Specify function that will be executed after file has been found
dynamicContent_ajaxObjects[ajaxIndex].runAJAX(); // Execute AJAX function
}