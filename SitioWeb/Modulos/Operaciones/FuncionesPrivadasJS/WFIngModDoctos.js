function CambioTipoCliente()
{
	if (window.document.forms[0].DP_TipoDeudor.value == '1')
	    {
	    window.document.forms[0].Txt_ApePaterno.visible = false;
	    window.document.forms[0].Txt_ApeMaterno.visible = false;
	    }
	else
		{
	    window.document.forms[0].Txt_ApePaterno.visible = true;
	    window.document.forms[0].Txt_ApeMaterno.visible = true;
		}	
	return;
}
