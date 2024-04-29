Imports System.Data
Imports System.Data.SqlClient


Public Class frmPrincipal

    Private Sub frmPrincipal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CargarDatosConfiguracion()
        If Not CargaConexionSQL() Then
            Dim a As New dlgConfiguracion
            a.ShowDialog()
            If Not a.ok Then
                Application.Exit()
            End If
        End If
        Dim b As New dlgLogin
        b.ShowDialog()
        If b.ok Then
            lbUserName.Text = NombreUsuario
            lbUserName.Tag = CodUsuario
        Else
            Button1.Enabled = False
            Button2.Enabled = False
        End If
    End Sub

 
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
        Application.Exit()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim b As New dlgLogin
        b.ShowDialog()
        If b.ok Then
            lbUserName.Text = NombreUsuario
            lbUserName.Tag = CodUsuario
            Button1.Enabled = True
            Button2.Enabled = True
        Else
            If CodUsuario = "" Then
                Button1.Enabled = False
                Button2.Enabled = False
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a As New dlgShipperRequest
        a.Owner = Me
        a.ShowDialog()
        If a.ok Then
            Dim b As New dlgShippingConfirmation
            b.Owner = Me
            b.ShipperID = a.shiperID
            b.ShowDialog()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim a As New dlgReceivingRequest
        a.Owner = Me
        a.ShowDialog()
        If a.ok Then
            Dim b As New dlgReceivingConfirmation
            b.Owner = Me
            b.ReceptionID = a.receptionID
            b.ShowDialog()
        End If
    End Sub
End Class
