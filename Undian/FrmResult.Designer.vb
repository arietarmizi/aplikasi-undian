<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmResult
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmResult))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.BtnShowAll = New System.Windows.Forms.Button()
        Me.BtnHapusGP = New System.Windows.Forms.Button()
        Me.lblGrandprize = New System.Windows.Forms.Label()
        Me.dgv2 = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgv2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(-1, 61)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(750, 514)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.BtnShowAll)
        Me.TabPage2.Controls.Add(Me.BtnHapusGP)
        Me.TabPage2.Controls.Add(Me.lblGrandprize)
        Me.TabPage2.Controls.Add(Me.dgv2)
        Me.TabPage2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(742, 488)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Selamat Kepada :"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'BtnShowAll
        '
        Me.BtnShowAll.BackColor = System.Drawing.Color.LightYellow
        Me.BtnShowAll.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnShowAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnShowAll.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnShowAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnShowAll.Location = New System.Drawing.Point(503, 444)
        Me.BtnShowAll.Name = "BtnShowAll"
        Me.BtnShowAll.Size = New System.Drawing.Size(123, 28)
        Me.BtnShowAll.TabIndex = 80
        Me.BtnShowAll.Text = "&Tampilkan Semua"
        Me.BtnShowAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnShowAll.UseVisualStyleBackColor = False
        '
        'BtnHapusGP
        '
        Me.BtnHapusGP.BackColor = System.Drawing.Color.Salmon
        Me.BtnHapusGP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnHapusGP.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnHapusGP.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnHapusGP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnHapusGP.Location = New System.Drawing.Point(635, 444)
        Me.BtnHapusGP.Name = "BtnHapusGP"
        Me.BtnHapusGP.Size = New System.Drawing.Size(100, 28)
        Me.BtnHapusGP.TabIndex = 79
        Me.BtnHapusGP.Text = "&Hapus Semua"
        Me.BtnHapusGP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnHapusGP.UseVisualStyleBackColor = False
        '
        'lblGrandprize
        '
        Me.lblGrandprize.AutoSize = True
        Me.lblGrandprize.Location = New System.Drawing.Point(173, 188)
        Me.lblGrandprize.Name = "lblGrandprize"
        Me.lblGrandprize.Size = New System.Drawing.Size(73, 16)
        Me.lblGrandprize.TabIndex = 78
        Me.lblGrandprize.Text = "Grandprize"
        Me.lblGrandprize.Visible = False
        '
        'dgv2
        '
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgv2.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgv2.Location = New System.Drawing.Point(1, 4)
        Me.dgv2.Margin = New System.Windows.Forms.Padding(2)
        Me.dgv2.Name = "dgv2"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv2.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv2.RowHeadersWidth = 35
        Me.dgv2.RowTemplate.Height = 28
        Me.dgv2.Size = New System.Drawing.Size(752, 425)
        Me.dgv2.TabIndex = 35
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Location = New System.Drawing.Point(-3, -1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(857, 56)
        Me.Panel2.TabIndex = 76
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(10, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(418, 39)
        Me.Label3.TabIndex = 76
        Me.Label3.Text = "Daftar Pemenang Undian"
        '
        'FrmResult
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(745, 576)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FrmResult"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pemenang Undian"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.dgv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents dgv2 As DataGridView
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents lblGrandprize As Label
    Friend WithEvents BtnHapusGP As Button
    Friend WithEvents BtnShowAll As Button
End Class
