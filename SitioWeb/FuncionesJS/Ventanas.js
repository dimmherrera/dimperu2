
function WinOpen(tipo,url,name,pWidth,pHeight,pLeft,pTop)
{
  
  if(tipo==1)
      var x = window.open(url, name, 'alwaysraised=yes,scrollbars=no,menubar=no,scrolbar=0,toolbar=0,status=no,width=' + pWidth + ',height=' + pHeight + ',left=' + pLeft + ',top=' + pTop + '');
  	
  if (tipo == 2) 
      var x = window.showModalDialog(url, window, 'scroll:yes;status:off;dialogWidth:' + pWidth + 'px;dialogHeight:' + pHeight + 'px;dialogLeft:' + pLeft + 'px;dialogTop:' + pTop + 'px'); 
  
  if (tipo==3)
  {	
  
	var cod = event.srcElement.parentElement.children(0).innerText;
	var x = window.open(url + "?id=" + cod, window, 'scrolbar=0,toolbar=0,status=no,scroll:no;status:off;dialogWidth:' + pWidth + 'px;dialogHeight:' + pHeight + 'px;dialogLeft:' + pLeft + 'px;dialogTop:' + pTop + 'px'); 
  }
  if(tipo==4)
  {
    var cod = event.srcElement.parentElement.children(0).innerText;
	var x=window.open(url + "?id="+ cod, name, 'alwaysraised=yes,scrollbars=no,menubar=no,scrolbar=0,toolbar=0,status=no,width='+pWidth+',height='+pHeight+',left='+pLeft+',top='+pTop+'');
}		
  
}

function ClosePopUp() {

    if (navigator.appName == "Microsoft Internet Explorer")
        window.close();
    else if (navigator.appName == "Netscape")
        window.close;
	
}


function WinCloseOpener(evenTarget){

try {


if(arguments.length>0){

if(evenTarget!='xx')

{

window.opener.__doPostBack(evenTarget,'');

}

}

opener.focus()

} 

//}

catch(e){}

finally {

ClosePopUp();
}

}

function ValidaOpener(evenTarget, operTarget){

try {

		if(opener){

		if(arguments.length>0)
			{

			window.opener.document.all(operTarget).style.visibility = 'visible';

			if(evenTarget!='xx') window.__doPostBack(evenTarget,'');

			}

		} 
	}

catch(e){

ClosePopUp();

}

finally {}

}

function CerrarVentana(evenTarget) {

    try {
        if (navigator.appName == "Microsoft Internet Explorer") {

            window.dialogArguments.__doPostBack(evenTarget, '');
            ClosePopUp();


        }

        else {

            window.dialogArguments.__doPostBack(evenTarget, '');

            ClosePopUp();

        }

    }

    catch (e) { alert('Error' + ' ' + e); }



}

function CerrarVentanaNeg(evenTarget) {
    try {

        window.dialogArguments.__doPostBack(evenTarget, '');
        ClosePopUp();

    }
    catch (e) { alert('Error' + ' ' + e) }
}




