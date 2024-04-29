<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmPrincipal
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Private mainMenu1 As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrincipal))
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.lbUserName = New System.Windows.Forms.Label
        Me.Button4 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(45, 33)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(153, 32)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Shipping"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Button2.Location = New System.Drawing.Point(45, 85)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(153, 32)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Receiving"
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Button3.Location = New System.Drawing.Point(45, 164)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(153, 32)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Login"
        '
        'lbUserName
        '
        Me.lbUserName.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbUserName.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lbUserName.ForeColor = System.Drawing.Color.White
        Me.lbUserName.Location = New System.Drawing.Point(0, 269)
        Me.lbUserName.Name = "lbUserName"
        Me.lbUserName.Size = New System.Drawing.Size(240, 25)
        Me.lbUserName.Text = "User Name"
        Me.lbUserName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Button4.Location = New System.Drawing.Point(45, 202)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(153, 32)
        Me.Button4.TabIndex = 4
        Me.Button4.Text = "Exit"
        '
        'frmPrincipal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.lbUserName)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPrincipal"
        Me.Text = "Shipping Receiving Control"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents lbUserName As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button

End Class
