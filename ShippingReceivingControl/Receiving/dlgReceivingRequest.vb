Public Class dlgReceivingRequest

    Public ok As Boolean = False
    Public receptionID As String

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ok = False
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ok = True
        receptionID = txtShipperID.Text
        Me.Close()
    End Sub

    Private Sub dlgShipperRequest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtShipperID.Focus()
    End Sub
End Class