Imports System.Data

Public Class dlgShippingConfirmation

    Public ShipperID As String = ""
    Dim dtPiezasShipper As DataTable
    Dim requestTimeout As Integer = 90
    Dim fechaRequest As DateTime
    Dim segundosRequest As Integer = 0

    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If dtPiezasShipper Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        
        If (dtPiezasShipper.Rows.Count > 0) Then
            If MsgBox("Are you sure to cancel the shipper validation process?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Me.Close()
            Else
                Exit Sub
            End If
        End If
        Me.Close()
    End Sub

    Private Sub dlgShippingConfirmation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lbShipperID.Text = ShipperID
        lbUserName.Text = NombreUsuario
        lbStatus.Text = "Requesting"
        Application.DoEvents()
        Habilitar(False)
        tmrArrancaShipper.Enabled = True

        
    End Sub

    Private Function CargaPiezasShipper(ByVal ShipperID As String) As Boolean
        CargaPiezasShipper = False
        While segundosRequest < requestTimeout
            Dim ts As New TimeSpan
            ts = DateTime.Now - fechaRequest
            segundosRequest = ts.TotalSeconds
            Dim dt As New DataTable
            ConsultaSQL("Select * from [ShipperRequests] Where ShipperId='" & ShipperID & "' and downloaded='1' ", dt)
            If dt.Rows.Count > 0 Then
                dtPiezasShipper = New DataTable()
                ConsultaSQL("Select * from [ShipperData] Where ShipperId='" & ShipperID & "'  ", dtPiezasShipper)
                If dtPiezasShipper.Rows.Count > 0 Then
                    CargaPiezasShipper = True
                    lbStatus.Text = "Downloaded"

                    Dim dv2 As New DataView(dtPiezasShipper, "Checked=true", "", DataViewRowState.CurrentRows)
                    lbLots.Text = dv2.ToTable().Rows.Count & " / " & dtPiezasShipper.Rows.Count & " lots"
                    Habilitar(True)
                    txtLotSerial.Text = ""
                    txtLotSerial.Focus()
                    Exit Function
                Else
                    MsgBox("No lot serials for this Shipper ID found.", MsgBoxStyle.Exclamation)
                    CargaPiezasShipper = False
                    btnCancel.Enabled = True
                    Exit Function
                End If
            End If
        End While
        MsgBox("Request timeout exceeded (" & requestTimeout & " seconds) ", MsgBoxStyle.Exclamation)
        btnCancel.Enabled = True
        CargaPiezasShipper = False
    End Function

    Private Sub Habilitar(ByVal activo As Boolean)

        btnFinish.Enabled = activo
        btnValidar.Enabled = activo
        Button1.Enabled = activo
        txtLotSerial.ReadOnly = Not activo
        Application.DoEvents()
    End Sub

    Private Sub btnValidar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidar.Click
        If txtLotSerial.Text <> "" Then
            If txtLotSerial.Text.Substring(0, 1).ToUpper() = "S" Then
                Validar(txtLotSerial.Text.Substring(1, txtLotSerial.Text.Length - 1))
            Else
                Validar(txtLotSerial.Text)
            End If
        End If
    End Sub

    Private Sub Validar(ByVal LotSerial As String)
        Dim dv As New DataView(dtPiezasShipper, "LotSerial='" & LotSerial & "'", "", DataViewRowState.CurrentRows)
        If dv.ToTable.Rows.Count > 0 Then

            dv(0).Item("Checked") = True

            Dim dv2 As New DataView(dtPiezasShipper, "Checked=true", "", DataViewRowState.CurrentRows)
            lbLots.Text = dv2.ToTable().Rows.Count & " / " & dtPiezasShipper.Rows.Count & " lots"
            txtLotSerial.Text = ""
            txtLotSerial.Focus()
        Else
            If MsgBox(LotSerial & " no exists on this shipper data, if you're sure this lot is for this shipping, please press 'Cancel' " & _
                "and talk with logistics department to change shipper.", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                Me.Close()
            Else
                txtLotSerial.Text = ""
                txtLotSerial.Focus()
            End If
        End If
    End Sub

    Private Sub txtLotSerial_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLotSerial.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    If txtLotSerial.Text <> "" Then
        '        If txtLotSerial.Text.Substring(0, 1).ToUpper() = "S" Then
        '            Validar(txtLotSerial.Text.Substring(1, txtLotSerial.Text.Length - 1))
        '        Else
        '            Validar(txtLotSerial.Text)
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub txtLotSerial_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLotSerial.KeyPress
        If e.KeyChar = vbCr Then
            If txtLotSerial.Text <> "" Then
                If txtLotSerial.Text.Substring(0, 1).ToUpper() = "S" Then
                    Validar(txtLotSerial.Text.Substring(1, txtLotSerial.Text.Length - 1))
                Else
                    Validar(txtLotSerial.Text)
                End If
            End If
        End If
    End Sub

    Private Sub txtLotSerial_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLotSerial.TextChanged
       
    End Sub

    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        Dim dv2 As New DataView(dtPiezasShipper, "Checked=true", "", DataViewRowState.CurrentRows)
        If dv2.ToTable.Rows.Count = dtPiezasShipper.Rows.Count Then
            Habilitar(False)
            lbStatus.Text = "VALIDATING"
            Application.DoEvents()
            EjecutaSQL("Update ShipperData Set Checked='1', CheckedUser='" & CodUsuario & "' Where ShipperID='" & ShipperID & "'")
            lbStatus.Text = "SENDING MAIL"
            Application.DoEvents()
            EjecutaSQL("Update ShipperRequests set MailingStatus='1' Where [ShipperID]='" + ShipperID + "'")
            Threading.Thread.Sleep(1500)
            Me.Close()
        Else
            Dim lotesfaltan As Integer = dtPiezasShipper.Rows.Count - dv2.ToTable.Rows.Count
            MsgBox("Can't finish until all lots are validated." & vbCrLf & _
                    lotesfaltan & " lots missing")
        End If
    End Sub

    Private Sub EnviarMailConfirmacion()

    End Sub

    Private Sub tmrArrancaShipper_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrArrancaShipper.Tick
        tmrArrancaShipper.Enabled = False

        Dim dt As New DataTable
        ConsultaSQL("Select * from ShipperRequests Where ShipperID='" & ShipperID & "'", dt)
        If dt.Rows.Count > 0 Then
            If (MsgBox("The Shipper ID " & ShipperID & " was downloaded on " & ExtraeCampo(dt.Rows(0).Item("Requestdate")) & _
             " " & ".Do you wish to download again?", MsgBoxStyle.YesNo) = MsgBoxResult.No) Then
                fechaRequest = DateTime.Now
                lbStatus.Text = "Downloaded"
                Application.DoEvents()
                CargaPiezasShipper(ShipperID)
                Habilitar(True)
                Exit Sub
            Else
                EjecutaSQL("Delete from ShipperRequests Where ShipperID='" & ShipperID & "'")
            End If
        End If

        fechaRequest = DateTime.Now
        EjecutaSQL("INSERT INTO [dbo].[ShipperRequests] ([ShipperID],[RequestDate])  VALUES (" + _
        "'" & ShipperID & "'," + _
        "GetDate())")
        CargaPiezasShipper(ShipperID)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a As New dlgShippingLots
        a.dtPiezasShipper = dtPiezasShipper
        a.ShowDialog()
        txtLotSerial.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim dv2 As New DataView(dtPiezasShipper, "Checked=true", "", DataViewRowState.CurrentRows)
        If MsgBox("You're going to save & close to continue before,¿Are you sure?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Habilitar(False)
            For Each row As DataRow In dv2.ToTable.Rows
                EjecutaSQL("Update ShipperData Set Checked='1', CheckedUser='" & CodUsuario & "' Where  LotSerial='" & row("LotSerial") & "' and  ShipperID='" & ShipperID & "'")
            Next



            Me.Close()
        End If
        
    End Sub
End Class