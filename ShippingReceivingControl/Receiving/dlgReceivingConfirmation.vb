Imports System.Data

Public Class dlgReceivingConfirmation

    Public ReceptionID As String = ""
    Dim dtPiezasReception As DataTable
    Dim requestTimeout As Integer = 90
    Dim fechaRequest As DateTime
    Dim segundosRequest As Integer = 0

    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If dtPiezasReception Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        If (dtPiezasReception.Rows.Count > 0) Then
            If MsgBox("Are you sure to cancel the reception validatioin process?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Me.Close()
            Else
                Exit Sub
            End If
        End If
        Me.Close()
    End Sub

    Private Sub dlgShippingConfirmation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lbReceptionID.Text = ReceptionID
        lbUserName.Text = NombreUsuario
        lbStatus.Text = "Requesting"
        Application.DoEvents()
        Habilitar(False)
        tmrArrancaShipper.Enabled = True
    End Sub

    Private Function CargaPiezasReception(ByVal PO_nbr As String) As Boolean
        CargaPiezasReception = False
        While segundosRequest < requestTimeout
            Dim ts As New TimeSpan
            ts = DateTime.Now - fechaRequest
            segundosRequest = ts.TotalSeconds
            Dim dt As New DataTable
            If ConsultaSQL("Select * from ReceptionRequests Where PO_nbr='" & PO_nbr & "' and downloaded='1' ", dt) = 0 Then
                Exit Function
            End If
            If dt.Rows.Count > 0 Then
                dtPiezasReception = New DataTable()
                If ConsultaSQL("Select po_nbr, ItemNumber, LotSerial, Reference, SupplierID, Qty, Checked, CheckedUser, isNull(Added,'false') As Added from ReceptionData Where PO_nbr='" & PO_nbr & "'  ", dtPiezasReception) = 0 Then
                    Exit Function
                End If
                If dtPiezasReception.Rows.Count > 0 Then
                    CargaPiezasReception = True
                    lbStatus.Text = "Downloaded"
                    Dim dv2 As New DataView(dtPiezasReception, "Checked=true", "", DataViewRowState.CurrentRows)
                    Dim lotesMal As Integer = dtPiezasReception.Rows.Count - dv2.ToTable().Rows.Count
                    lbLots.Text = dv2.ToTable().Rows.Count & " / " & lotesMal & " /" & dtPiezasReception.Rows.Count & " lots"
                    Habilitar(True)
                    txtLotSerial.Text = ""
                    txtLotSerial.Focus()
                    Exit Function
                Else
                    MsgBox("No lot serials found for this Reception ID.", MsgBoxStyle.Exclamation)
                    CargaPiezasReception = False
                    btnCancel.Enabled = True
                    Exit Function
                End If
            End If
        End While
        MsgBox("Request timeout exceeded (" & requestTimeout & " seconds) ", MsgBoxStyle.Exclamation)
        btnCancel.Enabled = True
        CargaPiezasReception = False
    End Function

    Private Sub Habilitar(ByVal activo As Boolean)

        btnFinish.Enabled = activo
        btnValidar.Enabled = activo
        txtLotSerial.ReadOnly = Not activo
        Button1.Enabled = activo
        Application.DoEvents()
    End Sub

    Private Sub btnValidar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidar.Click
        If txtLotSerial.Text <> "" Then
            If txtLotSerial.Text.Substring(0, 1).ToUpper() = "S" Then
                Validar(txtLotSerial.Text.Substring(1, txtLotSerial.Text.Length - 1), False)
            ElseIf txtLotSerial.Text.Substring(0, 1).ToUpper() = "R" Then
                Validar(txtLotSerial.Text.Substring(1, txtLotSerial.Text.Length - 1), True)
            Else
                Validar(txtLotSerial.Text.Substring(0, txtLotSerial.Text.Length), False)
            End If
        End If
    End Sub

    Private Sub Validar(ByVal LotSerial As String, ByVal Hilo As Boolean)
        If Not Hilo Then
            Dim dv As New DataView(dtPiezasReception, "LotSerial='" & LotSerial & "'", "", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count > 0 Then
                dv(0).Item("Checked") = True
                Dim dv2 As New DataView(dtPiezasReception, "Checked=true", "", DataViewRowState.CurrentRows)
                Dim lotesMal As Integer = dtPiezasReception.Rows.Count - dv2.ToTable().Rows.Count
                lbLots.Text = dv2.ToTable().Rows.Count & " / " & lotesMal & " / " & dtPiezasReception.Rows.Count & " lots"
                txtLotSerial.Text = ""
                txtLotSerial.Focus()
            Else
                If MsgBox(LotSerial & " no exists on this reception data, press ok if you want to add to the reception table, cancel to avoid this lot.", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                    If dtPiezasReception.Rows.Count > 0 Then
                        Dim rowC As System.Data.DataRow = dtPiezasReception.NewRow()
                        rowC("po_nbr") = ReceptionID
                        rowC("itemNumber") = ""
                        rowC("LotSerial") = LotSerial
                        rowC("Reference") = ""
                        rowC("SupplierID") = ""
                        rowC("Qty") = 0
                        rowC("CheckedUser") = ""
                        rowC("Checked") = False
                        rowC("Added") = True
                        dtPiezasReception.Rows.Add(rowC)
                        Dim dv2 As New DataView(dtPiezasReception, "Checked=true", "", DataViewRowState.CurrentRows)

                        Dim lotesMal As Integer = dtPiezasReception.Rows.Count - dv2.ToTable().Rows.Count
                        lbLots.Text = dv2.ToTable().Rows.Count & " / " & lotesMal & " / " & dtPiezasReception.Rows.Count & " lots"
                        txtLotSerial.Text = ""
                        txtLotSerial.Focus()
                    End If
                Else
                    txtLotSerial.Text = ""
                    txtLotSerial.Focus()
                End If
            End If
        Else
            Dim dv As New DataView(dtPiezasReception, "Reference='" & LotSerial & "'", "", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count > 0 Then
                dv(0).Item("Checked") = True
                Dim dv2 As New DataView(dtPiezasReception, "Checked=true", "", DataViewRowState.CurrentRows)
                Dim lotesMal As Integer = dtPiezasReception.Rows.Count - dv2.ToTable().Rows.Count
                lbLots.Text = dv2.ToTable().Rows.Count & " / " & lotesMal & " / " & dtPiezasReception.Rows.Count & " lots"
                txtLotSerial.Text = ""
                txtLotSerial.Focus()
            Else
                If MsgBox(LotSerial & " no exists on this reception data, press ok if you want to add to the reception table, cancel to avoid this lot.", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                    If dtPiezasReception.Rows.Count > 0 Then

                        Dim rowC As System.Data.DataRow = dtPiezasReception.NewRow()
                        rowC("po_nbr") = ReceptionID
                        rowC("itemNumber") = ""
                        rowC("LotSerial") = ""
                        rowC("Reference") = LotSerial
                        rowC("SupplierID") = ""
                        rowC("Qty") = 0
                        rowC("CheckedUser") = ""
                        rowC("Checked") = False
                        rowC("Added") = True
                        dtPiezasReception.Rows.Add(rowC)

                        Dim dv2 As New DataView(dtPiezasReception, "Checked=true", "", DataViewRowState.CurrentRows)

                        Dim lotesMal As Integer = dtPiezasReception.Rows.Count - dv2.ToTable().Rows.Count
                        lbLots.Text = dv2.ToTable().Rows.Count & " / " & lotesMal & " / " & dtPiezasReception.Rows.Count & " lots"
                        txtLotSerial.Text = ""
                        txtLotSerial.Focus()
                    End If
                Else
                    txtLotSerial.Text = ""
                    txtLotSerial.Focus()
                End If
            End If
        End If
        
    End Sub

    Private Sub txtLotSerial_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLotSerial.KeyDown

        If e.KeyCode = Keys.Enter Then
            
        End If
    End Sub

    Private Sub txtLotSerial_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLotSerial.KeyPress
        If e.KeyChar = vbCr Then
            If txtLotSerial.Text <> "" Then
                If txtLotSerial.Text.Substring(0, 1).ToUpper() = "S" Then
                    Validar(txtLotSerial.Text.Substring(1, txtLotSerial.Text.Length - 1), False)
                ElseIf txtLotSerial.Text.Substring(0, 1).ToUpper() = "R" Then
                    Validar(txtLotSerial.Text.Substring(1, txtLotSerial.Text.Length - 1), True)
                Else
                    Validar(txtLotSerial.Text.Substring(0, txtLotSerial.Text.Length), False)
                End If
            End If
        End If
    End Sub

    Private Sub txtLotSerial_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLotSerial.TextChanged

    End Sub

    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        Dim dv2 As New DataView(dtPiezasReception, "Checked=true", "", DataViewRowState.CurrentRows)
        Dim piezasOK As Integer = dv2.ToTable().Rows.Count
        Dim dv As New DataView(dtPiezasReception, "Added=true", "", DataViewRowState.CurrentRows)
        Dim piezasAñadidas As Integer = dv.ToTable().Rows.Count
        Dim dv3 As New DataView(dtPiezasReception, "Checked=false and Added=false", "", DataViewRowState.CurrentRows)
        Dim piezasFaltan As Integer = dv3.ToTable().Rows.Count
        Dim resultadoRecepcion As String = "OK"
        If piezasAñadidas > 0 Or piezasFaltan > 0 Then
            resultadoRecepcion = "NOT OK"
        End If

        If MsgBox("You're going to finish reception, reception result is " & resultadoRecepcion & vbCrLf & _
        piezasOK & " LOTS OK" & vbCrLf & _
        piezasFaltan & " MISSING LOTS " & vbCrLf & _
        piezasAñadidas & " ADDED LOTS " & vbCrLf & ", Do you wish to finish?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Habilitar(False)
            lbStatus.Text = "VALIDATING"
            Application.DoEvents()
            For Each row As DataRow In dtPiezasReception.Rows
                If ExtraeCampo(row("Added"), False) = True Then
                    'Añadimos el registro nuevo
                    EjecutaSQL("INSERT INTO [dbo].[ReceptionData] ([po_nbr],[ItemNumber],[LotSerial],[Reference]," + _
                    "[SupplierID],[Qty],[Checked],[CheckedUser],[Added]) values( " & _
                    "'" & ExtraeCampo(row("po_nbr")) & "'," & _
                    "'" & ExtraeCampo(row("ItemNumber")) & "'," & _
                    "'" & ExtraeCampo(row("LotSerial")) & "'," & _
                    "'" & ExtraeCampo(row("Reference")) & "'," & _
                    "'" & ExtraeCampo(row("SupplierID")) & "'," & _
                    "'" & ExtraeCampo(row("Qty")) & "'," & _
                    "'" & "0" & "'," & _
                    "'" & CodUsuario & "'," & _
                    "'" & "1" & "')")
                Else
                    'Actualizamos segun el serial Reference
                    EjecutaSQL("Update ReceptionData Set Checked='1', CheckedUser='" & CodUsuario & "' Where PO_nbr='" & ExtraeCampo(row("po_nbr")) & "' and LotSerial='" & ExtraeCampo(row("LotSerial")) & "' and Reference='" & ExtraeCampo(row("Reference")) & "'")
                End If
            Next

            lbStatus.Text = "SENDING MAIL"
            Application.DoEvents()
            EjecutaSQL("Update ReceptionRequests set MailingStatus='1' Where [PO_nbr]='" + ReceptionID + "'")
            Threading.Thread.Sleep(1500)
            Me.Close()
        End If

    End Sub

    Private Sub EnviarMailConfirmacion(ByVal poNbr As String)
       

    End Sub

    Private Sub tmrArrancaShipper_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrArrancaShipper.Tick
        tmrArrancaShipper.Enabled = False

        Dim dt As New DataTable
        ConsultaSQL("Select * from ReceptionRequests Where PO_nbr='" & ReceptionID & "'", dt)
        If dt.Rows.Count > 0 Then
            If (MsgBox("The Reception ID " & ReceptionID & " was downloaded on " & ExtraeCampo(dt.Rows(0).Item("Requestdate")) & _
             " " & ".Do you wish to download again?", MsgBoxStyle.YesNo) = MsgBoxResult.No) Then
                fechaRequest = DateTime.Now
                lbStatus.Text = "Downloaded"
                Application.DoEvents()
                EjecutaSQL("Delete ReceptionData Where po_nbr='" + ReceptionID + "' and added='1'")
                EjecutaSQL("Update ReceptionData  Set Checked='0' Where po_nbr='" + ReceptionID + "' ")
                CargaPiezasReception(ReceptionID)
                Habilitar(True)
                Exit Sub
            Else
                EjecutaSQL("Delete from ReceptionRequests Where PO_nbr='" & ReceptionID & "'")
            End If
        End If
        fechaRequest = DateTime.Now
        EjecutaSQL("INSERT INTO [dbo].ReceptionRequests ([PO_nbr],[RequestDate])  VALUES (" + _
        "'" & ReceptionID & "'," + _
        "GetDate())")
        CargaPiezasReception(ReceptionID)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a As New dlgShippingLots
        a.dtPiezasShipper = dtPiezasReception
        a.receiving = True
        a.ShowDialog()
        txtLotSerial.Focus()
    End Sub

    Private Sub lbStatus_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbStatus.ParentChanged

    End Sub

    Private Sub lbLots_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbLots.ParentChanged

    End Sub
End Class