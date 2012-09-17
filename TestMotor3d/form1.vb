Imports System.Math
Imports dx_lib32



Public Class Form1
    Dim F2 As Form2
    Dim F3 As Form3

    Dim MatrizObjetos() As Poliedro
    Dim BufferDibujo() As Integer
    Dim BMP, BMPFondo As Bitmap

    Dim CentroEje, EjeX, EjeY, EjeZ As Punto3D
    Dim PosicionCursor As Punto3D

    Dim CONTROL As Boolean = False

    Dim IndicePoliedroSeleccionado As Long
    Dim PoliedroSeleccionado As Poliedro

    Dim PosTimer As Punto3D
    Dim FocoLuz As Punto3D

    Friend Foco As Foco
    Public AngulosMedios As New List(Of Single)

    Public ColorFocos As Color = Color.Red
    Public BrilloFocos As Double = 1

    Public Motor As Motor3D.Motor3D
    Dim Sombrear As Boolean = True

    Dim sys As dx_System_Class

    Dim Cursor As Point

    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Long) As Integer

    Public Sub TestMatrices()
        Dim A(1, 1), B(2, 2), C(3, 3), D(9, 9) As Double
        Dim DetA, DetB, DetC, DetD As Double

        A(0, 0) = 1
        A(0, 1) = 1
        A(1, 0) = 0
        A(1, 1) = 0

        B(0, 0) = 1
        B(0, 1) = 1
        B(0, 2) = 1
        B(1, 0) = 1
        B(1, 1) = 1
        B(1, 2) = 1
        B(2, 0) = 1
        B(2, 1) = 1
        B(2, 2) = 1
        B = CALCMATR_MatrizUnitaria(3)

        C(0, 0) = 4
        C(0, 1) = 0
        C(0, 2) = 0
        C(0, 3) = 0
        C(1, 0) = 0
        C(1, 1) = 4
        C(1, 2) = 0
        C(1, 3) = 0
        C(2, 0) = 0
        C(2, 1) = 0
        C(2, 2) = 4
        C(2, 3) = 0
        C(3, 0) = 0
        C(3, 1) = 0
        C(3, 2) = 0
        C(3, 3) = 4

        C = CALCMATR_MatrizUnitaria(4)
        C = CALCMATR_ProductoMatricial(C, 4)

        'D = CALCMATR_MatrizUnitaria(10)
        'D = CALCMATR_ProductoMatricial(D, 22)

        DetA = CALCMATR_Determinante(A)
        DetB = CALCMATR_Determinante(B)
        DetC = CALCMATR_Determinante(C)
        'DetD = CALCMATR_Determinante(D)

        sys.DEBUG_SendText(vbNewLine & CALCMATR_MatrizToString(A, 0) & vbNewLine & CALCMATR_MatrizToString(B, 0) & CALCMATR_MatrizToString(C, 0) & vbNewLine)
        sys.DEBUG_SendText("Determinante de A: " & DetA & vbNewLine)
        sys.DEBUG_SendText("Determinante de B: " & DetB & vbNewLine)
        sys.DEBUG_SendText("Determinante de C: " & DetC & vbNewLine)
        sys.DEBUG_SendText("Determinante de D: " & DetD & vbNewLine & vbNewLine)
        sys.DEBUG_SendText("Traspuesta de A: " & CALCMATR_MatrizToString(CALCMATR_MatrizTraspuesta(A), 0))
        sys.DEBUG_SendText("Inversa de A: " & CALCMATR_MatrizToString(CALCMATR_MatrizInversa(A), 2))
        sys.DEBUG_SendText("Inversa de B: " & CALCMATR_MatrizToString(CALCMATR_MatrizInversa(B), 2))
        sys.DEBUG_SendText("Inversa de C: " & CALCMATR_MatrizToString(CALCMATR_MatrizInversa(C), 2))
        sys.DEBUG_SendText("Inversa de D: " & CALCMATR_MatrizToString(CALCMATR_MatrizInversa(D), 2))
        sys.DEBUG_SendText("Comprobaciones:" & vbNewLine)
        sys.DEBUG_SendText("A*A^-1: " & vbNewLine & CALCMATR_MatrizToString(CALCMATR_ProductoMatricial(A, CALCMATR_MatrizInversa(A)), 2) & vbNewLine)
        sys.DEBUG_SendText("B*B^-1: " & vbNewLine & CALCMATR_MatrizToString(CALCMATR_ProductoMatricial(B, CALCMATR_MatrizInversa(B)), 2) & vbNewLine)
        sys.DEBUG_SendText("C*C^-1: " & vbNewLine & CALCMATR_MatrizToString(CALCMATR_ProductoMatricial(C, CALCMATR_MatrizInversa(C)), 2) & vbNewLine)
        sys.DEBUG_SendText("D*D^-1: " & vbNewLine & CALCMATR_MatrizToString(CALCMATR_ProductoMatricial(D, CALCMATR_MatrizInversa(D)), 2) & vbNewLine)
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Size = New Point(Screen.PrimaryScreen.WorkingArea.Size.Width / 1, Screen.PrimaryScreen.WorkingArea.Size.Height)
        'Me.Width /= 2
        Me.Location = New Point(0, 0)
        PosiciónDeLaCámaraToolStripMenuItem.Enabled = False
        DistanciaDelPlanoToolStripMenuItem.Enabled = False
        ReDim MatrizObjetos(0)
        ReDim BufferDibujo(3)
        Pic.BackColor = Color.White
        CentroEje = New Punto3D(0, 0, 0)
        EjeX = New Punto3D(300, 0, 0)
        EjeY = New Punto3D(0, 300, 0)
        EjeZ = New Punto3D(0, 0, 300)

        PosicionCursor = New Punto3D(0, 0, 0)




        MatrizObjetos(0) = Motor3D.PRIM_Esfera(20)
        MatrizObjetos(0).CambiarEscala(New Punto3D(100, 100, 100))
        MatrizObjetos(0).InterpolarNormalesVertices = True
        MatrizObjetos(0).EstablecerColores(Color.White)

        Dim s As String = ""

        'For i As Long = 0 To MatrizObjetos(0).Caras.GetUpperBound(0)
        '    's = Mid(i.ToString, Len(i.ToString))
        '    'n = CInt(s)
        '    'Select Case n
        '    '    Case 1 To 3
        '    '        MatrizObjetos(0).Caras(i).Color = Color.Red
        '    '    Case 4 To 6
        '    '        MatrizObjetos(0).Caras(i).Color = Color.Green
        '    '    Case 6 To 8
        '    '        MatrizObjetos(0).Caras(i).Color = Color.Blue
        '    '    Case 9
        '    '        MatrizObjetos(0).Caras(i).Color = Color.White
        '    '    Case 0
        '    '        MatrizObjetos(0).Caras(i).Color = Color.White
        '    '    Case Else
        '    '        MatrizObjetos(0).Caras(i).Color = Color.FromArgb(255, 1, 1, 1)
        '    'End Select
        '    Dim R As New Random
        '    MatrizObjetos(0).Caras(i).Color = Color.FromArgb(255, R.Next(0, 255), R.Next(0, 255), R.Next(0, 255))
        'Next


        IndicePoliedroSeleccionado = 0

        Foco = New Foco(New Punto3D(120, 0, 0), Color.White, 1)
        Foco.Radio = 100
        Foco.Delta = 30
        Foco.VectorDireccion = New Vector3D(-1, 0, 0)

        InicioMotor()
        'Motor.POLI_TrasladarPoliedroADestino(0, New Punto3D(0, 0, 0))

        PosTimer = New Punto3D(0, 10, 0)
        Me.Show()
        Timer1.Stop()
    End Sub



    Public Sub InicioMotor()
        Dim Indice As Integer = 0

        'MegaCubo()

        Motor = New Motor3D.Motor3D(Pic.Handle, MatrizObjetos(0), Foco)

        AddHandler Motor.DEPU_SalidaDepuracionBuffer, AddressOf DepuracionBuffer
        AddHandler Motor.DEPU_SalidaDepuracionDibujo, AddressOf DepuracionDibujo
        AddHandler Motor.DEPU_SalidaDepuracionSombreado, AddressOf DepuracionSombreado

        FlatShadingToolStripMenuItem.Checked = True
        GouraudShadingToolStripMenuItem.Checked = False

        AddHandler Motor.PINT_FinRedibujado, AddressOf MostrarDatos
        AddHandler CALCSOM_ProgresoGouraudShading, AddressOf DatosGouraud
        'AddHandler Motor.BUFF_FinRecargaBuffer, AddressOf Form3.RellenarBuffer

        'g = New dx_GFX_Class
        'g.Init(Pic.Handle, Pic.Width, Pic.Height, 32, True, True, True, 60)
        'g.DEVICE_SetDrawCenter(Pic.Width / 2, Pic.Height / 2)

        'Motor.PINT_MetodoDibujadoPoligonoColor = AddressOf DibujadoColor
        'Motor.PINT_MetodoPintadoPoligonoColor = AddressOf PintadoColor
        'Motor.PINT_MetodoPintadoPoligonoColores = AddressOf PintadoColores

        sys = New dx_System_Class
        sys.DEBUG_OpenConsole("Consola de depuracion")



        ''BufferToolStripMenuItem.PerformClick()

        'TestMatrices()

        'sys.DEBUG_SendText("/////////////////////////////////////////////////" & vbNewLine)
        'sys.DEBUG_SendText("DATOS DE CAMARA" & vbNewLine)
        'sys.DEBUG_SendText(vbNewLine)
        'sys.DEBUG_SendText(" - Posicion: " & Motor.REPR_Camara.Posicion.ToString & vbNewLine)
        'sys.DEBUG_SendText(" - Matriz de transformacion: " & vbNewLine)
        'sys.DEBUG_SendText(CALCMATR_MatrizToString(Motor.REPR_Camara.MatrizTransformacion, 2) & vbNewLine)
        'sys.DEBUG_SendText(" - Matriz de transformacion (Inversa): " & vbNewLine)
        'sys.DEBUG_SendText(CALCMATR_MatrizToString(Motor.REPR_Camara.InversaMatrizTransformacion, 2) & vbNewLine)

        'For i As Integer = 1 To 3
        '    For j As Integer = 1 To 3
        '        For h As Integer = 1 To 3
        '            If Not i = j = h = 2 Then
        '                Motor.POLI_TrasladarPoliedroADestino(Indice, New Punto3D((i * 50) - 75, (j * 50) - 75, (h * 50) - 75))
        '                Indice += 1
        '            End If

        '            'If Indice >= Motor.POLI_Poliedros.GetUpperBound(0) + 1 Then Exit For
        '        Next
        '        'If Indice >= Motor.POLI_Poliedros.GetUpperBound(0) + 1 Then Exit For
        '        'Sys.DEBUG_SendText("X=" & x & " Y=" & y & vbNewLine)
        '    Next
        '    'If Indice >= Motor.POLI_Poliedros.GetUpperBound(0) + 1 Then Exit For
        'Next
        Me.Show()
        'Motor.IniciarRender()
    End Sub

    Public Sub MegaCubo()
        Dim Cubos(26) As Poliedro
        Dim Indice As Integer = 0

        For i As Integer = 0 To 26
            Cubos(i) = Motor3D.PRIM_Cubo
            Cubos(i).EscalarFijo(New Punto3D(50, 50, 50))
            Cubos(i).TrasladarADestino(New Punto3D(0, 0, 0))

            Cubos(i).Caras(0).Color = Color.Red
            Cubos(i).Caras(1).Color = Color.Orange
            Cubos(i).Caras(2).Color = Color.Green
            Cubos(i).Caras(3).Color = Color.White
            Cubos(i).Caras(4).Color = Color.Blue
            Cubos(i).Caras(5).Color = Color.Yellow

            Cubos(i).AplicarTransformaciones()

        Next



        'Motor.POLI_QuitarPoliedro(0)
        MatrizObjetos = Cubos
    End Sub

    Public Sub DepuracionBuffer(ByVal Mensaje As String)
        sys.DEBUG_OpenConsole("Consola de depuracion")
        sys.DEBUG_SendText(Mensaje, EGA_Color.Silver)
    End Sub
    Public Sub DepuracionDibujo(ByVal Mensaje As String)
        sys.DEBUG_OpenConsole("Consola de depuracion")
        sys.DEBUG_SendText(Mensaje, EGA_Color.Silver)
    End Sub
    Public Sub DepuracionSombreado(ByVal Mensaje As String)
        sys.DEBUG_OpenConsole("Consola de depuracion")
        sys.DEBUG_SendText(Mensaje, EGA_Color.Violet)
    End Sub


    Public Sub DestruirHandler(ByVal Form3 As Form3)
        RemoveHandler Motor.BUFF_FinRecargaBuffer, AddressOf F3.RellenarBuffer
        RemoveHandler Motor.PINT_FinRedibujado, AddressOf F3.EventoRepintado
    End Sub

    Public Sub DatosGouraud(ByVal Progreso As Integer, ByVal V1 As Point, ByVal V2 As Point, ByVal V3 As Point, ByVal X1 As Integer, ByVal Y1 As Integer, ByVal X2 As Integer, ByVal Y2 As Integer, ByVal X3 As Integer, ByVal Y3 As Integer, ByVal d1 As Double, ByVal d2 As Double, ByVal d3 As Double, ByVal d4 As Double, ByVal k1 As Double, ByVal k2 As Double, ByVal k3 As Double, ByVal k4 As Double, ByVal k5 As Double, ByVal k6 As Double, ByVal i1r As Double, ByVal i2r As Double, ByVal i1g As Double, ByVal i2g As Double, ByVal i1b As Double, ByVal i2b As Double, ByVal r As Double, ByVal g As Double, ByVal b As Double, ByVal rr As Byte, ByVal gg As Byte, ByVal bb As Byte)
        'Sys.DEBUG_SendText(FormatNumber(Progreso, 0) & "%...Actual: X1(" & X1 & ") Y1(" & Y1 & ") X2(" & X2 & ") Y2(" & Y2 & ") X3(" & X3 & ") Y3(" & Y3 & ")" & vbNewLine)
        Form4.Label1.Text = "Progreso..." & FormatNumber(Progreso, 0) & "%" & vbNewLine & " X1(" & X1 & ")" & vbNewLine & " Y1(" & Y1 & ")" & vbNewLine & " X2(" & X2 & ")" & vbNewLine & " Y2(" & Y2 & ")" & vbNewLine & " X3(" & X3 & ")" & vbNewLine & " Y3(" & Y3 & ")" & vbNewLine & _
                            "Denominadores de constantes de interpolación:" & vbNewLine & vbNewLine & _
                            "d1: " & d1 & vbNewLine & _
                            "d2: " & d2 & vbNewLine & _
                            "d3: " & d3 & vbNewLine & _
                            "Constantes de interpolación:" & vbNewLine & vbNewLine & _
                            "k1: " & k1 & vbNewLine & _
                            "k2: " & k2 & vbNewLine & _
                            "k3: " & k3 & vbNewLine & _
                            "k4: " & k4 & vbNewLine & _
                            "k5: " & k5 & vbNewLine & _
                            "k6: " & k6 & vbNewLine & _
                            "Intensidades de color:" & vbNewLine & vbNewLine & _
                            "i1r: " & i1r & vbNewLine & _
                            "i2r: " & i2r & vbNewLine & _
                            "i1g: " & i1g & vbNewLine & _
                            "i2g: " & i2g & vbNewLine & _
                            "i1b: " & i1b & vbNewLine & _
                            "i2b: " & i2b & vbNewLine & _
                            "Valores de color reales:" & vbNewLine & vbNewLine & _
                            "rr: " & rr & vbNewLine & _
                            "gg: " & gg & vbNewLine & _
                            "bb: " & bb & vbNewLine & _
                            "Valores de color transformados:" & vbNewLine & vbNewLine & _
                            "r: " & r & vbNewLine & _
                            "g: " & g & vbNewLine & _
                            "b: " & b

        Form4.Label1.Refresh()
    End Sub

    Public Sub ReordenarBuffer()
        Dim CopiaBuffer As Integer
        Dim Distancia, Distancia2 As Double

        For i As Integer = 0 To MatrizObjetos.GetUpperBound(0)
            BufferDibujo(i) = i
        Next

        For i As Integer = 0 To MatrizObjetos.GetUpperBound(0)
            For j As Integer = 0 To MatrizObjetos.GetUpperBound(0)
                Distancia = New Vector3D(New Punto3D(-10000, -10000, -10000), MatrizObjetos(BufferDibujo(i)).Centro).Modulo
                Distancia2 = New Vector3D(New Punto3D(-10000, -10000, -10000), MatrizObjetos(BufferDibujo(j)).Centro).Modulo
                If Distancia < Distancia2 Then
                    CopiaBuffer = BufferDibujo(i)
                    BufferDibujo(i) = BufferDibujo(j)
                    BufferDibujo(j) = CopiaBuffer
                End If

            Next
        Next
    End Sub

    'Public Sub SombrearEscenario()
    '    For Each Foco As Punto3D In FocosLuz
    '        Sys.DEBUG_SendText(vbNewLine & "Foco de luz: " & Foco.ToString & vbNewLine, dx_lib32.EGA_Color.Purple)
    '        For i As Integer = 0 To MatrizObjetos.GetUpperBound(0)
    '            Call DibujadoSombra(g, MatrizObjetos(i), Foco)
    '        Next
    '    Next

    'End Sub


    'Public Sub DibujarObjetos()
    '    Dim Pen As New Pen(Color.Blue)
    '    Dim B As Drawing2D.PathGradientBrush
    '    Dim VectorNormal, VectorCamara As Vector3D
    '    Dim PlanoCara As Plano3D
    '    Dim SEVE As Boolean
    '    Dim ColorCara As Color
    '    Dim Cont As Integer
    '    Dim N As DateTime = Now
    '    Dim TS As New TimeSpan
    '    Dim rr, gg, bb As Byte
    '    Dim Baricentro As Punto3D
    '    'Dim P() As Point = {New Point(-Pic.Width / 2, -Pic.Height / 2), New Point(Pic.Width / 2, -Pic.Height / 2), New Point(Pic.Width / 2, Pic.Height / 2), New Point(-Pic.Width / 2, Pic.Height / 2)}
    '    'Dim C() As Color = {Color.Black, Color.Black, Color.Black, Color.Black}
    '    'B = New Drawing2D.PathGradientBrush(P)
    '    'B.CenterPoint = New Point(Pic.Width / 2, Pic.Height / 2)
    '    'B.SetSigmaBellShape(1)
    '    'B.CenterColor = Color.White
    '    'B.SurroundColors = C
    '    'Pen.DashStyle = Drawing2D.DashStyle.DashDotDot
    '    'Pen.Color = Color.Green

    '    VectorCamara.X = -1
    '    VectorCamara.Y = -1
    '    VectorCamara.Z = -1

    '    rr = ColorFocos.R
    '    gg = ColorFocos.G
    '    bb = ColorFocos.B

    '    rr = IIf(rr * BrilloFocos <= 255, rr * BrilloFocos, 255)
    '    gg = IIf(gg * BrilloFocos <= 255, gg * BrilloFocos, 255)
    '    bb = IIf(bb * BrilloFocos <= 255, bb * BrilloFocos, 255)

    '    'g.FillRectangle(B, New Rectangle(-Pic.Width / 2, -Pic.Height / 2, Pic.Width, Pic.Height))
    '    g.Clear(Color.FromArgb(255, rr, gg, bb))

    '    For i As Integer = 0 To MatrizObjetos.GetUpperBound(0)
    '        MatrizObjetos(i).AplicarTransformaciones()
    '    Next


    '    Sys.DEBUG_SendText(vbNewLine & vbNewLine & vbNewLine)
    '    Sys.DEBUG_SendText("***********************************************************" & vbNewLine)
    '    Sys.DEBUG_SendText("///////////////////////////////////////////////////////////" & vbNewLine)
    '    Sys.DEBUG_SendText("***********************************************************" & vbNewLine)

    '    Call SombrearEscenario()

    '    For i As Integer = 0 To MatrizObjetos.GetUpperBound(0)
    '        Cont = BufferDibujo(i)
    '        Sys.DEBUG_SendText(vbNewLine & "--------------------------------------------------------------" & vbNewLine, EGA_Color.DarkRed)
    '        Sys.DEBUG_SendText(MatrizObjetos(Cont).ToString & ":" & vbNewLine, EGA_Color.DarkRed)
    '        For Each Cara As Cara In MatrizObjetos(Cont).Caras
    '            Sys.DEBUG_SendText(vbNewLine & "Cara(" & Array.IndexOf(MatrizObjetos(Cont).Caras, Cara) & "):" & Cara.ToString & vbNewLine)

    '            PlanoCara = New Plano3D(MatrizObjetos(Cont).Vertices(Cara.Vertices(0)).Coordenadas, MatrizObjetos(Cont).Vertices(Cara.Vertices(1)).Coordenadas, MatrizObjetos(Cont).Vertices(Cara.Vertices(2)).Coordenadas)
    '            Sys.DEBUG_SendText("Plano de cara: " & PlanoCara.ToString(0) & vbNewLine)
    '            Sys.DEBUG_SendText("Vector normal:" & PlanoCara.VectorNormal.ToString & " (" & PlanoCara.VectorNormal.DireccionPolar.ToString & ") " & vbNewLine)
    '            Sys.DEBUG_SendText("Angulo respecto de la camara: " & FormatNumber(AnguloVectores3D(PlanoCara.VectorNormal, VectorCamara), 2) & "º [")

    '            SEVE = Not AnguloVectores3D(PlanoCara.VectorNormal, VectorCamara) > 90
    '            If Not SEVE Then
    '                Sys.DEBUG_SendText("NO A LA VISTA]" & vbNewLine & vbNewLine)
    '                Continue For
    '            Else
    '                Sys.DEBUG_SendText("A LA VISTA]" & vbNewLine & vbNewLine)
    '            End If
    '            Dim Puntos(Cara.Vertices.GetUpperBound(0)) As Point
    '            For j As Integer = 0 To Cara.Vertices.GetUpperBound(0)
    '                Puntos(j) = MatrizObjetos(Cont).Vertices(Cara.Vertices(j)).Coordenadas.Representacion2D
    '                'g.DrawString(Cara.Vertices(j).ToString, New Font(Me.Font, FontStyle.Bold), Brushes.Red, Puntos(j).X, Puntos(j).Y)
    '                'Sys.DEBUG_SendText("    " & MatrizObjetos(Cont).Vertices(Cara.Vertices(j)).Coordenadas.ToString & "  resultado " & Puntos(j).ToString & vbNewLine, EGA_Color.Blue)
    '            Next
    '            ColorCara = ColorProporcional(0, MatrizObjetos.GetUpperBound(0) * 100, Cont * 100)
    '            ColorCara = ColorIluminacion(ColorCara, BrilloFocos, MatrizObjetos(Cont), Cara)
    '            'Me.Text = MatrizObjetos(Cont).Caras.GetUpperBound(0).ToString & "  " & ColorCara.ToString
    '            g.FillPolygon(New SolidBrush(ColorCara), Puntos)
    '            Baricentro = Baricentro3D(MatrizObjetos(Cont).Vertices(Cara.Vertices(0)).Coordenadas, MatrizObjetos(Cont).Vertices(Cara.Vertices(1)).Coordenadas, MatrizObjetos(Cont).Vertices(Cara.Vertices(2)).Coordenadas)
    '            g.FillEllipse(Brushes.Yellow, New Rectangle(Baricentro.Representacion2D.X, Baricentro.Representacion2D.Y, 2, 2))
    '            'For Each Foco As Punto3D In FocosLuz
    '            '    g.FillPolygon(New SolidBrush(NegroSombreado(MatrizObjetos(Cont), Cara, Foco)), Puntos)
    '            'Next
    '            'ColorCara = ColorIluminacion(ColorCara, MatrizObjetos(Cont), Cara)

    '            'g.DrawPolygon(New Pen(Color.Gray, 1), Puntos)
    '        Next
    '    Next
    '    g.Clear(Color.FromArgb(255, rr, gg, bb))
    '    PintarEsfera(1000)

    '    'g.FillEllipse(Brushes.Blue, New Rectangle(-2, -2, 2, 2))

    '    DibujarEjes()
    '    g2.DrawImage(BMP, CSng(-Pic.Width / 2), CSng(-Pic.Height / 2))
    '    Pic.BackgroundImage = BMPFondo
    '    Me.Refresh()

    '    TS = New TimeSpan((Now - N).Ticks)
    '    Me.Text = "Motor3D v0.0.1 - " & MatrizObjetos.GetUpperBound(0) + 1 & " poliedros. " & FocosLuz.Count & " focos. " & TS.TotalMilliseconds & " milisegundos por ciclo."
    'End Sub

    Private Sub Pic_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Pic.MouseMove
        Dim ts As New TimeSpan
        Dim n As DateTime = Now

        Select Case e.Button
            Case Windows.Forms.MouseButtons.Right
                If Not (Motor.REPR_Camara.Representacion = TipoRepresentacion.Conica Or Motor.REPR_Camara.Representacion = TipoRepresentacion.OrtogonalGeneral) Then Exit Sub

                Motor.REPR_IncrementarCoordenadasCamara(EnumEje.EjeX, IIf(Cursor.X >= e.Location.X, -5, 5))
                Motor.REPR_IncrementarCoordenadasCamara(EnumEje.EjeY, IIf(Cursor.Y >= e.Location.Y, -5, 5))
                Motor.POLI_ActualizarPoliedros()
            Case Windows.Forms.MouseButtons.Left
                If Not (Motor.REPR_Camara.Representacion = TipoRepresentacion.Conica Or Motor.REPR_Camara.Representacion = TipoRepresentacion.OrtogonalGeneral) Then Exit Sub

                Dim PosicionAnt, PosicionNueva As Punto3D
                Dim A(3, 3), B(3, 3), C(3, 3) As Double
                A = CALCMATR_MatrizUnitaria(4)
                B = CALCMATR_MatrizUnitaria(4)
                C = CALCMATR_MatrizUnitaria(4)

                If Abs(Cursor.X - e.Location.X) > 5 Then A = CALCTRANS3D_RotacionAlrededorEje(EnumEje.EjeZ, IIf(Cursor.X >= e.Location.X, -2, 2))
                If Abs(Cursor.Y - e.Location.Y) > 5 Then B = CALCTRANS3D_RotacionAlrededorEje(EnumEje.EjeY, IIf(Cursor.Y >= e.Location.Y, -2, 2))
                C = CALCMATR_ProductoMatricial(B, A)

                PosicionAnt = Motor.REPR_Camara.Posicion
                Motor.REPR_EstablecerPosicionCamara(CALCTRANS3D_TransformarPunto(Motor.REPR_Camara.Posicion, C))
                PosicionNueva = Motor.REPR_Camara.Posicion
                'MsgBox(PosicionAnt.ToString & "==>" & PosicionNueva.ToString)
                Motor.REPR_EstablecerVectorDirecionCamara(New Vector3D(Motor.REPR_Camara.Posicion, New Punto3D(0, 0, 0)))

                Motor.POLI_ActualizarPoliedros()
            Case Else
                If Not MoverFocosConElCursorToolStripMenuItem.Checked Then Exit Sub

                'For i As Integer = 0 To Motor.ILUM_Focos.Count - 1
                'If i Mod 2 = 0 Then
                Motor.ILUM_EstablerPosicionFoco(0, CALCREP_Isometrico2DAPunto3D(New Point(e.Location.X - (Pic.Width / 2), e.Location.Y - (Pic.Height / 2)), Motor.ILUM_Focos(0).Coordenadas.Z))
                'Else
                '    Motor.ILUM_EstablerPosicionFoco(i, CALCREP_Isometrico2DAPunto3D(New Point(e.Location.X + 1000 - (Pic.Width / 2), e.Location.Y - (Pic.Height / 2)), Motor.ILUM_Focos(i).Coordenadas.Z))
                'End If

                'Next


                ts = New TimeSpan((Now - n).Ticks)
                Me.Text = "Motor3D v0.0.1 - " & Motor.POLI_Poliedros.GetUpperBound(0) + 1 & " poliedros. " & Motor.ILUM_Focos.GetUpperBound(0) + 1 & " focos. " & FormatNumber(1 / ts.TotalSeconds, 0) & " FPS (" & ts.TotalMilliseconds & " milisegundos por ciclo.)"

        End Select
        Cursor = e.Location
    End Sub

    Private Sub Form1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseWheel
        If Motor.REPR_Camara.Representacion = TipoRepresentacion.Conica Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
            Else
                If e.Delta > 0 Then
                    Motor.REPR_EstablecerDistanciaPlanoProyeccionCamara(Motor.REPR_Camara.Distancia + 100)
                Else
                    Motor.REPR_EstablecerDistanciaPlanoProyeccionCamara(Motor.REPR_Camara.Distancia + 100)
                End If
            End If

            Motor.POLI_ActualizarPoliedros()
        End If
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt Then
            Select Case e.KeyCode

            End Select
            Exit Sub
        End If

        CONTROL = False
        If Not e.Control Then
            Select Case e.KeyCode
                Case Keys.Right
                    Motor.POLI_Poliedros(IndicePoliedroSeleccionado).Trasladar(New Punto3D(10, 0, 0))
                Case Keys.Left
                    Motor.POLI_Poliedros(IndicePoliedroSeleccionado).Trasladar(New Punto3D(-10, 0, 0))
                Case Keys.Up
                    Motor.POLI_Poliedros(IndicePoliedroSeleccionado).Trasladar(New Punto3D(0, 10, 0))
                Case Keys.Down
                    Motor.POLI_Poliedros(IndicePoliedroSeleccionado).Trasladar(New Punto3D(0, -10, 0))
                Case Keys.W
                    Motor.POLI_Poliedros(IndicePoliedroSeleccionado).Rotar(EnumEje.EjeX, 10)
                Case Keys.S
                    Motor.POLI_Poliedros(IndicePoliedroSeleccionado).Rotar(EnumEje.EjeX, -10)
                Case Keys.A
                    Motor.POLI_Poliedros(IndicePoliedroSeleccionado).Rotar(EnumEje.EjeY, 10)
                Case Keys.D
                    Motor.POLI_Poliedros(IndicePoliedroSeleccionado).Rotar(EnumEje.EjeY, -10)
                Case Keys.C
                    Motor.POLI_Poliedros(IndicePoliedroSeleccionado).Trasladar(New Punto3D(0, 0, 10))
                Case Keys.V
                    Motor.POLI_Poliedros(IndicePoliedroSeleccionado).Trasladar(New Punto3D(0, 0, -10))
                Case Keys.Z
                    Motor.POLI_Poliedros(IndicePoliedroSeleccionado).Rotar(EnumEje.EjeZ, 10)
                Case Keys.X
                    Motor.POLI_Poliedros(IndicePoliedroSeleccionado).Rotar(EnumEje.EjeZ, -10)
                Case Keys.Control
                    CONTROL = True
                Case Keys.R
                    Motor.BUFF_RecargarBuffer()
                    Exit Sub
                Case Keys.B
                    Motor.BUFF_EstablecerAlgoritmoReordenacion(0)
                    Motor.BUFF_ReOrdenarBuffer()
                    MostrarDatos()
                Case Keys.G
                    Motor.BUFF_EstablecerAlgoritmoReordenacion(3)
                    Motor.BUFF_ReOrdenarBuffer()
                    MostrarDatos()
                Case Keys.Q
                    Motor.BUFF_EstablecerAlgoritmoReordenacion(1)
                    Motor.BUFF_ReOrdenarBuffer()
                    MostrarDatos()
                Case Keys.L
                    Motor.BUFF_EstablecerAlgoritmoReordenacion(2)
                    Motor.BUFF_ReOrdenarBuffer()
                    MostrarDatos()
                Case Keys.T
                    Call TestMatrices()
                    Exit Sub
                Case Keys.P
                    If Timer1.Enabled Then Timer1.Stop() Else Timer1.Start()
                    Exit Sub
                Case Else
                    Exit Sub
            End Select
        Else
            If e.Shift Then
                Select Case e.KeyCode
                    Case Keys.Up
                        Motor.REPR_IncrementarCoordenadasCamara(EnumEje.EjeY, 10)
                    Case Keys.Down
                        Motor.REPR_IncrementarCoordenadasCamara(EnumEje.EjeY, -10)
                    Case Keys.Right
                        Motor.REPR_IncrementarCoordenadasCamara(EnumEje.EjeX, -10)
                    Case Keys.Left
                        Motor.REPR_IncrementarCoordenadasCamara(EnumEje.EjeX, 10)
                    Case Keys.W
                        Motor.POLI_RotarPoliedros(0, Motor.POLI_Poliedros.GetUpperBound(0), EnumEje.EjeX, 10)
                    Case Keys.S
                        Motor.POLI_RotarPoliedros(0, Motor.POLI_Poliedros.GetUpperBound(0), EnumEje.EjeX, -10)
                    Case Keys.A
                        Motor.POLI_RotarPoliedros(0, Motor.POLI_Poliedros.GetUpperBound(0), EnumEje.EjeY, 10)
                    Case Keys.D
                        Motor.POLI_RotarPoliedros(0, Motor.POLI_Poliedros.GetUpperBound(0), EnumEje.EjeY, -10)
                    Case Keys.Z
                        Motor.POLI_RotarPoliedros(0, Motor.POLI_Poliedros.GetUpperBound(0), EnumEje.EjeZ, 10)
                    Case Keys.X
                        Motor.POLI_RotarPoliedros(0, Motor.POLI_Poliedros.GetUpperBound(0), EnumEje.EjeZ, -10)
                End Select
            Else
                Select Case e.KeyCode
                    Case Keys.W
                        Motor.POLI_RotarPoliedroSobreSiMismo(IndicePoliedroSeleccionado, EnumEje.EjeX, 10)
                    Case Keys.S
                        Motor.POLI_RotarPoliedroSobreSiMismo(IndicePoliedroSeleccionado, EnumEje.EjeX, -10)
                    Case Keys.A
                        Motor.POLI_RotarPoliedroSobreSiMismo(IndicePoliedroSeleccionado, EnumEje.EjeY, 10)
                    Case Keys.D
                        Motor.POLI_RotarPoliedroSobreSiMismo(IndicePoliedroSeleccionado, EnumEje.EjeY, -10)
                    Case Keys.Z
                        Motor.POLI_RotarPoliedroSobreSiMismo(IndicePoliedroSeleccionado, EnumEje.EjeZ, 10)
                    Case Keys.X
                        Motor.POLI_RotarPoliedroSobreSiMismo(IndicePoliedroSeleccionado, EnumEje.EjeZ, -10)
                End Select
            End If

        End If

        Motor.POLI_ActualizarPoliedro(IndicePoliedroSeleccionado)
    End Sub

    Private Sub MostrarDatos()
        Try
            F3.Label1.Text = FormatNumber(1000 / Motor.INFO_TiempoTotal, 0) & " FPS (" & FormatNumber(Motor.INFO_TiempoTotal, 2) & " milisegundos)"
            F3.Label2.Text = Motor.INFO_TotalVertices
            F3.Label3.Text = Motor.INFO_TotalCaras & "  (" & Motor.INFO_TotalTriangulos & " triángulos)"
            F3.Label4.Text = Motor.INFO_CarasEnBuffer
            F3.Label5.Text = Motor.INFO_TotalPoliedros

            F3.Label12.Text = Motor.INFO_TiempoCalculoTransformaciones & " milisegundos"
            F3.Label14.Text = Motor.INFO_TiempoRecargaBuffer & " milisegundos"
            F3.Label16.Text = Motor.INFO_TiempoReordenacionBuffer & " milisegundos"
            F3.Label18.Text = Motor.INFO_TiempoReasignacionIndicesBuffer & " milisegundos"
            F3.Label20.Text = Motor.INFO_TiempoCalculoSombreado & " milisegundos"
            F3.Label22.Text = Motor.INFO_TiempoRepintado & " milisegundos"

            'g.Frame()
        Catch ex As Exception
            Exit Sub
        End Try

    End Sub

    Private Sub Form1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Control Then CONTROL = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim n As DateTime = Now
        Dim ts As TimeSpan

        For i As Integer = 0 To Motor.POLI_Poliedros.GetUpperBound(0)
            If Motor.POLI_Poliedros(i).Vertices.GetUpperBound(0) < 10000 Then Motor.POLI_RotarPoliedro(i, EnumEje.EjeZ, 2, False)
            'Motor.POLI_RotarPoliedro(i + 1, EnumEje.EjeX, 2 * (IIf((i + 1) Mod 2 = 0, 1, -1)))
            'Motor.ILUM_EstablerPosicionFoco(i, CALCTRANS3D_TransformarPunto(Motor.ILUM_Focos(i).Coordenadas, CALCTRANS3D_RotacionAlrededorEje(EnumEje.EjeZ, 2 * (IIf((i + 1) Mod 2 = 0, 1, 1)))))
        Next

        Motor.POLI_ActualizarPoliedros()

        ts = New TimeSpan((Now - n).Ticks)
        Me.Text = "Motor3D v0.0.1 - " & Motor.POLI_Poliedros.GetUpperBound(0) + 1 & " poliedros. " & Motor.ILUM_Focos.GetUpperBound(0) + 1 & " focos. " & FormatNumber(1 / ts.TotalSeconds, 0) & " FPS (" & ts.TotalMilliseconds & " milisegundos por ciclo.)"
    End Sub

    Private Sub Form1_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        End
    End Sub

    Private Sub PararReanudarMovimientoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PararReanudarMovimientoToolStripMenuItem.Click
        If Timer1.Enabled Then Timer1.Stop() Else Timer1.Start()
    End Sub

    Private Sub AñadirFocoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AñadirFocoToolStripMenuItem.Click
        Dim Foco As Foco
        Dim P As Punto3D
        Dim r, g, b As Byte

        Try
            P.X = CDbl(InputBox("Especifique la coordenada X del nuevo foco", "Motor3D - Añadir foco", "1000"))
            P.Y = CDbl(InputBox("Especifique la coordenada Y del nuevo foco", "Motor3D - Añadir foco", "1000"))
            P.Z = CDbl(InputBox("Especifique la coordenada Z del nuevo foco", "Motor3D - Añadir foco", "1000"))

            r = CByte(InputBox("Especifique el componente rojo del color del foco (0 a 255)", "Motor3D - Añadir foco", "255"))
            g = CByte(InputBox("Especifique el componente verde del color del foco (0 a 255)", "Motor3D - Añadir foco", "255"))
            b = CByte(InputBox("Especifique el componente azul del color del foco (0 a 255)", "Motor3D - Añadir foco", "255"))

            Foco.Color = Color.FromArgb(255, r, g, b)
            Foco.Brillo = 1
            Foco.Coordenadas = P
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Las coordenadas de un punto o vector tridimensional son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        Motor.ILUM_AñadirFoco(Foco)

    End Sub

    Private Sub QuitarFocoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitarFocoToolStripMenuItem.Click
        Try
            Motor.ILUM_QuitarFoco(CInt(InputBox("Especifique el indice del foco que desea eliminar", "Motor3D - Eliminar foco", "0")))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: El índice es un valor numérico entero", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try
    End Sub

    Private Sub QuitarPoliedroToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitarPoliedroToolStripMenuItem.Click
        Dim Indice As Integer
        Try
            Indice = CInt(InputBox("Especifique el indice del poliedro que desea eliminar", "Motor3D - Eliminar poliedro", "0"))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: El índice es un valor numérico entero", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try

        If Timer1.Enabled Then Timer1.Stop()
        Motor.POLI_QuitarPoliedro(Indice)
        IndicePoliedroSeleccionado = 0
        Label1.Text = "Poliedro seleccionado: " & IndicePoliedroSeleccionado
    End Sub

    Private Sub CuboToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CuboToolStripMenuItem.Click
        NuevoPoliedro(Motor3D.PRIM_Cubo, "Cubo")
    End Sub

    Private Sub SeleccionarPoliedroToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeleccionarPoliedroToolStripMenuItem.Click
        Dim Indice As Integer
        Try
            Indice = CInt(InputBox("Especifique el índice del poliedro que desea seleccionar", "Motor3D - Seleccionar poliedro", "0"))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: El índice es un valor numérico entero", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End Try


        IndicePoliedroSeleccionado = IIf(Indice <= Motor.POLI_Poliedros.GetUpperBound(0) AndAlso Indice >= 0, Indice, 0)
        Label1.Text = "Poliedro seleccionado: " & IndicePoliedroSeleccionado
    End Sub

    Private Sub FalsoConToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FalsoConToolStripMenuItem.Click
        Dim Cono As Poliedro
        Dim Indice As Integer
        Try
            Indice = CInt(InputBox("Especifique el número de generatrices del nuevo falso cono", "Motor3D - Nuevo falso cono", "3"))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: El número de generatrices debe ser un número entero", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        If Indice < 3 Then Indice = 3
        Cono = Motor3D.PRIM_Cono(Indice)

        Try
            Cono.Trasladar(New Punto3D(CDbl(InputBox("Especifique el valor X de la posición del nuevo falso cono", "Motor3D - Nuevo falso cono", "10")), CDbl(InputBox("Especifique el valor Y de la posición del nuevo falso cono", "Motor3D - Nuevo falso cono", "10")), CDbl(InputBox("Especifique el valor Z de la posición del nuevo falso cono", "Motor3D - Nuevo falso cono", "10"))))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Los valores de traslación de poliedros son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        Try
            Cono.CambiarEscala(New Punto3D(CDbl(InputBox("Especifique el ancho del nuevo falso cono", "Motor3D - Nuevo falso cono", "100")), CDbl(InputBox("Especifique el largo del nuevo falso cono", "Motor3D - Nuevo falso cono", "100")), CDbl(InputBox("Especifique el alto del nuevo falso cono", "Motor3D - Nuevo falso cono", "100"))))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Los valores de escalado de poliedros son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        'Cono.AplicarTransformaciones()

        Motor.POLI_AñadirPoliedro(Cono)
        Motor.POLI_ActualizarPoliedro(Array.IndexOf(Motor.POLI_Poliedros, Cono))
    End Sub

    Private Sub PiramideToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PiramideToolStripMenuItem.Click
        NuevoPoliedro(Motor3D.PRIM_Piramide, "Pirámide")
    End Sub

    Private Sub EscalarPoliedroToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EscalarPoliedroToolStripMenuItem.Click
        Dim Escalado As Punto3D
        Try
            Escalado.X = CDbl(InputBox("Especifique el valor de escalado X", "Motor3D - Escalado de poliedro seleccionado", "1"))
            Escalado.Y = CDbl(InputBox("Especifique el valor de escalado Y", "Motor3D - Escalado de poliedro seleccionado", "1"))
            Escalado.Z = CDbl(InputBox("Especifique el valor de escalado Z", "Motor3D - Escalado de poliedro seleccionado", "1"))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Los valores de escalado de poliedros son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Motor.POLI_EscalarPoliedro(IndicePoliedroSeleccionado, Escalado)
    End Sub

    Private Sub ReiniciarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReiniciarToolStripMenuItem.Click
        Me.Form1_Load(sender, e)
    End Sub

    Private Sub IcosaedroToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IcosaedroToolStripMenuItem.Click
        NuevoPoliedro(Motor3D.PRIM_Icosaedro, "Icosaedro")
    End Sub

    Private Sub AnilloToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnilloToolStripMenuItem.Click
        NuevoPoliedro(Motor3D.PRIM_Anillo, "Anillo")
    End Sub

    Private Sub NuevoPoliedro(ByVal Poliedro As Poliedro, ByVal Tipo As String)

        Try
            Poliedro.Trasladar(New Punto3D(CDbl(InputBox("Especifique el valor X de la posición del nuevo poliedro (" & Tipo & ")", "Motor3D - Nuevo poliedro(" & Tipo & ")", "10")), CDbl(InputBox("Especifique el valor Y del nuevo poliedro (" & Tipo & ")", "Motor3D - Nuevo poliedro(" & Tipo & ")", "10")), CDbl(InputBox("Especifique el valor Z de la posición del nuevo poliedro (" & Tipo & ")", "Motor3D - Nuevo poliedro(" & Tipo & ")", "10"))))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Los valores de traslación de poliedros son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        Try
            Poliedro.CambiarEscala(New Punto3D(CDbl(InputBox("Especifique el ancho del nuevo poliedro (" & Tipo & ")", "Motor3D - Nuevo poliedro(" & Tipo & ")", "100")), CDbl(InputBox("Especifique el largo del nuevo poliedro (" & Tipo & ")", "Motor3D - Nuevo poliedro(" & Tipo & ")", "100")), CDbl(InputBox("Especifique el alto del nuevo poliedro (" & Tipo & ")", "Motor3D - Nuevo poliedro(" & Tipo & ")", "100"))))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Los valores de escalado de poliedros son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        'Piramide.AplicarTransformaciones()

        Motor.POLI_AñadirPoliedro(Poliedro)
        Motor.POLI_ActualizarPoliedro(Array.IndexOf(Motor.POLI_Poliedros, Poliedro))
    End Sub

    Private Sub DodecaedroToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DodecaedroToolStripMenuItem.Click
        NuevoPoliedro(Motor3D.PRIM_Dodecaedro, "Dodecaedro")
    End Sub

    Private Sub CambiarColorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CambiarColorToolStripMenuItem.Click
        Dim Color As Color
        ColorDialog.Color = Drawing.Color.White
        ColorDialog.ShowDialog()
        Color = ColorDialog.Color

        Motor.POLI_CambiarColorPoliedro(IndicePoliedroSeleccionado, Color)
    End Sub

    Private Sub TrasladaraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrasladaraToolStripMenuItem.Click
        Dim Destino As Punto3D
        Try
            Destino.X = CDbl(InputBox("Especifique la coordenada X del punto de destino", "Motor 3D - Traslación a destino", "0"))
            Destino.Y = CDbl(InputBox("Especifique la coordenada Y del punto de destino", "Motor 3D - Traslación a destino", "0"))
            Destino.Z = CDbl(InputBox("Especifique la coordenada Z del punto de destino", "Motor 3D - Traslación a destino", "0"))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Las coordenadas de un punto o vector tridimensional son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Motor.POLI_TrasladarPoliedroADestino(IndicePoliedroSeleccionado, Destino)
    End Sub

    Private Sub IsometricaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IsometricaToolStripMenuItem.Click
        Motor.REPR_CambiarRepresentacion(TipoRepresentacion.Isometrica)
        OrtogonalGeneralToolStripMenuItem.Checked = False
        IsometricaToolStripMenuItem.Checked = True
        XOYToolStripMenuItem.Checked = False
        ZOXToolStripMenuItem.Checked = False
        ZOYToolStripMenuItem.Checked = False
        ConicaToolStripMenuItem.Checked = False
        PosiciónDeLaCámaraToolStripMenuItem.Enabled = False
        DistanciaDelPlanoToolStripMenuItem.Enabled = False
    End Sub

    Private Sub OrtogonalGeneralToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrtogonalGeneralToolStripMenuItem.Click
        Motor.REPR_CambiarRepresentacion(TipoRepresentacion.OrtogonalGeneral)
        OrtogonalGeneralToolStripMenuItem.Checked = True
        IsometricaToolStripMenuItem.Checked = False
        XOYToolStripMenuItem.Checked = False
        ZOXToolStripMenuItem.Checked = False
        ZOYToolStripMenuItem.Checked = False
        ConicaToolStripMenuItem.Checked = False
        PosiciónDeLaCámaraToolStripMenuItem.Enabled = True
        DistanciaDelPlanoToolStripMenuItem.Enabled = False
    End Sub

    Private Sub PosiciónDeACámaraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PosiciónDeLaCámaraToolStripMenuItem.Click
        Dim Punto As Punto3D
        If Not (Motor.REPR_Camara.Representacion = TipoRepresentacion.Conica Or Motor.REPR_Camara.Representacion = TipoRepresentacion.OrtogonalLibre Or Motor.REPR_Camara.Representacion = TipoRepresentacion.OrtogonalGeneral) Then Exit Sub
        Try
            Punto.X = CDbl(InputBox("Especifique la coordenada X de la nueva posición", "Motor 3D - Cambio de posición de la cámara", "0"))
            Punto.Y = CDbl(InputBox("Especifique la coordenada Y de la nueva posición", "Motor 3D - Cambio de posición de la cámara", "0"))
            Punto.Z = CDbl(InputBox("Especifique la coordenada Z de la nueva posición", "Motor 3D - Cambio de posición de la cámara", "0"))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Las coordenadas de un punto o vector tridimensional son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Motor.REPR_Camara.EstablecerPosicion(Punto)
        Motor.REPR_Camara.EstablecerVectorDireccion(New Vector3D(Punto, New Punto3D(0, 0, 0)))
        Motor = Motor

        Motor.POLI_ActualizarPoliedros()
    End Sub

    Private Sub XOYToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XOYToolStripMenuItem.Click
        Motor.REPR_CambiarRepresentacion(TipoRepresentacion.OrtogonalXOY)
        OrtogonalGeneralToolStripMenuItem.Checked = False
        IsometricaToolStripMenuItem.Checked = False
        XOYToolStripMenuItem.Checked = True
        ZOXToolStripMenuItem.Checked = False
        ZOYToolStripMenuItem.Checked = False
        ConicaToolStripMenuItem.Checked = False
        PosiciónDeLaCámaraToolStripMenuItem.Enabled = False
        DistanciaDelPlanoToolStripMenuItem.Enabled = False
    End Sub

    Private Sub ZOXToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZOXToolStripMenuItem.Click
        Motor.REPR_CambiarRepresentacion(TipoRepresentacion.OrtogonalZOX)
        OrtogonalGeneralToolStripMenuItem.Checked = False
        IsometricaToolStripMenuItem.Checked = False
        XOYToolStripMenuItem.Checked = False
        ZOXToolStripMenuItem.Checked = True
        ZOYToolStripMenuItem.Checked = False
        ConicaToolStripMenuItem.Checked = False
        PosiciónDeLaCámaraToolStripMenuItem.Enabled = False
        DistanciaDelPlanoToolStripMenuItem.Enabled = False
    End Sub

    Private Sub ZOYToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZOYToolStripMenuItem.Click
        Motor.REPR_CambiarRepresentacion(TipoRepresentacion.OrtogonalZOY)
        OrtogonalGeneralToolStripMenuItem.Checked = False
        IsometricaToolStripMenuItem.Checked = False
        XOYToolStripMenuItem.Checked = False
        ZOXToolStripMenuItem.Checked = False
        ZOYToolStripMenuItem.Checked = True
        ConicaToolStripMenuItem.Checked = False
        PosiciónDeLaCámaraToolStripMenuItem.Enabled = False
        DistanciaDelPlanoToolStripMenuItem.Enabled = False
    End Sub

    Private Sub ConicaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConicaToolStripMenuItem.Click
        'MessageBox.Show("La representación cónica está desabilitada", "Motor3D - Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'Exit Sub
        'Camara.Posicion = New Punto3D(-600, 0, 0)
        'Camara.VectorDireccion = New Vector3D(Camara.Posicion, New Punto3D(0, 0, 0))
        Motor.REPR_CambiarRepresentacion(TipoRepresentacion.Conica)
        'Camara.Distancia = 300
        OrtogonalGeneralToolStripMenuItem.Checked = False
        IsometricaToolStripMenuItem.Checked = False
        XOYToolStripMenuItem.Checked = False
        ZOXToolStripMenuItem.Checked = False
        ZOYToolStripMenuItem.Checked = False
        ConicaToolStripMenuItem.Checked = True
        PosiciónDeLaCámaraToolStripMenuItem.Enabled = True
        DistanciaDelPlanoToolStripMenuItem.Enabled = True
    End Sub

    Private Sub DibujarEjesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DibujarEjesToolStripMenuItem.Click
        DibujarEjesToolStripMenuItem.Checked = Not DibujarEjesToolStripMenuItem.Checked
        Motor.PINT_VisibilidadEjes(DibujarEjesToolStripMenuItem.Checked)
    End Sub

    Private Sub DistanciaDelPlanoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DistanciaDelPlanoToolStripMenuItem.Click
        Dim Distancia As Double
        Try
            Distancia = CDbl(InputBox("Especifique la distancia desde la cámara hasta el plano de proyección", "Motor3D - Distancia del plano de proyección", "10000"))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: La distancia al plano de proyección es un valor numérico de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        Motor.REPR_Camara.EstablecerDistancia(Distancia)
        Motor.POLI_ActualizarPoliedros()
    End Sub

    Private Sub MoverFocosConElCursorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoverFocosConElCursorToolStripMenuItem.Click
        MoverFocosConElCursorToolStripMenuItem.Checked = Not MoverFocosConElCursorToolStripMenuItem.Checked
    End Sub

    Private Sub EstablecerPosiciónDeUnFocoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EstablecerPosiciónDeUnFocoToolStripMenuItem.Click
        Dim Posicion As Punto3D
        Dim Indice As Integer
        Try
            Do
                Indice = CInt(InputBox("Especifique el índice del foco que desea recolocar", "Motor3D - Recolocar foco", "0"))

                If Indice > Motor.ILUM_Focos.GetUpperBound(0) OrElse Indice < 0 Then
                    MessageBox.Show("El índice especificado está fuera de los límites de la matriz" & vbNewLine & "Recuerde: El índice debe estar comprendido entre " & 0 & " y " & Motor.ILUM_Focos.GetUpperBound(0), "Motor3D - Recolocar foco", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Loop While Indice > Motor.ILUM_Focos.GetUpperBound(0) OrElse Indice < 0

            Posicion.X = CDbl(InputBox("Especifique la coordenada X de la nueva posición", "Motor3D - Recolocar foco", "0"))
            Posicion.Y = CDbl(InputBox("Especifique la coordenada Y de la nueva posición", "Motor3D - Recolocar foco", "0"))
            Posicion.Z = CDbl(InputBox("Especifique la coordenada Z de la nueva posición", "Motor3D - Recolocar foco", "0"))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Las coordenadas de un punto o vector tridimensional son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Motor.ILUM_EstablerPosicionFoco(Indice, Posicion)
    End Sub

    Private Sub EscalarFijoToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EscalarFijoToolStripMenuItem3.Click
        Dim Escalado As Punto3D
        Try
            Escalado.X = CDbl(InputBox("Especifique el valor de escalado X", "Motor3D - Escalado fijo de poliedro seleccionado", "1"))
            Escalado.Y = CDbl(InputBox("Especifique el valor de escalado Y", "Motor3D - Escalado fijo de poliedro seleccionado", "1"))
            Escalado.Z = CDbl(InputBox("Especifique el valor de escalado Z", "Motor3D - Escalado fijo de poliedro seleccionado", "1"))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Los valores de escalado de poliedros son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Motor.POLI_EscalarFijoPoliedro(IndicePoliedroSeleccionado, Escalado)
    End Sub

    Private Sub DireccionCamaraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DireccionCamaraToolStripMenuItem.Click
        Dim Direccion As Vector3D
        Try
            Direccion.X = CDbl(InputBox("Especifique la coordenada X de la nueva dirección", "Motor3D - Dirección de la cámara", "0"))
            Direccion.Y = CDbl(InputBox("Especifique la coordenada Y de la nueva dirección", "Motor3D - Dirección de la cámara", "0"))
            Direccion.Z = CDbl(InputBox("Especifique la coordenada Z de la nueva dirección", "Motor3D - Dirección de la cámara", "0"))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Las coordenadas de un punto o vector tridimensional son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Motor.REPR_EstablecerVectorDirecionCamara(Direccion)
    End Sub

    Private Sub OrtogonalLibreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrtogonalLibreToolStripMenuItem.Click
        Motor.REPR_CambiarRepresentacion(TipoRepresentacion.OrtogonalLibre)
    End Sub

    Private Sub CilindroToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CilindroToolStripMenuItem.Click
        Dim cilindro As Poliedro
        Dim Indice As Integer
        Try
            Indice = CInt(InputBox("Especifique el número de generatrices del nuevo falso cilindro", "Motor3D - Nuevo falso cilindro", "3"))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: El número de generatrices debe ser un número entero", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        If Indice < 3 Then Indice = 3
        cilindro = Motor3D.PRIM_Cilindro(Indice)

        Try
            cilindro.Trasladar(New Punto3D(CDbl(InputBox("Especifique el valor X de la posición del nuevo falso cilindro", "Motor3D - Nuevo falso cilindro", "10")), CDbl(InputBox("Especifique el valor Y de la posición del nuevo falso cilindro", "Motor3D - Nuevo falso cilindro", "10")), CDbl(InputBox("Especifique el valor Z de la posición del nuevo falso cilindro", "Motor3D - Nuevo falso cilindro", "10"))))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Los valores de traslación de poliedros son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        Try
            cilindro.CambiarEscala(New Punto3D(CDbl(InputBox("Especifique el ancho del nuevo falso cilindro", "Motor3D - Nuevo falso cilindro", "100")), CDbl(InputBox("Especifique el largo del nuevo falso cilindro", "Motor3D - Nuevo falso cilindro", "100")), CDbl(InputBox("Especifique el alto del nuevo falso cilindro", "Motor3D - Nuevo falso cilindro", "100"))))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Los valores de escalado de poliedros son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        'cilindro.AplicarTransformaciones()

        Motor.POLI_AñadirPoliedro(cilindro)
        Motor.POLI_ActualizarPoliedro(Array.IndexOf(Motor.POLI_Poliedros, cilindro))
    End Sub

    Private Sub ActivadoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActivadoToolStripMenuItem.Click
        Motor.PINT_Sombreado = Not Motor.PINT_Sombreado
        ActivadoToolStripMenuItem.Checked = Motor.PINT_Sombreado
    End Sub

    Private Sub CambiarColorFocoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CambiarColorFocoToolStripMenuItem.Click
        Dim r, g, b As Byte
        Dim Indice As Long = 0
        Try
            Do
                Indice = CInt(InputBox("Especifique el índice del foco que desea recolocar", "Motor3D - Recolocar foco", "0"))

                If Indice > Motor.ILUM_Focos.GetUpperBound(0) OrElse Indice < 0 Then
                    MessageBox.Show("El índice especificado está fuera de los límites de la matriz" & vbNewLine & "Recuerde: El índice debe estar comprendido entre " & 0 & " y " & Motor.ILUM_Focos.GetUpperBound(0), "Motor3D - Cambiar color de foco", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Loop While Indice > Motor.ILUM_Focos.GetUpperBound(0) OrElse Indice < 0

            r = CByte(InputBox("Especifique el componente rojo del color del foco (0 a 255)", "Motor3D - Cambiar color de foco", "255"))
            g = CByte(InputBox("Especifique el componente verde del color del foco (0 a 255)", "Motor3D - Cambiar color de foco", "255"))
            b = CByte(InputBox("Especifique el componente azul del color del foco (0 a 255)", "Motor3D - Cambiar color de foco", "255"))

            Motor.ILUM_EstablerColorFoco(Indice, Color.FromArgb(255, r, g, b))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Los componentes de color son valores byte (Entero comprendido en el intervalo cerrado 0 a 255)", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub ConfiguracionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfiguracionToolStripMenuItem.Click
        F2 = New Form2
        F2.HScrollBar1.Value = Motor.ILUM_DatosIluminacion.AportacionAmbiente * 100
        F2.HScrollBar2.Value = Motor.ILUM_DatosIluminacion.AportacionDifusa * 100
        F2.HScrollBar3.Value = Motor.ILUM_DatosIluminacion.AportacionEspecular * 100

        F2.NumericUpDown1.Value = Motor.ILUM_DatosIluminacion.ExponenteEspecular

        F2.Label1.Text = F2.HScrollBar1.Value & " %"
        F2.Label6.Text = F2.HScrollBar2.Value & " %"
        F2.Label7.Text = F2.HScrollBar3.Value & " %"

        F2.TextBox1.Text = Motor.ILUM_DatosIluminacion.ConstanteDistancia1.ToString
        F2.TextBox2.Text = Motor.ILUM_DatosIluminacion.ConstanteDistancia2.ToString
        F2.TextBox3.Text = Motor.ILUM_DatosIluminacion.ConstanteDistancia3.ToString

        F2.Show()
    End Sub

    Private Sub EsferaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EsferaToolStripMenuItem.Click
        Dim Esfera As Poliedro
        Dim Indice As Integer
        Try
            Indice = CInt(InputBox("Especifique el número de generatrices de la nueva falsa esfera", "Motor3D - Nueva falsa esfera", "3"))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: El número de generatrices debe ser un número entero", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        If Indice < 3 Then Indice = 3
        Esfera = Motor3D.PRIM_Esfera(Indice)

        Try
            Esfera.Trasladar(New Punto3D(CDbl(InputBox("Especifique el valor X de la posición de la nueva falsa esfera", "Motor3D - Nueva falsa esfera", "10")), CDbl(InputBox("Especifique el valor Y de la posición de la nueva falsa esfera", "Motor3D - Nueva falsa esfera", "10")), CDbl(InputBox("Especifique el valor Z de la posición de la nueva falsa esfera", "Motor3D - Nuevo Nueva falsa esfera", "10"))))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Los valores de traslación de poliedros son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        Try
            Esfera.CambiarEscala(New Punto3D(CDbl(InputBox("Especifique el ancho de la nueva falsa esfera", "Motor3D - Nueva falsa esfera", "100")), CDbl(InputBox("Especifique el largo de la nueva falsa esfera", "Motor3D - Nueva falsa esfera", "100")), CDbl(InputBox("Especifique el alto de la nueva falsa esfera", "Motor3D -Nueva falsa esfera", "100"))))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Los valores de escalado de poliedros son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        'cilindro.AplicarTransformaciones()

        Motor.POLI_AñadirPoliedro(Esfera)
        Motor.POLI_ActualizarPoliedro(Array.IndexOf(Motor.POLI_Poliedros, Esfera))
    End Sub

    Private Sub DatosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DatosToolStripMenuItem.Click
        F3 = New Form3
        F3.Show()
        AddHandler Motor.BUFF_FinRecargaBuffer, AddressOf F3.RellenarBuffer
        AddHandler Motor.PINT_FinRedibujado, AddressOf F3.EventoRepintado
        Motor.BUFF_RecargarBuffer()
    End Sub

    Private Sub RotarAlrededorToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RotarAlrededorToolStripMenuItem2.Click
        Dim AnguloRotacion As Single
        Dim EjeRotacion As EnumEje
        Try
            EjeRotacion = CInt(InputBox("Especifique el eje sobre el que se realizará el giro (0 para EjeX, 1 para EjeY, 2 para EjeZ)", "Motor3D - Rotación sobre sí mismo", "0"))

            AnguloRotacion = CSng(InputBox("Especifique el angulo de rotación en grados", "Motor3D - Rotación sobre si mismo", "0"))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: El eje de giro se establece mediante un valor entero comprendido entre 0 y 2" & vbNewLine & _
                            "Recuerde: El ángulo de rotación se establece mediante un valor numérico de coma flotante de precisión simple", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Motor.POLI_RotarPoliedroSobreSiMismo(IndicePoliedroSeleccionado, EjeRotacion, AnguloRotacion)
    End Sub

    Private Sub FlatShadingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FlatShadingToolStripMenuItem.Click
        Motor.PINT_Shader = Shader.FlatShading
        WireFrameShadingToolStripMenuItem.Checked = False
        FlatShadingToolStripMenuItem.Checked = True
        GouraudShadingToolStripMenuItem.Checked = False
    End Sub

    Private Sub GouraudShadingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GouraudShadingToolStripMenuItem.Click
        Motor.PINT_Shader = Shader.GouraudShading
        WireFrameShadingToolStripMenuItem.Checked = False
        FlatShadingToolStripMenuItem.Checked = False
        GouraudShadingToolStripMenuItem.Checked = True
    End Sub

    Private Sub WireFrameShadingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WireFrameShadingToolStripMenuItem.Click
        Motor.PINT_Shader = Shader.WireFrameShading
        WireFrameShadingToolStripMenuItem.Checked = True
        FlatShadingToolStripMenuItem.Checked = False
        GouraudShadingToolStripMenuItem.Checked = False
    End Sub

    Private Sub MostrarNormalesDeLasCarasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MostrarNormalesDeLasCarasToolStripMenuItem.Click
        MostrarNormalesDeLasCarasToolStripMenuItem.Checked = Not MostrarNormalesDeLasCarasToolStripMenuItem.Checked
        Motor.DEPU_MostrarNormalesCaras = MostrarNormalesDeLasCarasToolStripMenuItem.Checked
    End Sub

    Private Sub MostrarNormalesDeLosVértiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MostrarNormalesDeLosVértiToolStripMenuItem.Click
        MostrarNormalesDeLosVértiToolStripMenuItem.Checked = Not MostrarNormalesDeLosVértiToolStripMenuItem.Checked
        Motor.DEPU_MostrarNormalesVertices = MostrarNormalesDeLosVértiToolStripMenuItem.Checked
    End Sub

    Private Sub MostrarDatosDeLosVérticesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MostrarDatosDeLosVérticesToolStripMenuItem.Click
        MostrarDatosDeLosVérticesToolStripMenuItem.Checked = Not MostrarDatosDeLosVérticesToolStripMenuItem.Checked
        Motor.DEPU_MostrarDatosVertices = MostrarDatosDeLosVérticesToolStripMenuItem.Checked
    End Sub

    Private Sub MostrarTrianToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MostrarTrianToolStripMenuItem.Click
        MostrarTrianToolStripMenuItem.Checked = Not MostrarTrianToolStripMenuItem.Checked
        Motor.DEPU_MostrarTriangulacion = MostrarTrianToolStripMenuItem.Checked
    End Sub

    Private Sub MostrarCarabufferSeñaladaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MostrarCarabufferSeñaladaToolStripMenuItem.Click
        MostrarCarabufferSeñaladaToolStripMenuItem.Checked = Not MostrarCarabufferSeñaladaToolStripMenuItem.Checked
        Motor.DEPU_SeñalarCaraBuffer = MostrarCarabufferSeñaladaToolStripMenuItem.Checked
    End Sub

    Private Sub BufferToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BufferToolStripMenuItem.Click
        BufferToolStripMenuItem.Checked = Not BufferToolStripMenuItem.Checked
        Motor.DEPU_DepuracionBuffer = BufferToolStripMenuItem.Checked
    End Sub

    Private Sub DibujoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DibujoToolStripMenuItem.Click
        DibujoToolStripMenuItem.Checked = Not DibujoToolStripMenuItem.Checked
        Motor.DEPU_DepuracionDibujo = DibujoToolStripMenuItem.Checked
    End Sub

    Private Sub SombreadoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SombreadoToolStripMenuItem.Click
        SombreadoToolStripMenuItem.Checked = Not SombreadoToolStripMenuItem.Checked
        Motor.DEPU_DepuracionSombreado = SombreadoToolStripMenuItem.Checked
    End Sub

    Private Sub TodoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TodoToolStripMenuItem.Click
        TodoToolStripMenuItem.Checked = Not TodoToolStripMenuItem.Checked
        Motor.DEPU_DepuracionTodo = TodoToolStripMenuItem.Checked
    End Sub

    Private Sub MostrarIndicesBufferToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MostrarIndicesBufferToolStripMenuItem.Click
        MostrarIndicesBufferToolStripMenuItem.Checked = Not MostrarIndicesBufferToolStripMenuItem.Checked
        Motor.DEPU_MostrarIndicesBuffer = MostrarIndicesBufferToolStripMenuItem.Checked
    End Sub

    Private Sub DibujarLucesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DibujarLucesToolStripMenuItem.Click
        DibujarLucesToolStripMenuItem.Checked = Not DibujarLucesToolStripMenuItem.Checked
        Motor.PINT_PintarFocos = DibujarLucesToolStripMenuItem.Checked
    End Sub

    Private Sub ReestablecerTransformacionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReestablecerTransformacionesToolStripMenuItem.Click
        Motor.POLI_ReestablecerTransformacionesPoliedro(IndicePoliedroSeleccionado)
    End Sub

    Private Sub CambiarTipoFocoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CambiarTipoFocoToolStripMenuItem.Click
        Dim Indice As Long
        Dim Tipo As TipoFoco
        Do
            Indice = CInt(InputBox("Especifique el índice del foco que desea modificar", "Motor3D - Modificar foco", "0"))

            If Indice > Motor.ILUM_Focos.GetUpperBound(0) OrElse Indice < 0 Then
                MessageBox.Show("El índice especificado está fuera de los límites de la matriz" & vbNewLine & "Recuerde: El índice debe estar comprendido entre " & 0 & " y " & Motor.ILUM_Focos.GetUpperBound(0), "Motor3D - Modificar foco", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Loop While Indice > Motor.ILUM_Focos.GetUpperBound(0) OrElse Indice < 0

        Try
            Tipo = CInt(InputBox("Especifique el tipo del foco (0 puntual, 1 focal, 2 plano)", "Motor3D - Modificar foco", "0"))
        Catch ex As Exception
            MessageBox.Show("El tipo de foco se especifica con un número comprendido entre el cero y el dos", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Sub
        End Try

        Motor.ILUM_EstablerTipoFoco(Indice, Tipo)
    End Sub

    Private Sub DirecciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DirecciónToolStripMenuItem.Click
        Dim Indice As Long
        Dim Direccion As Vector3D
        Do
            Indice = CInt(InputBox("Especifique el índice del foco que desea modificar", "Motor3D - Modificar foco", "0"))

            If Indice > Motor.ILUM_Focos.GetUpperBound(0) OrElse Indice < 0 Then
                MessageBox.Show("El índice especificado está fuera de los límites de la matriz" & vbNewLine & "Recuerde: El índice debe estar comprendido entre " & 0 & " y " & Motor.ILUM_Focos.GetUpperBound(0), "Motor3D - Modificar foco", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Loop While Indice > Motor.ILUM_Focos.GetUpperBound(0) OrElse Indice < 0


        Try
            Direccion.X = CDbl(InputBox("Especifique la coordenada X de la nueva dirección", "Motor3D - Modificar foco", "0"))
            Direccion.Y = CDbl(InputBox("Especifique la coordenada Y de la nueva dirección", "Motor3D - Modificar foco", "0"))
            Direccion.Z = CDbl(InputBox("Especifique la coordenada Z de la nueva dirección", "Motor3D - Modificar foco", "0"))
        Catch ex As Exception
            MessageBox.Show("El parámetro no es válido" & vbNewLine & "Recuerde: Las coordenadas de un punto o vector tridimensional son valores numéricos de coma flotante de doble precisión", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Motor.ILUM_EstablerDireccionFoco(Indice, Direccion)
    End Sub

    Private Sub DeltaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeltaToolStripMenuItem.Click
        Dim Indice As Long
        Dim Delta As Single
        Do
            Indice = CInt(InputBox("Especifique el índice del foco que desea modificar", "Motor3D - Modificar foco", "0"))

            If Indice > Motor.ILUM_Focos.GetUpperBound(0) OrElse Indice < 0 Then
                MessageBox.Show("El índice especificado está fuera de los límites de la matriz" & vbNewLine & "Recuerde: El índice debe estar comprendido entre " & 0 & " y " & Motor.ILUM_Focos.GetUpperBound(0), "Motor3D - Modificar foco", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Loop While Indice > Motor.ILUM_Focos.GetUpperBound(0) OrElse Indice < 0

        Try
            Delta = CSng(InputBox("Especifique el ángulo delta del foco", "Motor3D - Modificar foco", "0"))
            If Delta < 0 Then Delta = 360 + Delta
            If Delta > 359 Then Delta = 360 Mod Delta
        Catch ex As Exception
            MessageBox.Show("El ángulo delta es un valor de coma flotante de precisión simple comprendido entre 0 y 359", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Sub
        End Try

        Motor.ILUM_EstablerDeltaFoco(Indice, Delta)
    End Sub

    Private Sub RadioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioToolStripMenuItem.Click
        Dim Indice As Long
        Dim Radio As Double
        Do
            Indice = CInt(InputBox("Especifique el índice del foco que desea modificar", "Motor3D - Modificar foco", "0"))

            If Indice > Motor.ILUM_Focos.GetUpperBound(0) OrElse Indice < 0 Then
                MessageBox.Show("El índice especificado está fuera de los límites de la matriz" & vbNewLine & "Recuerde: El índice debe estar comprendido entre " & 0 & " y " & Motor.ILUM_Focos.GetUpperBound(0), "Motor3D - Modificar foco", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Loop While Indice > Motor.ILUM_Focos.GetUpperBound(0) OrElse Indice < 0

        Try
            Radio = CDbl(InputBox("Especifique el radio de iluminación del foco", "Motor3D - Modificar foco", "0"))
            If Radio <= 0 Then Radio = 0.00001
        Catch ex As Exception
            MessageBox.Show("El radio es un valor de coma flotante de precisión doble mayor que cero", "Motor3D - Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Sub
        End Try

        Motor.ILUM_EstablerRadioFoco(Indice, Radio)
    End Sub

    Private Sub TeselacionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TeselacionToolStripMenuItem.Click
        TeselacionToolStripMenuItem.Checked = Not TeselacionToolStripMenuItem.Checked
        Motor.PINT_Teselacion = TeselacionToolStripMenuItem.Checked
    End Sub

    Private Sub Form1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Motor.IniciarRender()
    End Sub
End Class
