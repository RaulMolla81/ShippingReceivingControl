Public Class dlgShipperRequest

    Public ok As Boolean = False
    Public shiperID As String

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ok = False
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ok = True
        shiperID = "s" + txtShipperID.Text
        Me.Close()
    End Sub

    Private Sub dlgShipperRequest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtShipperID.Focus()
    End Sub

    Private Sub txtShipperID_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtShipperID.KeyDown

    End Sub
End Class