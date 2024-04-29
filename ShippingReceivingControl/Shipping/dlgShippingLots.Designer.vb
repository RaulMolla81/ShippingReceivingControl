<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class dlgShippingLots
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    Private mainMenu1 As System.Windows.Forms.MainMenu

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.btnCancel = New System.Windows.Forms.Button
        Me.Tabla = New System.Windows.Forms.ListView
        Me.cLot = New System.Windows.Forms.ColumnHeader
        Me.cChecked = New System.Windows.Forms.ColumnHeader
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btnCancel.Location = New System.Drawing.Point(45, 249)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(153, 27)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Close"
        '
        'Tabla
        '
        Me.Tabla.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Tabla.Columns.Add(Me.cLot)
        Me.Tabla.Columns.Add(Me.cChecked)
        Me.Tabla.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.Tabla.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Tabla.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.Tabla.Location = New System.Drawing.Point(13, 14)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.Size = New System.Drawing.Size(216, 229)
        Me.Tabla.TabIndex = 5
        Me.Tabla.View = System.Windows.Forms.View.Details
        '
        'cLot
        '
        Me.cLot.Text = "LOT SERIAL"
        Me.cLot.Width = 190
        '
        'cChecked
        '
        Me.cChecked.Text = "Checked"
        Me.cChecked.Width = 0
        '
        'dlgShippingLots
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.Tabla)
        Me.Controls.Add(Me.btnCancel)
        Me.KeyPreview = True
        Me.Name = "dlgShippingLots"
        Me.Text = "Shipping Lots"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Tabla As System.Windows.Forms.ListView
    Friend WithEvents cLot As System.Windows.Forms.ColumnHeader
    Friend WithEvents cChecked As System.Windows.Forms.ColumnHeader
End Class
