// Archivo JScript
/*
function CalculaPuntos()
{
                If Trim(Me.Txt_Tasa_Base.Text) = "" Then Exit Sub
                If Trim(Me.Txt_Spread.Text) = "" Then Exit Sub
                If Trim(Me.Txt_Tnego.Text) = "" Then Exit Sub

                If Trim(Txt_Tasa_Base.Text) = fmt_dec Then Txt_Tasa_Base.Text = fmt_sdm
                If Trim(Txt_Spread.Text) = fmt_dec Then Txt_Spread.Text = fmt_sdm
                If Trim(Txt_Tnego.Text) = fmt_dec Then Txt_Tnego.Text = fmt_sdm

                If Txt_Spread.Text > 0 Then
                    Me.Txt_Puntos.Text = Format(Txt_Tnego.Text - Txt_Spread.Text - Txt_Tasa_Base.Text, fmt_cdm)
                Else
                    Txt_Spread.Text = Format(Txt_Tnego.Text - Txt_Tasa_Base.Text, fmt_cdm)
                    Txt_Puntos.Text = "0.0"
                End If
                

             If Trim(TB_SIMULACION(0).Text) = "" Then Exit Sub
             If Trim(TB_SIMULACION(1).Text) = "" Then Exit Sub
             If Trim(TB_SIMULACION(3).Text) = "" Then Exit Sub
    
             If Trim(TB_SIMULACION(0).Text) = RUTINAS.DEC() Then TB_SIMULACION(0).Text = RUTINAS.CeroDec()
             If Trim(TB_SIMULACION(1).Text) = RUTINAS.DEC() Then TB_SIMULACION(1).Text = RUTINAS.CeroDec()
             If Trim(TB_SIMULACION(3).Text) = RUTINAS.DEC() Then TB_SIMULACION(3).Text = RUTINAS.CeroDec()
    
             If TB_SIMULACION(1).Tag > 0 Then             
                TB_SIMULACION(2).Text = TB_SIMULACION(3).Text - TB_SIMULACION(1).Text - TB_SIMULACION(0).Text
             Else
                TB_SIMULACION(1).Text = TB_SIMULACION(3).Text - TB_SIMULACION(0).Text
                TB_SIMULACION(2).Text = "0.0"
             End If
}

*/