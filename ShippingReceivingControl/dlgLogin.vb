Imports System.Data
Public Class dlgLogin

    Public ok As Boolean
    Public inicio As Boolean


    Private Sub dlgLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtGlobalID.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim dt As New DataTable
        ConsultaSQL("Select * from Users where userId='" & txtGlobalID.Text & "'", dt)
        If dt.Rows.Count > 0 Then
            NombreUsuario = ExtraeCampo(dt.Rows(0).Item("UserName"))
            CodUsuario = ExtraeCampo(dt.Rows(0).Item("userId"))
            ok = True
            Me.Close()
        Else
            MsgBox("No valid global id.")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ok = False
        Me.Close()
    End Sub

    Private Sub txtGlobalID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtGlobalID.KeyDown
         
    End Sub

    Private Sub txtGlobalID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGlobalID.KeyPress
       
    End Sub

    Private Sub txtGlobalID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGlobalID.TextChanged

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim a As New dlgConfiguracion
        a.ShowDialog()
    End Sub
End Class