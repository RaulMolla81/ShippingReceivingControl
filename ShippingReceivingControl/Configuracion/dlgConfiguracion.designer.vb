<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class dlgConfiguracion
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
        Me.txtServidor = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtBaseProd = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtUserBD = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtPassBD = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'txtServidor
        '
        Me.txtServidor.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.txtServidor.Location = New System.Drawing.Point(3, 30)
        Me.txtServidor.Name = "txtServidor"
        Me.txtServidor.Size = New System.Drawing.Size(224, 23)
        Me.txtServidor.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(3, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 17)
        Me.Label1.Text = "Server:"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(3, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 18)
        Me.Label3.Text = "Database:"
        '
        'txtBaseProd
        '
        Me.txtBaseProd.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.txtBaseProd.Location = New System.Drawing.Point(3, 75)
        Me.txtBaseProd.Name = "txtBaseProd"
        Me.txtBaseProd.Size = New System.Drawing.Size(224, 23)
        Me.txtBaseProd.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(3, 101)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 15)
        Me.Label5.Text = "User BD:"
        '
        'txtUserBD
        '
        Me.txtUserBD.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.txtUserBD.Location = New System.Drawing.Point(3, 117)
        Me.txtUserBD.Name = "txtUserBD"
        Me.txtUserBD.Size = New System.Drawing.Size(224, 23)
        Me.txtUserBD.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(-1, 141)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(94, 20)
        Me.Label6.Text = "Pass BD:"
        '
        'txtPassBD
        '
        Me.txtPassBD.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.txtPassBD.Location = New System.Drawing.Point(3, 162)
        Me.txtPassBD.Name = "txtPassBD"
        Me.txtPassBD.Size = New System.Drawing.Size(224, 23)
        Me.txtPassBD.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Green
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(7, 227)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(90, 33)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "Accept"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Red
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(130, 227)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(97, 33)
        Me.Button2.TabIndex = 10
        Me.Button2.Text = "Cancel"
        '
        'dlgConfiguracion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtPassBD)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtUserBD)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtBaseProd)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtServidor)
        Me.KeyPreview = True
        Me.Name = "dlgConfiguracion"
        Me.Text = "SQL Settings"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtServidor As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBaseProd As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtUserBD As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPassBD As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
