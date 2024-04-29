Imports System.IO
Imports System.Text
Imports System.Collections.Generic

Public Class dlgConfiguracion
    Public ok As Boolean = False

    Private Sub dlgConfiguracion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode 
            Case Keys.ControlKey, Keys.Control, 194
                Me.Close()
            Case Keys.Tab
            Case Keys.Alt, 193
                Guardar()
        End Select
    End Sub

    Private Sub dlgConfiguracion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CargarDatosConfiguracion()
        txtBaseProd.Text = BaseDatos
        txtServidor.Text = Servidor
        txtUserBD.Text = userBD
        txtPassBD.Text = passBD

      
        txtServidor.Focus()
    End Sub


    Private Sub Guardar()


        Dim mydocpath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) & "\" & "Config.ini"
        Dim sb As New StringBuilder()
        sb.Append("BASEDATOS=" & txtBaseProd.Text & vbCrLf)
        sb.Append("SERVIDOR=" & txtServidor.Text & vbCrLf)
        sb.Append("USERBD=" & txtUserBD.Text & vbCrLf)
        sb.Append("PASSBD=" & txtPassBD.Text & vbCrLf)

        BaseDatos = txtBaseProd.Text
        Servidor = txtServidor.Text
        userBD = txtUserBD.Text
        passBD = txtPassBD.Text

        'If IO.File.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) & "\" & "Config.ini") Then
        '    IO.File.Delete(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) & "\" & "Config.ini")
        'End If



        Using outfile As New StreamWriter(mydocpath)
            outfile.Write(sb.ToString())
        End Using

        CargarDatosConfiguracion()
        Me.Close()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtServidor_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtServidor.GotFocus, txtBaseProd.GotFocus, txtPassBD.GotFocus, txtUserBD.GotFocus
        Dim txt As TextBox = sender
        txt.SelectionStart = 0
        txt.SelectionLength = txt.Text.Length
        txt.SelectAll()
    End Sub

    Private Sub txtServidor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServidor.TextChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        
        Guardar()
        If CargaConexionSQL() Then
            ok = True
            Me.Close()
         
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ok = False
        Me.Close()
    End Sub
End Class