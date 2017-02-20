function DetalleRequisito(pTabla, pClass, jClass, sClass) 
{
    var id = event.srcElement.parentElement.rowIndex;
    
    if (event.srcElement.parentElement.tagName == 'TR') {
        if (event.srcElement.parentElement.cells(0).className == pClass) {

            for (var i = 0; i < event.srcElement.parentElement.cells.length; i++)
                event.srcElement.parentElement.cells(i).className = sClass;
                
                document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Codigo.value = '';
                document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = '';
                document.aspnetForm.ctl00_ContentPlaceHolder1_DP_Estados.value = 'S';
                
                document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.readOnly = true;
                document.aspnetForm.ctl00_ContentPlaceHolder1_DP_Estados.disabled = true;
                
                document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.className = 'clsDisabled';
                
            return;

        }

        
        var cod = event.srcElement.parentElement.cells(0).innerText;
        var des = event.srcElement.parentElement.cells(1).innerText;
        var est = event.srcElement.parentElement.cells(2).innerText;
                 
        NormalClass(pTabla, pClass, jClass);
        J_RolClass_Tabla(pTabla, pClass);
        var posicion = event.srcElement.parentElement.rowIndex;
        document.aspnetForm.ctl00_ContentPlaceHolder1_Hf_pos.value = posicion;
        //document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = des;
        
        if (est == 'ACTIVO')
        {
            document.aspnetForm.ctl00_ContentPlaceHolder1_DP_Estados.value = 'A';
        }
        else
        {
            document.aspnetForm.ctl00_ContentPlaceHolder1_DP_Estados.value = 'I';
        }
        
                
        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.readOnly = false;
        document.aspnetForm.ctl00_ContentPlaceHolder1_DP_Estados.disabled = false;

        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Codigo.value = cod;
        
        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.value = des;
        document.aspnetForm.ctl00_ContentPlaceHolder1_Txt_Descripcion.className = 'clsMandatorio';
        document.aspnetForm.ctl00_ContentPlaceHolder1_DP_Estados.className = 'clsMandatorio';

       __doPostBack('ctl00$ContentPlaceHolder1$Lb_Mod', '');

        //document.getElementById('ctl00_ContentPlaceHolder1_btn_Guardar').disabled = false;
  
        
        return;
    }
}