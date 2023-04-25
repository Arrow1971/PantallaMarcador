Imports System.Threading
Imports SharpDX.XInput

Public Class Form1
    Private Minutos As Integer
    Private Segundos As Byte
    Private MinutosDescanso As Integer
    Private SegundosDescanso As Byte
    Private Asaltos As Byte
    Private ReadOnly Controller1 As New Controller(UserIndex.One)
    Private ReadOnly Controller2 As New Controller(UserIndex.Two)
    Private ReadOnly Controller3 As New Controller(UserIndex.Three)
    '----- Faltas -------------------------
    Private FaltasAzul As Byte
    Private FaltasRojo As Byte
    Private FaltasMax As Byte

    '----- puntos por acción --------------
    Private cabeza As Byte
    Private peto As Byte
    Private puño As Byte
    Private girocabeza As Byte
    Private giropeto As Byte

    '----- botones del mando para azul------
    Private BAM1 As Boolean ' -- Botón A del Mando 1 ---
    Private BAM2 As Boolean
    Private BAM3 As Boolean
    Private BBM1 As Boolean
    Private BBM2 As Boolean
    Private BBM3 As Boolean
    Private BYM1 As Boolean
    Private BYM2 As Boolean
    Private BYM3 As Boolean
    Private BXM1 As Boolean
    Private BXM2 As Boolean
    Private BXM3 As Boolean
    Private BRSM1 As Boolean
    Private BRSM2 As Boolean
    Private BRSM3 As Boolean

    '----- botones del mando para rojo------
    Private BDM1 As Boolean ' -- Boton Down del Mando 1 ---
    Private BDM2 As Boolean
    Private BDM3 As Boolean
    Private BLM1 As Boolean
    Private BLM2 As Boolean
    Private BLM3 As Boolean
    Private BUM1 As Boolean
    Private BUM2 As Boolean
    Private BUM3 As Boolean
    Private BRM1 As Boolean
    Private BRM2 As Boolean
    Private BRM3 As Boolean
    Private BLSM1 As Boolean
    Private BLSM2 As Boolean
    Private BLSM3 As Boolean

    '----- intervalos para puntos azules --
    Private IPA1 As Boolean ' -- Intervalo Punto Azul 1 ---
    Private IPA2 As Boolean
    Private IPA3 As Boolean
    Private IPGCA As Boolean  ' -- Intervalo Punto Giro Azul ---
    Private IPGPA As Boolean

    '----- intervalos para puntos rojos --
    Private IPR1 As Boolean
    Private IPR2 As Boolean
    Private IPR3 As Boolean
    Private IPGCR As Boolean
    Private IPGPR As Boolean

    '------ Hilo para la lectura de mandos --------
    Private HiloMando As Thread
    Public PuntosRojo_Form2 As Label = Form2.Label1
    Public PuntosAzul_Form2 As Label = Form2.Label2

    '------- Hilos para la entrada del punto -------
    Private PuntosAzul As Integer = 0
    Private Punto1A As Thread
    Private Punto2A As Thread
    Private Punto3A As Thread
    Private PuntoGiroCascoA As Thread
    Private PuntoGiroPetoA As Thread
    Private PuntosRojo As Integer = 0
    Private Punto1R As Thread
    Private Punto2R As Thread
    Private Punto3R As Thread
    Private PuntoGiroPetoR As Thread
    Private PuntoGiroCascoR As Thread

    Private final As Boolean = True
    Public Pantalla As Boolean
    Public TipoCombate As Boolean = True ' false por puntos -- true por asaltos


    Public Sub XInputController()

        '' mandamos imagen al formulario
        If Controller1.IsConnected Then
            Mando1.Image = My.Resources.MandoOn
        Else
            Mando1.Image = My.Resources.MandoOff
        End If
        If Controller2.IsConnected Then
            Mando2.Image = My.Resources.MandoOn
        Else
            Mando2.Image = My.Resources.MandoOff
        End If
        If Controller3.IsConnected Then
            Mando3.Image = My.Resources.MandoOn
        Else
            Mando3.Image = My.Resources.MandoOff
        End If

    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Marcador TKD"
        CheckForIllegalCrossThreadCalls = False
    End Sub
    Private Sub LecturaMandos()
        Do
            If Controller1.IsConnected Then

                Dim GPS1 As Gamepad = Controller1.GetState.Gamepad
                Mando1.Image = My.Resources.MandoOn
                'Label2.Text = GPS1.Buttons.ToString 

                Label2.Text = Controller1.GetBatteryInformation(BatteryDeviceType.Gamepad).BatteryLevel.ToString
                Select Case GPS1.Buttons.ToString
                    Case "A"
                        Mando1.Image = My.Resources.A
                        If IPA1 = False Then
                            BAM1 = True
                            Punto1A = New Thread(AddressOf CuentaAtras1A)
                            Punto1A.Start()
                        Else
                            If BAM2 = True Or BAM3 Then
                                PuntosAzul += puño
                                LabelPuAzul.Text += 1
                                BAM2 = False
                                BAM3 = False

                            End If

                        End If

                    Case "B"
                        Mando1.Image = My.Resources.B
                        If IPA2 = False Then
                            BBM1 = True
                            Punto2A = New Thread(AddressOf CuentaAtras2A)
                            Punto2A.Start()
                        Else
                            If BBM2 = True Or BBM3 = True Then
                                PuntosAzul += peto
                                LabelPAzul.Text += 1
                                BBM2 = False
                                BBM3 = False


                            End If

                        End If

                    Case "Y"
                        Mando1.Image = My.Resources.Y
                        If IPA3 = False Then
                            BYM1 = True
                            Punto3A = New Thread(AddressOf CuentaAtras3A)
                            Punto3A.Start()
                        Else
                            If BYM2 = True Or BYM3 = True Then
                                PuntosAzul += cabeza
                                LabelCAzul.Text += 1
                                BYM2 = False
                                BYM3 = False


                            End If

                        End If

                    Case "X" 'IPGPR
                        Mando1.Image = My.Resources.X
                        If IPGPA = False Then
                            BXM1 = True
                            PuntoGiroPetoA = New Thread(AddressOf CuentaAtrasGiroPetoA)
                            PuntoGiroPetoA.Start()
                        Else
                            If BXM2 = True Or BXM3 = True Then
                                PuntosAzul += giropeto
                                LabelGPAzul.Text += 1
                                BXM2 = False
                                BXM3 = False
                            End If
                        End If

                    Case "RightShoulder"
                        Mando1.Image = My.Resources.TR
                        If IPGCA = False Then
                            BRSM1 = True
                            PuntoGiroCascoA = New Thread(AddressOf CuentaAtrasGiroCascoA)
                            PuntoGiroCascoA.Start()
                        Else
                            If BRSM2 = True Or BRSM3 = True Then
                                PuntosAzul += girocabeza
                                LabelGCAzul.Text += 1
                                BRSM2 = False
                                BRSM3 = False
                            End If
                        End If

                    Case "DPadDown"
                        Mando1.Image = My.Resources.D
                        If IPR1 = False Then
                            BDM1 = True
                            Punto1R = New Thread(AddressOf CuentaAtras1R)
                            Punto1R.Start()
                        Else
                            If BDM2 = True Or BDM3 = True Then
                                PuntosRojo += puño
                                LabelPuRojo.Text += 1
                                BDM2 = False
                                BDM3 = False
                            End If
                        End If

                    Case "DPadLeft"
                        Mando1.Image = My.Resources.L
                        If IPR2 = False Then
                            BLM1 = True
                            Punto2R = New Thread(AddressOf CuentaAtras2R)
                            Punto2R.Start()

                        Else
                            If BLM2 = True Or BLM3 = True Then
                                PuntosRojo += peto
                                LabelPRojo.Text += 1
                                BLM2 = False
                                BLM3 = False
                            End If

                        End If

                    Case "DPadUp"
                        Mando1.Image = My.Resources.U
                        If IPR3 = False Then
                            BUM1 = True
                            Punto3R = New Thread(AddressOf CuentaAtras3R)
                            Punto3R.Start()
                        Else
                            If BUM2 = True Or BUM3 = True Then
                                PuntosRojo += cabeza
                                LabelCRojo.Text += 1
                                BUM2 = False
                                BUM3 = False

                            End If

                        End If
                    Case "DPadRight"
                        Mando1.Image = My.Resources.R
                        If IPGPR = False Then
                            BRM1 = True
                            PuntoGiroPetoR = New Thread(AddressOf CuentaAtrasGiroPetoR)
                            PuntoGiroPetoR.Start()
                        Else
                            If BRM2 = True Or BRM3 = True Then
                                PuntosRojo += giropeto
                                LabelGPRojo.Text += 1
                                BRM2 = False
                                BRM3 = False
                            End If

                        End If
                    Case "LeftShoulder"
                        Mando1.Image = My.Resources.TL
                        If IPGCR = False Then
                            BLSM1 = True
                            PuntoGiroCascoR = New Thread(AddressOf CuentaAtrasGiroCascoR)
                            PuntoGiroCascoR.Start()
                        Else
                            If BLSM2 = True Or BLSM3 = True Then
                                PuntosRojo += girocabeza
                                LabelGCRojo.Text += 1
                                BLSM2 = False
                                BLSM3 = False
                            End If
                        End If
                End Select


            End If
            If Controller2.IsConnected Then
                Dim GPS2 As Gamepad = Controller2.GetState.Gamepad
                Mando2.Image = My.Resources.MandoOn
                'Label3.Text = GPS2.Buttons.ToString
                Label3.Text = Controller2.GetBatteryInformation(BatteryDeviceType.Gamepad).BatteryLevel.ToString
                Select Case GPS2.Buttons.ToString
                    Case "A"
                        Mando2.Image = My.Resources.A
                        If IPA1 = False Then
                            BAM2 = True
                            Punto1A = New Thread(AddressOf CuentaAtras1A)
                            Punto1A.Start()
                        Else
                            If BAM1 = True Or BAM3 = True Then
                                PuntosAzul += puño
                                LabelPuAzul.Text += 1
                                BAM1 = False
                                BAM3 = False
                            End If

                        End If

                    Case "B"
                        Mando2.Image = My.Resources.B
                        If IPA2 = False Then
                            BBM2 = True
                            Punto2A = New Thread(AddressOf CuentaAtras2A)
                            Punto2A.Start()
                        Else
                            If BBM1 = True Or BBM3 = True Then
                                PuntosAzul += peto
                                LabelPAzul.Text += 1
                                BBM1 = False
                                BBM3 = False

                            End If

                        End If
                    Case "Y"
                        Mando2.Image = My.Resources.Y
                        If IPA3 = False Then
                            BYM2 = True
                            Punto3A = New Thread(AddressOf CuentaAtras3A)
                            Punto3A.Start()
                        Else
                            If BYM1 = True Or BYM3 = True Then
                                PuntosAzul += cabeza
                                LabelCAzul.Text += 1
                                BYM1 = False
                                BYM3 = False

                            End If

                        End If
                    Case "X"
                        Mando2.Image = My.Resources.X
                        If IPGPA = False Then
                            BXM2 = True
                            PuntoGiroPetoA = New Thread(AddressOf CuentaAtrasGiroPetoA)
                            PuntoGiroPetoA.Start()
                        Else
                            If BXM1 = True Or BXM3 = True Then
                                PuntosAzul += giropeto
                                LabelGPAzul.Text += 1
                                BXM1 = False
                                BXM3 = False
                            End If
                        End If
                    Case "RightShoulder"
                        Mando2.Image = My.Resources.TR
                        If IPGCA = False Then
                            BRSM2 = True
                            PuntoGiroCascoA = New Threading.Thread(AddressOf CuentaAtrasGiroCascoA)
                            PuntoGiroCascoA.Start()
                        Else
                            If BRSM1 = True Or BRSM3 = True Then
                                PuntosAzul += girocabeza
                                LabelGCAzul.Text += 1
                                BRSM1 = False
                                BRSM3 = False
                            End If
                        End If

                    Case "DPadDown"
                        Mando2.Image = My.Resources.D
                        If IPR1 = False Then
                            BDM2 = True
                            Punto1R = New Thread(AddressOf CuentaAtras1R)
                            Punto1R.Start()
                        Else
                            If BDM1 = True Or BDM3 = True Then
                                PuntosRojo += puño
                                LabelPuRojo.Text += 1
                                BDM1 = False
                                BDM3 = False

                            End If
                        End If

                    Case "DPadLeft"
                        Mando2.Image = My.Resources.L
                        If IPR2 = False Then
                            BLM2 = True
                            Punto2R = New Thread(AddressOf CuentaAtras2R)
                            Punto2R.Start()
                        Else
                            If BLM1 = True Or BLM3 = True Then
                                PuntosRojo += peto
                                LabelPRojo.Text += 1
                                BLM1 = False
                                BLM3 = False

                            End If

                        End If

                    Case "DPadUp"
                        Mando2.Image = My.Resources.U
                        If IPR3 = False Then
                            BUM2 = True
                            Punto3R = New Threading.Thread(AddressOf CuentaAtras3R)
                            Punto3R.Start()
                        Else
                            If BUM1 = True Or BUM3 = True Then
                                PuntosRojo += cabeza
                                LabelCRojo.Text += 1
                                BUM1 = False
                                BUM3 = False

                            End If

                        End If

                    Case "DPadRight"
                        Mando2.Image = My.Resources.R
                        If IPGPR = False Then
                            BRM2 = True
                            PuntoGiroPetoR = New Thread(AddressOf CuentaAtrasGiroPetoR)
                            PuntoGiroPetoR.Start()
                        Else
                            If BRM1 = True Or BRM3 = True Then
                                PuntosRojo += giropeto
                                LabelGPRojo.Text += 1
                                BRM1 = False
                                BRM3 = False
                            End If

                        End If
                    Case "LeftShoulder"
                        Mando2.Image = My.Resources.TL
                        If IPGCR = False Then
                            BLSM2 = True
                            PuntoGiroCascoR = New Threading.Thread(AddressOf CuentaAtrasGiroCascoR)
                            PuntoGiroCascoR.Start()
                        Else
                            If BLSM1 = True Or BLSM3 = True Then
                                PuntosRojo += girocabeza
                                LabelGCRojo.Text += 1
                                BLSM1 = False
                                BLSM3 = False
                            End If
                        End If
                End Select
            End If


            If Controller3.IsConnected Then
                Dim GPS3 As Gamepad = Controller3.GetState.Gamepad
                Mando3.Image = My.Resources.MandoOn
                'Label4.Text = GPS3.Buttons.ToString
                Label4.Text = Controller3.GetBatteryInformation(BatteryDeviceType.Gamepad).BatteryLevel.ToString
                Select Case GPS3.Buttons.ToString
                    Case "A"
                        Mando3.Image = My.Resources.A
                        If IPA1 = False Then
                            BAM3 = True
                            Punto1A = New Thread(AddressOf CuentaAtras1A)
                            Punto1A.Start()
                        Else
                            If BAM1 = True Or BAM2 = True Then
                                PuntosAzul += puño
                                LabelPuAzul.Text += 1
                                BAM1 = False
                                BAM2 = False
                            End If

                        End If

                    Case "B"
                        Mando3.Image = My.Resources.B
                        If IPA2 = False Then
                            BBM3 = True
                            Punto2A = New Thread(AddressOf CuentaAtras2A)
                            Punto2A.Start()
                        Else
                            If BBM1 = True Or BBM2 = True Then
                                PuntosAzul += peto
                                LabelPAzul.Text += 1
                                BBM1 = False
                                BBM2 = False

                            End If

                        End If
                    Case "Y"
                        Mando3.Image = My.Resources.Y
                        If IPA3 = False Then
                            BYM3 = True
                            Punto3A = New Thread(AddressOf CuentaAtras3A)
                            Punto3A.Start()
                        Else
                            If BYM1 = True Or BYM2 = True Then
                                PuntosAzul += cabeza
                                LabelCAzul.Text += 1
                                BYM1 = False
                                BYM2 = False

                            End If

                        End If
                    Case "X"
                        Mando3.Image = My.Resources.X
                        If IPGPA = False Then
                            BXM3 = True
                            PuntoGiroPetoA = New Thread(AddressOf CuentaAtrasGiroPetoA)
                            PuntoGiroPetoA.Start()
                        Else
                            If BXM1 = True Or BXM2 = True Then
                                PuntosAzul += giropeto
                                LabelGPAzul.Text += 1
                                BXM1 = False
                                BXM2 = False
                            End If
                        End If
                    Case "RightShoulder"
                        Mando3.Image = My.Resources.TR
                        If IPGCA = False Then
                            BRSM3 = True
                            PuntoGiroCascoA = New Threading.Thread(AddressOf CuentaAtrasGiroCascoA)
                            PuntoGiroCascoA.Start()
                        Else
                            If BRSM1 = True Or BRSM2 = True Then
                                PuntosAzul += girocabeza
                                LabelGCAzul.Text += 1
                                BRSM1 = False
                                BRSM2 = False
                            End If
                        End If

                    Case "DPadDown"
                        Mando3.Image = My.Resources.D
                        If IPR1 = False Then
                            BDM3 = True
                            Punto1R = New Thread(AddressOf CuentaAtras1R)
                            Punto1R.Start()
                        Else
                            If BDM1 = True Or BDM2 = True Then
                                PuntosRojo += puño
                                LabelPuRojo.Text += 1
                                BDM1 = False
                                BDM2 = False

                            End If
                        End If

                    Case "DPadLeft"
                        Mando3.Image = My.Resources.L
                        If IPR2 = False Then
                            BLM3 = True
                            Punto2R = New Thread(AddressOf CuentaAtras2R)
                            Punto2R.Start()
                        Else
                            If BLM1 = True Or BLM2 = True Then
                                PuntosRojo += peto
                                LabelPRojo.Text += 1
                                BLM1 = False
                                BLM2 = False

                            End If

                        End If

                    Case "DPadUp"
                        Mando3.Image = My.Resources.U
                        If IPR3 = False Then
                            BUM3 = True
                            Punto3R = New Threading.Thread(AddressOf CuentaAtras3R)
                            Punto3R.Start()
                        Else
                            If BUM1 = True Or BUM2 = True Then
                                PuntosRojo += cabeza
                                LabelCRojo.Text += 1
                                BUM1 = False
                                BUM2 = False

                            End If

                        End If

                    Case "DPadRight"
                        Mando3.Image = My.Resources.R
                        If IPGPR = False Then
                            BRM3 = True
                            PuntoGiroPetoR = New Thread(AddressOf CuentaAtrasGiroPetoR)
                            PuntoGiroPetoR.Start()
                        Else
                            If BRM1 = True Or BRM2 = True Then
                                PuntosRojo += giropeto
                                LabelGPRojo.Text += 1
                                BRM1 = False
                                BRM2 = False
                            End If

                        End If
                    Case "LeftShoulder"
                        Mando3.Image = My.Resources.TL
                        If IPGCR = False Then
                            BLSM3 = True
                            PuntoGiroCascoR = New Threading.Thread(AddressOf CuentaAtrasGiroCascoR)
                            PuntoGiroCascoR.Start()
                        Else
                            If BLSM1 = True Or BLSM2 = True Then
                                PuntosRojo += girocabeza
                                LabelGCRojo.Text += 1
                                BLSM1 = False
                                BLSM2 = False
                            End If
                        End If
                End Select
            End If
            PuntoAzulLabel.Text = PuntosAzul.ToString
            PuntoRojoLabel.Text = PuntosRojo.ToString

            PuntosAzul_Form2.Text = PuntosAzul.ToString
            PuntosRojo_Form2.Text = PuntosRojo.ToString

        Loop Until final = True
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        XInputController()

    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick '  cronometro para asaltos
        Timer1.Interval = 1000
        If Segundos > 0 Then
            Segundos -= 1
        ElseIf Segundos = 0 Then
            Minutos -= 1
            If Minutos = -1 Then
                Minutos = 0
                Label1.BackColor = Color.DarkGray
                Label1.Text = Format(Minutos & ":" & Segundos, "Short Time")
                Form2.Label3.ForeColor = Color.Black
                Form2.Label3.Text = Format(Minutos & ":" & Segundos, "Short Time")
                Timer1.Enabled = False ' final del asalto
                final = True
                If Asaltos <= 1 Then ' último asalto
                    Select Case TipoCombate
                        Case True ' asaltos
                            If LabelGanadoAzul.Text < 2 And LabelGanadoRojo.Text < 2 Then
                                If PuntosAzul > PuntosRojo Then
                                    LabelGanadoAzul.Text += 1
                                    Form2.Label9.Text += 1
                                ElseIf PuntosAzul < PuntosRojo Then
                                    LabelGanadoRojo.Text += 1
                                    Form2.Label8.Text += 1
                                Else ' no hay ganador del asalto por los puntos
                                    Dim resultado As MsgBoxResult
                                    resultado = MsgBox("No hay ganador por puntos." & vbCrLf & "¿El ganador es competidor Azul?", vbYesNo, "Marcador TKD")
                                    Select Case resultado
                                        Case vbYes
                                            LabelGanadoAzul.Text += 1
                                            Form2.Label9.Text += 1
                                        Case vbNo
                                            LabelGanadoRojo.Text += 1
                                            Form2.Label8.Text += 1
                                    End Select
                                End If
                            End If
                            If LabelGanadoAzul.Text = 2 Then
                                MsgBox("Final Del Combate. Ganador Azul",, "Marcador TKD")
                            Else
                                MsgBox("Final Del Combate. Ganador Rojo",, "Marcador TKD")

                            End If
                        Case False ' puntos
                            If PuntosAzul > PuntosRojo And FaltasAzul < FaltasMax Then MsgBox("Final Del Combate. Ganador Azul",, "Marcador TKD")
                            If PuntosAzul < PuntosRojo And FaltasRojo < FaltasMax Then MsgBox("Final Del Combate. Ganador Rojo",, "Marcador TKD")
                            If PuntosAzul = PuntosRojo And FaltasAzul < FaltasMax And FaltasRojo < FaltasMax Then MsgBox("Final Del Combate. Ganador por decisión arbitral",, "Marcador TKD")
                    End Select


                Else '  quedan asaltos 
                    Select Case TipoCombate
                        Case True ' asaltos
                            If LabelGanadoAzul.Text < 2 And LabelGanadoRojo.Text < 2 Then
                                If PuntosAzul > PuntosRojo Then
                                    LabelGanadoAzul.Text += 1
                                    Form2.Label9.Text += 1
                                ElseIf PuntosAzul < PuntosRojo Then
                                    LabelGanadoRojo.Text += 1
                                    Form2.Label8.Text += 1
                                Else ' no hay ganador del asalto por los puntos
                                    Dim resultado As MsgBoxResult
                                    resultado = MsgBox("No hay ganador por puntos." & vbCrLf & "¿El ganador es competidor Azul?", vbYesNo, "Marcador TKD")
                                    Select Case resultado
                                        Case vbYes
                                            LabelGanadoAzul.Text += 1
                                            Form2.Label9.Text += 1
                                        Case vbNo
                                            LabelGanadoRojo.Text += 1
                                            Form2.Label8.Text += 1
                                    End Select
                                End If
                            End If

                            If LabelGanadoAzul.Text = 2 Then
                                MsgBox("Final Del Combate. Ganador Azul",, "Marcador TKD")
                            ElseIf LabelGanadoRojo.Text = 2 Then
                                MsgBox("Final Del Combate. Ganador Rojo",, "Marcador TKD")
                            Else
                                FaltasAzul = 0
                                PictureBox20.Image = ImageFaltas(FaltasAzul)
                                Form2.PictureBox2.Image = ImageFaltas(FaltasAzul)
                                FaltasRojo = 0
                                PictureBox15.Image = ImageFaltas(FaltasRojo)
                                Form2.PictureBox1.Image = ImageFaltas(FaltasRojo)
                                PuntosRojo = 0
                                PuntosAzul = 0
                                PuntoRojoLabel.Text = 0
                                PuntoAzulLabel.Text = 0
                                Form2.Label1.Text = 0
                                Form2.Label2.Text = 0
                                MinutosDescanso = Int(TBDescansoMinutos.Text)
                                SegundosDescanso = Int(TBDescansoSegundos.Text)
                                Asaltos -= 1
                                Timer2.Start()
                            End If

                        Case False ' puntos
                            MsgBox("Final Del Asalto " & LAsaltos.Text,, "Marcador TKD")
                            MinutosDescanso = Int(TBDescansoMinutos.Text)
                            SegundosDescanso = Int(TBDescansoSegundos.Text)
                            Asaltos -= 1
                            Timer2.Start()
                    End Select

                End If


            Else
                Segundos = 59
            End If
        End If
        Label1.ForeColor = Color.Black
        Label1.Text = Format(Minutos & ":" & Segundos, "Short Time")
        Form2.Label3.ForeColor = Color.Black
        Form2.Label3.Text = Format(Minutos & ":" & Segundos, "Short Time")
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Minutos = Int(TBMinutos.Text)
        Segundos = Int(TBSegundos.Text)
        Asaltos = Int(TBoxAsaltos.Text)
        MinutosDescanso = Int(TBDescansoMinutos.Text)
        SegundosDescanso = Int(TBDescansoSegundos.Text)
        PuntosAzul = 0
        PuntosRojo = 0
        FaltasAzul = 0
        FaltasRojo = 0
        FaltasMax = 5
        If TipoCombate = False Then FaltasMax = 10
        PictureBox15.Image = ImageFaltas(0)
        Form2.PictureBox1.Image = ImageFaltas(0)
        Form2.PictureBox2.Image = ImageFaltas(0)
        PictureBox20.Image = ImageFaltas(0)
        LAsaltos.Text = 1
        Form2.Label4.Text = 1
        LabelCAzul.Text = 0
        LabelCRojo.Text = 0
        LabelPAzul.Text = 0
        LabelPRojo.Text = 0
        LabelPuAzul.Text = 0
        LabelPuRojo.Text = 0
        LabelGCAzul.Text = 0
        LabelGCRojo.Text = 0
        LabelGPAzul.Text = 0
        LabelGPRojo.Text = 0
        LabelGanadoAzul.Text = 0
        LabelGanadoRojo.Text = 0
        Form2.Label8.Text = 0
        Form2.Label9.Text = 0
        cabeza = TextBoxCasco.Text
        peto = TextBoxPeto.Text
        puño = TextBoxPuño.Text
        giropeto = TextBoxGiroPeto.Text
        girocabeza = TextBoxGiroCasco.Text
        PuntoAzulLabel.Text = 0
        Form2.Label2.Text = 0
        PuntoRojoLabel.Text = 0
        Form2.Label1.Text = 0
        Label1.Text = Format(Minutos & ":" & Segundos, "Short Time")
        Form2.Label3.Text = Format(Minutos & ":" & Segundos, "Short Time")

    End Sub

    Private Sub CuentaAtras1A()
        IPA1 = True
        Thread.Sleep(TextBoxRetardoPuño.Text * 100)
        BAM1 = False
        BAM2 = False
        BAM3 = False
        IPA1 = False
    End Sub
    Private Sub CuentaAtras1R()
        IPR1 = True
        Thread.Sleep(TextBoxRetardoPuño.Text * 100)
        BDM1 = False
        BDM2 = False
        BDM3 = False
        IPR1 = False
    End Sub
    Private Sub CuentaAtras2A()
        IPA2 = True
        Thread.Sleep(TextBoxRetardoPeto.Text * 100)
        BBM1 = False
        BBM2 = False
        BBM3 = False
        IPA2 = False
    End Sub
    Private Sub CuentaAtras2R()
        IPR2 = True
        Thread.Sleep(TextBoxRetardoPeto.Text * 100)
        BLM1 = False
        BLM2 = False
        BLM3 = False
        IPR2 = False
    End Sub
    Private Sub CuentaAtras3A()
        IPA3 = True
        Thread.Sleep(TextBoxRetardoCasco.Text * 100)
        BYM1 = False
        BYM2 = False
        BYM3 = False
        IPA3 = False
    End Sub
    Private Sub CuentaAtras3R()
        IPR3 = True
        Thread.Sleep(TextBoxRetardoCasco.Text * 100)
        BUM1 = False
        BUM2 = False
        BUM3 = False
        IPR3 = False
    End Sub
    Private Sub CuentaAtrasGiroPetoA()
        IPGPA = True
        Thread.Sleep(TextBoxRetardoGiroPeto.Text * 100)
        BXM1 = False
        BXM2 = False
        BXM3 = False
        IPGPA = False
    End Sub
    Private Sub CuentaAtrasGiroCascoA()
        IPGCA = True
        Thread.Sleep(TextBoxRetardoGiroCasco.Text * 100)
        BRSM1 = False
        BRSM2 = False
        BRSM3 = False
        IPGCA = False
    End Sub

    Private Sub CuentaAtrasGiroPetoR()
        IPGPR = True
        Thread.Sleep(TextBoxRetardoGiroPeto.Text * 100)
        BRM1 = False
        BRM2 = False
        BRM3 = False
        IPGPR = False
    End Sub
    Private Sub CuentaAtrasGiroCascoR()
        IPGCR = True
        Thread.Sleep(TextBoxRetardoGiroCasco.Text * 100)
        BLSM1 = False
        BLSM2 = False
        BLSM3 = False
        IPGCR = False
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Timer1.Interval = 1000
        If SegundosDescanso > 0 Then
            SegundosDescanso -= 1
        ElseIf SegundosDescanso = 0 Then
            MinutosDescanso -= 1
            If MinutosDescanso = -1 Then
                MinutosDescanso = 0
                Minutos = Int(TBMinutos.Text)
                Segundos = Int(TBSegundos.Text)
                Label1.ForeColor = Color.Black
                Label1.Text = Format(MinutosDescanso & ":" & SegundosDescanso, "Short Time")
                Form2.Label3.ForeColor = Color.Black
                Form2.Label3.Text = Format(MinutosDescanso & ":" & SegundosDescanso, "Short Time")
                LAsaltos.Text += 1
                Form2.Label4.Text += 1
                Timer2.Enabled = False
            Else
                SegundosDescanso = 59
            End If
        End If
        Label1.ForeColor = Color.Red
        Label1.Text = Format(MinutosDescanso & ":" & SegundosDescanso, "Short Time")
        Form2.Label3.ForeColor = Color.Red
        Form2.Label3.Text = Format(MinutosDescanso & ":" & SegundosDescanso, "Short Time")
    End Sub


    Private Sub LabelCRojo_Click(sender As Object, e As EventArgs) Handles LabelCRojo.Click
        Quitar_Puntos_Rojo(sender, 1, cabeza)
    End Sub

    Private Sub LabelPRojo_Click(sender As Object, e As EventArgs) Handles LabelPRojo.Click
        Quitar_Puntos_Rojo(sender, 1, peto)
    End Sub

    Private Sub LabelPuRojo_Click(sender As Object, e As EventArgs) Handles LabelPuRojo.Click
        Quitar_Puntos_Rojo(sender, 1, puño)
    End Sub
    Private Sub LabelGCRojo_Click(sender As Object, e As EventArgs) Handles LabelGCRojo.Click
        Quitar_Puntos_Rojo(sender, 1, girocabeza)
    End Sub
    Private Sub LabelGPRojo_Click(sender As Object, e As EventArgs) Handles LabelGPRojo.Click
        Quitar_Puntos_Rojo(sender, 1, giropeto)
    End Sub

    Private Sub LabelCAzul_Click(sender As Object, e As EventArgs) Handles LabelCAzul.Click
        Quitar_Puntos_Azul(sender, 1, cabeza)
    End Sub

    Private Sub LabelPAzul_Click(sender As Object, e As EventArgs) Handles LabelPAzul.Click
        Quitar_Puntos_Azul(sender, 1, peto)
    End Sub

    Private Sub LabelPuAzul_Click(sender As Object, e As EventArgs) Handles LabelPuAzul.Click
        Quitar_Puntos_Azul(sender, 1, puño)
    End Sub
    Private Sub LabelGAzul_Click(sender As Object, e As EventArgs) Handles LabelGCAzul.Click
        Quitar_Puntos_Azul(sender, 1, girocabeza)
    End Sub

    Private Sub LabelGPAzul_Click(sender As Object, e As EventArgs) Handles LabelGPAzul.Click
        Quitar_Puntos_Azul(sender, 1, giropeto)
    End Sub

    Private Sub Quitar_Puntos_Azul(label As Label, puntoslabel As Integer, puntos As Integer)
        Dim continua As Integer
        continua = label.Text
        continua -= puntoslabel
        If continua >= 0 Then
            label.Text -= puntoslabel
            PuntosAzul -= puntos
            PuntoAzulLabel.Text = PuntosAzul
            Form2.Label2.Text = PuntosAzul

        End If
    End Sub
    Private Sub Quitar_Puntos_Rojo(label As Label, puntoslabel As Integer, puntos As Integer)
        Dim continua As Integer
        continua = label.Text
        continua -= puntoslabel
        If continua >= 0 Then
            label.Text -= puntoslabel
            PuntosRojo -= puntos
            PuntoRojoLabel.Text = PuntosRojo
            Form2.Label1.Text = PuntosRojo
        End If
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        Quitar_Puntos_Rojo(LabelCRojo, -1, -cabeza)
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        Quitar_Puntos_Rojo(LabelPRojo, -1, -peto)
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Quitar_Puntos_Rojo(LabelPuRojo, -1, -puño)
    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        Quitar_Puntos_Rojo(LabelGCRojo, -1, -girocabeza)
    End Sub

    Private Sub PictureBox26_Click(sender As Object, e As EventArgs) Handles PictureBox26.Click
        Quitar_Puntos_Rojo(LabelGPRojo, -1, -giropeto)
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Quitar_Puntos_Azul(LabelCAzul, -1, -cabeza)
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Quitar_Puntos_Azul(LabelPAzul, -1, -peto)
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Quitar_Puntos_Azul(LabelPuAzul, -1, -puño)
    End Sub

    Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click
        Quitar_Puntos_Azul(LabelGCAzul, -1, -girocabeza)
    End Sub
    Private Sub PictureBox25_Click(sender As Object, e As EventArgs) Handles PictureBox25.Click
        Quitar_Puntos_Azul(LabelGPAzul, -1, -giropeto)
    End Sub

    Private Sub PictureBox16_Click(sender As Object, e As EventArgs) Handles PictureBox16.Click
        If FaltasRojo < FaltasMax Then
            Faltas(True, 1)
        Else
            Minutos = 0
            Segundos = 0
            If TipoCombate = True Then
                PuntosAzul = 1
                PuntosRojo = 0
                MsgBox("Final Del Asalto Por Acumulación de faltas." & vbCrLf & " Ganador Azul",, "Marcador TKD")
            Else
                Asaltos = 0
                MsgBox("Final Del Combate Por Acumulación de faltas." & vbCrLf & " Ganador Azul",, "Marcador TKD")
            End If



        End If
    End Sub

    Private Sub PictureBox17_Click(sender As Object, e As EventArgs) Handles PictureBox17.Click
        If FaltasRojo > 0 Then
            Faltas(True, -1)
        End If

    End Sub
    Private Sub Faltas(competidor As Boolean, falta As Integer)

        If competidor = True Then ' competidor = true es competidor rojo 
            PuntosAzul += falta
            FaltasRojo += falta
            PictureBox15.Image = ImageFaltas(FaltasRojo)
            Form2.PictureBox1.Image = ImageFaltas(FaltasRojo)
            PuntoAzulLabel.Text = PuntosAzul
            Form2.Label2.Text = PuntosAzul

        Else
            PuntosRojo += falta
            FaltasAzul += falta
            PictureBox20.Image = ImageFaltas(FaltasAzul)
            Form2.PictureBox2.Image = ImageFaltas(FaltasAzul)
            PuntoRojoLabel.Text = PuntosRojo
            Form2.Label1.Text = PuntosRojo
        End If
    End Sub
    Function ImageFaltas(faltas As Byte)
        Dim imagen As Image
        Select Case faltas
            Case 0
                imagen = My.Resources._0Faltas
            Case 1
                imagen = My.Resources._1Falta
            Case 2
                imagen = My.Resources._2Faltas
            Case 3
                imagen = My.Resources._3Faltas
            Case 4
                imagen = My.Resources._4Faltas
            Case 5
                imagen = My.Resources._5Faltas
            Case 6
                imagen = My.Resources._6Faltas
            Case 7
                imagen = My.Resources._7Faltas
            Case 8
                imagen = My.Resources._8Faltas
            Case 9
                imagen = My.Resources._9Faltas
            Case Else
                imagen = My.Resources._10Faltas
        End Select

        Return (imagen)
    End Function

    Private Sub PictureBox18_Click(sender As Object, e As EventArgs) Handles PictureBox18.Click
        If FaltasAzul > 0 Then
            Faltas(False, -1)
        End If
    End Sub

    Private Sub PictureBox19_Click(sender As Object, e As EventArgs) Handles PictureBox19.Click
        If FaltasAzul < FaltasMax Then
            Faltas(False, 1)
        Else
            Minutos = 0
            Segundos = 0
            If TipoCombate = True Then
                PuntosAzul = 0
                PuntosRojo = 1
                MsgBox("Final Del Asalto Por Acumulación de faltas." & vbCrLf & " Ganador Rojo",, "Marcador TKD")
            Else
                Asaltos = 0
                MsgBox("Final Del Combate Por Acumulación de faltas." & vbCrLf & " Ganador Rojo",, "Marcador TKD")
            End If

        End If
    End Sub

    Private Sub PictureBox24_Click(sender As Object, e As EventArgs) Handles PictureBox24.Click
        If final = True Then

            If Segundos < 60 Then
                Segundos += 1
            Else
                Minutos += 1
                Segundos = 0
            End If

            Label1.Text = Format(Minutos & ":" & Segundos, "Short Time")
            Form2.Label3.Text = Format(Minutos & ":" & Segundos, "Short Time")
        End If
    End Sub

    Private Sub PictureBox21_Click(sender As Object, e As EventArgs) Handles PictureBox21.Click
        If final = True Then

            If Segundos > 0 Then
                Segundos -= 1
            Else
                If Minutos > 0 Then
                    Minutos -= 1
                    Segundos = 59
                End If
            End If
            Label1.Text = Format(Minutos & ":" & Segundos, "Short Time")
            Form2.Label3.Text = Format(Minutos & ":" & Segundos, "Short Time")
        End If
    End Sub

    Private Sub PictureBox23_Click(sender As Object, e As EventArgs) Handles PictureBox23.Click
        final = False
        XInputController()
        HiloMando = New Threading.Thread(AddressOf LecturaMandos)
        HiloMando.Start()
        Timer1.Start()
    End Sub

    Private Sub PictureBox22_Click(sender As Object, e As EventArgs) Handles PictureBox22.Click
        Timer1.Stop()
        final = True

    End Sub
    Private Sub PictureBox22_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles PictureBox22.MouseDoubleClick
        Timer1.Stop()
        final = True
        Minutos = 0
        Segundos = 0
        Label1.Text = Format(Minutos & ":" & Segundos, "Short Time")
        Form2.Label3.Text = Format(Minutos & ":" & Segundos, "Short Time")
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        final = True
    End Sub

    Private Sub Retardo_Click(sender As Object, e As EventArgs) Handles rmec.Click, rmagc.Click, rmegc.Click, rmepu.Click, rmapu.Click, rmep.Click, rmap.Click, rmac.Click, rmegp.Click, rmagp.Click
        Dim picture As String = sender.Name
        Select Case picture
            Case "rmec"
                TextBoxRetardoCasco.Text -= 1
            Case "rmep"
                TextBoxRetardoPeto.Text -= 1
            Case "rmepu"
                TextBoxRetardoPuño.Text -= 1
            Case "rmegc"
                TextBoxRetardoGiroCasco.Text -= 1
            Case "rmegp"
                TextBoxRetardoGiroPeto.Text -= 1
            Case "rmac"
                TextBoxRetardoCasco.Text += 1
            Case "rmap"
                TextBoxRetardoPeto.Text += 1
            Case "rmapu"
                TextBoxRetardoPuño.Text += 1
            Case "rmagc"
                TextBoxRetardoGiroCasco.Text += 1
            Case "rmagp"
                TextBoxRetardoGiroPeto.Text += 1
        End Select


    End Sub

    Private Sub Combate_Click(sender As Object, e As EventArgs) Handles cmea.Click, cmaa.Click, cmem.Click, cmam.Click, cmemd.Click, cmamd.Click, cmes.Click, cmas.Click, cmesd.Click, cmasd.Click
        Dim picture As String = sender.Name
        Select Case picture
            Case "cmea"
                TBoxAsaltos.Text -= 1
            Case "cmem"
                TBMinutos.Text -= 1
            Case "cmes"
                TBSegundos.Text -= 10
            Case "cmemd"
                TBDescansoMinutos.Text -= 1
            Case "cmesd"
                TBDescansoSegundos.Text -= 10
            Case "cmaa"
                TBoxAsaltos.Text += 1
            Case "cmam"
                TBMinutos.Text += 1
            Case "cmas"
                TBSegundos.Text += 10
            Case "cmamd"
                TBDescansoMinutos.Text += 1
            Case "cmasd"
                TBDescansoSegundos.Text += 10
        End Select
    End Sub
    Private Sub Puntox_Click(sender As Object, e As EventArgs) Handles pmec.Click, pmac.Click, pmep.Click, pmap.Click, pmepu.Click, pmapu.Click, pmegc.Click, pmagc.Click, pmegp.Click, pmagp.Click
        Dim picture As String = sender.Name
        Select Case picture
            Case "pmec"
                TextBoxCasco.Text -= 1
            Case "pmep"
                TextBoxPeto.Text -= 1
            Case "pmepu"
                TextBoxPuño.Text -= 1
            Case "pmegc"
                TextBoxGiroCasco.Text -= 1
            Case "pmegp"
                TextBoxGiroPeto.Text -= 1
            Case "pmac"
                TextBoxCasco.Text += 1
            Case "pmap"
                TextBoxPeto.Text += 1
            Case "pmapu"
                TextBoxPuño.Text += 1
            Case "pmagc"
                TextBoxGiroCasco.Text += 1
            Case "pmagp"
                TextBoxGiroPeto.Text += 1
        End Select
    End Sub
    Private Sub Numero_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBMinutos.KeyPress, TBoxAsaltos.KeyPress, TBSegundos.KeyPress, TBDescansoSegundos.KeyPress, TBDescansoMinutos.KeyPress, TextBoxCasco.KeyPress, TextBoxGiroCasco.KeyPress, TextBoxPeto.KeyPress, TextBoxPuño.KeyPress, TextBoxRetardoCasco.KeyPress, TextBoxRetardoGiroCasco.KeyPress, TextBoxRetardoPuño.KeyPress, TextBoxRetardoPeto.KeyPress, TextBoxRetardoGiroPeto.KeyPress, TextBoxGiroPeto.KeyPress
        e.Handled = Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar)

    End Sub

    Private Sub PictureBox27_Click(sender As Object, e As EventArgs) Handles PictureBox27.Click ' marcador público
        If Pantalla = True Then
            Pantalla = False
            PictureBox27.Image = My.Resources.Off
            Form2.Hide()
        Else
            Pantalla = True
            PictureBox27.Image = My.Resources._On
            Form2.Show()
        End If
    End Sub
    Private Sub NoCero_Leave(sender As Object, e As EventArgs) Handles TBMinutos.Leave, TBoxAsaltos.Leave, TBSegundos.Leave, TBDescansoSegundos.Leave, TBDescansoMinutos.Leave, TextBoxCasco.Leave, TextBoxGiroCasco.Leave, TextBoxPeto.Leave, TextBoxPuño.Leave, TextBoxRetardoCasco.Leave, TextBoxRetardoGiroCasco.Leave, TextBoxRetardoPuño.Leave, TextBoxRetardoPeto.Leave, TextBoxRetardoGiroPeto.Leave, TextBoxGiroPeto.Leave
        If sender.Text = "" Then
            sender.Text = 0
        End If

    End Sub
    Private Sub No_Negativos(sender As Object, e As EventArgs) Handles TextBoxRetardoCasco.TextChanged, TBMinutos.TextChanged, TBoxAsaltos.TextChanged, TBSegundos.TextChanged, TBDescansoSegundos.TextChanged, TBDescansoMinutos.TextChanged, TextBoxCasco.TextChanged, TextBoxGiroCasco.TextChanged, TextBoxPeto.TextChanged, TextBoxPuño.TextChanged, TextBoxRetardoGiroCasco.TextChanged, TextBoxRetardoPuño.TextChanged, TextBoxRetardoPeto.TextChanged, TextBoxRetardoGiroPeto.TextChanged, TextBoxGiroPeto.TextChanged
        If sender.Text < 0 Then sender.Text = 0
    End Sub

    Private Sub PictureBox28_Click(sender As Object, e As EventArgs) Handles PictureBox28.Click ' tipo de combate true asaltos -- false puntos
        If TipoCombate = False Then
            sender.image = My.Resources.Combate_Asaltos
            LabelGanadoAzul.Show()
            LabelGanadoRojo.Show()
            Form2.Label8.Show()
            Form2.Label9.Show()
            TBoxAsaltos.Text = 3
            cmea.Hide()
            cmaa.Hide()
            TBoxAsaltos.ReadOnly = True
            TipoCombate = True
            FaltasMax = 5
            PictureFaltasMax.Image = My.Resources._5Faltas

        Else
            sender.image = My.Resources.Combate_Puntos
            LabelGanadoAzul.Hide()
            LabelGanadoRojo.Hide()
            Form2.Label8.Hide()
            Form2.Label9.Hide()
            TBoxAsaltos.ReadOnly = False
            cmaa.Show()
            cmea.Show()
            TipoCombate = False
            FaltasMax = 10
            PictureFaltasMax.Image = My.Resources._10Faltas

        End If
    End Sub


End Class


