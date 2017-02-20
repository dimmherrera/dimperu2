Imports FuncionesGenerales.FComunes
Imports System.Data.SqlClient

Public Class Formulas

    Public Function MAX(ByVal Par1 As Object, ByVal Par2 As Object) As Object

        MAX = IIf(Par1 > Par2, Par1, Par2)

    End Function

    Public Function MIN(ByVal Par1 As Object, ByVal Par2 As Object) As Object
        MIN = IIf(Par1 < Par2, Par1, Par2)
    End Function

    

End Class
