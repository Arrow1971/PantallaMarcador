Public Class Form2

    Private Sub Form2_Resize(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
        Dim W1 As Integer = 800
        Dim W2 As Integer = Me.Width
        Dim scaleW As Single = W2 / W1
        Me.Height = 480 * scaleW
        ' MsgBox("Escala Ancho: " & scaleW & " Largo: " & Me.Height)
        Rescale(Label1, 150, scaleW, 15, 20, 325) ' puntos rojo
        Rescale(Label2, 150, scaleW, 445, 20, 325) ' puntos azul
        Rescale(Label3, 80, scaleW, 230, 290, 315) ' timer
        Rescale(Label4, 65, scaleW, 347, 140, 0) ' asaltos
        Rescale(Label5, 9, scaleW, 360, 120, 0) ' asaltos etiqueta
        Rescale(Label6, 10, scaleW, 15, 0, 0) ' rojo etiqueta
        Rescale(Label7, 10, scaleW, 445, 0, 0) ' azul etiqueta
        Rescale(Label8, 40, scaleW, 150, 250, 80) ' asaltos ganados rojo
        Rescale(Label9, 40, scaleW, 550, 250, 80) ' asaltos ganados rojo
        Rescale_1(PictureBox1, scaleW, 10, 260) 'faltas rojo
        Rescale_1(PictureBox2, scaleW, 670, 260) 'faltas azul
        ' Label1.Text = "Escala Ancho: " & scaleW & " Largo: " & Me.Height
    End Sub
    Private Sub Rescale(label As Label, fuente As Integer, scale As Single, x As Integer, y As Integer, minum As Integer)
        label.Font = New Font(label.Font.Bold, fuente * scale)
        label.Left = x * scale
        label.Top = y * scale
        label.MinimumSize = New Drawing.Size(Int(minum * scale), 0)
    End Sub

    Private Sub Rescale_1(picture As PictureBox, scale As Single, x As Integer, y As Integer)

        picture.Left = x * scale
        picture.Top = y * scale
        picture.Size = New Size(110 * scale, 110 * scale)
    End Sub

    Private Sub Form2_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Form1.Pantalla = False
        Form1.PictureBox27.Image = My.Resources.Off

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class