<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        PictureBox1 = New PictureBox()
        PictureBox2 = New PictureBox()
        Label8 = New Label()
        Label9 = New Label()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Red
        Label1.Font = New Font("Segoe UI", 150F, FontStyle.Regular, GraphicsUnit.Point)
        Label1.ForeColor = Color.White
        Label1.Image = My.Resources.Resources.PantallaRojoPublico
        Label1.Location = New Point(2, 31)
        Label1.MinimumSize = New Size(330, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(330, 265)
        Label1.TabIndex = 0
        Label1.Text = "00"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Blue
        Label2.Font = New Font("Segoe UI", 150F, FontStyle.Regular, GraphicsUnit.Point)
        Label2.ForeColor = Color.White
        Label2.Image = My.Resources.Resources.PantallaAzulPublico
        Label2.Location = New Point(450, 34)
        Label2.MinimumSize = New Size(330, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(330, 265)
        Label2.TabIndex = 1
        Label2.Text = "00"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 80.25F, FontStyle.Regular, GraphicsUnit.Point)
        Label3.Image = My.Resources.Resources.PantallaCronoPublico
        Label3.Location = New Point(232, 372)
        Label3.Name = "Label3"
        Label3.Size = New Size(315, 142)
        Label3.TabIndex = 2
        Label3.Text = "00:00"
        Label3.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BackColor = Color.Black
        Label4.Font = New Font("Segoe UI", 72F, FontStyle.Regular, GraphicsUnit.Point)
        Label4.ForeColor = Color.Yellow
        Label4.Location = New Point(338, 95)
        Label4.Name = "Label4"
        Label4.Size = New Size(106, 128)
        Label4.TabIndex = 3
        Label4.Text = "0"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(366, 73)
        Label5.Name = "Label5"
        Label5.Size = New Size(40, 15)
        Label5.TabIndex = 4
        Label5.Text = "Asalto"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label6.ForeColor = Color.Red
        Label6.Location = New Point(12, 9)
        Label6.Name = "Label6"
        Label6.Size = New Size(113, 17)
        Label6.TabIndex = 5
        Label6.Text = "Competidor Rojo"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label7.ForeColor = Color.Blue
        Label7.Location = New Point(657, 9)
        Label7.Name = "Label7"
        Label7.Size = New Size(112, 17)
        Label7.TabIndex = 6
        Label7.Text = "Competidor Azul"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = My.Resources.Resources._0Faltas
        PictureBox1.Location = New Point(2, 299)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(102, 111)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 7
        PictureBox1.TabStop = False
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = My.Resources.Resources._0Faltas
        PictureBox2.Location = New Point(678, 302)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(102, 111)
        PictureBox2.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox2.TabIndex = 8
        PictureBox2.TabStop = False
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.BackColor = Color.Red
        Label8.Font = New Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point)
        Label8.ForeColor = SystemColors.ButtonHighlight
        Label8.Location = New Point(216, 302)
        Label8.Name = "Label8"
        Label8.Size = New Size(56, 65)
        Label8.TabIndex = 9
        Label8.Text = "0"
        Label8.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.BackColor = Color.Blue
        Label9.Font = New Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point)
        Label9.ForeColor = SystemColors.ButtonHighlight
        Label9.Location = New Point(495, 302)
        Label9.Name = "Label9"
        Label9.Size = New Size(56, 65)
        Label9.TabIndex = 10
        Label9.Text = "0"
        Label9.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Form2
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(784, 509)
        Controls.Add(Label9)
        Controls.Add(Label8)
        Controls.Add(PictureBox2)
        Controls.Add(PictureBox1)
        Controls.Add(Label7)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        MinimumSize = New Size(800, 480)
        Name = "Form2"
        Text = "Público"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
End Class
