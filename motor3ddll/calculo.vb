Imports System.Math 

Public Module Calculo
#Region "Calculo Espacio 3D"

    ''' <summary>
    ''' Obtiene un valor de tipo Vector3D que representa el producto vectorial de dos vectores
    ''' </summary>
    ''' <param name="V1">Primer vector</param>
    ''' <param name="V2">Segundo vector</param>
    Public Function CALC3D_ProductoVectorial(ByVal V1 As Vector3D, ByVal V2 As Vector3D) As Vector3D
        Dim x, y, z As Double

        x = (V1.Y * V2.Z) - (V1.Z * V2.Y)
        y = (V1.Z * V2.X) - (V1.X * V2.Z)
        z = (V1.X * V2.Y) - (V1.Y * V2.X)

        Return New Vector3D(x, y, z)
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Double que representa el producto escalar de dos vectores
    ''' </summary>
    ''' <param name="V1">Primer vector</param>
    ''' <param name="V2">Segundo vector</param>
    Public Function CALC3D_ProductoEscalar3D(ByVal V1 As Vector3D, ByVal V2 As Vector3D) As Double
        Return (V1.X * V2.X) + (V1.Y * V2.Y) + (V1.Z * V2.Z)
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Single que representa el ángulo (en grados) formado por dos vectores
    ''' </summary>
    ''' <param name="V1">Primer vector</param>
    ''' <param name="V2">Segundo vector</param>
    Public Function CALC3D_AnguloVectores3D(ByVal V1 As Vector3D, ByVal V2 As Vector3D) As Single
        Dim Retorno, RetornoRad As Single

        RetornoRad = Acos(CALC3D_ProductoEscalar3D(V1, V2) / (V1.Modulo * V2.Modulo))
        Retorno = 180 * RetornoRad / PI
        'If Retorno < 0 Then Retorno = 360 + Retorno

        Return Retorno
    End Function

    Public Function CALC3D_Distancia(ByVal P1 As Punto3D, ByVal P2 As Punto3D) As Double
        Return Sqrt(((P2.X - P1.X) ^ 2) + ((P2.Y - P1.Y) ^ 2) + ((P2.Z - P1.Z) ^ 2))
    End Function

    ''' <summary>
    ''' Obtiene un punto específico perteneciente a dos planos.Si los planos son paralelos devuelve el punto (0,0,0)
    ''' </summary>
    ''' <param name="Plano1">Primer plano</param>
    ''' <param name="Plano2">Segundo plano</param>
    ''' <param name="Parametro">Parámetro que especifica el punto buscado</param>
    Public Function CALC3D_PuntoDeDosPlanos(ByVal Plano1 As Plano3D, ByVal Plano2 As Plano3D, ByVal Parametro As Double) As Punto3D
        Dim Retorno As Punto3D
        Dim a, b, c, d As Double
        Dim aa, bb, cc, dd As Double
        If CALC3D_AnguloVectores3D(Plano1.VectorNormal, Plano2.VectorNormal) = 0 Then Return New Punto3D(0, 0, 0)

        a = Plano1.A
        b = Plano1.B
        c = Plano1.C
        d = Plano1.D
        aa = Plano2.A
        bb = Plano2.B
        cc = Plano2.C
        dd = Plano2.D

        Retorno.X = (((dd * bb / b) - d - (cc * Parametro)) / (aa - (a * bb / b)))
        Retorno.Y = ((-d * (a * Retorno.X) / (aa - (a * bb / b)) - (c * Parametro)) / b)

        Return Retorno
    End Function

    ''' <summary>
    ''' Obtiene un punto cualquiera perteneciente a dos planos.Si los planos son paralelos devuelve el punto (0,0,0)
    ''' </summary>
    ''' <param name="Plano1">Primer plano</param>
    ''' <param name="Plano2">Segundo plano</param>
    Public Function CALC3D_PuntoDeDosPlanos(ByVal Plano1 As Plano3D, ByVal Plano2 As Plano3D) As Punto3D
        Dim Retorno As Punto3D
        Dim Parametro As Double = 0
        Dim a, b, c, d As Double
        Dim aa, bb, cc, dd As Double
        If CALC3D_AnguloVectores3D(Plano1.VectorNormal, Plano2.VectorNormal) = 0 Then Return New Punto3D(0, 0, 0)

        a = Plano1.A
        b = Plano1.B
        c = Plano1.C
        d = Plano1.D
        aa = Plano2.A
        bb = Plano2.B
        cc = Plano2.C
        dd = Plano2.D

        Retorno.X = (((dd * bb / b) - d - (cc * Parametro)) / (aa - (a * bb / b)))
        Retorno.Y = ((-d * (a * Retorno.X) / (aa - (a * bb / b)) - (c * Parametro)) / b)

        Return Retorno
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Punto3D que representa el punto de intersección entre un plano y una recta.
    ''' Si el plano y la recta son paralelos, devuelve el punto (0,0,0)
    ''' </summary>
    Public Function CALC3D_InterseccionPlanoRecta(ByVal Plano As Plano3D, ByVal Recta As Recta3D) As Punto3D
        Dim Landa As Double
        Dim Retorno As Punto3D

        Dim a, b, c, d As Double
        Dim vx, vy, vz As Double
        Dim xx, yy, zz As Double

        If CALC3D_AnguloVectores3D(Plano.VectorNormal, Recta.VectorDirector) = 90 Then Return New Punto3D(0, 0, 0)
        Recta = Recta
        a = Plano.A
        b = Plano.B
        c = Plano.C
        d = Plano.D
        vx = Recta.VectorDirector.X
        vy = Recta.VectorDirector.Y
        vz = Recta.VectorDirector.Z
        xx = Recta.PuntoDirector.X
        yy = Recta.PuntoDirector.Y
        zz = Recta.PuntoDirector.Z

        Landa = (-1) * ((a * xx + b * yy + c * zz + d) / (a * vx + b * vy + c * vz))
        Retorno = Recta.PuntoDeLaRecta(Landa)

        Return Retorno
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Plano3D que representa el plano mediatriz de dos puntos en el espacio tridimensional
    ''' </summary>
    Public Function CALC3D_Mediatriz(ByVal P1 As Punto3D, ByVal P2 As Punto3D) As Plano3D
        Dim V As Vector3D
        Dim PuntoMedio As Punto3D

        V = New Vector3D(P1, P2)

        PuntoMedio.X = V.X / 2
        PuntoMedio.Y = V.Y / 2
        PuntoMedio.Z = V.Z / 2

        Return New Plano3D(V, PuntoMedio)
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Plano3D que representa el plano que contiene a la cara especificada
    ''' </summary>
    ''' <param name="Poliedro">Poliedro al que pretenece la cara</param>
    ''' <param name="Cara">La cara especificada</param>
    Public Function CALC3D_CalculoPlanoCara(ByVal Poliedro As Poliedro, ByVal Cara As Cara) As Plano3D
        Return New Plano3D(Poliedro.Vertices(Cara.Vertices(0)).Coordenadas, Poliedro.Vertices(Cara.Vertices(1)).Coordenadas, Poliedro.Vertices(Cara.Vertices(2)).Coordenadas)
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Vector3D que representa el vector normal de la cara especificada
    ''' </summary>
    ''' <param name="Poliedro">Poliedro al que pretenece la cara</param>
    ''' <param name="Cara">La cara especificada</param>
    Public Function CALC3D_CalculoNormalCara(ByVal Poliedro As Poliedro, ByVal Cara As Cara) As Vector3D
        Dim V1, V2, Retorno As Vector3D

        V1 = New Vector3D(Poliedro.Vertices(Cara.Vertices(0)).Coordenadas, Poliedro.Vertices(Cara.Vertices(1)).Coordenadas)
        V2 = New Vector3D(Poliedro.Vertices(Cara.Vertices(0)).Coordenadas, Poliedro.Vertices(Cara.Vertices(Cara.Vertices.GetUpperBound(0))).Coordenadas)
        Retorno = CALC3D_ProductoVectorial(V1, V2).VectorUnitario
        Return Retorno
    End Function

    ''' <summary>
    ''' Recalcula el vector normal de la cara especificada
    ''' </summary>
    ''' <param name="Poliedro">Poliedro al que pretenece la cara</param>
    ''' <param name="IndiceCara">Indice de la cara</param>
    Public Sub CALC3D_CalculoNormalCara(ByRef Poliedro As Poliedro, ByVal IndiceCara As Long)
        Dim V1, V2 As Vector3D

        V1 = New Vector3D(Poliedro.Vertices(Poliedro.Caras(IndiceCara).Vertices(0)).Coordenadas, Poliedro.Vertices(Poliedro.Caras(IndiceCara).Vertices(1)).Coordenadas)
        V2 = New Vector3D(Poliedro.Vertices(Poliedro.Caras(IndiceCara).Vertices(0)).Coordenadas, Poliedro.Vertices(Poliedro.Caras(IndiceCara).Vertices(Poliedro.Caras(IndiceCara).Vertices.GetUpperBound(0))).Coordenadas)
        Poliedro.Caras(IndiceCara).Baricentro.Normal = CALC3D_ProductoVectorial(V1, V2).VectorUnitario
    End Sub

    ''' <summary>
    ''' Obtiene un valor de tipo Boolean que indica si una cara está orientada hacia una cámara concreta
    ''' </summary>
    ''' <param name="Camara"></param>
    Public Function CALC3D_OrientacionCara(ByVal Camara As DatosCamara, ByVal Cara As Cara) As Boolean
        Return (Cara.Baricentro.Normal * Camara.VectorDireccion > 0)
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Punto3D que representa el baricentro del triángulo formado por una terna de puntos
    ''' </summary>
    Public Function CALC3D_Baricentro(ByVal P1 As Punto3D, ByVal P2 As Punto3D, ByVal P3 As Punto3D) As Punto3D
        Dim Retorno As Punto3D
        Retorno.X = (P1.X + P2.X + P3.X) / 3
        Retorno.Y = (P1.Y + P2.Y + P3.Y) / 3
        Retorno.Z = (P1.Z + P2.Z + P3.Z) / 3

        Return Retorno
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Punto3D que representa el baricentro de una cara concreta
    ''' </summary>
    ''' <param name="Poliedro">Poliedro al que pretenece la cara</param>
    Public Function CALC3D_BaricentroCara(ByVal Poliedro As Poliedro, ByVal Cara As Cara)
        Dim Poligono(Cara.Vertices.GetUpperBound(0)) As Punto3D

        For i As Long = 0 To Cara.Vertices.GetUpperBound(0)
            Poligono(i) = Poliedro.Vertices(Cara.Vertices(i)).Coordenadas
        Next

        Return CALC3D_Baricentro(Poligono)
    End Function

    ''' <summary>
    ''' Recalcula el baricentro de una cara concreta
    ''' </summary>
    ''' <param name="Poliedro">Poliedro al que pretenece la cara</param>
    ''' <param name="IndiceCara">Indice de la cara</param>
    Public Sub CALC3D_BaricentroCara(ByRef Poliedro As Poliedro, ByVal IndiceCara As Long)
        Dim Poligono(Poliedro.Caras(IndiceCara).Vertices.GetUpperBound(0)) As Punto3D

        For i As Long = 0 To Poliedro.Caras(IndiceCara).Vertices.GetUpperBound(0)
            Poligono(i) = Poliedro.Vertices(Poliedro.Caras(IndiceCara).Vertices(i)).Coordenadas
        Next

        Poliedro.Caras(IndiceCara).Baricentro.Coordenadas = CALC3D_Baricentro(Poligono)
    End Sub

    ''' <summary>
    ''' Obtiene un valor de tipo Punto3D que representa el baricentro de un polígono
    ''' </summary>
    ''' <param name="Poligono">Matriz de valores Punto3D que representan los vértices del polígono</param>
    Public Function CALC3D_Baricentro(ByVal Poligono() As Punto3D) As Punto3D
        Dim Sumatorio As Punto3D
        Dim Numero As Long = Poligono.GetUpperBound(0) + 1

        Sumatorio = New Punto3D
        For Each Punto As Punto3D In Poligono
            Sumatorio += Punto
        Next

        Return Sumatorio / Numero
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Punto3D que representa un punto simétrico a otro respecto de una recta
    ''' </summary>
    ''' <param name="Recta">Recta que actua como eje de simetría</param>
    Public Function CALC3D_Simetrico(ByVal Recta As Recta3D, ByVal Punto As Punto3D) As Punto3D
        Dim Plano As Plano3D
        Dim M, Retorno As Punto3D

        Plano = New Plano3D(Recta.VectorDirector, Punto)
        M = CALC3D_InterseccionPlanoRecta(Plano, Recta)

        Retorno.X = (2 * M.X) - Punto.X
        Retorno.Y = (2 * M.Y) - Punto.Y
        Retorno.Z = (2 * M.Z) - Punto.Z

        Return Retorno
    End Function
#End Region
#Region "Calculo Espacio 2D"
    ''' <summary>
    ''' Comprueba si un punto está contenido dentro del área de un polígono mediante el algoritmo radial
    ''' </summary>
    ''' <param name="Punto">Punto a comprobar</param>
    ''' <param name="Poligono">Matriz da valores Point que representan los vértices del polígono</param>
    Public Function CALC2D_PuntoDentroPoligono2(ByVal Punto As Point, ByVal Poligono() As Point) As Boolean
        Dim Angulos As Integer
        Dim Angulo, AnguloTotal As Single
        Dim V1, V2 As Vector2D

        Angulo = 0
        AnguloTotal = 0
        Angulos = 0

        For i As Integer = 0 To Poligono.GetUpperBound(0)
            V1 = New Vector2D(Punto, Poligono(i))
            If i < Poligono.GetUpperBound(0) Then
                V2 = New Vector2D(Punto, Poligono(i + 1))
            Else
                V2 = New Vector2D(Punto, Poligono(0))
            End If

            Angulo = CALC2D_AnguloVectores(V1, V2, False)
            AnguloTotal += Angulo
            'g.DrawLine(Pens.Red, Punto, Puntos(i))

            Angulos += 1

            'sys.DEBUG_SendText("Vector p-p(" & i & "): " & V1.ToString & vbNewLine)
            'sys.DEBUG_SendText("Vector p-p(" & i + 1 & "): " & V2.ToString & vbNewLine)
            'sys.DEBUG_SendText("Angulo: " & Angulo & " radianes" & vbNewLine)
            'sys.DEBUG_SendText("Angulo total: " & AnguloTotal & " radianes" & vbNewLine & vbNewLine)
        Next

        Select Case AnguloTotal
            Case Is >= 2 * Math.PI Or (2 * Math.PI - AnguloTotal) <= 0
                'sys.DEBUG_SendText("Punto dentro de poligono" & "(AnguloTotal: " & AnguloTotal & ")  [" & Angulos & "]" & vbNewLine, EGA_Color.Green)
                Return True
            Case Else
                'sys.DEBUG_SendText("Punto fuera de poligono" & "(AnguloTotal: " & AnguloTotal & ")  [" & Angulos & "]" & vbNewLine, EGA_Color.Blue)
                Return False
        End Select
        'sys.DEBUG_SendText(vbNewLine)
    End Function

    ''' <summary>
    ''' Comprueba si un punto está contenido dentro del área de un polígono mediante la comparación de envolventes
    ''' </summary>
    ''' <param name="Punto">Punto a comprobar</param>
    ''' <param name="Poligono">Matriz da valores Point que representan los vértices del polígono</param>
    Public Function CALC2D_PuntoDentroPoligono(ByVal Punto As Point, ByVal Poligono() As Point) As Boolean
        Dim HULL1(), HULL2() As Point
        Dim Cloud(Poligono.GetUpperBound(0) + 1) As Point

        For i As Integer = 0 To Poligono.GetUpperBound(0)
            Cloud(i) = Poligono(i)
        Next
        Cloud(Cloud.GetUpperBound(0)) = Punto

        HULL1 = CALC2D_EnvolventeGraham(Cloud)
        HULL2 = CALC2D_EnvolventeGraham(Poligono)

        If HULL1.GetUpperBound(0) <> HULL2.GetUpperBound(0) Then Return False

        For i As Integer = 0 To HULL1.GetUpperBound(0)
            If HULL1(i) <> HULL2(i) Then Return False
        Next
        Return True
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Single que representa el ángulo (en grados) formado por dos vectores
    ''' </summary>
    ''' <param name="V1">Primer vector</param>
    ''' <param name="V2">Segundo vector</param>
    Public Function CALC2D_AnguloVectores(ByVal V1 As Vector2D, ByVal V2 As Vector2D, ByVal EnGrados As Boolean) As Single
        Dim Retorno As Single

        Retorno = Math.Acos(CALC2D_ProductoEscalar(V1, V2) / (V1.Modulo * V2.Modulo))
        If Retorno > Math.PI Then Retorno -= Math.PI
        If EnGrados Then Retorno = 180 * Retorno / Math.PI

        Return Retorno
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Vector2D que representa un vector perpendicular al vector especificado
    ''' </summary>
    ''' <param name="Vector"></param>
    Public Function CALC2D_VectorNormal(ByVal Vector As Vector2D) As Vector2D
        Return New Vector2D(-Vector.Y, Vector.X)
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Double que representa el producto escalar de dos vectores
    ''' </summary>
    ''' <param name="V1">Primer vector</param>
    ''' <param name="V2">Segundo vector</param>
    Public Function CALC2D_ProductoEscalar(ByVal V1 As Vector2D, ByVal V2 As Vector2D) As Double
        Return (V1.X * V2.X) + (V1.Y * V2.Y)
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Point que representa la intersección de dos rectas
    ''' </summary>
    ''' <param name="r">Primera recta</param>
    ''' <param name="s">Segunda recta</param>
    Public Function CALC2D_InterseccionRectas(ByVal r As Recta2D, ByVal s As Recta2D) As Point
        Dim X, Y As Long
        Dim a, aa, b, bb, c, cc As Double
        a = r.A
        aa = s.A
        b = r.B
        bb = s.B
        c = r.C
        cc = s.C

        X = (((bb / b) * c) - cc) / ((a * (bb / -b)) + a)
        Y = ((a * X) + c) / (-b)

        Return New Point(X, Y)
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Double que representa la distancia entre un punto y una recta
    ''' </summary>
    ''' <param name="Punto"></param>
    ''' <param name="Recta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CALC2D_DistanciaPuntoRecta(ByVal Punto As Point, ByVal Recta As Recta2D) As Double
        Dim P As Point
        Dim R As Recta2D

        R = New Recta2D(CALC2D_VectorNormal(Recta.VectorDirector), Punto)
        P = CALC2D_InterseccionRectas(Recta, R)
        Return New Vector2D(Punto, P).Modulo
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Double que representa la distancia del punto más alejado de una terna de puntos a otro punto determiado
    ''' </summary>
    ''' <param name="PuntoOrigen">Punto desde el cual se mide la distancia</param>
    Public Function CALC2D_MaximaDistancia(ByVal PuntoOrigen As Point, ByVal P1 As Point, ByVal P2 As Point, ByVal P3 As Point) As Point
        Dim Indice As Integer
        Dim ModMax As Double = 0
        Dim M(2) As Point
        M(0) = P1
        M(1) = P2
        M(2) = P3

        For i As Integer = 0 To 2
            If New Vector2D(PuntoOrigen, M(i)).Modulo > ModMax Then
                ModMax = New Vector2D(PuntoOrigen, M(i)).Modulo
                Indice = i
            End If
        Next

        Return M(Indice)

    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Integer que representa el producto cruzado de una terna de puntos
    ''' </summary>
    ''' <param name="P1"></param>
    ''' <param name="P2"></param>
    ''' <param name="P3"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CALC2D_Cross(ByVal P1 As Point, ByVal P2 As Point, ByVal P3 As Point) As Integer
        Dim Retorno As Double
        Dim V1, V2 As Vector2D
        V1 = New Vector2D(P1, P2).VectorUnitario
        V2 = New Vector2D(P2, P3).VectorUnitario
        Try
            Retorno = V1.X * V2.Y - V1.Y * V2.X

            Select Case Retorno
                Case Is < Integer.MinValue
                    Return Integer.MinValue
                Case Integer.MinValue To Integer.MaxValue
                    Return CInt(Retorno)
                Case Is > Integer.MaxValue
                    Return Integer.MaxValue
            End Select
        Catch ex As OverflowException
            Return 0
        End Try

    End Function

    ''' <summary>
    ''' Obtiene una matriz de tipo Point que representa la envolvente convexa de una nube de puntos,calculada mediante el algoritmo de Graham
    ''' </summary>
    ''' <param name="NubePuntos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CALC2D_EnvolventeGraham(ByVal NubePuntos() As Point) As Point()
        Dim Envolvente As New List(Of Point)
        Dim CopiaNube As New List(Of Point)
        Dim MatrizCopiaNube(NubePuntos.GetUpperBound(0)) As Point
        Dim V1, V2, DireccionComparacion As Vector2D
        Dim PMenor As Point
        Dim CopiaPunto As Point

        CopiaNube.Clear()
        For Each P As Point In NubePuntos
            CopiaNube.Add(P)
        Next


        'IMPLEMENTACION DEL ALGORITMO DE GRAHAM DE CALCULO DE LA ENVOLVENTE DE UNA NUBE DE PUNTOS:
        '-----------------------------------------------------------------------------------------

        'Toma como referencia una linea horizontal:
        DireccionComparacion = New Vector2D(1, 0)
        PMenor = CopiaNube(0)

        'Busca el punto de menor cota (Si existen varios,elige el de menor ordenada):
        For i As Integer = 0 To CopiaNube.Count - 1
            If CopiaNube(i).Y > PMenor.Y Then PMenor = CopiaNube(i)
            If CopiaNube(i).Y = PMenor.Y AndAlso CopiaNube(i).X < PMenor.X Then PMenor = CopiaNube(i)
            'Form1.Sys.DEBUG_SendText("Cota actual: " & CopiaNube(i).Y & " Cota minima: " & PMenor.Y & vbNewLine, dx_lib32.EGA_Color.Silver)
        Next

        'PMenor = CopiaNube(0)

        'Form1.Sys.DEBUG_SendText("Punto menor cota: " & PMenor.ToString & vbNewLine, dx_lib32.EGA_Color.Silver)

        'Situa el punto de menor cota (PMnenor) al principio de la pila:
        CopiaNube.Remove(PMenor)
        CopiaNube.Insert(0, PMenor)

        'Busca puntos con el mismo angulo de referencia y elimina todos menos el mas alejado de PMenor:
        For i As Integer = 1 To NubePuntos.GetUpperBound(0)
            V1 = New Vector2D(PMenor, NubePuntos(i))
            For j As Integer = 1 To NubePuntos.GetUpperBound(0)
                V2 = New Vector2D(PMenor, NubePuntos(j))
                If CALC2D_AnguloVectores(DireccionComparacion, V1, True) = CALC2D_AnguloVectores(DireccionComparacion, V2, True) Then
                    If V1.Modulo > V2.Modulo Then
                        CopiaNube.Remove(NubePuntos(j))
                        'Form1.Sys.DEBUG_SendText("Eliminado: " & NubePuntos(j).ToString & vbNewLine, dx_lib32.EGA_Color.Silver)
                    End If
                End If
            Next
        Next

        ''MODIFICACION PROPIA:
        ''Busca puntos alineados:
        'Dim CopiaCopia As New List(Of Point)
        'For i As Integer = 1 To CopiaNube.Count - 1
        '    For j As Integer = 1 To CopiaNube.Count - 1
        '        For k As Integer = 1 To CopiaNube.Count - 1
        '            If Cross(CopiaNube(i), CopiaNube(j), CopiaNube(k)) = 0 Then
        '                CopiaCopia.Add(MaximaDistancia(PMenor, CopiaNube(i), CopiaNube(1), CopiaNube(2)))
        '            End If
        '        Next
        '    Next
        'Next

        MatrizCopiaNube = CopiaNube.ToArray

        'Metodo de la burbuja para ordenar la matriz segun el angulo de referencia de cada punto:
        For i As Integer = 1 To MatrizCopiaNube.GetUpperBound(0)
            For j As Integer = 1 To MatrizCopiaNube.GetUpperBound(0)
                V1 = New Vector2D(PMenor, MatrizCopiaNube(i))
                V2 = New Vector2D(PMenor, MatrizCopiaNube(j))

                If CALC2D_AnguloVectores(V1, DireccionComparacion, True) < CALC2D_AnguloVectores(V2, DireccionComparacion, True) Then
                    CopiaPunto = MatrizCopiaNube(i)
                    MatrizCopiaNube(i) = MatrizCopiaNube(j)
                    MatrizCopiaNube(j) = CopiaPunto
                    'Form1.Sys.DEBUG_SendText("Ordenando... Intercambiado " & MatrizCopiaNube(i).ToString & " con " & MatrizCopiaNube(j).ToString & vbNewLine, dx_lib32.EGA_Color.Silver)
                End If
            Next
        Next
        Envolvente.AddRange(MatrizCopiaNube.ToList)
        'Se añaden los tres primeros puntos a la pila solucion (PMenor,1,2):
        'Envolvente.Add(PMenor)
        'Envolvente.Add(MatrizCopiaNube(1))
        'Envolvente.Add(MatrizCopiaNube(2))


        'Test de Graham:
        For i As Integer = 2 To MatrizCopiaNube.GetUpperBound(0)
            'Form1.Sys.DEBUG_SendText("Analizando P(" & i & ") [" & MatrizCopiaNube(i).ToString & "]", dx_lib32.EGA_Color.Silver)
            If CALC2D_Cross(Envolvente(Envolvente.Count - 2), Envolvente(Envolvente.Count - 1), MatrizCopiaNube(i)) >= 0 Then
                Envolvente.RemoveAt(Envolvente.Count - 1)
                'Form1.Sys.DEBUG_SendText("Eliminado ultimo elemento de la pila(" & Envolvente(Envolvente.Count - 1).ToString & ") " & PMenor.ToString, dx_lib32.EGA_Color.Silver)
            End If

            'Envolvente.Add(MatrizCopiaNube(i))
        Next

        'For i As Integer = Envolvente.Count - 1 To 2 Step -1
        '    If Cross(Envolvente(i), Envolvente(i - 1), Envolvente(i - 2)) < 0 Then
        '        Envolvente.RemoveAt(i - 1)
        '    End If

        'Next


        'Envolvente.Add(PMenor)
        'Form1.Sys.DEBUG_SendText("------------------------------------------------------------------------" & vbNewLine, dx_lib32.EGA_Color.Silver)
        Return Envolvente.ToArray

    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Point que representa el baricentro de un polígono
    ''' </summary>
    ''' <param name="Poligono"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CALC2D_Baricentro(ByVal Poligono() As Point) As Point
        Dim Sumatorio As Point
        Dim Numero As Long = Poligono.GetUpperBound(0) + 1

        Sumatorio = New Point
        For Each Punto As Point In Poligono
            Sumatorio += Punto
        Next

        Return New Point(Sumatorio.X / Numero, Sumatorio.Y / Numero)
    End Function
#End Region

#Region "Calculo Matrices"
    ''' <summary>
    ''' Obtiene un valor de tipo Double que representa el adjunto de un elemento específico de una matriz
    ''' </summary>
    ''' <param name="Matriz"></param>
    ''' <param name="Elemento">Índices del elemento (Fila,Columna)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CALCMATR_Adjunto(ByVal Matriz(,) As Double, ByVal Elemento As Point) As Double
        Dim Signo As Integer
        Dim ElementoAdjunto As Point
        Dim MatrizAdjunto(Matriz.GetUpperBound(0) - 1, Matriz.GetUpperBound(1) - 1) As Double

        Signo = (-1) ^ ((Elemento.X + 1) + (Elemento.Y + 1))
        For i As Integer = 0 To Matriz.GetUpperBound(0)
            For j As Integer = 0 To Matriz.GetUpperBound(1)
                If i = Elemento.X Or j = Elemento.Y Then Continue For
                ElementoAdjunto = New Point(i, j)
                If i > Elemento.X Then ElementoAdjunto.X -= 1
                If j > Elemento.Y Then ElementoAdjunto.Y -= 1

                MatrizAdjunto(ElementoAdjunto.X, ElementoAdjunto.Y) = Matriz(i, j)
            Next
        Next
        'MsgBox(Signo & "  " & Matriz(Elemento.X, Elemento.Y) * Determinante(MatrizAdjunto))
        Return Signo * CALCMATR_Determinante(MatrizAdjunto)
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Integer que representa el rango de una matriz
    ''' </summary>
    ''' <param name="Matriz"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CALCMATR_Rango(ByVal Matriz(,) As Double) As Integer
        Dim m As Integer
        Dim Nulos As Integer = 0
        For i As Integer = 0 To Matriz.GetUpperBound(0)
            For j As Integer = 0 To Matriz.GetUpperBound(1)
                m = CALCMATR_Adjunto(Matriz, New Point(i, j)) * Matriz(i, j)
                If m = 0 Then Nulos += 1
            Next
        Next
        If Matriz.GetUpperBound(0) * Matriz.GetUpperBound(1) = Nulos Then

        End If
    End Function

    ''' <summary>
    ''' Obtiene un valor de tipo Double que representa el determinante de la matriz especificada
    ''' </summary>
    Public Function CALCMATR_Determinante(ByVal Matriz(,) As Double) As Double
        Dim Positivos, Negativos As Double

        If Matriz.GetUpperBound(0) <> Matriz.GetUpperBound(1) Then
            Return -1
        End If

        Select Case Matriz.GetUpperBound(0)
            Case 0
                Return Matriz(0, 0)
            Case 1
                Positivos = Matriz(0, 0) * Matriz(1, 1)
                Negativos = Matriz(0, 1) * Matriz(1, 0)
                'MsgBox(Positivos & vbNewLine & Negativos & vbNewLine & Positivos - Negativos)
                Return Positivos - Negativos
            Case 2
                Positivos = (Matriz(0, 0) * Matriz(1, 1) * Matriz(2, 2)) + (Matriz(0, 1) * Matriz(1, 2) * Matriz(2, 0)) + (Matriz(1, 0) * Matriz(2, 1) * Matriz(0, 2))
                Negativos = (Matriz(0, 2) * Matriz(1, 1) * Matriz(2, 0)) + (Matriz(1, 0) * Matriz(0, 1) * Matriz(2, 2)) + (Matriz(0, 0) * Matriz(1, 2) * Matriz(2, 1))
                Return Positivos - Negativos
            Case Else
                Dim Det As Double = 0
                For i As Integer = 0 To Matriz.GetUpperBound(0)
                    Det += CALCMATR_Adjunto(Matriz, New Point(i, 0)) * Matriz(i, 0)
                Next
                Return Det
        End Select


    End Function

    Public Function CALCMATR_ProductoMatricial(ByVal A(,) As Double, ByVal B(,) As Double) As Double(,)
        Dim Retorno(A.GetUpperBound(1), A.GetUpperBound(1)) As Double
        Dim ValorElemento As Double

        If A.GetUpperBound(1) <> B.GetUpperBound(0) Then
            Retorno = CALCMATR_MatrizUnitaria(A.GetUpperBound(1) + 1)
            Return Retorno
        End If

        For i As Integer = 0 To A.GetUpperBound(0)
            For j As Integer = 0 To B.GetUpperBound(1)
                ValorElemento = 0
                For k As Integer = 0 To A.GetUpperBound(1)
                    ValorElemento += A(i, k) * B(k, j)
                Next
                Retorno(i, j) = ValorElemento
            Next
        Next

        Return Retorno
    End Function

    Public Function CALCMATR_ProductoMatricial(ByVal Matriz(,) As Double, ByVal Factor As Double) As Double(,)
        Dim Retorno(,) As Double
        ReDim Retorno(Matriz.GetUpperBound(0), Matriz.GetUpperBound(1))

        For i As Integer = 0 To Matriz.GetUpperBound(0)
            For j As Integer = 0 To Matriz.GetUpperBound(1)
                Retorno(i, j) = Matriz(i, j) * Factor
            Next
        Next

        Return Retorno
    End Function

    Public Function CALCMATR_MatrizUnitaria(ByVal Dimensiones As Integer) As Double(,)
        Dim Retorno(Dimensiones - 1, Dimensiones - 1) As Double

        For i As Integer = 0 To Dimensiones - 1
            For j As Integer = 0 To Dimensiones - 1
                If i = j Then
                    Retorno(i, j) = 1
                Else
                    Retorno(i, j) = 0
                End If
            Next
        Next

        'Retorno(0, 0) = 1
        'Retorno(0, 1) = 0
        'Retorno(0, 2) = 0
        'Retorno(0, 3) = 0

        'Retorno(1, 0) = 0
        'Retorno(1, 1) = 1
        'Retorno(1, 2) = 0
        'Retorno(1, 3) = 0

        'Retorno(2, 0) = 0
        'Retorno(2, 1) = 0
        'Retorno(2, 2) = 1
        'Retorno(2, 3) = 0

        'Retorno(3, 0) = 0
        'Retorno(3, 1) = 0
        'Retorno(3, 2) = 0
        'Retorno(3, 3) = 1

        Return Retorno
    End Function

    Public Function CALCMATR_MatrizTraspuesta(ByVal Matriz(,) As Double) As Double(,)
        Dim Retorno(,) As Double
        ReDim Retorno(Matriz.GetUpperBound(1), Matriz.GetUpperBound(0))

        For i As Integer = 0 To Matriz.GetUpperBound(0)
            For j As Integer = 0 To Matriz.GetUpperBound(1)
                Retorno(j, i) = Matriz(i, j)
            Next
        Next

        Return Retorno
    End Function

    Public Function CALCMATR_MatrizAdjunta(ByVal Matriz(,) As Double) As Double(,)
        'Dim Sys As New dx_lib32.dx_System_Class

        Dim Signo As Integer
        Dim ElementoAdjunto As Point
        Dim MatrizAdjunto(Matriz.GetUpperBound(0) - 1, Matriz.GetUpperBound(1) - 1) As Double
        Dim Retorno(Matriz.GetUpperBound(0), Matriz.GetUpperBound(1)) As Double

        For a As Integer = 0 To Matriz.GetUpperBound(0)
            For b As Integer = 0 To Matriz.GetUpperBound(1)
                Signo = (-1) ^ ((a + 1) + (b + 1))
                For i As Integer = 0 To Matriz.GetUpperBound(0)
                    For j As Integer = 0 To Matriz.GetUpperBound(1)
                        If i = a Or j = b Then Continue For
                        ElementoAdjunto = New Point(i, j)
                        If i > a Then ElementoAdjunto.X -= 1
                        If j > b Then ElementoAdjunto.Y -= 1

                        MatrizAdjunto(ElementoAdjunto.X, ElementoAdjunto.Y) = Matriz(i, j)
                    Next
                Next
                'Form1.Sys.DEBUG_SendText("Matriz adjunta de Matriz(" & a & "," & b & "):" & vbNewLine & MatrizToString(MatrizAdjunto, 0) & vbNewLine)
                'Form1.Sys.DEBUG_SendText("Signo: " & Signo & vbNewLine)
                'Form1.Sys.DEBUG_SendText("Determinante: " & Determinante(MatrizAdjunto) & vbNewLine)
                'Form1.Sys.DEBUG_SendText("Retorno(" & a & "," & b & "): " & Signo * Determinante(MatrizAdjunto) & vbNewLine & vbNewLine)
                Retorno(a, b) = Signo * CALCMATR_Determinante(MatrizAdjunto)
            Next
        Next
        Return Retorno
    End Function

    Public Function CALCMATR_MatrizInversa(ByVal Matriz(,) As Double) As Double(,)
        Dim Traspuesta(Matriz.GetUpperBound(0), Matriz.GetUpperBound(1)), AdjuntaTraspuesta(Matriz.GetUpperBound(0), Matriz.GetUpperBound(1)) As Double
        If CALCMATR_Determinante(Matriz) = 0 Or Matriz.GetUpperBound(0) <> Matriz.GetUpperBound(1) Then
            Return CALCMATR_MatrizUnitaria(Matriz.GetUpperBound(0) + 1)
        End If


        Traspuesta = CALCMATR_MatrizTraspuesta(Matriz)
        AdjuntaTraspuesta = CALCMATR_MatrizAdjunta(Traspuesta)

        Return CALCMATR_ProductoMatricial(AdjuntaTraspuesta, 1 / CALCMATR_Determinante(Matriz))
    End Function

    Public Function CALCMATR_MatrizToString(ByVal Matriz(,) As Double, ByVal NumeroDecimales As Integer) As String
        Dim Retorno As String
        Retorno = "Matriz de " & Matriz.GetUpperBound(0) + 1 & "x" & Matriz.GetUpperBound(1) + 1 & ": " & vbNewLine
        For i As Integer = 0 To Matriz.GetUpperBound(0)
            For j As Integer = 0 To Matriz.GetUpperBound(1)
                Retorno &= FormatNumber(Matriz(i, j), NumeroDecimales) & " "
            Next
            Retorno &= vbNewLine
        Next
        Return Retorno
    End Function

    Public Function CALCMATR_MatrizToString(ByVal Matriz(,) As Double, ByVal NumeroDecimales As Integer, ByVal Tabulacion As Integer) As String
        Dim Retorno As String
        Dim Tab As String = StrDup(Tabulacion, " ")

        Retorno = Tab & "Matriz de " & Matriz.GetUpperBound(0) + 1 & "x" & Matriz.GetUpperBound(1) + 1 & ": " & vbNewLine
        For i As Integer = 0 To Matriz.GetUpperBound(0)
            Retorno &= Tab
            For j As Integer = 0 To Matriz.GetUpperBound(1)
                Retorno &= FormatNumber(Matriz(i, j), NumeroDecimales) & " "
            Next
            Retorno &= vbNewLine
        Next
        Return Retorno
    End Function

    Public Function CALCMATR_MatrizToString(ByVal Matriz() As Double, ByVal NumeroDecimales As Integer) As String
        Dim Retorno As String
        Retorno = "Matriz columna de " & Matriz.GetUpperBound(0) + 1 & "x0" & ": " & vbNewLine
        For i As Integer = 0 To Matriz.GetUpperBound(0)
            Retorno &= FormatNumber(Matriz(i), NumeroDecimales) & " "
            Retorno &= vbNewLine
        Next
        Return Retorno
    End Function

    Public Function CALCMATR_MatrizToString(ByVal Matriz() As Double, ByVal NumeroDecimales As Integer, ByVal Tabulacion As Integer) As String
        Dim Retorno As String
        Dim Tab As String = StrDup(Tabulacion, " ")

        Retorno = Tab & "Matriz columna de " & Matriz.GetUpperBound(0) + 1 & "x0" & ": " & vbNewLine
        For i As Integer = 0 To Matriz.GetUpperBound(0)
            Retorno &= Tab
            Retorno &= FormatNumber(Matriz(i), NumeroDecimales) & " "
            Retorno &= vbNewLine
        Next
        Return Retorno
    End Function
#End Region
#Region "Calculo Transformaciones 3D"
    Public Function CALCTRANS3D_TransformarPunto(ByVal Punto As Punto3D, ByVal MatrizTransformacion(,) As Double) As Punto3D
        Dim MatrizResultante(3) As Double
        Dim ValorElemento As Double

        If MatrizTransformacion Is Nothing Then Return Punto

        For i As Integer = 0 To 3
            ValorElemento = 0
            For j As Integer = 0 To 3
                ValorElemento += MatrizTransformacion(i, j) * Punto.RepresentacionMatricial(j)
            Next
            MatrizResultante(i) = ValorElemento
        Next

        Return New Punto3D(MatrizResultante(0), MatrizResultante(1), MatrizResultante(2))
    End Function

    Public Function CALCTRANS3D_TraslacionOrigen(ByVal TraslacionX As Double, ByVal TraslacionY As Double, ByVal TraslacionZ As Double) As Double(,)
        Dim Retorno(3, 3) As Double

        '--> La matriz de traslación es la siguiente:
        '
        ' |1  0  0  -TraslaciónX|
        ' |0  1  0  -TraslaciónY|
        ' |0  0  1  -TraslaciónZ|
        ' |0  0  0       1      |


        Retorno(0, 0) = 1
        Retorno(0, 1) = 0
        Retorno(0, 2) = 0
        Retorno(0, 3) = -TraslacionX

        Retorno(1, 0) = 0
        Retorno(1, 1) = 1
        Retorno(1, 2) = 0
        Retorno(1, 3) = -TraslacionY

        Retorno(2, 0) = 0
        Retorno(2, 1) = 0
        Retorno(2, 2) = 1
        Retorno(2, 3) = -TraslacionZ

        Retorno(3, 0) = 0
        Retorno(3, 1) = 0
        Retorno(3, 2) = 0
        Retorno(3, 3) = 1

        Return Retorno
    End Function

    Public Function CALCTRANS3D_Traslacion(ByVal TraslacionX As Double, ByVal TraslacionY As Double, ByVal TraslacionZ As Double) As Double(,)
        Dim Retorno(3, 3) As Double

        '--> La matriz de traslación es la siguiente:
        '
        ' |1  0  0  TraslaciónX|
        ' |0  1  0  TraslaciónY|
        ' |0  0  1  TraslaciónZ|
        ' |0  0  0       1      |


        Retorno(0, 0) = 1
        Retorno(0, 1) = 0
        Retorno(0, 2) = 0
        Retorno(0, 3) = TraslacionX

        Retorno(1, 0) = 0
        Retorno(1, 1) = 1
        Retorno(1, 2) = 0
        Retorno(1, 3) = TraslacionY

        Retorno(2, 0) = 0
        Retorno(2, 1) = 0
        Retorno(2, 2) = 1
        Retorno(2, 3) = TraslacionZ

        Retorno(3, 0) = 0
        Retorno(3, 1) = 0
        Retorno(3, 2) = 0
        Retorno(3, 3) = 1

        Return Retorno
    End Function

    Public Function CALCTRANS3D_CambioEscala(ByVal EscalaX As Double, ByVal EscalaY As Double, ByVal EscalaZ As Double) As Double(,)
        Dim Retorno(3, 3) As Double

        '--> La matriz de escalado es la siguiente:
        '
        ' |EscalaX  0  0  0|
        ' |0  EscalaY  0  0|
        ' |0  0  EscalaZ  0|
        ' |0    0    0    1|



        Retorno(0, 0) = EscalaX
        Retorno(0, 1) = 0
        Retorno(0, 2) = 0
        Retorno(0, 3) = 0

        Retorno(1, 0) = 0
        Retorno(1, 1) = EscalaY
        Retorno(1, 2) = 0
        Retorno(1, 3) = 0

        Retorno(2, 0) = 0
        Retorno(2, 1) = 0
        Retorno(2, 2) = EscalaZ
        Retorno(2, 3) = 0

        Retorno(3, 0) = 0
        Retorno(3, 1) = 0
        Retorno(3, 2) = 0
        Retorno(3, 3) = 1

        Return Retorno
    End Function

    Public Function CALCTRANS3D_RotacionAlrededorEje(ByVal Eje As EnumEje, ByVal Angulo As Single) As Double(,)
        Dim Retorno(3, 3) As Double
        Dim AnguloEnRadianes As Single

        AnguloEnRadianes = PI * Angulo / 180

        Select Case Eje
            Case EnumEje.EjeZ
                '--> La matriz de rotación es la siguiente:
                '
                ' | Cos(AnguloEnRadianes)  Sen(AnguloEnRadianes)  0  0|
                ' |-Sen(AnguloEnRadianes)  Cos(AnguloEnRadianes)  0  0|
                ' | 0                0                1              0|
                ' | 0                0                0              1|



                Retorno(0, 0) = Cos(AnguloEnRadianes)
                Retorno(0, 1) = Sin(AnguloEnRadianes)
                Retorno(0, 2) = 0
                Retorno(0, 3) = 0

                Retorno(1, 0) = -Sin(AnguloEnRadianes)
                Retorno(1, 1) = Cos(AnguloEnRadianes)
                Retorno(1, 2) = 0
                Retorno(1, 3) = 0

                Retorno(2, 0) = 0
                Retorno(2, 1) = 0
                Retorno(2, 2) = 1
                Retorno(2, 3) = 0

                Retorno(3, 0) = 0
                Retorno(3, 1) = 0
                Retorno(3, 2) = 0
                Retorno(3, 3) = 1

                Return Retorno

            Case EnumEje.EjeY
                '--> La matriz de rotación es la siguiente:
                '
                ' |Cos(AnguloEnRadianes)  0  -Sen(AnguloEnRadianes)  0|
                ' |0                1                0               0|
                ' |Sen(AnguloEnRadianes)  0  Cos(AnguloEnRadianes)   0|
                ' |0                0                0               1|



                Retorno(0, 0) = Cos(AnguloEnRadianes)
                Retorno(0, 1) = 0
                Retorno(0, 2) = -Sin(AnguloEnRadianes)
                Retorno(0, 3) = 0

                Retorno(1, 0) = 0
                Retorno(1, 1) = 1
                Retorno(1, 2) = 0
                Retorno(1, 3) = 0

                Retorno(2, 0) = Sin(AnguloEnRadianes)
                Retorno(2, 1) = 0
                Retorno(2, 2) = Cos(AnguloEnRadianes)
                Retorno(2, 3) = 0

                Retorno(3, 0) = 0
                Retorno(3, 1) = 0
                Retorno(3, 2) = 0
                Retorno(3, 3) = 1

                Return Retorno

            Case EnumEje.EjeX
                '--> La matriz de rotación es la siguiente:
                '
                ' |1                0                0              0|
                ' |0  Cos(AnguloEnRadianes)  Sen(AnguloEnRadianes)  0|
                ' |0  -Sen(AnguloEnRadianes) Cos(AnguloEnRadianes)  0|
                ' |0                0                0              1|



                Retorno(0, 0) = 1
                Retorno(0, 1) = 0
                Retorno(0, 2) = 0
                Retorno(0, 3) = 0

                Retorno(1, 0) = 0
                Retorno(1, 1) = Cos(AnguloEnRadianes)
                Retorno(1, 2) = Sin(AnguloEnRadianes)
                Retorno(1, 3) = 0

                Retorno(2, 0) = 0
                Retorno(2, 1) = -Sin(AnguloEnRadianes)
                Retorno(2, 2) = Cos(AnguloEnRadianes)
                Retorno(2, 3) = 0

                Retorno(3, 0) = 0
                Retorno(3, 1) = 0
                Retorno(3, 2) = 0
                Retorno(3, 3) = 1

                Return Retorno

        End Select
    End Function

    Public Function CALCTRANS3D_RotacionDesdeCentro(ByVal Eje As EnumEje, ByVal Centro As Punto3D, ByVal Angulo As Single) As Double(,)
        Dim CopiaCentro As Punto3D = Centro

        Return CALCMATR_ProductoMatricial(CALCTRANS3D_Traslacion(-CopiaCentro.X, -CopiaCentro.Y, -CopiaCentro.Z), CALCMATR_ProductoMatricial(CALCTRANS3D_Traslacion(CopiaCentro.X, CopiaCentro.Y, CopiaCentro.Z), CALCTRANS3D_RotacionAlrededorEje(Eje, Angulo)))
    End Function
#End Region

#Region "Calculo Representaciones"

    Public Function CALCREP_EstablecerRepresentacion(ByVal Camara As DatosCamara, ByVal TipoRepresentacion As TipoRepresentacion) As DatosCamara
        Select Case TipoRepresentacion
            Case TipoRepresentacion.Isometrica
                Camara.EstablecerDistancia(1000000)
                Camara.EstablecerPosicion(New Punto3D(1000000, 1000000, 1000000))
                Camara.EstablecerVectorDireccion(New Vector3D(-1, -1, -1))
            Case TipoRepresentacion.OrtogonalZOX
                Camara.EstablecerDistancia(1000000)
                Camara.EstablecerPosicion(New Punto3D(0, 10000000000, 0))
                Camara.EstablecerVectorDireccion(New Vector3D(0, -1, 0))
            Case TipoRepresentacion.OrtogonalZOY
                Camara.EstablecerDistancia(1000000)
                Camara.EstablecerPosicion(New Punto3D(10000000000, 0, 0))
                Camara.EstablecerVectorDireccion(New Vector3D(-1, 0, 0))
            Case TipoRepresentacion.OrtogonalXOY
                Camara.EstablecerDistancia(1000000)
                Camara.EstablecerPosicion(New Punto3D(0, 0, 10000000000))
                Camara.EstablecerVectorDireccion(New Vector3D(0, 0, -1))
        End Select
        Camara.EstablecerRepresentacion(TipoRepresentacion)

        Return Camara
    End Function


    Public Function CALCREP_Representacion(ByVal Camara As DatosCamara, ByVal Punto As Punto3D) As Point
        Select Case Camara.Representacion
            Case TipoRepresentacion.Conica
                Return CALCREP_RepresentacionConica(Camara, Punto)
            Case TipoRepresentacion.Isometrica
                Return CALCREP_RepresentacionIsometrica(Punto)
            Case TipoRepresentacion.OrtogonalGeneral
                Return CALCREP_RepresentacionOrtograficaGeneral(Camara, Punto)
            Case TipoRepresentacion.OrtogonalLibre
                Return CALCREP_RepresentacionOrtograficaLibre(Camara, Punto)
            Case TipoRepresentacion.OrtogonalXOY
                Return CALCREP_RepresentacionOrtogonalXOY(Punto)
            Case TipoRepresentacion.OrtogonalZOX
                Return CALCREP_RepresentacionOrtogonalZOX(Punto)
            Case TipoRepresentacion.OrtogonalZOY
                Return CALCREP_RepresentacionOrtogonalZOY(Punto)
        End Select
    End Function


    Public Function CALCREP_RepresentacionConica(ByVal Camara As DatosCamara, ByVal Punto As Punto3D) As Point
        Dim Interseccion As Punto3D
        Dim Retorno As Point

        'Interseccion = CALC3D_InterseccionPlanoRecta(Camara.PlanoProyeccion, New Recta3D(Punto, Camara.Posicion))
        Interseccion = Punto
        Interseccion = CALCTRANS3D_TransformarPunto(Interseccion, Camara.InversaMatrizTransformacion)
        If Interseccion.X < Integer.MinValue Then Interseccion.X = Integer.MinValue
        If Interseccion.X > Integer.MaxValue Then Interseccion.X = Integer.MaxValue

        If Interseccion.Y < Integer.MinValue Then Interseccion.Y = Integer.MinValue
        If Interseccion.Y > Integer.MaxValue Then Interseccion.Y = Integer.MaxValue

        If Interseccion.Z < Integer.MinValue Then Interseccion.Z = Integer.MinValue
        If Interseccion.Z > Integer.MaxValue Then Interseccion.Z = Integer.MaxValue

        Try
            If Interseccion.Z <> 0 Then
                Retorno = New Point(Camara.Distancia * Interseccion.X / Interseccion.Z, Camara.Distancia * Interseccion.Y / Interseccion.Z)
            Else
                Retorno = New Point(0, 0)
            End If
        Catch ex As OverflowException
            Retorno = New Point(0, 0)
        End Try


        Return Retorno
    End Function

    Public Function CALCREP_RepresentacionOrtogonalXOY(ByVal Punto As Punto3D) As Point
        Return New Point(-Punto.Y, -Punto.X)
    End Function
    Public Function CALCREP_RepresentacionOrtogonalZOX(ByVal Punto As Punto3D) As Point
        Return New Point(Punto.X, -Punto.Z)
    End Function
    Public Function CALCREP_RepresentacionOrtogonalZOY(ByVal Punto As Punto3D) As Point
        Try
            Return New Point(-Punto.Y, -Punto.Z)
        Catch ex As Exception
            Return New Point(0, 0)
        End Try

    End Function

    Public Function CALCREP_RepresentacionOrtograficaGeneral(ByVal Camara As DatosCamara, ByVal Punto As Punto3D) As Point
        Dim r, s As Double
        Dim D(3, 3), E(3, 3) As Double
        Dim x, y, z As Double
        Dim MatrizTransformacion(3, 3) As Double
        Dim PuntoTransformado As Punto3D

        If Camara.Posicion = New Punto3D(0, 0, 0) Then Camara.EstablecerPosicion(New Punto3D(1000, 1000, 1000))

        x = Camara.Posicion.X
        y = Camara.Posicion.Y
        z = Camara.Posicion.Z

        D = CALCMATR_MatrizUnitaria(4)
        E = CALCMATR_MatrizUnitaria(4)
        r = Sqrt((x ^ 2) + (y ^ 2))
        s = Sqrt((x ^ 2) + (y ^ 2) + (z ^ 2))

        D(0, 0) = x / r
        D(0, 1) = y / r
        D(1, 0) = -y / r
        D(1, 1) = x / r

        E(0, 0) = -z / s
        E(0, 2) = r / s
        E(2, 0) = -r / s
        E(2, 2) = -z / s

        MatrizTransformacion = CALCMATR_ProductoMatricial(E, D)
        PuntoTransformado = CALCTRANS3D_TransformarPunto(Punto, MatrizTransformacion)
        'PuntoTransformado.X = -PuntoTransformado.X
        PuntoTransformado.Y = -PuntoTransformado.Y

        If PuntoTransformado.X < Integer.MinValue Then PuntoTransformado.X = Integer.MinValue
        If PuntoTransformado.X > Integer.MaxValue Then PuntoTransformado.X = Integer.MaxValue

        If PuntoTransformado.Y < Integer.MinValue Then PuntoTransformado.Y = Integer.MinValue
        If PuntoTransformado.Y > Integer.MaxValue Then PuntoTransformado.Y = Integer.MaxValue

        If PuntoTransformado.Z < Integer.MinValue Then PuntoTransformado.Z = Integer.MinValue
        If PuntoTransformado.Z > Integer.MaxValue Then PuntoTransformado.Z = Integer.MaxValue

        Return CALCREP_RepresentacionOrtogonalXOY(PuntoTransformado)
    End Function

    Public Function CALCREP_RepresentacionOrtograficaLibre(ByVal Camara As DatosCamara, ByVal Punto As Punto3D) As Point
        Dim Plano As Plano3D
        Dim Recta As Recta3D
        Dim Interseccion As Punto3D

        Recta = New Recta3D(Camara.VectorDireccion, Punto)
        Plano = New Plano3D(Camara.VectorDireccion, Camara.Posicion)

        Interseccion = CALC3D_InterseccionPlanoRecta(Plano, Recta)

        If Interseccion.X < Integer.MinValue Then Interseccion.X = Integer.MinValue
        If Interseccion.X > Integer.MaxValue Then Interseccion.X = Integer.MaxValue

        If Interseccion.Y < Integer.MinValue Then Interseccion.Y = Integer.MinValue
        If Interseccion.Y > Integer.MaxValue Then Interseccion.Y = Integer.MaxValue

        If Interseccion.Z < Integer.MinValue Then Interseccion.Z = Integer.MinValue
        If Interseccion.Z > Integer.MaxValue Then Interseccion.Z = Integer.MaxValue

        Return CALCREP_RepresentacionOrtogonalZOX(Interseccion)
    End Function

    Public Function CALCREP_RepresentacionIsometrica(ByVal ValX As Long, ByVal ValY As Long, ByVal ValZ As Long) As Point
        Dim Retorno As Point

        Retorno.X = ((Sqrt(2) / 2) * (ValX - ValY))
        Retorno.Y = ((Sqrt(2 / 3) + ValZ) - ((1 / Sqrt(6)) * (ValX + ValY)))
        Retorno.X = -Retorno.X + 0
        Retorno.Y = -Retorno.Y + 0
        Return Retorno
    End Function

    Public Function CALCREP_RepresentacionIsometrica(ByVal ValX As Long, ByVal ValY As Long, ByVal ValZ As Long, ByVal DeltaX As Long, ByVal DeltaY As Long) As Point
        Dim Retorno As Point

        Retorno.X = ((Sqrt(2) / 2) * (ValX - ValY))
        Retorno.Y = ((Sqrt(2 / 3) + ValZ) - ((1 / Sqrt(6)) * (ValX + ValY)))
        Retorno.X = -Retorno.X + DeltaX
        Retorno.Y = -Retorno.Y + DeltaY
        Return Retorno
    End Function

    Public Function CALCREP_RepresentacionIsometrica(ByVal Punto As Punto3D) As Point
        Dim Retorno As Point
        Dim ValX, ValY, ValZ As Double
        ValX = Punto.X
        ValY = Punto.Y
        ValZ = Punto.Z

        Retorno.X = ((Sqrt(2) / 2) * (ValX - ValY))
        Retorno.Y = ((Sqrt(2 / 3) + ValZ) - ((1 / Sqrt(6)) * (ValX + ValY)))
        Retorno.X = -Retorno.X + 0
        Retorno.Y = -Retorno.Y + 0
        Return Retorno
    End Function

    Public Function CALCREP_RepresentacionIsometrica(ByVal Punto As Punto3D, ByVal Delta As Point) As Point
        Dim Retorno As Point
        Dim ValX, ValY, ValZ As Double
        ValX = Punto.X
        ValY = Punto.Y
        ValZ = Punto.Z

        Retorno.X = ((Sqrt(2) / 2) * (ValX - ValY))
        Retorno.Y = ((Sqrt(2 / 3) + ValZ) - ((1 / Sqrt(6)) * (ValX + ValY)))
        Retorno.X = -Retorno.X + Delta.X
        Retorno.Y = -Retorno.Y + Delta.Y
        Return Retorno
    End Function

    Public Function CALCREP_Isometrico2DAPunto3D(ByVal Punto As Point, ByVal Z As Double) As Punto3D
        Dim Retorno As Punto3D
        Punto.X = -Punto.X
        Punto.Y = -Punto.Y

        Retorno.X = (((Punto.Y - (Sqrt(2) / 3) - Z) / -(1 / Sqrt(6))) + (2 * Punto.X / Sqrt(2))) / 2
        Retorno.Y = -(2 * Punto.X / Sqrt(2)) + Retorno.X
        Retorno.Z = Z

        Return Retorno
    End Function
#End Region

#Region "Calculo Sombreado"

    Public Function CALCSOM_PoligonoSombra2D(ByVal Poliedro As Poliedro, ByVal Cara As Cara, ByVal Plano As Plano3D, ByVal FocoLuz As Punto3D, ByVal Camara As DatosCamara) As Point()
        Dim Vertice As Vertice
        Dim PlanoCara As Plano3D
        Dim Rayo As Recta3D
        Dim Punto As Punto3D
        Dim Retorno() As Point
        Dim Cont As Integer
        Dim DireccionLuz As Vector3D
        ReDim Retorno(Cara.Vertices.GetUpperBound(0))
        PlanoCara = CALC3D_CalculoPlanoCara(Poliedro, Cara)
        Cont = 0
        For Each IndiceVertice As Integer In Cara.Vertices
            Vertice = Poliedro.Vertices(IndiceVertice)
            DireccionLuz = New Vector3D(Vertice.Coordenadas, FocoLuz)

            If CALC3D_AnguloVectores3D(DireccionLuz, PlanoCara.VectorNormal) = 90 Then
                For i As Integer = 0 To Retorno.GetUpperBound(0)
                    Retorno(i) = New Point(0, 0)
                Next
                Return Retorno
            End If
            Rayo = New Recta3D(DireccionLuz, Vertice.Coordenadas)
            Punto = CALC3D_InterseccionPlanoRecta(Plano, Rayo)
            Retorno(Cont) = Punto.Representacion2D(Camara)
            'Form1.Sys.DEBUG_SendText("Vertice analizado: " & Vertice.ToString & vbNewLine, dx_lib32.EGA_Color.Purple)
            'Form1.Sys.DEBUG_SendText("Direccion rayo de luz: " & Rayo.VectorDirector.ToString & vbNewLine, dx_lib32.EGA_Color.Purple)
            'Form1.Sys.DEBUG_SendText("Interseccion rayo-plano(suelo): " & Punto.ToString & " --> " & Punto.Representacion2D.ToString & vbNewLine, dx_lib32.EGA_Color.Purple)
            Cont += 1
        Next

        Return Retorno
    End Function

    Public Function CALCSOM_IluminacionModeloPhong(ByVal Foco As Foco, ByVal Iluminacion As DatosIluminacion, ByVal Normal As Vector3D, ByVal Punto As Punto3D, ByVal ColorBase As Color, ByVal Camara As DatosCamara) As Color
        Dim Rayo, Salida, Medio, Vista As Vector3D
        Dim r, g, b As Byte
        Dim rr, gg, bb As Long
        Dim Escalar, Distancia, Distanciamiento As Double
        Dim Ambiente, Difusa, Especular As Double
        Dim AmbienteR, DifusaR, EspecularR As Double
        Dim AmbienteG, DifusaG, EspecularG As Double
        Dim AmbienteB, DifusaB, EspecularB As Double

        Rayo = New Vector3D(Foco.Coordenadas, Punto)
        Salida = ((2 * (Normal * Rayo)) * Normal) - Rayo
        Vista = New Vector3D(Punto, Camara.Posicion)
        Medio = Rayo + Vista

        Rayo = Rayo.VectorUnitario
        Normal = Normal.VectorUnitario
        Salida = Salida.VectorUnitario
        Vista = Vista.VectorUnitario
        Medio = Medio.VectorUnitario


        Distancia = New Vector3D(Punto, Foco.Coordenadas).Modulo
        Distanciamiento = Min(1 / (Iluminacion.ConstanteDistancia1 + (Iluminacion.ConstanteDistancia2 * Distancia) + (Iluminacion.ConstanteDistancia3 * (Distancia ^ 2))), 1)

        Ambiente = Iluminacion.AportacionAmbiente
        Difusa = Foco.Brillo * (Iluminacion.AportacionDifusa * (Normal * Rayo)) * Distanciamiento
        Escalar = (Salida * Vista)
        If Escalar < 0 Then
            Especular = Abs((Iluminacion.AportacionEspecular * Escalar ^ Iluminacion.ExponenteEspecular)) * Distanciamiento
        Else
            Especular = 0
        End If

        AmbienteR = Ambiente * (Foco.Color.R / 255)
        DifusaR = Difusa * (Foco.Color.R / 255)
        EspecularR = Especular * (Foco.Color.R / 255)

        AmbienteG = Ambiente * (Foco.Color.G / 255)
        DifusaG = Difusa * (Foco.Color.G / 255)
        EspecularG = Especular * (Foco.Color.G / 255)

        AmbienteB = Ambiente * (Foco.Color.B / 255)
        DifusaB = Difusa * (Foco.Color.B / 255)
        EspecularB = Especular * (Foco.Color.B / 255)

        rr = ColorBase.R * (AmbienteR + DifusaR + EspecularR)
        'If rr > Foco.Color.R Then rr = rr + ((Foco.Color.R - rr) * EspecularR)

        gg = ColorBase.G * (AmbienteG + DifusaG + EspecularG)
        'If gg > Foco.Color.G Then gg = gg + ((Foco.Color.G - gg) * EspecularG)

        bb = ColorBase.B * (AmbienteB + DifusaB + EspecularB)
        'If bb > Foco.Color.B Then bb = bb + ((Foco.Color.B - bb) * EspecularB)

        If rr > 255 Then rr = 255
        If rr < 0 Then rr = 0

        If gg > 255 Then gg = 255
        If gg < 0 Then gg = 0

        If bb > 255 Then bb = 255
        If bb < 0 Then bb = 0

        r = rr
        g = gg
        b = bb

        Return Color.FromArgb(255, r, g, b)
    End Function


    Public Function CALCSOM_IluminacionModeloPhong(ByVal Focos() As Foco, ByVal Iluminacion As DatosIluminacion, ByVal Normal As Vector3D, ByVal Punto As Punto3D, ByVal ColorBase As Color, ByVal Camara As DatosCamara) As Color
        Dim Rayo, Salida, Vista As Vector3D
        Dim r, g, b As Byte
        Dim rr, gg, bb As Long
        Dim trr, tgg, tbb As Long
        Dim Escalar, Distancia, Distanciamiento As Double
        Dim Ambiente, Difusa, Especular As Double

        Dim AmbienteR, DifusaR, EspecularR As Double
        Dim AmbienteG, DifusaG, EspecularG As Double
        Dim AmbienteB, DifusaB, EspecularB As Double

        trr = 0
        tgg = 0
        tbb = 0

        Vista = New Vector3D(Punto, Camara.Posicion)
        Vista.Normalizar()
        Normal.Normalizar()

        Ambiente = Iluminacion.AportacionAmbiente

        For i As Long = 0 To Focos.GetUpperBound(0)
            If Focos(i).EsVisible(Punto, Normal) Then
                If Focos(i).Tipo <> TipoFoco.Plana Then
                    Rayo = New Vector3D(Focos(i).Coordenadas, Punto)
                Else
                    Rayo = Focos(i).VectorDireccion
                End If
            Else
                Continue For
            End If
            Salida = ((2 * (Normal * Rayo)) * Normal) - Rayo

            Rayo = Rayo.VectorUnitario
            Salida = Salida.VectorUnitario

            Distancia = New Vector3D(Punto, Focos(i).Coordenadas).Modulo
            Distanciamiento = Min(1 / (Iluminacion.ConstanteDistancia1 + (Iluminacion.ConstanteDistancia2 * Distancia) + (Iluminacion.ConstanteDistancia3 * (Distancia ^ 2))), 1)

            Difusa = Focos(i).Brillo * (Iluminacion.AportacionDifusa * (Normal * Rayo)) * Distanciamiento
            Escalar = (Salida * Vista)
            If Escalar < 0 Then
                Especular = Abs((Iluminacion.AportacionEspecular * Escalar ^ Iluminacion.ExponenteEspecular)) * Distanciamiento
            Else
                Especular = 0
            End If

            AmbienteR = Ambiente * (Focos(i).Color.R / 255)
            DifusaR = Difusa * (Focos(i).Color.R / 255)
            EspecularR = Especular * (Focos(i).Color.R / 255)

            AmbienteG = Ambiente * (Focos(i).Color.G / 255)
            DifusaG = Difusa * (Focos(i).Color.G / 255)
            EspecularG = Especular * (Focos(i).Color.G / 255)

            AmbienteB = Ambiente * (Focos(i).Color.B / 255)
            DifusaB = Difusa * (Focos(i).Color.B / 255)
            EspecularB = Especular * (Focos(i).Color.B / 255)

            rr = ColorBase.R * (AmbienteR + DifusaR)
            rr = rr + ((Focos(i).Color.R - rr) * EspecularR)

            gg = ColorBase.G * (AmbienteG + DifusaG)
            gg = gg + ((Focos(i).Color.G - gg) * EspecularG)

            bb = ColorBase.B * (AmbienteB + DifusaB)
            bb = bb + ((Focos(i).Color.B - bb) * EspecularB)

            If rr < 0 Then rr = 0
            If gg < 0 Then gg = 0
            If bb < 0 Then bb = 0

            trr += rr
            tgg += gg
            tbb += bb
        Next




        If trr > 255 Then trr = 255
        If trr < 0 Then trr = 0

        If tgg > 255 Then tgg = 255
        If tgg < 0 Then tgg = 0

        If tbb > 255 Then tbb = 255
        If tbb < 0 Then tbb = 0

        r = trr
        g = tgg
        b = tbb

        Return Color.FromArgb(255, r, g, b)
    End Function

    Public Function CALCSOM_IluminacionModeloPhong(ByVal Foco As Foco, ByVal VectorIluminacion As Vector3D, ByVal Iluminacion As DatosIluminacion, ByVal Normal As Vector3D, ByVal Punto As Punto3D, ByVal ColorBase As Color, ByVal VectorCamara As Vector3D) As Color
        Dim Salida, Medio As Vector3D
        Dim r, g, b As Byte
        Dim rr, gg, bb As Long
        Dim Escalar As Double
        Dim Ambiente, Difusa, Especular As Double
        Dim AmbienteR, DifusaR, EspecularR As Double
        Dim AmbienteG, DifusaG, EspecularG As Double
        Dim AmbienteB, DifusaB, EspecularB As Double

        Salida = ((2 * (Normal * VectorIluminacion)) * Normal) - VectorIluminacion
        Medio = VectorIluminacion + VectorCamara

        VectorIluminacion = VectorIluminacion.VectorUnitario
        Normal = Normal.VectorUnitario
        Salida = Salida.VectorUnitario
        VectorCamara = VectorCamara.VectorUnitario
        Medio = Medio.VectorUnitario

        Ambiente = Iluminacion.AportacionAmbiente
        Difusa = Foco.Brillo * (Iluminacion.AportacionDifusa * (Normal * VectorIluminacion))
        Escalar = (Salida * VectorCamara)
        If Escalar < 0 Then
            Especular = Abs((Iluminacion.AportacionEspecular * Escalar ^ Iluminacion.ExponenteEspecular))
        Else
            Especular = 0
        End If

        AmbienteR = Ambiente * (Foco.Color.R / 255)
        DifusaR = Difusa * (Foco.Color.R / 255)
        EspecularR = Especular * (Foco.Color.R / 255)

        AmbienteG = Ambiente * (Foco.Color.G / 255)
        DifusaG = Difusa * (Foco.Color.G / 255)
        EspecularG = Especular * (Foco.Color.G / 255)

        AmbienteB = Ambiente * (Foco.Color.B / 255)
        DifusaB = Difusa * (Foco.Color.B / 255)
        EspecularB = Especular * (Foco.Color.B / 255)

        rr = ColorBase.R * (AmbienteR + DifusaR + EspecularR)
        'If rr > Foco.Color.R Then rr = rr + ((Foco.Color.R - rr) * EspecularR)

        gg = ColorBase.G * (AmbienteG + DifusaG + EspecularG)
        'If gg > Foco.Color.G Then gg = gg + ((Foco.Color.G - gg) * EspecularG)

        bb = ColorBase.B * (AmbienteB + DifusaB + EspecularB)
        'If bb > Foco.Color.B Then bb = bb + ((Foco.Color.B - bb) * EspecularB)

        If rr > 255 Then rr = 255
        If rr < 0 Then rr = 0

        If gg > 255 Then gg = 255
        If gg < 0 Then gg = 0

        If bb > 255 Then bb = 255
        If bb < 0 Then bb = 0

        r = rr
        g = gg
        b = bb

        Return Color.FromArgb(255, r, g, b)
    End Function

    Public Event CALCSOM_ProgresoGouraudShading(ByVal Progreso As Integer, ByVal V1 As Point, ByVal V2 As Point, ByVal V3 As Point, ByVal X1 As Integer, ByVal Y1 As Integer, ByVal X2 As Integer, ByVal Y2 As Integer, ByVal X3 As Integer, ByVal Y3 As Integer, ByVal d1 As Double, ByVal d2 As Double, ByVal d3 As Double, ByVal d4 As Double, ByVal k1 As Double, ByVal k2 As Double, ByVal k3 As Double, ByVal k4 As Double, ByVal k5 As Double, ByVal k6 As Double, ByVal i1r As Double, ByVal i2r As Double, ByVal i1g As Double, ByVal i2g As Double, ByVal i1b As Double, ByVal i2b As Double, ByVal r As Double, ByVal g As Double, ByVal b As Double, ByVal rr As Byte, ByVal gg As Byte, ByVal bb As Byte)

    Public Sub CALCSOM_FlatShading(ByRef MetodoDibujo As Motor3D.PINT_DelPintarPoligonoColor, ByVal Focos() As Foco, ByVal Iluminacion As DatosIluminacion, ByVal CaraBuffer As CaraBuffer, ByVal Cara As Cara, ByVal Camara As DatosCamara)
        MetodoDibujo.Invoke(Cara.Representacion, CALCSOM_IluminacionModeloPhong(Focos, Iluminacion, Cara.Baricentro.Normal, Cara.Baricentro.Coordenadas, Cara.Color, Camara))
    End Sub

    Public Sub CALCSOM_FlatShading(ByRef MetodoDibujo As Motor3D.PINT_DelPintarPoligonoColor, ByVal Focos() As Foco, ByVal Iluminacion As DatosIluminacion, ByVal CaraBuffer As CaraBuffer, ByVal Cara As Cara, ByVal Camara As DatosCamara, ByVal Teselacion As Boolean)
        Dim T(2) As Point

        If Not Teselacion Then
            MetodoDibujo.Invoke(Cara.Representacion, CALCSOM_IluminacionModeloPhong(Focos, Iluminacion, Cara.Baricentro.Normal, Cara.Baricentro.Coordenadas, Cara.Color, Camara))
        Else
            For i As Integer = 0 To Cara.Triangulacion.GetUpperBound(0)
                T(0) = Cara.Representacion(i)
                T(1) = Cara.Representacion(IIf(i + 1 > Cara.Representacion.GetUpperBound(0), 0, i + 1))
                T(2) = Cara.Baricentro.Representacion

                MetodoDibujo.Invoke(T, CALCSOM_IluminacionModeloPhong(Focos, Iluminacion, Cara.Baricentro.Normal, Cara.Baricentros(i), Cara.Color, Camara))
            Next
        End If

    End Sub

    Public Sub CALCSOM_WireFrameShading(ByRef MetodoDibujo As Motor3D.PINT_DelDibujarPoligonoColor, ByVal Focos() As Foco, ByVal Iluminacion As DatosIluminacion, ByVal CaraBuffer As CaraBuffer, ByVal Cara As Cara, ByVal Camara As DatosCamara)
        MetodoDibujo.Invoke(Cara.Representacion, CALCSOM_IluminacionModeloPhong(Focos, Iluminacion, Cara.Baricentro.Normal, Cara.Baricentro.Coordenadas, Cara.Color, Camara))
    End Sub

    Public Sub CALCSOM_GouraudShading(ByRef MetodoDibujo As Motor3D.PINT_DelPintarPoligonoColores, ByVal Poliedro As Poliedro, ByVal IndiceCara As Long, ByVal Camara As DatosCamara)
        Dim ColorVertices(2) As Color
        Dim Vertices(2) As Point
        Dim Triangulo As Triangulo

        Dim Pintados As New List(Of Point)

        For i As Integer = 0 To Poliedro.Triangulos.GetUpperBound(0)

            Triangulo = Poliedro.Triangulos(i)

            If Triangulo.IndiceCara = IndiceCara Then
                Vertices(0) = Poliedro.Vertices(Triangulo.PrimerVertice).Representacion
                Vertices(1) = Poliedro.Vertices(Triangulo.SegundoVertice).Representacion
                Vertices(2) = Poliedro.Vertices(Triangulo.TercerVertice).Representacion

                ColorVertices(0) = Poliedro.Vertices(Triangulo.PrimerVertice).ColorIluminacion
                ColorVertices(1) = Poliedro.Vertices(Triangulo.SegundoVertice).ColorIluminacion
                ColorVertices(2) = Poliedro.Vertices(Triangulo.TercerVertice).ColorIluminacion

                MetodoDibujo.Invoke(Vertices, ColorVertices)
            End If
        Next
    End Sub

    Public Sub CALCSOM_GouraudShading(ByRef MetodoDibujo As Motor3D.PINT_DelPintarPoligonoColores, ByVal Poliedro As Poliedro, ByVal IndiceCara As Long, ByVal Camara As DatosCamara, ByVal Teselacion As Boolean)
        Dim ColorVertices(2) As Color
        Dim Vertices(2) As Point
        Dim Triangulo As Triangulo

        If Not Teselacion Then
            CALCSOM_GouraudShading(MetodoDibujo, Poliedro, IndiceCara, Camara)
            Exit Sub
        End If

        For i As Integer = 0 To Poliedro.Caras(IndiceCara).Triangulacion.GetUpperBound(0)
            Triangulo = Poliedro.Caras(IndiceCara).Triangulacion(i)
            Vertices(0) = Poliedro.Caras(IndiceCara).Representacion(Triangulo.PrimerVertice)
            Vertices(1) = Poliedro.Caras(IndiceCara).Representacion(Triangulo.SegundoVertice)
            Vertices(2) = Poliedro.Caras(IndiceCara).Baricentro.Representacion

            ColorVertices(0) = Poliedro.Vertices(Poliedro.Caras(IndiceCara).Vertices(Triangulo.PrimerVertice)).ColorIluminacion
            ColorVertices(1) = Poliedro.Vertices(Poliedro.Caras(IndiceCara).Vertices(Triangulo.SegundoVertice)).ColorIluminacion
            ColorVertices(2) = Poliedro.Caras(IndiceCara).Baricentro.ColorIluminacion

            MetodoDibujo.Invoke(Vertices, ColorVertices)
        Next
    End Sub

    Public Sub CALCSOM_PhongShading(ByVal MetodoDibujo As [Delegate], ByVal Focos() As Foco, ByVal Poliedro As Poliedro, ByVal CaraBuffer As CaraBuffer, ByVal Camara As DatosCamara, ByVal Iluminacion As DatosIluminacion)
        Dim ColorVertices(2) As Color
        Dim Vertices(2) As Point
        Dim Vertices3D(2) As Punto3D
        Dim Vectores(2) As Vector3D

        Dim V1, V2, V3 As Vector3D

        Dim Triangulo As Triangulo

        Dim y1, y2, y3 As Integer
        Dim l, m, n As Recta2D

        Dim Delta1x, Delta2x, DeltaV1x, DeltaV2x, Paso1x, Paso2x As Double
        Dim Delta1y, Delta2y, DeltaV1y, DeltaV2y, Paso1y, Paso2y As Double

        Dim Pintados As New List(Of Point)

        For i As Integer = 0 To Poliedro.Triangulos.GetUpperBound(0)
            If Poliedro.Triangulos(i).IndiceCara = CaraBuffer.Indices.Y Then
                Triangulo = Poliedro.Triangulos(i)
                Vertices(0) = Poliedro.Vertices(Triangulo.PrimerVertice).Coordenadas.Representacion2D(Camara)
                Vertices(1) = Poliedro.Vertices(Triangulo.SegundoVertice).Coordenadas.Representacion2D(Camara)
                Vertices(2) = Poliedro.Vertices(Triangulo.TercerVertice).Coordenadas.Representacion2D(Camara)

                Vertices3D(0) = Poliedro.Vertices(Triangulo.PrimerVertice).Coordenadas
                Vertices3D(1) = Poliedro.Vertices(Triangulo.SegundoVertice).Coordenadas
                Vertices3D(2) = Poliedro.Vertices(Triangulo.TercerVertice).Coordenadas

                Vectores(0) = Poliedro.Vertices(Triangulo.PrimerVertice).Normal
                Vectores(1) = Poliedro.Vertices(Triangulo.SegundoVertice).Normal
                Vectores(2) = Poliedro.Vertices(Triangulo.TercerVertice).Normal

                l = New Recta2D(Vertices(0), Vertices(1))
                m = New Recta2D(Vertices(0), Vertices(2))

                Delta1x = Vertices(1).X - Vertices(0).X
                Delta2x = Vertices(2).X - Vertices(0).X

                DeltaV1x = Vectores(1).X - Vectores(0).X
                DeltaV2x = Vectores(2).X - Vectores(0).X

                Paso1x = Delta1x / DeltaV1x
                Paso2x = Delta2x / DeltaV2x

                Delta1y = Vertices(1).Y - Vertices(0).Y
                Delta2y = Vertices(2).Y - Vertices(0).Y

                DeltaV1y = Vectores(1).Y - Vectores(0).Y
                DeltaV2y = Vectores(2).Y - Vectores(0).Y

                Paso1y = Delta1y / DeltaV1y
                Paso2y = Delta2y / DeltaV2y

                V1 = Vectores(0)
                V2 = Vectores(0)
                V3 = Vectores(0)

                For x1 As Integer = Vertices(0).X To Vertices(1).X Step IIf(Vertices(0).X < Vertices(1).X, 1, -1)
                    y1 = l.Funcion(x1)
                    For x2 As Integer = Vertices(0).X To Vertices(2).X Step IIf(Vertices(0).X < Vertices(2).X, 1, -1)
                        y2 = m.Funcion(x2)
                        n = New Recta2D(New Point(x1, y1), New Point(x2, y2))
                        'x1 += IIf(Vertices(0).X < Vertices(1).X, 1, -1)
                        'y1 = m.Funcion(x1)

                        For x3 As Integer = x1 To x2 Step IIf(x1 < x2, 1, -1)
                            y3 = n.Funcion(x3)

                            If Pintados.Contains(New Point(x3, y3)) Then Continue For

                            'Graphics.FillRectangle(new SolidBrush(calcsom_iluminacionmodelophong(Focos,Iluminacion,V3,

                            Pintados.Add(New Point(x3, y3))
                        Next
                        V2.X += Paso2x
                        V2.Y += Paso2y
                    Next
                    V1.X += Paso1x
                    V1.Y += Paso1y
                Next
            End If
        Next
    End Sub

#End Region

#Region "Calculo Colisiones"
    Public Structure Box
        Public Centro As Punto3D
        Public Dimensiones As Punto3D
    End Structure


    Public Function CALCCOL_PrimerPaso(ByVal IndicePoliedro As Integer, ByVal ListaPoliedros() As Poliedro) As Boolean
        Dim Poliedro As Poliedro
        Dim Distancia, DistanciaMaxima, Radio As Double
        Dim Intersecciones As New List(Of Poliedro)
        Dim MaxVX, MaxVY, MinVX, MinVY As Vertice


        Poliedro = ListaPoliedros(IndicePoliedro)

        Distancia = 0
        DistanciaMaxima = 0
        Intersecciones.Clear()

        For i As Integer = 0 To Poliedro.Vertices.GetUpperBound(0)
            For j As Integer = 0 To Poliedro.Vertices.GetUpperBound(0)
                Distancia = New Vector3D(Poliedro.Vertices(i).Coordenadas, Poliedro.Vertices(j).Coordenadas).Modulo
                If Distancia > DistanciaMaxima Then DistanciaMaxima = Distancia
            Next
        Next
        Radio = DistanciaMaxima

        For i As Integer = 0 To ListaPoliedros.GetUpperBound(0)
            If i <> IndicePoliedro Then
                If New Vector3D(ListaPoliedros(i).Centro, Poliedro.Centro).Modulo < Radio Then Intersecciones.Add(ListaPoliedros(i))
            End If
        Next

        Return (Intersecciones.Count > 0)
    End Function
#End Region

#Region "Utilidades"
    Public Function UTIL_ColorProporcional(ByVal Minimo As Double, ByVal Maximo As Double, ByVal Valor As Double) As Color
        Dim Rango As Double
        Maximo = Maximo + (Maximo / 6)
        Rango = Maximo - Minimo
        Dim r, g, b As Byte
        Dim m, n As Double
        Select Case Valor
            Case 0 To (Rango / 6)
                m = (Rango / 6)
                n = Math.Abs(Valor - m)
                r = 255
                g = 255 - ((255 / (Rango / 6)) * n)
                b = 0
                'OK
            Case (Rango / 6) To (Rango / 6) * 2
                m = (Rango / 6) * 2
                n = Math.Abs(Valor - m)
                r = (255 / (Rango / 6)) * n
                g = 255
                b = 0
                'OK
            Case ((Rango / 6) * 2) To (Rango / 6) * 3
                m = (Rango / 6) * 3
                n = Math.Abs(Valor - m)
                r = 0
                g = 255
                b = 255 - ((255 / (Rango / 6)) * n)
                'OK
            Case ((Rango / 6) * 3) To (Rango / 6) * 4
                m = (Rango / 6) * 4
                n = Math.Abs(Valor - m)
                r = 0
                g = (255 / (Rango / 6)) * n
                b = 255
                'OK
            Case ((Rango / 6) * 4) To (Rango / 6) * 5
                m = (Rango / 6) * 5
                n = Math.Abs(Valor - m)
                r = 255 - ((255 / (Rango / 6)) * n)
                g = 0
                b = 255
                'OK
            Case ((Rango / 6) * 5) To (Rango / 6) * 6
                m = (Rango / 6) * 6
                n = Math.Abs(Valor - m)
                r = 255
                g = 0
                b = (255 / (Rango / 6)) * n
                'OK
        End Select


        Return Color.FromArgb(r, g, b)
    End Function


    Private Function UTIL_QuickSort(ByVal vec() As Integer, ByVal primero As Integer, ByVal ultimo As Integer) As Array
        Dim a, b, piv, Npiv, aux As Integer
        piv = vec(primero) 'sacamos pivote
        Npiv = primero
        For a = primero To ultimo
            If a <> Npiv Then 'si el pivote no se esta queriendo comparar con el mismo pivote
                If vec(a) < piv Then
                    'mandamos los numeros a la izquierda
                    aux = vec(a)
                    'recorremos sub-vector (desde piv hasta vec(a))
                    For b = a To Npiv + 1 Step -1
                        vec(b) = vec(b - 1)
                    Next
                    vec(Npiv) = aux   'colocamos numero menor a la izquierda de piv
                    Npiv += 1        'actualizamos posicion de piv
                End If
            End If
        Next
        'enviamos recursivamente el vector a la izuierda de piv
        If (Npiv - primero <> 0) Then 'si el sub-vector de la izquierda tiene elementos, entonces se hace recursividad
            vec = UTIL_QuickSort(vec, primero, Npiv - 1)
        End If
        'enviamos recursivamente el vector a la derecha de piv
        If (ultimo - Npiv <> 0) Then 'si el sub-vector de la derecha tiene elementos, entonces se hace recursividad
            vec = UTIL_QuickSort(vec, Npiv + 1, ultimo)
        End If
        Return vec
    End Function

    Public Function UTIL_QuickSort(ByVal vec As List(Of CaraBuffer), ByVal primero As Integer, ByVal ultimo As Integer) As List(Of CaraBuffer)
        Try
            Dim piv, aux As CaraBuffer
            Dim a, b, Npiv As Integer

            If vec.Count - 1 = 0 Then Return vec

            piv = vec(primero) 'sacamos pivote
            Npiv = primero
            For a = primero To ultimo
                If a <> Npiv Then 'si el pivote no se esta queriendo comparar con el mismo pivote
                    If vec(a).DistanciaCamara > piv.DistanciaCamara Then

                        'mandamos los numeros a la izquierda
                        aux = vec(a)
                        'recorremos sub-vector (desde piv hasta vec(a))
                        For b = a To Npiv + 1 Step -1
                            vec(b) = vec(b - 1)
                        Next
                        vec(Npiv) = aux   'colocamos numero menor a la izquierda de piv
                        Npiv += 1        'actualizamos posicion de piv
                    End If
                End If
            Next
            'enviamos recursivamente el vector a la izuierda de piv
            If (Npiv - primero <> 0) Then 'si el sub-vector de la izquierda tiene elementos, entonces se hace recursividad
                vec = UTIL_QuickSort(vec, primero, Npiv - 1)
            End If
            'enviamos recursivamente el vector a la derecha de piv
            If (ultimo - Npiv <> 0) Then 'si el sub-vector de la derecha tiene elementos, entonces se hace recursividad
                vec = UTIL_QuickSort(vec, Npiv + 1, ultimo)
            End If
            Return vec
        Catch ex As Exception
            Return vec
        End Try

    End Function

    Public Function UTIL_BogoSort(ByVal Lista As List(Of CaraBuffer), ByVal PosicionCamara As Punto3D) As List(Of CaraBuffer)
        Dim Copia As CaraBuffer

        For i As Long = 0 To Lista.Count - 2
            If Lista(i).DistanciaCamara > Lista(i + 1).DistanciaCamara Then
                Copia = Lista(i)
                Lista(i) = Lista(i + 1)
                Lista(i + 1) = Copia
                i = 0
            End If
        Next

        Return Lista
    End Function

    Public Function UTIL_Burbuja(ByVal Lista As List(Of CaraBuffer), ByVal PosicionCamara As Punto3D) As List(Of CaraBuffer)
        Dim Copia As CaraBuffer

        For i As Long = 0 To Lista.Count - 1
            For j As Long = 0 To Lista.Count - 1
                If Lista(i).DistanciaCamara > Lista(j).DistanciaCamara Then
                    Copia = Lista(i)
                    Lista(i) = Lista(j)
                    Lista(j) = Copia
                End If
            Next
        Next

        Return Lista
    End Function

    Public Function UTIL_AlgoritmoRelleno2D(ByVal Poligono() As Point) As Rectangle()
        Dim NodoActual As Point
        Dim Nodos, NodosSiguientes, NodosRellenos As ArrayList
        Dim PilaRetorno As New List(Of Rectangle)
        Dim Pila As New ArrayList
        Dim P1, P2, P3, P4, Actual As Point
        'f.Location = New Point(0, 0)
        'f.Size = New Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height)
        'f.Show()

        ' P1 el más a la derecha
        ' P2 el más a la izquierda
        ' P3 el más arriba
        ' P4 el más abajo
        Actual = Poligono(0)
        For i As Integer = 0 To Poligono.GetUpperBound(0)
            If Poligono(i).Y > Actual.Y Then
                P1 = Poligono(i)
                P2 = Poligono(i)
                P3 = Poligono(i)
                P4 = Poligono(i)
            End If
        Next

        For i As Long = 0 To Poligono.GetUpperBound(0)
            If Poligono(i).X < P1.X Then P1 = Poligono(i)
            If Poligono(i).X > P2.X Then P2 = Poligono(i)
            If Poligono(i).Y < P3.Y Then P3 = Poligono(i)
            If Poligono(i).Y > P4.Y Then P4 = Poligono(i)

            'g.DrawString("V" & i + 1 & " (" & Poligono(i).X & "," & Poligono(i).Y & ")", f.Font, Brushes.Purple, Poligono(i).X, Poligono(i).Y)
        Next

        'g.FillEllipse(Brushes.Green, New Rectangle(P1.X - 3, P1.Y - 3, 3, 3))
        'g.FillEllipse(Brushes.Green, New Rectangle(P2.X - 3, P2.Y - 3, 3, 3))
        'g.FillEllipse(Brushes.Green, New Rectangle(P3.X - 3, P3.Y - 3, 3, 3))
        'g.FillEllipse(Brushes.Green, New Rectangle(P4.X - 3, P4.Y - 3, 3, 3))

        'g.DrawString("P1 (" & P1.X & "," & P1.Y & ")", f.Font, Brushes.White, P1.X, P1.Y)
        'g.DrawString("P2 (" & P2.X & "," & P2.Y & ")", f.Font, Brushes.White, P2.X, P2.Y)
        'g.DrawString("P3 (" & P3.X & "," & P3.Y & ")", f.Font, Brushes.White, P3.X, P3.Y)
        'g.DrawString("P4 (" & P4.X & "," & P4.Y & ")", f.Font, Brushes.White, P4.X, P4.Y)

        Nodos = New ArrayList
        NodosRellenos = New ArrayList
        NodosSiguientes = New ArrayList

        NodoActual = P1
        Nodos.Add(NodoActual)



        Do While Nodos.Count > 0
            For Each Nodo As Point In Nodos
                'Nodo derecha:
                If Nodo.X + 1 <= P2.X Then
                    If CALC2D_PuntoDentroPoligono(New Point(Nodo.X + 1, Nodo.Y), Poligono) AndAlso Not PilaRetorno.Contains(New Rectangle(Nodo.X + 1, Nodo.Y, 1, 1)) Then
                        NodosSiguientes.Add(New Point(Nodo.X + 1, Nodo.Y))
                        PilaRetorno.Add(New Rectangle(Nodo.X + 1, Nodo.Y, 1, 1))
                    End If
                End If
                'Nodo izquierda:
                If Nodo.X - 1 >= P1.X Then
                    If CALC2D_PuntoDentroPoligono(New Point(Nodo.X - 1, Nodo.Y), Poligono) AndAlso Not PilaRetorno.Contains(New Rectangle(Nodo.X - 1, Nodo.Y, 1, 1)) Then
                        NodosSiguientes.Add(New Point(Nodo.X - 1, Nodo.Y))
                        PilaRetorno.Add(New Rectangle(Nodo.X - 1, Nodo.Y, 1, 1))
                    End If
                End If
                'Nodo abajo:
                If Nodo.Y + 1 <= P4.Y Then
                    If CALC2D_PuntoDentroPoligono(New Point(Nodo.X, Nodo.Y + 1), Poligono) AndAlso Not PilaRetorno.Contains(New Rectangle(Nodo.X, Nodo.Y + 1, 1, 1)) Then
                        NodosSiguientes.Add(New Point(Nodo.X, Nodo.Y + 1))
                        PilaRetorno.Add(New Rectangle(Nodo.X, Nodo.Y + 1, 1, 1))
                    End If
                End If
                'Nodo arriba:
                If Nodo.Y - 1 >= P3.Y Then
                    If CALC2D_PuntoDentroPoligono(New Point(Nodo.X, Nodo.Y - 1), Poligono) AndAlso Not PilaRetorno.Contains(New Rectangle(Nodo.X, Nodo.Y - 1, 1, 1)) Then
                        NodosSiguientes.Add(New Point(Nodo.X, Nodo.Y - 1))
                        PilaRetorno.Add(New Rectangle(Nodo.X, Nodo.Y - 1, 1, 1))
                    End If
                End If
            Next

            Nodos.Clear()
            Nodos = NodosSiguientes.Clone
            NodosSiguientes.Clear()

            'My.Application.DoEvents()
        Loop

        If PilaRetorno.Count = 0 Then
            For i As Integer = 0 To Poligono.GetUpperBound(0)
                PilaRetorno.Add(New Rectangle(Poligono(i).X, Poligono(i).Y, 1, 1))
            Next
        End If
        Return PilaRetorno.ToArray
    End Function
    Public Function UTIL_AlgoritmoRelleno2D2(ByVal Poligono() As Point) As Rectangle()
        Dim PilaRetorno As New List(Of Rectangle)
        Dim Pila As New List(Of Point)
        Dim P1, P2, P3, P4 As Point

        ' P1 el más a la derecha
        ' P2 el más a la izquierda
        ' P3 el más arriba
        ' P4 el más abajo

        For i As Long = 0 To Poligono.GetUpperBound(0)
            If Poligono(i).X < P1.Y Then P1 = Poligono(i)
            If Poligono(i).X > P2.Y Then P2 = Poligono(i)
            If Poligono(i).Y < P3.Y Then P3 = Poligono(i)
            If Poligono(i).Y > P4.Y Then P4 = Poligono(i)
        Next

        For i As Integer = P1.X To P2.X
            For j As Integer = P3.Y To P4.Y
                If CALC2D_PuntoDentroPoligono(New Point(i, j), Poligono) Then PilaRetorno.Add(New Rectangle(i, j, 1, 1))
            Next
        Next
        If PilaRetorno.Count = 0 Then
            For i As Integer = 0 To Poligono.GetUpperBound(0)
                PilaRetorno.Add(New Rectangle(Poligono(i).X, Poligono(i).Y, 1, 1))
            Next
        End If
        Return PilaRetorno.ToArray
    End Function
#End Region

#Region "Modelado de primitivas"

    Public Function PRIM_Anillo() As Poliedro
        Dim a As New Poliedro(16, 16)

        a.EsCurvo = False

        a.VerticesOriginales(0) = New Punto3D(0, 0, 0)
        a.VerticesOriginales(1) = New Punto3D(3, 0, 0)
        a.VerticesOriginales(2) = New Punto3D(3, 3, 0)
        a.VerticesOriginales(3) = New Punto3D(0, 3, 0)

        a.VerticesOriginales(4) = New Punto3D(1, 1, 0)
        a.VerticesOriginales(5) = New Punto3D(2, 1, 0)
        a.VerticesOriginales(6) = New Punto3D(2, 2, 0)
        a.VerticesOriginales(7) = New Punto3D(1, 2, 0)

        a.VerticesOriginales(8) = New Punto3D(0, 0, 1)
        a.VerticesOriginales(9) = New Punto3D(3, 0, 1)
        a.VerticesOriginales(10) = New Punto3D(3, 3, 1)
        a.VerticesOriginales(11) = New Punto3D(0, 3, 1)

        a.VerticesOriginales(12) = New Punto3D(1, 1, 1)
        a.VerticesOriginales(13) = New Punto3D(2, 1, 1)
        a.VerticesOriginales(14) = New Punto3D(2, 2, 1)
        a.VerticesOriginales(15) = New Punto3D(1, 2, 1)

        For i As Integer = 0 To a.Caras.GetUpperBound(0)
            a.Caras(i) = New Cara(4)
        Next

        a.Caras(0).Vertices(0) = 0
        a.Caras(0).Vertices(1) = 1
        a.Caras(0).Vertices(2) = 5
        a.Caras(0).Vertices(3) = 4

        a.Caras(1).Vertices(0) = 5
        a.Caras(1).Vertices(1) = 1
        a.Caras(1).Vertices(2) = 2
        a.Caras(1).Vertices(3) = 6

        a.Caras(2).Vertices(0) = 7
        a.Caras(2).Vertices(1) = 6
        a.Caras(2).Vertices(2) = 2
        a.Caras(2).Vertices(3) = 3

        a.Caras(3).Vertices(0) = 0
        a.Caras(3).Vertices(1) = 4
        a.Caras(3).Vertices(2) = 7
        a.Caras(3).Vertices(3) = 3


        a.Caras(4).Vertices(3) = 8
        a.Caras(4).Vertices(2) = 9
        a.Caras(4).Vertices(1) = 13
        a.Caras(4).Vertices(0) = 12

        a.Caras(5).Vertices(3) = 13
        a.Caras(5).Vertices(2) = 9
        a.Caras(5).Vertices(1) = 10
        a.Caras(5).Vertices(0) = 14

        a.Caras(6).Vertices(3) = 15
        a.Caras(6).Vertices(2) = 14
        a.Caras(6).Vertices(1) = 10
        a.Caras(6).Vertices(0) = 11

        a.Caras(7).Vertices(3) = 8
        a.Caras(7).Vertices(2) = 12
        a.Caras(7).Vertices(1) = 15
        a.Caras(7).Vertices(0) = 11


        a.Caras(8).Vertices(3) = 9
        a.Caras(8).Vertices(2) = 8
        a.Caras(8).Vertices(1) = 0 '-------------------------
        a.Caras(8).Vertices(0) = 1

        a.Caras(9).Vertices(3) = 10
        a.Caras(9).Vertices(2) = 9
        a.Caras(9).Vertices(1) = 1
        a.Caras(9).Vertices(0) = 2

        a.Caras(10).Vertices(3) = 11
        a.Caras(10).Vertices(2) = 10
        a.Caras(10).Vertices(1) = 2
        a.Caras(10).Vertices(0) = 3

        a.Caras(11).Vertices(3) = 8
        a.Caras(11).Vertices(2) = 11
        a.Caras(11).Vertices(1) = 3
        a.Caras(11).Vertices(0) = 0


        a.Caras(12).Vertices(3) = 12
        a.Caras(12).Vertices(2) = 13
        a.Caras(12).Vertices(1) = 5
        a.Caras(12).Vertices(0) = 4

        a.Caras(13).Vertices(3) = 13
        a.Caras(13).Vertices(2) = 14
        a.Caras(13).Vertices(1) = 6
        a.Caras(13).Vertices(0) = 5 'OK

        a.Caras(14).Vertices(3) = 14
        a.Caras(14).Vertices(2) = 15
        a.Caras(14).Vertices(1) = 7
        a.Caras(14).Vertices(0) = 6 'OK

        a.Caras(15).Vertices(3) = 15
        a.Caras(15).Vertices(2) = 12
        a.Caras(15).Vertices(1) = 4
        a.Caras(15).Vertices(0) = 7 'OK

        a.CentroOriginal = New Punto3D(1.5, 1.5, 0.5)
        a.Rotar(EnumEje.EjeY, 90)

        a.TrasladarADestino(New Punto3D(0, 0, 0))
        a.TriangulacionCaras()
        a.EstablecerCarasVertices()

        Return a
    End Function

    Public Function PRIM_Cono(ByVal Caras As Integer) As Poliedro
        Dim c As Poliedro
        Dim cont, contc As Integer
        c = New Poliedro((Caras + 1), (Caras + 1))
        c.EsCurvo = True

        For i As Integer = 0 To Caras - 1
            c.Caras(i) = New Cara(3)
        Next

        cont = 1
        contc = 0
        c.VerticesOriginales(0) = New Punto3D(0, 0, 2)
        c.Caras(0).Vertices(0) = 1


        For a As Double = 0 To 2 * PI Step PI / (Caras / 2)
            If cont > Caras Then Exit For
            c.VerticesOriginales(cont) = New Punto3D(Cos(a), Sin(a), 0)
            cont += 1
        Next

        cont = 1
        For i As Integer = 0 To Caras - 1
            c.Caras(i).Vertices(1) = cont
            c.Caras(i).Vertices(0) = cont + 1
            c.Caras(i).Vertices(2) = 0
            If cont < Caras - 1 Then cont += 1 Else Exit For
        Next
        c.Caras(Caras - 1).Vertices(1) = Caras
        c.Caras(Caras - 1).Vertices(0) = 1
        c.Caras(Caras - 1).Vertices(2) = 0
        'c.Caras(c.Caras.GetUpperBound(0) - 1).Vertices(2) = c.Vertices.GetUpperBound(0)
        'c.Caras(c.Caras.GetUpperBound(0) - 1).Vertices(1) = 1
        'c.Caras(c.Caras.GetUpperBound(0) - 1).Vertices(0) = 0

        c.Caras(Caras) = New Cara(c.Vertices.GetUpperBound(0))
        For i As Integer = 1 To c.Vertices.GetUpperBound(0)
            c.Caras(Caras).Vertices(i - 1) = i
        Next
        'c.Caras(Caras - 1).Vertices(Caras - 2) = 1
        'For i As Integer = 0 To Caras - 2
        '    c.Caras(Caras - 1).Vertices(i) = i + 2
        'Next

        c.CentroOriginal = New Punto3D(0, 0, 0)
        c.TriangulacionCaras()
        c.EstablecerCarasVertices()
        Return c
    End Function

    Public Function PRIM_Esfera(ByVal Pasos As Integer) As Poliedro
        Dim e As Poliedro
        Dim cont, contc As Integer
        Dim Radio As Double = 1
        e = New Poliedro((Pasos ^ 2) - Pasos + 2, (Pasos ^ 2))

        e.EsCurvo = True
        For i As Integer = 0 To Pasos - 1
            e.Caras(i) = New Cara(3)
        Next
        For i As Integer = Pasos To (Pasos ^ 2) - Pasos - 1
            e.Caras(i) = New Cara(4)
        Next
        For i As Integer = (Pasos ^ 2) - Pasos To (Pasos ^ 2) - 1
            e.Caras(i) = New Cara(3)
        Next

        e = e

        cont = 1
        contc = 0

        e.VerticesOriginales(0) = New Punto3D(0, 0, 1)
        e.VerticesOriginales(e.Vertices.GetUpperBound(0)) = New Punto3D(0, 0, -1)

        For a As Double = 0 To PI Step PI / (Pasos / 1)
            If a = 0 Or a = PI Then Continue For
            Radio = Sin(a)
            For b As Double = 0 To 2 * PI Step PI / (Pasos / 2)
                If b = 2 * PI Then Continue For
                e.VerticesOriginales(cont) = New Punto3D(Radio * Cos(b), Radio * Sin(b), Cos(a))

                If cont = e.VerticesOriginales.GetUpperBound(0) Then Exit For
                cont += 1
            Next
            If cont = (Pasos ^ 2) - 1 Then Exit For
        Next

        cont = 1
        For i As Integer = 0 To Pasos - 1
            e.Caras(i).Vertices(0) = 0
            e.Caras(i).Vertices(1) = IIf(cont + 1 <= Pasos - 0, cont + 1, 1)
            e.Caras(i).Vertices(2) = cont
            If cont = Pasos Then Exit For
            cont += 1
        Next

        cont = e.VerticesOriginales.GetUpperBound(0) - Pasos - 1
        For i As Integer = e.Caras.GetUpperBound(0) - Pasos To e.Caras.GetUpperBound(0)
            e.Caras(i).Vertices(0) = cont
            e.Caras(i).Vertices(1) = IIf(cont + 1 < e.Vertices.GetUpperBound(0) - 1, cont + 1, e.Vertices.GetUpperBound(0) - Pasos - 1)
            e.Caras(i).Vertices(2) = e.Vertices.GetUpperBound(0)
            If cont = e.Vertices.GetUpperBound(0) - 1 Then Exit For
            cont += 1
        Next

        e = e

        For i As Integer = 1 To Pasos - 2
            For j As Integer = 0 To Pasos - 1
                e.Caras((i * Pasos) + j).Vertices(0) = ((i - 1) * Pasos) + j + 1
                e.Caras((i * Pasos) + j).Vertices(1) = IIf(j + 1 <= Pasos - 1, ((i - 1) * Pasos) + j + 2, ((i - 1) * Pasos) + 1)
                e.Caras((i * Pasos) + j).Vertices(2) = IIf(j + 1 <= Pasos - 1, ((i) * Pasos) + j + 2, ((i) * Pasos) + 1)
                e.Caras((i * Pasos) + j).Vertices(3) = ((i) * Pasos) + j + 1
            Next
        Next
        'For i As Integer = 0 To Pasos - 2
        '    e.Caras(i).Vertices(1) = cont
        '    e.Caras(i).Vertices(0) = cont + 1
        '    e.Caras(i).Vertices(2) = cont + Pasos
        '    e.Caras(i).Vertices(3) = cont + Pasos + 1
        '    cont += 1
        'Next
        'e.Caras(Pasos - 1).Vertices(0) = Pasos + Pasos - 1
        'e.Caras(Pasos - 1).Vertices(1) = Pasos
        'e.Caras(Pasos - 1).Vertices(2) = 0
        'e.Caras(Pasos - 1).Vertices(3) = Pasos - 1
        'c.Caras(c.Caras.GetUpperBound(0) - 1).Vertices(2) = c.Vertices.GetUpperBound(0)
        'c.Caras(c.Caras.GetUpperBound(0) - 1).Vertices(1) = 1
        'c.Caras(c.Caras.GetUpperBound(0) - 1).Vertices(0) = 0

        'e.Caras(Pasos) = New Cara(Pasos)
        'e.Caras(Pasos + 1) = New Cara(Pasos)
        'For i As Integer = 0 To Pasos - 1
        '    e.Caras(Pasos).Vertices(i) = i
        'Next

        'For i As Integer = 0 To Pasos - 1
        '    e.Caras(Pasos + 1).Vertices(Pasos - i - 1) = i + Pasos
        'Next
        'c.Caras(Caras - 1).Vertices(Caras - 2) = 1
        'For i As Integer = 0 To Caras - 2
        '    c.Caras(Caras - 1).Vertices(i) = i + 2
        'Next

        e.CentroOriginal = New Punto3D(0, 0, 0)
        e.TriangulacionCaras()
        e.EstablecerCarasVertices()
        Return e
    End Function

    Public Function PRIM_Cilindro(ByVal Caras As Integer) As Poliedro
        Dim c As Poliedro
        Dim cont, contc As Integer
        c = New Poliedro((Caras) * 2, (Caras + 2))

        c.EsCurvo = True
        For i As Integer = 0 To Caras - 1
            c.Caras(i) = New Cara(4)
        Next

        cont = 0
        contc = 0


        For a As Double = 0 To 2 * PI Step PI / (Caras / 2)
            If cont >= Caras Then Exit For
            c.VerticesOriginales(cont) = New Punto3D(Cos(a), Sin(a), 0)
            c.VerticesOriginales(cont + Caras) = New Punto3D(Cos(a), Sin(a), 1)
            cont += 1
        Next

        cont = 0
        For i As Integer = 0 To Caras - 2
            c.Caras(i).Vertices(1) = cont
            c.Caras(i).Vertices(0) = cont + 1
            c.Caras(i).Vertices(2) = cont + Caras
            c.Caras(i).Vertices(3) = cont + Caras + 1
            cont += 1
        Next
        c.Caras(Caras - 1).Vertices(0) = Caras + Caras - 1
        c.Caras(Caras - 1).Vertices(1) = Caras
        c.Caras(Caras - 1).Vertices(2) = 0
        c.Caras(Caras - 1).Vertices(3) = Caras - 1
        'c.Caras(c.Caras.GetUpperBound(0) - 1).Vertices(2) = c.Vertices.GetUpperBound(0)
        'c.Caras(c.Caras.GetUpperBound(0) - 1).Vertices(1) = 1
        'c.Caras(c.Caras.GetUpperBound(0) - 1).Vertices(0) = 0

        c.Caras(Caras) = New Cara(Caras)
        c.Caras(Caras + 1) = New Cara(Caras)
        For i As Integer = 0 To Caras - 1
            c.Caras(Caras).Vertices(i) = i
        Next

        For i As Integer = 0 To Caras - 1
            c.Caras(Caras + 1).Vertices(Caras - i - 1) = i + Caras
        Next
        'c.Caras(Caras - 1).Vertices(Caras - 2) = 1
        'For i As Integer = 0 To Caras - 2
        '    c.Caras(Caras - 1).Vertices(i) = i + 2
        'Next

        c.CentroOriginal = New Punto3D(0, 0, 0.5)
        c.TriangulacionCaras()
        c.EstablecerCarasVertices()
        Return c
    End Function

    Public Function PRIM_Cubo() As Poliedro
        Dim c As Poliedro
        c = New Poliedro(8, 6)
        c.EsCurvo = False

        c.VerticesOriginales(0) = New Punto3D(0, 0, 0)
        c.VerticesOriginales(1) = New Punto3D(1, 0, 0)
        c.VerticesOriginales(2) = New Punto3D(1, 1, 0)
        c.VerticesOriginales(3) = New Punto3D(0, 1, 0)
        c.VerticesOriginales(4) = New Punto3D(0, 0, 1)
        c.VerticesOriginales(5) = New Punto3D(1, 0, 1)
        c.VerticesOriginales(6) = New Punto3D(1, 1, 1)
        c.VerticesOriginales(7) = New Punto3D(0, 1, 1)

        c.Caras(0) = New Cara(4)
        c.Caras(1) = New Cara(4)
        c.Caras(2) = New Cara(4)
        c.Caras(3) = New Cara(4)
        c.Caras(4) = New Cara(4)
        c.Caras(5) = New Cara(4)

        c.Caras(0).Vertices(0) = 0
        c.Caras(0).Vertices(1) = 1
        c.Caras(0).Vertices(2) = 2
        c.Caras(0).Vertices(3) = 3

        c.Caras(1).Vertices(0) = 7
        c.Caras(1).Vertices(1) = 6
        c.Caras(1).Vertices(2) = 5
        c.Caras(1).Vertices(3) = 4

        c.Caras(2).Vertices(0) = 3
        c.Caras(2).Vertices(1) = 2
        c.Caras(2).Vertices(2) = 6
        c.Caras(2).Vertices(3) = 7

        c.Caras(3).Vertices(0) = 0
        c.Caras(3).Vertices(1) = 3
        c.Caras(3).Vertices(2) = 7
        c.Caras(3).Vertices(3) = 4

        c.Caras(4).Vertices(0) = 1
        c.Caras(4).Vertices(1) = 0
        c.Caras(4).Vertices(2) = 4
        c.Caras(4).Vertices(3) = 5

        c.Caras(5).Vertices(0) = 2
        c.Caras(5).Vertices(1) = 1
        c.Caras(5).Vertices(2) = 5
        c.Caras(5).Vertices(3) = 6

        c.CentroOriginal = New Punto3D(1 / 2, 1 / 2, 1 / 2)
        c.TriangulacionCaras()
        c.EstablecerCarasVertices()
        Return c
    End Function

    Public Function PRIM_Piramide() As Poliedro
        Dim P As Poliedro

        P.EsCurvo = False

        P = New Poliedro(4, 4)
        P.VerticesOriginales(0) = New Punto3D(0, 20, 0)
        P.VerticesOriginales(1) = New Punto3D(20, 0, 0)
        P.VerticesOriginales(2) = New Punto3D(0, 0, 0)
        P.VerticesOriginales(3) = New Punto3D(0, 0, 20)


        P.Caras(0) = New Cara(3)
        P.Caras(1) = New Cara(3)
        P.Caras(2) = New Cara(3)
        P.Caras(3) = New Cara(3)

        P.Caras(0).Vertices(0) = P.Vertices(2).Indice
        P.Caras(0).Vertices(1) = P.Vertices(1).Indice
        P.Caras(0).Vertices(2) = P.Vertices(0).Indice

        P.Caras(1).Vertices(0) = P.Vertices(0).Indice
        P.Caras(1).Vertices(1) = P.Vertices(1).Indice
        P.Caras(1).Vertices(2) = P.Vertices(3).Indice

        P.Caras(2).Vertices(0) = P.Vertices(3).Indice
        P.Caras(2).Vertices(1) = P.Vertices(1).Indice
        P.Caras(2).Vertices(2) = P.Vertices(2).Indice

        P.Caras(3).Vertices(0) = P.Vertices(0).Indice
        P.Caras(3).Vertices(1) = P.Vertices(3).Indice
        P.Caras(3).Vertices(2) = P.Vertices(2).Indice

        P.CentroOriginal = New Punto3D(0, 0, 0)

        P.CambiarEscala(New Punto3D(1 / 20, 1 / 20, 1 / 20))
        P.AplicarTransformaciones()
        P.TriangulacionCaras()
        P.EstablecerCarasVertices()
        Return P
    End Function

    Public Function PRIM_Icosaedro() As Poliedro
        Dim Ico As Poliedro
        Ico = New Poliedro(12, 20)
        Ico.EsCurvo = False

        For i As Integer = 0 To 19
            Ico.Caras(i) = New Cara(3)
        Next

        Ico.VerticesOriginales(0) = New Punto3D(1, 0, 1.618033)
        Ico.VerticesOriginales(1) = New Punto3D(1, 0, -1.618033)
        Ico.VerticesOriginales(2) = New Punto3D(-1, 0, -1.618033)
        Ico.VerticesOriginales(3) = New Punto3D(-1, 0, 1.618033)
        Ico.VerticesOriginales(4) = New Punto3D(1.618033, 1, 0)
        Ico.VerticesOriginales(5) = New Punto3D(-1.618033, 1, 0)
        Ico.VerticesOriginales(6) = New Punto3D(-1.618033, -1, 0)
        Ico.VerticesOriginales(7) = New Punto3D(1.618033, -1, 0)
        Ico.VerticesOriginales(8) = New Punto3D(0, 1.618033, 1)
        Ico.VerticesOriginales(9) = New Punto3D(0, -1.618033, 1)
        Ico.VerticesOriginales(10) = New Punto3D(0, -1.618033, -1)
        Ico.VerticesOriginales(11) = New Punto3D(0, 1.618033, -1)

        Ico.Caras(0).Vertices(2) = 11
        Ico.Caras(0).Vertices(1) = 8
        Ico.Caras(0).Vertices(0) = 4

        Ico.Caras(1).Vertices(2) = 6
        Ico.Caras(1).Vertices(1) = 10
        Ico.Caras(1).Vertices(0) = 9

        Ico.Caras(2).Vertices(2) = 5
        Ico.Caras(2).Vertices(1) = 6
        Ico.Caras(2).Vertices(0) = 3

        Ico.Caras(3).Vertices(2) = 4
        Ico.Caras(3).Vertices(1) = 7
        Ico.Caras(3).Vertices(0) = 1

        Ico.Caras(4).Vertices(2) = 8
        Ico.Caras(4).Vertices(1) = 0
        Ico.Caras(4).Vertices(0) = 4

        Ico.Caras(5).Vertices(2) = 2
        Ico.Caras(5).Vertices(1) = 10
        Ico.Caras(5).Vertices(0) = 6

        Ico.Caras(6).Vertices(2) = 11
        Ico.Caras(6).Vertices(1) = 4
        Ico.Caras(6).Vertices(0) = 1

        Ico.Caras(7).Vertices(2) = 6
        Ico.Caras(7).Vertices(1) = 9
        Ico.Caras(7).Vertices(0) = 3

        Ico.Caras(8).Vertices(2) = 8
        Ico.Caras(8).Vertices(1) = 11
        Ico.Caras(8).Vertices(0) = 5

        Ico.Caras(9).Vertices(2) = 9
        Ico.Caras(9).Vertices(1) = 10
        Ico.Caras(9).Vertices(0) = 7

        Ico.Caras(10).Vertices(2) = 5
        Ico.Caras(10).Vertices(1) = 11
        Ico.Caras(10).Vertices(0) = 2

        Ico.Caras(11).Vertices(2) = 9
        Ico.Caras(11).Vertices(1) = 7
        Ico.Caras(11).Vertices(0) = 0

        Ico.Caras(12).Vertices(2) = 7
        Ico.Caras(12).Vertices(1) = 4
        Ico.Caras(12).Vertices(0) = 0

        Ico.Caras(13).Vertices(2) = 6
        Ico.Caras(13).Vertices(1) = 5
        Ico.Caras(13).Vertices(0) = 2

        Ico.Caras(14).Vertices(2) = 8
        Ico.Caras(14).Vertices(1) = 5
        Ico.Caras(14).Vertices(0) = 3

        Ico.Caras(15).Vertices(2) = 7
        Ico.Caras(15).Vertices(1) = 10
        Ico.Caras(15).Vertices(0) = 1

        Ico.Caras(16).Vertices(2) = 8
        Ico.Caras(16).Vertices(1) = 3
        Ico.Caras(16).Vertices(0) = 0

        Ico.Caras(17).Vertices(2) = 2
        Ico.Caras(17).Vertices(1) = 1
        Ico.Caras(17).Vertices(0) = 10

        Ico.Caras(18).Vertices(2) = 3
        Ico.Caras(18).Vertices(1) = 9
        Ico.Caras(18).Vertices(0) = 0

        Ico.Caras(19).Vertices(2) = 2
        Ico.Caras(19).Vertices(1) = 11
        Ico.Caras(19).Vertices(0) = 1

        Ico.CentroOriginal = New Punto3D(0, 0, 0)

        Ico.TriangulacionCaras()
        Ico.EstablecerCarasVertices()
        Return Ico
    End Function

    Public Function PRIM_Dodecaedro() As Poliedro
        Dim d As Poliedro
        d = New Poliedro(20, 12)
        d.EsCurvo = False

        For i As Integer = 0 To 11
            d.Caras(i) = New Cara(5)
        Next


        d.VerticesOriginales(0) = New Punto3D(1, 1, 1)
        d.VerticesOriginales(1) = New Punto3D(1, 1, -1)
        d.VerticesOriginales(2) = New Punto3D(-1, 1, -1)
        d.VerticesOriginales(3) = New Punto3D(-1, 1, 1)
        d.VerticesOriginales(4) = New Punto3D(1, -1, 1)
        d.VerticesOriginales(5) = New Punto3D(1, -1, -1)
        d.VerticesOriginales(6) = New Punto3D(-1, -1, -1)
        d.VerticesOriginales(7) = New Punto3D(-1, -1, 1)
        d.VerticesOriginales(8) = New Punto3D(0.618033, 0, 1.618033)
        d.VerticesOriginales(9) = New Punto3D(0.618033, 0, -1.618033)
        d.VerticesOriginales(10) = New Punto3D(-0.618033, 0, -1.618033)
        d.VerticesOriginales(11) = New Punto3D(-0.618033, 0, 1.618033)
        d.VerticesOriginales(12) = New Punto3D(1.618033, 0.618033, 0)
        d.VerticesOriginales(13) = New Punto3D(-1.618033, 0.618033, 0)
        d.VerticesOriginales(14) = New Punto3D(-1.618033, -0.618033, 0) 'Valores:'0.618033
        d.VerticesOriginales(15) = New Punto3D(1.618033, -0.618033, 0) '1.618033
        d.VerticesOriginales(16) = New Punto3D(0, 1.618033, 0.618033)
        d.VerticesOriginales(17) = New Punto3D(0, -1.618033, 0.618033)
        d.VerticesOriginales(18) = New Punto3D(0, -1.618033, -0.618033)
        d.VerticesOriginales(19) = New Punto3D(0, 1.618033, -0.618033)

        d.Caras(0).Vertices(4) = 12
        d.Caras(0).Vertices(3) = 1
        d.Caras(0).Vertices(2) = 19
        d.Caras(0).Vertices(1) = 16
        d.Caras(0).Vertices(0) = 0

        d.Caras(1).Vertices(4) = 18
        d.Caras(1).Vertices(3) = 17
        d.Caras(1).Vertices(2) = 7
        d.Caras(1).Vertices(1) = 14
        d.Caras(1).Vertices(0) = 6

        d.Caras(2).Vertices(4) = 7
        d.Caras(2).Vertices(3) = 17
        d.Caras(2).Vertices(2) = 4
        d.Caras(2).Vertices(1) = 8
        d.Caras(2).Vertices(0) = 11

        d.Caras(3).Vertices(4) = 9
        d.Caras(3).Vertices(3) = 10
        d.Caras(3).Vertices(2) = 2
        d.Caras(3).Vertices(1) = 19
        d.Caras(3).Vertices(0) = 1

        d.Caras(4).Vertices(4) = 14
        d.Caras(4).Vertices(3) = 7
        d.Caras(4).Vertices(2) = 11
        d.Caras(4).Vertices(1) = 3
        d.Caras(4).Vertices(0) = 13

        d.Caras(5).Vertices(4) = 9
        d.Caras(5).Vertices(3) = 1
        d.Caras(5).Vertices(2) = 12
        d.Caras(5).Vertices(1) = 15
        d.Caras(5).Vertices(0) = 5

        d.Caras(6).Vertices(4) = 6
        d.Caras(6).Vertices(3) = 14
        d.Caras(6).Vertices(2) = 13
        d.Caras(6).Vertices(1) = 2
        d.Caras(6).Vertices(0) = 10

        d.Caras(7).Vertices(4) = 8
        d.Caras(7).Vertices(3) = 4
        d.Caras(7).Vertices(2) = 15
        d.Caras(7).Vertices(1) = 12
        d.Caras(7).Vertices(0) = 0

        d.Caras(8).Vertices(4) = 18
        d.Caras(8).Vertices(3) = 6
        d.Caras(8).Vertices(2) = 10
        d.Caras(8).Vertices(1) = 9
        d.Caras(8).Vertices(0) = 5

        d.Caras(9).Vertices(4) = 11
        d.Caras(9).Vertices(3) = 8
        d.Caras(9).Vertices(2) = 0
        d.Caras(9).Vertices(1) = 16
        d.Caras(9).Vertices(0) = 3

        d.Caras(10).Vertices(4) = 19
        d.Caras(10).Vertices(3) = 2
        d.Caras(10).Vertices(2) = 13
        d.Caras(10).Vertices(1) = 3
        d.Caras(10).Vertices(0) = 16

        d.Caras(11).Vertices(4) = 4
        d.Caras(11).Vertices(3) = 17
        d.Caras(11).Vertices(2) = 18
        d.Caras(11).Vertices(1) = 5
        d.Caras(11).Vertices(0) = 15

        d.CentroOriginal = New Punto3D(0, 0, 0)
        d.TriangulacionCaras()
        d.EstablecerCarasVertices()
        Return d
    End Function
#End Region
End Module
