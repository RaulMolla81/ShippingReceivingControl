<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class dlgShippingConfirmation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgShippingConfirmation))
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.btnCancel = New System.Windows.Forms.Button
        Me.lbUserName = New System.Windows.Forms.Label
        Me.lbShipperID = New System.Windows.Forms.Label
        Me.lbStatus = New System.Windows.Forms.Label
        Me.btnFinish = New System.Windows.Forms.Button
        Me.txtLotSerial = New System.Windows.Forms.TextBox
        Me.btnValidar = New System.Windows.Forms.Button
        Me.lbLots = New System.Windows.Forms.Label
        Me.tmrArrancaShipper = New System.Windows.Forms.Timer
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btnCancel.Location = New System.Drawing.Point(44, 219)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(153, 24)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
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
        'lbShipperID
        '
        Me.lbShipperID.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbShipperID.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lbShipperID.ForeColor = System.Drawing.Color.White
        Me.lbShipperID.Location = New System.Drawing.Point(0, 0)
        Me.lbShipperID.Name = "lbShipperID"
        Me.lbShipperID.Size = New System.Drawing.Size(240, 25)
        Me.lbShipperID.Text = "Shipper ID"
        Me.lbShipperID.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lbStatus
        '
        Me.lbStatus.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbStatus.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lbStatus.ForeColor = System.Drawing.Color.White
        Me.lbStatus.Location = New System.Drawing.Point(0, 25)
        Me.lbStatus.Name = "lbStatus"
        Me.lbStatus.Size = New System.Drawing.Size(240, 25)
        Me.lbStatus.Text = "STATUS"
        Me.lbStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnFinish
        '
        Me.btnFinish.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btnFinish.Location = New System.Drawing.Point(44, 192)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(153, 25)
        Me.btnFinish.TabIndex = 2
        Me.btnFinish.Text = "Finish"
        '
        'txtLotSerial
        '
        Me.txtLotSerial.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular)
        Me.txtLotSerial.Location = New System.Drawing.Point(7, 59)
        Me.txtLotSerial.Name = "txtLotSerial"
        Me.txtLotSerial.Size = New System.Drawing.Size(224, 29)
        Me.txtLotSerial.TabIndex = 0
        '
        'btnValidar
        '
        Me.btnValidar.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btnValidar.Location = New System.Drawing.Point(44, 93)
        Me.btnValidar.Name = "btnValidar"
        Me.btnValidar.Size = New System.Drawing.Size(153, 28)
        Me.btnValidar.TabIndex = 1
        Me.btnValidar.Text = "Validate"
        '
        'lbLots
        '
        Me.lbLots.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbLots.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lbLots.ForeColor = System.Drawing.Color.White
        Me.lbLots.Location = New System.Drawing.Point(0, 247)
        Me.lbLots.Name = "lbLots"
        Me.lbLots.Size = New System.Drawing.Size(240, 22)
        Me.lbLots.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'tmrArrancaShipper
        '
        Me.tmrArrancaShipper.Interval = 1000
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(44, 127)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(153, 26)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "View Lots"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Button2.Location = New System.Drawing.Point(44, 161)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(153, 25)
        Me.Button2.TabIndex = 11
        Me.Button2.Text = "Save && Close"
        '
        'dlgShippingConfirmation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lbLots)
        Me.Controls.Add(Me.btnValidar)
        Me.Controls.Add(Me.txtLotSerial)
        Me.Controls.Add(Me.btnFinish)
        Me.Controls.Add(Me.lbStatus)
        Me.Controls.Add(Me.lbShipperID)
        Me.Controls.Add(Me.lbUserName)
        Me.Controls.Add(Me.btnCancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "dlgShippingConfirmation"
        Me.Text = "Shipper Confirmation"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lbUserName As System.Windows.Forms.Label
    Friend WithEvents lbShipperID As System.Windows.Forms.Label
    Friend WithEvents lbStatus As System.Windows.Forms.Label
    Friend WithEvents btnFinish As System.Windows.Forms.Button
    Friend WithEvents txtLotSerial As System.Windows.Forms.TextBox
    Friend WithEvents btnValidar As System.Windows.Forms.Button
    Friend WithEvents lbLots As System.Windows.Forms.Label
    Friend WithEvents tmrArrancaShipper As System.Windows.Forms.Timer
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
