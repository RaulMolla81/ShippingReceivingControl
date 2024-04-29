Imports System.Data

Public Class dlgShippingLots
    Public dtPiezasShipper As DataTable
    Public receiving As Boolean = False
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub dlgShippingLots_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If receiving Then
            Me.Text = "Receiving Lots"
        End If
        Tabla.Items.Clear()
        Dim dv As New DataView(dtPiezasShipper, "", "Checked asc", DataViewRowState.CurrentRows)
        For Each row As DataRow In dv.ToTable.Rows
            Dim item As New ListViewItem
            Dim lotSerial As String = row.Item("LotSerial")
            item.Text = lotSerial
            Dim checkeado As String = IIf(ExtraeCampo(row("Checked"), False), "Y", "N")
            item.SubItems.Add(checkeado)
            If checkeado = "N" Then
                item.BackColor = Color.LightSalmon
                item.ForeColor = Color.Black
            Else
                item.BackColor = Color.LightGreen
                item.ForeColor = Color.Black
            End If

            Tabla.Items.Add(item)
        Next
    End Sub
End Class