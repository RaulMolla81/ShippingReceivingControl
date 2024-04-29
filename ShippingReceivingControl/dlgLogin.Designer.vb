<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class dlgLogin
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
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtGlobalID = New System.Windows.Forms.TextBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Red
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(136, 163)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(97, 33)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Cancel"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(9, 163)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(90, 33)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Accept"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular)
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(13, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 33)
        Me.Label3.Text = "Global ID:"
        '
        'txtGlobalID
        '
        Me.txtGlobalID.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular)
        Me.txtGlobalID.Location = New System.Drawing.Point(9, 96)
        Me.txtGlobalID.Name = "txtGlobalID"
        Me.txtGlobalID.Size = New System.Drawing.Size(224, 29)
        Me.txtGlobalID.TabIndex = 0
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(13, 235)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(86, 23)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Settings"
        '
        'dlgLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtGlobalID)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "dlgLogin"
        Me.Text = "Login"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtGlobalID As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
End Class
