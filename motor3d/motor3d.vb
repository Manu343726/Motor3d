Imports System.Math
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Threading
Imports System.Windows.Forms
Imports Dx_Lib32Wrapper


#Region "Enumeraciones"
Public Enum EnumModo
    Normal
    TestSombreado
    InformacionCompleta
    InformacionTransformaciones
    InformacionSombreadoIluminacion
End Enum

Public Enum EnumEje
    EjeX = 0
    EjeY = 1
    EjeZ = 2
End Enum

Public Enum TipoRepresentacion
    Conica
    OrtogonalXOY
    OrtogonalZOX
    OrtogonalZOY
    OrtogonalGeneral
    OrtogonalLibre
    Isometrica
End Enum

Public Enum Shader
    WireFrameShading
    FlatShading
    GouraudShading
    PhongShading
End Enum

Public Enum AlgoritmoOrdenacion
    BubbleSort
    QuickSort
    ListSort
    BogoSort
End Enum

Public Enum DepuracionActiva
    Buffer
    Dibujado
    Sombreado
End Enum

Public Enum TipoFoco
    Puntual
    Focal
    Plana
End Enum

#End Region

#Region "Varios"

Public Structure Foco
    Private mCoordenadas As Punto3D
    Public Color As Color
    Public Brillo As Double
    Public Tipo As TipoFoco
    Private mVectorDireccion As Vector3D
    Public Radio As Double
    Public Delta As Single
    Private mPLano As Plano3D

    Public ReadOnly Property Plano() As Plano3D
        Get
            Return mPLano
        End Get
    End Property

    Public Property Coordenadas() As Punto3D
        Get
            Return mCoordenadas
        End Get
        Set(ByVal value As Punto3D)
            mCoordenadas = value
            mPLano = New Plano3D(mVectorDireccion, mCoordenadas)
        End Set
    End Property

    Public Property VectorDireccion() As Vector3D
        Get
            Return mVectorDireccion
        End Get
        Set(ByVal value As Vector3D)
            mVectorDireccion = value
            mPLano = New Plano3D(mVectorDireccion, mCoordenadas)
        End Set
    End Property

    Public Sub New(ByVal ValCoordenadas As Punto3D, ByVal ValColor As Color, ByVal ValBrillo As Double)
        Coordenadas = ValCoordenadas
        Color = ValColor
        If ValBrillo < 0 Then ValBrillo = 0
        If ValBrillo > 1 Then ValBrillo = 1
        Brillo = ValBrillo
    End Sub

    Public Function EsVisible(ByRef Punto As Punto3D, ByRef Direccion As Vector3D) As Boolean
        Dim V As Vector3D = New Vector3D(Punto, Coordenadas)
        Dim R As Recta3D
        Dim P As Punto3D
        Dim Angulo As Single
        Select Case Tipo
            Case TipoFoco.Puntual
                Return (V * Direccion) < 0
            Case TipoFoco.Focal
                Angulo = CALC3D_AnguloVectores3D(VectorDireccion, V)
                'Debug.WriteLine("ANGULO=" & Angulo & "º DELTA=" & Delta & "º (REAL=" & 180 - Delta & "º)")
                Return (mVectorDireccion * Direccion) > 0 AndAlso Angulo > (180 - Delta)
            Case TipoFoco.Plana
                R = New Recta3D(mVectorDireccion, Punto)
                P = CALC3D_InterseccionPlanoRecta(mPLano, R)
                Return (mVectorDireccion * Direccion) > 0 AndAlso CALC3D_Distancia(P, mCoordenadas) <= Radio
        End Select
    End Function

    Public Function RepresentacionFoco(ByRef Camara As Camara) As Point()
        Dim P(3) As Punto3D
        Dim PR(3) As Point
        Return PR
    End Function
End Structure

Public Structure Camara
    Private mPosicion As Punto3D
    Private mVectorDireccion As Vector3D
    Private mDistancia As Double
    Private mRepresentacion As TipoRepresentacion

    Private mMatrizTransformacion(,) As Double
    Private mMatrizProyeccion(,) As Double

    Private AnguloX, AnguloY, AnguloZ As Single

    Public ReadOnly Property MatrizTransformacion() As Double(,)
        Get
            Return mMatrizTransformacion
        End Get
    End Property

    Public ReadOnly Property MatrizProyeccion() As Double(,)
        Get
            Return mMatrizProyeccion
        End Get
    End Property

    Public ReadOnly Property InversaMatrizTransformacion() As Double(,)
        Get
            Return CALCMATR_MatrizInversa(mMatrizTransformacion)
        End Get
    End Property

    Public ReadOnly Property Representacion() As TipoRepresentacion
        Get
            Return mRepresentacion
        End Get
    End Property

    Public ReadOnly Property MatrizTotal() As Double(,)
        Get
            Return CALCMATR_ProductoMatricial(mMatrizProyeccion, mMatrizTransformacion)
        End Get
    End Property

    Public ReadOnly Property Distancia() As Double
        Get
            Return mDistancia
        End Get
    End Property

    Public Property Posicion() As Punto3D
        Get
            Return mPosicion
        End Get
        Set(ByVal value As Punto3D)
            mPosicion = value
            RecalcularMatrizProyeccion()
        End Set
    End Property
    Public ReadOnly Property VectorDireccion() As Vector3D
        Get
            Return mVectorDireccion
        End Get
    End Property

    Private Sub RecalcularMatrizProyeccion()
        mMatrizProyeccion(0, 0) = mPosicion.Z
        mMatrizProyeccion(0, 1) = 0
        mMatrizProyeccion(0, 2) = -mPosicion.X
        mMatrizProyeccion(0, 3) = 0
        mMatrizProyeccion(1, 0) = 0
        mMatrizProyeccion(1, 1) = mPosicion.Z
        mMatrizProyeccion(1, 2) = -mPosicion.Y
        mMatrizProyeccion(1, 3) = 0
        mMatrizProyeccion(2, 0) = 0
        mMatrizProyeccion(2, 1) = 0
        mMatrizProyeccion(2, 2) = 0
        mMatrizProyeccion(2, 3) = 0
        mMatrizProyeccion(3, 0) = 0
        mMatrizProyeccion(3, 1) = 0
        mMatrizProyeccion(3, 2) = -1
        mMatrizProyeccion(3, 3) = mPosicion.Z
    End Sub

    Public Sub EstablecerVectorDireccion(ByVal NuevoVector As Vector3D)
        mVectorDireccion = NuevoVector
    End Sub

    Public Sub EstablecerRepresentacion(ByVal NuevaRepresentacion As TipoRepresentacion)
        CalculoDatosRepresentacion(NuevaRepresentacion)
        mRepresentacion = NuevaRepresentacion
    End Sub

    Public Sub EstablecerDistancia(ByVal NuevaDistancia As Double)
        mDistancia = NuevaDistancia
    End Sub

    Public Sub EstablecerAngulo(ByVal Eje As EnumEje, ByVal Angulo As Single)
        Select Case Eje
            Case EnumEje.EjeX
                mMatrizTransformacion = CALCMATR_ProductoMatricial(CALCTRANS3D_RotacionAlrededorEje(Eje, -AnguloX), mMatrizTransformacion)
                AnguloX = Angulo
            Case EnumEje.EjeY
                mMatrizTransformacion = CALCMATR_ProductoMatricial(CALCTRANS3D_RotacionAlrededorEje(Eje, -AnguloY), mMatrizTransformacion)
                AnguloY = Angulo
            Case EnumEje.EjeZ
                mMatrizTransformacion = CALCMATR_ProductoMatricial(CALCTRANS3D_RotacionAlrededorEje(Eje, -AnguloZ), mMatrizTransformacion)
                AnguloZ = Angulo
        End Select
        mMatrizTransformacion = CALCMATR_ProductoMatricial(CALCTRANS3D_RotacionAlrededorEje(Eje, Angulo), mMatrizTransformacion)
    End Sub


    Public Sub EstablecerPosicion(ByVal NuevaPosicion As Punto3D)
        Dim p As Punto3D
        mMatrizTransformacion = CALCMATR_ProductoMatricial(CALCTRANS3D_Traslacion(-mPosicion.X, -mPosicion.Y, -mPosicion.Z), mMatrizTransformacion)
        mPosicion = NuevaPosicion
        mMatrizTransformacion = CALCMATR_ProductoMatricial(CALCTRANS3D_Traslacion(NuevaPosicion.X, NuevaPosicion.Y, NuevaPosicion.Z), mMatrizTransformacion)
        p = Posicion
    End Sub

    Public Sub Reiniciar()
        ReestablecerMatrices()
    End Sub

    Private Sub ReestablecerMatrices()
        mMatrizTransformacion = CALCMATR_MatrizUnitaria(4)
    End Sub

    Private Sub CalculoDatosRepresentacion(ByVal TipoRepresentacion As TipoRepresentacion)
        ReestablecerMatrices()
        Select Case TipoRepresentacion
            Case TipoRepresentacion.Isometrica
                mDistancia = 1000000
                EstablecerAngulo(EnumEje.EjeX, 135)
                EstablecerAngulo(EnumEje.EjeZ, 45)
                mVectorDireccion = New Vector3D(-1, -1, -1)
                EstablecerPosicion(New Punto3D(1000000, 1000000, 1000000))
            Case TipoRepresentacion.OrtogonalZOX
                mDistancia = 1000000
                EstablecerAngulo(EnumEje.EjeX, 90)
                EstablecerPosicion(New Punto3D(0, 1000000, 0))
                mVectorDireccion = New Vector3D(0, -1, 0)
            Case TipoRepresentacion.OrtogonalZOY
                mDistancia = 1000000
                EstablecerAngulo(EnumEje.EjeX, 90)
                EstablecerAngulo(EnumEje.EjeZ, 90)
                EstablecerPosicion(New Punto3D(1000000, 0, 0))
                mVectorDireccion = New Vector3D(-1, 0, 0)
            Case TipoRepresentacion.OrtogonalXOY
                mDistancia = 1000000
                EstablecerAngulo(EnumEje.EjeX, 180)
                EstablecerPosicion(New Punto3D(0, 0, 1000000))
                mVectorDireccion = New Vector3D(0, 0, -1)
            Case TipoRepresentacion.Conica
                'mDistancia = 1000
                'EstablecerAngulo(EnumEje.EjeX, 90)
                'EstablecerPosicion(New Punto3D(0, 1000, 0))
                'mVectorDireccion = New Vector3D(0, -1, 0)
        End Select
    End Sub

    Public Sub IncrementarCoordenadaPosicion(ByVal Coordenada As EnumEje, ByVal Incremento As Double)
        Select Case Coordenada
            Case EnumEje.EjeX
                mPosicion.X += Incremento
                mMatrizTransformacion = CALCMATR_ProductoMatricial(CALCTRANS3D_Traslacion(Incremento, 0, 0), mMatrizTransformacion)
            Case EnumEje.EjeY
                mPosicion.Y += Incremento
                mMatrizTransformacion = CALCMATR_ProductoMatricial(CALCTRANS3D_Traslacion(0, Incremento, 0), mMatrizTransformacion)
            Case EnumEje.EjeZ
                mPosicion.Z += Incremento
                mMatrizTransformacion = CALCMATR_ProductoMatricial(CALCTRANS3D_Traslacion(0, 0, Incremento), mMatrizTransformacion)
        End Select
    End Sub

    Public Sub New(ByVal ValPosicion As Punto3D, ByVal Direccion As Vector3D, ByVal ValDistancia As Double)
        mPosicion = ValPosicion
        mVectorDireccion = Direccion
        mDistancia = ValDistancia
    End Sub
End Structure

Public Structure DatosIluminacion
    Private mKDistancia1, mKDistancia2, mKDistancia3 As Double
    Private mAportacionAmbiente, mAportacionDifusa, mAportacionEspecular As Double
    Private mExponenteEspecular As Integer

    Public Sub EstablecerAportaciones(ByVal ValAmbiente As Double, ByVal ValDifusa As Double, ByVal ValEspecular As Double)
        If ValAmbiente < 0 Then ValAmbiente = 0
        If ValAmbiente > 1 Then ValAmbiente = 1

        If ValDifusa < 0 Then ValDifusa = 0
        If ValDifusa > 1 Then ValDifusa = 1

        If ValEspecular < 0 Then ValEspecular = 0
        If ValEspecular > 1 Then ValEspecular = 1

        mAportacionAmbiente = ValAmbiente
        mAportacionDifusa = ValDifusa
        mAportacionEspecular = ValEspecular
    End Sub

    Public Sub EstablecerConstantesDistancia(ByVal K1 As Double, ByVal K2 As Double, ByVal K3 As Double)
        If K1 < 0 Then K1 = 0
        If K1 > 1 Then K1 = 1

        If K2 < 0 Then K2 = 0
        If K2 > 1 Then K2 = 1

        If K3 < 0 Then K3 = 0
        If K3 > 1 Then K3 = 1

        mKDistancia1 = K1
        mKDistancia2 = K2
        mKDistancia3 = K3
    End Sub

    Public Sub EstablecerExponenteEspecular(ByVal ValExponente As Integer)
        If ValExponente < 1 Then ValExponente = 1
        mExponenteEspecular = ValExponente
    End Sub

    Public ReadOnly Property ConstanteDistancia1() As Double
        Get
            Return mKDistancia1
        End Get
    End Property
    Public ReadOnly Property ConstanteDistancia2() As Double
        Get
            Return mKDistancia2
        End Get
    End Property
    Public ReadOnly Property ConstanteDistancia3() As Double
        Get
            Return mKDistancia3
        End Get
    End Property

    Public ReadOnly Property AportacionAmbiente() As Double
        Get
            Return mAportacionAmbiente
        End Get
    End Property
    Public ReadOnly Property AportacionDifusa() As Double
        Get
            Return mAportacionDifusa
        End Get
    End Property
    Public ReadOnly Property AportacionEspecular() As Double
        Get
            Return mAportacionEspecular
        End Get
    End Property

    Public ReadOnly Property ExponenteEspecular() As Integer
        Get
            Return mExponenteEspecular
        End Get
    End Property
End Structure
#End Region

#Region "TiposEspacio2D"
Public Structure Vector2D
    Public X As Double
    Public Y As Double

    Public ReadOnly Property Modulo() As Double
        Get
            Return Math.Sqrt((X ^ 2) + (Y ^ 2))
        End Get
    End Property

    Public ReadOnly Property VectorUnitario() As Vector2D
        Get
            Dim Retorno As Vector2D

            If Modulo <> 0 Then
                Retorno.X = X / Modulo
                Retorno.Y = Y / Modulo
            Else
                Retorno.X = X
                Retorno.Y = Y
            End If

            Return Retorno
        End Get
    End Property

    Public Sub New(ByVal ValX As Double, ByVal ValY As Double)
        X = ValX
        Y = ValY
    End Sub

    Public Sub New(ByVal P1 As Point, ByVal P2 As Point)
        X = P2.X - P1.X
        Y = P2.Y - P1.Y
    End Sub

    Public Overrides Function ToString() As String
        Return "{" & X & "," & Y & "}"
    End Function
End Structure

Public Structure Recta2D
    Public ReadOnly A As Double
    Public ReadOnly B As Double
    Public ReadOnly C As Double

    Private mPuntoDirector As Point
    Private mVectorDirector As Vector2D

    Public ReadOnly Property VectorDirector() As Vector2D
        Get
            Return mVectorDirector
        End Get
    End Property

    Public ReadOnly Property PuntoDirector() As Point
        Get
            Return mPuntoDirector
        End Get
    End Property

    Public Sub New(ByVal P1 As Point, ByVal P2 As Point)
        Dim V As Vector2D

        V.X = P2.X - P1.X
        V.Y = P2.Y - P1.Y

        A = V.Y
        B = -V.X
        C = -((A * (P1.X)) + (B * (P1.Y)))

        mVectorDirector = New Vector2D(-B, A)
        mPuntoDirector = P1
    End Sub

    Public Sub New(ByVal Vector As Vector2D, ByVal Punto As Point)
        A = Vector.Y
        B = -Vector.X
        C = -((A * (Punto.X)) + (B * (Punto.Y)))
        mVectorDirector = Vector
        mPuntoDirector = Punto
    End Sub

    Public Function Pertenece(ByVal Punto As Point) As Boolean
        Return ((A * Punto.X) + (B * Punto.Y) + C = 0)
    End Function

    Public Function PuntoDeLaRecta(ByVal Parametro As Double) As Point
        Return New Point(mPuntoDirector.X + (mVectorDirector.X * Parametro), mPuntoDirector.Y + (mVectorDirector.Y * Parametro))
    End Function

    Public Function ParametroDePunto(ByVal Punto As Point) As Double
        If Pertenece(Punto) Then
            Return ((Punto.X - mPuntoDirector.X) / mVectorDirector.X)
        Else
            Return 0
        End If
    End Function

    Public Function Funcion(ByVal X As Double) As Double
        If B <> 0 Then
            Return ((A * X) + C) / -B
        Else
            If ((A * X) + C) = 0 Then
                Return 0
            Else
                Return 0
            End If
        End If
    End Function

    Public Overrides Function ToString() As String
        Return A & "x+" & B & "y+" & C & "=0"
    End Function
End Structure
#End Region
#Region "TiposEspacio3D"

Public Interface Coordenadas3D
    Property X() As Double
    Property Y() As Double
    Property Z() As Double

    Function Representacion2D(ByVal Camara As Camara) As Point
    Function ToString() As String
End Interface

Public Structure Punto3D
    Implements Coordenadas3D

    Private m_X, m_Y, m_Z As Double
    Private mNulo As Boolean

    Public Shared Operator <>(ByVal P1 As Punto3D, ByVal P2 As Punto3D) As Boolean
        Return Not (P1.X = P2.X AndAlso P1.Y = P2.Y AndAlso P1.Z = P2.Z)
    End Operator
    Public Shared Operator =(ByVal P1 As Punto3D, ByVal P2 As Punto3D) As Boolean
        Return (P1.X = P2.X AndAlso P1.Y = P2.Y AndAlso P1.Z = P2.Z)
    End Operator

    Public Shared Operator +(ByVal P1 As Punto3D, ByVal P2 As Punto3D) As Punto3D
        Return New Punto3D(P1.X + P2.X, P1.Y + P2.Y, P1.Z + P2.Z)
    End Operator

    Public Shared Operator *(ByVal Punto As Punto3D, ByVal Numero As Double) As Punto3D
        Return New Punto3D(Punto.X * Numero, Punto.Y * Numero, Punto.Z * Numero)
    End Operator

    Public Shared Operator *(ByVal Numero As Double, ByVal Punto As Punto3D) As Punto3D
        Return New Punto3D(Punto.X * Numero, Punto.Y * Numero, Punto.Z * Numero)
    End Operator

    Public Shared Operator /(ByVal Punto As Punto3D, ByVal Numero As Double) As Punto3D
        If Numero = 0 Then Return Punto
        Return New Punto3D(Punto.X / Numero, Punto.Y / Numero, Punto.Z / Numero)
    End Operator

    Public Shared Operator -(ByVal P1 As Punto3D, ByVal P2 As Punto3D) As Punto3D
        Return New Punto3D(P1.X - P2.X, P1.Y - P2.Y, P1.Z - P2.Z)
    End Operator

    Public Function Representacion2D(ByVal Camara As Camara) As System.Drawing.Point Implements Coordenadas3D.Representacion2D
        Return CALCREP_Representacion(Camara, Me)
    End Function

    Public Overrides Function ToString() As String Implements Coordenadas3D.ToString
        Return "{X=" & FormatNumber(X, 2) & ";Y=" & FormatNumber(Y, 2) & ";Z=" & FormatNumber(Z, 2) & "}"
    End Function

    Public Sub New(ByVal ValX As Double, ByVal ValY As Double, ByVal ValZ As Double)
        m_X = ValX
        m_Y = ValY
        m_Z = ValZ
    End Sub

    Public Property X() As Double Implements Coordenadas3D.X
        Get
            Return m_X
        End Get
        Set(ByVal value As Double)
            m_X = value
        End Set
    End Property

    Public Property Y() As Double Implements Coordenadas3D.Y
        Get
            Return m_Y
        End Get
        Set(ByVal value As Double)
            m_Y = value
        End Set
    End Property

    Public Property Z() As Double Implements Coordenadas3D.Z
        Get
            Return m_Z
        End Get
        Set(ByVal value As Double)
            m_Z = value
        End Set
    End Property

    Public ReadOnly Property RepresentacionMatricial() As Double()
        Get
            Dim Retorno(3) As Double

            Retorno(0) = Me.X
            Retorno(1) = Me.Y
            Retorno(2) = Me.Z
            Retorno(3) = 1

            Return Retorno
        End Get
    End Property

    Public Property Nulo() As Boolean
        Get
            Return mNulo
        End Get
        Set(ByVal value As Boolean)
            mNulo = value
        End Set
    End Property
End Structure

Public Structure Vector3D

#Region "Hereda de Punto3D"
    Implements Coordenadas3D

    Private m_X, m_Y, m_Z As Double

    Public Shared Operator <>(ByVal V1 As Vector3D, ByVal V2 As Vector3D) As Boolean
        Return Not (V1.X = V2.X AndAlso V1.Y = V2.Y AndAlso V1.Z = V2.Z)
    End Operator
    Public Shared Operator =(ByVal V1 As Vector3D, ByVal V2 As Vector3D) As Boolean
        Return (V1.X = V2.X AndAlso V1.Y = V2.Y AndAlso V1.Z = V2.Z)
    End Operator

    Public Shared Operator +(ByVal V1 As Vector3D, ByVal V2 As Vector3D) As Vector3D
        Return New Vector3D(V1.X + V2.X, V1.Y + V2.Y, V1.Z + V2.Z)
    End Operator

    Public Shared Operator -(ByVal V1 As Vector3D, ByVal V2 As Vector3D) As Vector3D
        Return New Vector3D(V1.X - V2.X, V1.Y - V2.Y, V1.Z - V2.Z)
    End Operator

    Public Shared Operator *(ByVal Numero As Double, ByVal Vector As Vector3D) As Vector3D
        Return New Vector3D(Numero * Vector.X, Numero * Vector.Y, Numero * Vector.Z)
    End Operator

    Public Shared Operator *(ByVal Vector As Vector3D, ByVal Numero As Double) As Vector3D
        Return New Vector3D(Numero * Vector.X, Numero * Vector.Y, Numero * Vector.Z)
    End Operator

    Public Shared Operator *(ByVal V1 As Vector3D, ByVal V2 As Vector3D) As Double
        Return CALC3D_ProductoEscalar3D(V1, V2)
    End Operator

    Public Shared Operator &(ByVal V1 As Vector3D, ByVal V2 As Vector3D) As Vector3D
        Return CALC3D_ProductoVectorial(V1, V2)
    End Operator

    Public Shared Operator ^(ByVal V1 As Vector3D, ByVal V2 As Vector3D) As Single
        Return CALC3D_AnguloVectores3D(V1, V2)
    End Operator

    Public Function Representacion2D(ByVal Camara As Camara) As System.Drawing.Point Implements Coordenadas3D.Representacion2D
        Return CALCREP_Representacion(Camara, Me.PuntoPosicion)
    End Function

    Public Overrides Function ToString() As String Implements Coordenadas3D.ToString
        Return "{X=" & FormatNumber(X, 2) & ";Y=" & FormatNumber(Y, 2) & ";Z=" & FormatNumber(Z, 2) & "}"
    End Function

    Public Property X() As Double Implements Coordenadas3D.X
        Get
            Return m_X
        End Get
        Set(ByVal value As Double)
            m_X = value
        End Set
    End Property

    Public Property Y() As Double Implements Coordenadas3D.Y
        Get
            Return m_Y
        End Get
        Set(ByVal value As Double)
            m_Y = value
        End Set
    End Property

    Public Property Z() As Double Implements Coordenadas3D.Z
        Get
            Return m_Z
        End Get
        Set(ByVal value As Double)
            m_Z = value
        End Set
    End Property
#End Region

    Public ReadOnly Property DireccionPolar() As Point
        Get
            Dim Axy, Axz As Single
            Select Case X
                Case Is > 0
                    If Y >= 0 Then
                        Axy = Atan(Y / X)
                    Else
                        Axy = (2 * PI) + Atan(Y / X)
                    End If
                Case Is = 0
                    Axy = (PI / 2) * Sign(Y)
                Case Is < 0
                    Axy = PI + Atan(Y / X)
            End Select

            Select Case Z
                Case Is > 0
                    Axz = Atan(Sqrt((X ^ 2) + (Y ^ 2)) / Z)
                Case Is = 0
                    Axz = PI / 2
                Case Is < 0
                    Axz = PI + Atan(Sqrt((X ^ 2) + (Y ^ 2)) / Z)
            End Select

            Return New Point(Axz * 180 / PI, Axy * 180 / PI)
        End Get
    End Property

    Public ReadOnly Property Modulo() As Double
        Get
            Return Sqrt((X ^ 2) + (Y ^ 2) + (Z ^ 2))
        End Get
    End Property

    Public ReadOnly Property ProyeccionZOX() As Vector3D
        Get
            Return New Vector3D(X, 0, Z)
        End Get
    End Property

    Public ReadOnly Property ProyeccionZOY() As Vector3D
        Get
            Return New Vector3D(0, Y, Z)
        End Get
    End Property

    Public ReadOnly Property ProyeccionXOY() As Vector3D
        Get
            Return New Vector3D(X, Y, 0)
        End Get
    End Property

    Public ReadOnly Property VectorUnitario() As Vector3D
        Get
            Dim Retorno As Vector3D

            If Modulo <> 0 Then
                Retorno.X = X / Modulo
                Retorno.Y = Y / Modulo
                Retorno.Z = Z / Modulo
            Else
                Retorno.X = X
                Retorno.Y = Y
                Retorno.Z = Z
            End If

            Return Retorno
        End Get
    End Property

    Public ReadOnly Property PuntoPosicion() As Punto3D
        Get
            Return New Punto3D(X, Y, Z)
        End Get
    End Property

    Public Sub New(ByVal ValX As Double, ByVal ValY As Double, ByVal ValZ As Double)
        X = ValX
        Y = ValY
        Z = ValZ
    End Sub

    Public Sub New(ByVal P1 As Punto3D, ByVal P2 As Punto3D)
        X = P2.X - P1.X
        Y = P2.Y - P1.Y
        Z = P2.Z - P1.Z
    End Sub

    Public Function Pendiente(ByVal Eje As EnumEje) As Double
        Dim V As Vector3D
        Select Case Eje
            Case EnumEje.EjeX
                V = New Vector3D(1, 0, 0)
            Case EnumEje.EjeY
                V = New Vector3D(0, 1, 0)
            Case EnumEje.EjeZ
                V = New Vector3D(0, 0, 1)
        End Select

        Return (Me & V).Modulo
    End Function

    Public Sub Normalizar()
        Dim m As Double = Modulo
        If m = 1 Then Exit Sub
        m_X /= m
        m_Y /= m
        m_Z /= m
    End Sub

End Structure

Public Structure Plano3D
    Public ReadOnly A As Double
    Public ReadOnly B As Double
    Public ReadOnly C As Double
    Public ReadOnly D As Double

    Public ReadOnly Property VectorNormal() As Vector3D
        Get
            Return New Vector3D(A, B, C).VectorUnitario
        End Get
    End Property

    Public Shadows Function ToString(ByVal Decimales As Integer) As String
        Return FormatNumber(A, Decimales) & "X+" & FormatNumber(B, Decimales) & "Y+" & FormatNumber(C, Decimales) & "Z+" & FormatNumber(D, Decimales) & "=0"
    End Function


    Public Sub New(ByVal ValA As Double, ByVal ValB As Double, ByVal ValC As Double, ByVal ValD As Double)
        A = ValA
        B = ValB
        C = ValC
        D = ValD
    End Sub

    Public Sub New(ByVal P1 As Punto3D, ByVal P2 As Punto3D, ByVal P3 As Punto3D)
        Dim V1, V2, ABC As Vector3D

        V1 = New Vector3D(P2.X - P1.X, P2.Y - P1.Y, P2.Z - P1.Z).VectorUnitario
        V2 = New Vector3D(P3.X - P1.X, P3.Y - P1.Y, P3.Z - P1.Z).VectorUnitario

        ABC = CALC3D_ProductoVectorial(V1, V2)

        A = ABC.X
        B = ABC.Y
        C = ABC.Z
        D = -((P1.X * A) + (P1.Y * B) + (P1.Z * C))
    End Sub

    Public Sub New(ByVal V1 As Vector3D, ByVal V2 As Vector3D, ByVal P As Punto3D)
        Dim ABC As Vector3D

        ABC = CALC3D_ProductoVectorial(V1.VectorUnitario, V2.VectorUnitario)

        A = ABC.X
        B = ABC.Y
        C = ABC.Z
        D = -((P.X * A) + (P.Y * B) + (P.Z * C))
    End Sub

    Public Sub New(ByVal VectorNormal As Vector3D, ByVal Punto As Punto3D)
        VectorNormal = VectorNormal.VectorUnitario
        A = VectorNormal.X
        B = VectorNormal.Y
        C = VectorNormal.Z
        D = -((Punto.X * A) + (Punto.Y * B) + (Punto.Z * C))
    End Sub

    Public Function PuntoDelPlano(ByVal Parametro1 As Double, ByVal Parametro2 As Double) As Punto3D
        Dim P1, P2, P3 As Punto3D
        Dim V1, V2 As Vector3D
        Dim Retorno As Punto3D

        P1.X = 0
        P1.Y = 0
        P1.Z = (-D / C)

        P2.X = 0
        P2.Z = 0
        P2.Y = (-D / B)

        P3.Y = 0
        P3.Z = 0
        P3.X = (-D / A)

        V1 = New Vector3D(P1, P2)
        V2 = New Vector3D(P1, P3)

        Retorno.X = P1.X + (Parametro1 * V1.X) + (Parametro2 * V2.X)
        Retorno.Y = P1.Y + (Parametro1 * V1.Y) + (Parametro2 * V2.Y)
        Retorno.Z = P1.Z + (Parametro1 * V1.Z) + (Parametro2 * V2.Z)

        Return Retorno
    End Function

    Public Function Pertenece(ByVal Punto As Punto3D) As Boolean
        Return ((A * Punto.X) + (B * Punto.Y) + (C * Punto.Z) + D = 0)
    End Function

End Structure

Public Structure Recta3D
    Private V As Vector3D
    Private P As Punto3D
    Private Plano1 As Plano3D
    Private Plano2 As Plano3D

    Public ReadOnly Property VectorDirector() As Vector3D
        Get
            Return V
        End Get
    End Property
    Public ReadOnly Property PuntoDirector() As Punto3D
        Get
            Return P
        End Get
    End Property
    Public ReadOnly Property PlanoCorte1() As Plano3D
        Get
            Return Plano1
        End Get
    End Property
    Public ReadOnly Property PlanoCorte2() As Plano3D
        Get
            Return Plano2
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return "(X,Y,Z)=" & PuntoDirector.ToString & " + K* " & VectorDirector.ToString
    End Function

    Public Sub New(ByVal P1 As Punto3D, ByVal P2 As Punto3D)
        V = New Vector3D(P1, P2).VectorUnitario
        P = P1
        ObtenerPlanos()
    End Sub

    Public Sub New(ByVal Vector As Vector3D, ByVal Punto As Punto3D)
        V = Vector.VectorUnitario
        P = Punto
        ObtenerPlanos()
    End Sub

    Public Sub New(ByVal PLN1 As Plano3D, ByVal PLN2 As Plano3D)
        Plano1 = PLN1
        Plano2 = PLN2
        V = CALC3D_ProductoVectorial(PLN1.VectorNormal, PLN2.VectorNormal).VectorUnitario
        P = CALC3D_PuntoDeDosPlanos(PLN1, PLN2)
    End Sub

    Private Sub ObtenerPlanos()
        Plano1 = New Plano3D(V.Y, -V.X, 0, (V.X * P.Y) - (V.Y * P.X))
        Plano2 = New Plano3D(V.Z, 0, -V.X, (V.X * P.Z) - (V.Z * P.X))
    End Sub

    Public Function Pertenece(ByVal Punto As Punto3D) As Boolean
        Return (Plano1.Pertenece(Punto) AndAlso Plano2.Pertenece(Punto))
    End Function

    Public Function PuntoDeLaRecta(ByVal Parametro As Double) As Punto3D
        Dim Retorno As Punto3D

        Retorno.X = P.X + (Parametro * V.X)
        Retorno.Y = P.Y + (Parametro * V.Y)
        Retorno.Z = P.Z + (Parametro * V.Z)

        Return Retorno
    End Function

    Public Function Pendiente(ByVal Eje As EnumEje) As Double
        Dim V As Vector3D
        Select Case Eje
            Case EnumEje.EjeX
                V = New Vector3D(1, 0, 0)
            Case EnumEje.EjeY
                V = New Vector3D(0, 1, 0)
            Case EnumEje.EjeZ
                V = New Vector3D(0, 0, 1)
        End Select

        Return (Me.VectorDirector & V).Modulo
    End Function
End Structure
#End Region
#Region "TiposObjetos3D"
Public Structure Vertice
    Public Coordenadas As Punto3D
    Public Indice As Long
    Public Color As Color
    Public ColorIluminacion As Color
    Public Normal As Vector3D
    Public Representacion As Point
    Public Caras() As Long

    Public Sub New(ByVal ValIndice As Long, ByVal ValVertice As Punto3D)
        Indice = ValIndice
        Coordenadas = ValVertice
    End Sub

    Public Overrides Function ToString() As String
        Return "Indice: " & Indice & " - Coordenadas: " & Coordenadas.ToString
    End Function
End Structure

Public Structure Arista
    Public VerticeInicial As Long
    Public VerticeFinal As Long
    Public Indice As Long

    Public Sub New(ByVal IndiceVerticeInicial As Long, ByVal IndiceVerticeFinal As Long, ByVal IndiceArista As Long)
        VerticeInicial = IndiceVerticeInicial
        VerticeFinal = IndiceVerticeFinal
        Indice = IndiceArista
    End Sub
End Structure

Public Structure Cara
    Public Vertices() As Long
    Public Representacion() As Point
    Public Baricentro As Vertice
    Public CargadaEnBuffer As Boolean
    Public Color As Color
    Private mTriangulacion() As Triangulo
    Private mBaricentros() As Punto3D

    Public ReadOnly Property Triangulacion() As Triangulo()
        Get
            Return mTriangulacion
        End Get
    End Property

    Public ReadOnly Property SimpleTriangles() As SimpleTriangle()
        Get
            Dim Retorno() As SimpleTriangle
            ReDim Retorno(mTriangulacion.GetUpperBound(0))

            For i As Integer = 0 To mTriangulacion.GetUpperBound(0)
                Retorno(i) = mTriangulacion(i).ToSimpleTriangle
            Next

            Return Retorno
        End Get
    End Property

    Public ReadOnly Property Baricentros() As Punto3D()
        Get
            Return mBaricentros
        End Get
    End Property

    Public Sub Teselar(ByVal Poliedro As Poliedro, ByVal IndiceCara As Integer)
        ReDim mTriangulacion(Vertices.GetUpperBound(0))
        ReDim mBaricentros(Vertices.GetUpperBound(0))

        For i As Integer = 0 To Vertices.GetUpperBound(0)
            mTriangulacion(i) = New Triangulo(i, IIf(i + 1 > Vertices.GetUpperBound(0), 0, i + 1), 0, 0)
            mBaricentros(i) = CALC3D_Baricentro(Poliedro.Vertices(Vertices(i)).Coordenadas, Poliedro.Vertices(Vertices(IIf(i + 1 > Vertices.GetUpperBound(0), 0, i + 1))).Coordenadas, Baricentro.Coordenadas)
        Next
    End Sub

    Public Sub New(ByVal NumeroVertices As Long)
        ReDim Vertices(NumeroVertices - 1)
        Baricentro.Color = Drawing.Color.White
    End Sub

    Public Overrides Function ToString() As String
        Return "Cara de " & Vertices.Count & " vertices"
    End Function
End Structure

Public Structure Triangulo
    Public PrimerVertice As Long
    Public SegundoVertice As Long
    Public TercerVertice As Long

    Public IndiceCara As Long

    Public Sub New(ByVal V1 As Long, ByVal V2 As Long, ByVal V3 As Long, ByVal Cara As Long)
        PrimerVertice = V1
        SegundoVertice = V2
        TercerVertice = V3
        IndiceCara = Cara
    End Sub

    Public Function ToSimpleTriangle() As Dx_Lib32Wrapper.SimpleTriangle
        Return New SimpleTriangle(PrimerVertice, SegundoVertice, TercerVertice)
    End Function
End Structure

'Public Structure TesselationData
'    Public Centro As Punto3D
'    Public Cara As Long
'    Public Vertices() As Vertice
'    Public TTriangulos() As Triangulo

'    Public Sub Teselar(ByVal 
'End Structure

Public Structure Poliedro
    Public VerticesOriginales() As Punto3D
    Public Vertices() As Vertice

    Public Aristas() As Arista
    Public Caras() As Cara
    Public Triangulos() As Triangulo

    Dim mPendienteAplicacionTransformaciones As Boolean
    Dim mPendienteCalculoColores As Boolean
    Public Modificado As Boolean

    Public EsCurvo As Boolean

    Public ReadOnly Property PendienteAplicacionTransformaciones() As Boolean
        Get
            Return mPendienteAplicacionTransformaciones
        End Get
    End Property

    Public ReadOnly Property PendienteCalculoColores() As Boolean
        Get
            Return mPendienteCalculoColores
        End Get
    End Property

    Public Centro As Punto3D
    Public CentroOriginal As Punto3D

    Public InterpolarNormalesVertices As Boolean

    Private MatrizTraslacion(,) As Double
    Private MatrizEscalado(,) As Double
    Private MatrizRotacion(,) As Double
    Private MatrizTransformacion(,) As Double

    Public Sub TriangulacionCaras()
        Dim Triangulo As Triangulo
        Dim ListaTriangulos As New List(Of Triangulo)
        For i As Long = 0 To Caras.GetUpperBound(0)
            For j As Long = 1 To Caras(i).Vertices.GetUpperBound(0) - 1
                Triangulo = New Triangulo(Caras(i).Vertices(0), Caras(i).Vertices(j), Caras(i).Vertices(j + 1), i)
                ListaTriangulos.Add(Triangulo)
            Next
        Next

        Triangulos = ListaTriangulos.ToArray
        Triangulos = Triangulos
    End Sub

    Public Sub CalcularColores(ByVal Focos() As Foco, ByVal Iluminacion As DatosIluminacion, ByVal Camara As Camara)
        Dim Calculados(Vertices.GetUpperBound(0)) As Boolean

        For i As Integer = 0 To Caras.GetUpperBound(0)
            If CALC3D_OrientacionCara(Camara, Caras(i)) Then
                For j As Integer = 0 To Caras(i).Vertices.GetUpperBound(0)
                    If Not Calculados(Caras(i).Vertices(j)) Then
                        Vertices(Caras(i).Vertices(j)).ColorIluminacion = CALCSOM_IluminacionModeloPhong(Focos, Iluminacion, Vertices(Caras(i).Vertices(j)).Normal, Vertices(Caras(i).Vertices(j)).Coordenadas, Caras(i).Color, Camara)
                        Calculados(Caras(i).Vertices(j)) = True
                    End If
                Next
                Caras(i).Baricentro.ColorIluminacion = CALCSOM_IluminacionModeloPhong(Focos, Iluminacion, Caras(i).Baricentro.Normal, Caras(i).Baricentro.Coordenadas, Caras(i).Color, Camara)
            End If
        Next
        mPendienteCalculoColores = False
    End Sub

    Public Sub EstablecerCarasVertices()
        Dim CarasVertice As New List(Of Long)

        For i As Long = 0 To Vertices.GetUpperBound(0)
            CarasVertice.Clear()
            For j As Long = 0 To Caras.GetUpperBound(0)
                If Caras(j).Vertices.Contains(i) Then CarasVertice.Add(j)
            Next
            Vertices(i).Caras = CarasVertice.ToArray
        Next
    End Sub


    Public Sub New(ByVal ValNumeroVertices As Long, ByVal ValNumeroCaras As Integer)
        ReDim Vertices(ValNumeroVertices - 1)
        ReDim VerticesOriginales(ValNumeroVertices - 1)
        ReDim Aristas(ValNumeroVertices + ValNumeroCaras - 3)
        ReDim Caras(ValNumeroCaras - 1)

        EstablecerColores(Color.White)

        ReestablecerMatrices()
        mPendienteAplicacionTransformaciones = False
    End Sub

    Public Sub New(ByVal ValNumeroVertices As Long, ByVal ValNumeroCaras As Integer, ByVal ValColor As Color)
        ReDim Vertices(ValNumeroVertices - 1)
        ReDim VerticesOriginales(ValNumeroVertices - 1)
        ReDim Aristas(ValNumeroVertices + ValNumeroCaras - 3)
        ReDim Caras(ValNumeroCaras - 1)

        ReDim MatrizTransformacion(3, 3)

        EstablecerColores(Color.White)

        ReestablecerMatrices()
        mPendienteAplicacionTransformaciones = False
    End Sub

    Public Sub EstablecerColores(ByVal Color As Color)
        For i As Long = 0 To Caras.GetUpperBound(0)
            Caras(i).Color = Color
        Next
    End Sub

    Public Overrides Function ToString() As String
        Return "Poliedro de " & Vertices.GetUpperBound(0) + 1 & " vertices, " & Aristas.GetUpperBound(0) + 1 & " aristas, " & Caras.GetUpperBound(0) + 1 & " caras."
    End Function

    Private Sub ActualizarMatrizTransformacion(ByVal Transformacion(,) As Double)
        MatrizTransformacion = CALCMATR_ProductoMatricial(Transformacion, MatrizTransformacion)
    End Sub

    Public Sub ReestablecerMatrices()
        MatrizTransformacion = CALCMATR_MatrizUnitaria(4)
    End Sub

    Public Sub CambiarEscala(ByVal ValorEscala As Punto3D)
        ActualizarMatrizTransformacion(CALCTRANS3D_CambioEscala(ValorEscala.X, ValorEscala.Y, ValorEscala.Z))
        mPendienteAplicacionTransformaciones = True
    End Sub

    Public Sub Trasladar(ByVal ValorTraslacion As Punto3D)
        ActualizarMatrizTransformacion(CALCTRANS3D_Traslacion(ValorTraslacion.X, ValorTraslacion.Y, ValorTraslacion.Z))
        mPendienteAplicacionTransformaciones = True
    End Sub

    Public Sub TrasladarADestino(ByVal Destino As Punto3D)
        Dim Traslacion As Punto3D
        Traslacion.X = Destino.X - Centro.X
        Traslacion.Y = Destino.Y - Centro.Y
        Traslacion.Z = Destino.Z - Centro.Z

        Trasladar(Traslacion)
        mPendienteAplicacionTransformaciones = True
    End Sub

    Public Sub EscalarFijo(ByVal Escalado As Punto3D)
        Dim ValCentro As Punto3D = Centro

        'MatrizTraslacion = CALCMATR_ProductoMatricial(CALCTRANS3D_Traslacion(-Centro.X, -Centro.Y, -Centro.Z), MatrizTraslacion)
        Trasladar(New Punto3D(-Centro.X, -Centro.Y, -Centro.Z))
        CambiarEscala(Escalado)
        Trasladar(ValCentro)
        mPendienteAplicacionTransformaciones = True
    End Sub

    Public Sub Rotar(ByVal EjeRotacion As EnumEje, ByVal AnguloRotacion As Double)
        ActualizarMatrizTransformacion(CALCTRANS3D_RotacionAlrededorEje(EjeRotacion, AnguloRotacion))
        mPendienteAplicacionTransformaciones = True
    End Sub

    Public Sub Rotar(ByVal Recta As Recta3D, ByVal Angulo As Double)
        Dim ValCentro As Punto3D = Centro
        Dim Vxoy, Vzoy As Vector3D
        Dim AngXOY, AngZOY As Single

        Vxoy = New Vector3D(Recta.VectorDirector.X, Recta.VectorDirector.Y, 0)
        Vzoy = New Vector3D(0, Recta.VectorDirector.Y, Recta.VectorDirector.Z)
        AngXOY = CALC3D_AnguloVectores3D(New Vector3D(0, 1, 0), Vxoy)
        AngZOY = CALC3D_AnguloVectores3D(New Vector3D(1, 0, 0), Vzoy)
        '//----NO FUNCIONA----//
        Trasladar(New Punto3D(-Centro.X, -Centro.Y, -Centro.Z))
        AplicarTransformaciones()
        Rotar(EnumEje.EjeZ, -AngXOY)
        AplicarTransformaciones()
        Rotar(EnumEje.EjeX, -AngZOY)
        AplicarTransformaciones()
        Rotar(EnumEje.EjeY, Angulo)
        AplicarTransformaciones()
        Rotar(EnumEje.EjeX, AngXOY)
        AplicarTransformaciones()
        Rotar(EnumEje.EjeZ, AngZOY)
        AplicarTransformaciones()
        Trasladar(New Punto3D(ValCentro.X, ValCentro.Y, ValCentro.Z))
        mPendienteAplicacionTransformaciones = True
    End Sub

    Public Sub RotarAlrededorPunto(ByVal Punto As Punto3D, ByVal EjeGiro As EnumEje, ByVal AnguloRotacion As Single)
        Trasladar(New Punto3D(-Punto.X, -Punto.Y, -Punto.Z))
        Rotar(EjeGiro, AnguloRotacion)
        Trasladar(Punto)
        mPendienteAplicacionTransformaciones = True
    End Sub

    Public Sub RotarAlrededorCentro(ByVal Eje As EnumEje, ByVal AnguloRotacion As Single)
        RotarAlrededorPunto(Centro, Eje, AnguloRotacion)
    End Sub

    Public Sub RotarUnidades(ByVal EjeRotacion As EnumEje, ByVal Unidades As Integer)
        Dim AnguloRotacion As Double
        Dim Radio As Double
        Select Case EjeRotacion
            Case EnumEje.EjeX
                Radio = New Vector3D(Vertices(0).Coordenadas, New Punto3D(Vertices(0).Coordenadas.X, 0, 0)).Modulo
            Case EnumEje.EjeY
                Radio = New Vector3D(Vertices(0).Coordenadas, New Punto3D(0, Vertices(0).Coordenadas.Y, 0)).Modulo
            Case EnumEje.EjeZ
                Radio = New Vector3D(Vertices(0).Coordenadas, New Punto3D(0, 0, Vertices(0).Coordenadas.Z)).Modulo
        End Select
        AnguloRotacion = (Unidades / Radio)
        ActualizarMatrizTransformacion(CALCTRANS3D_RotacionAlrededorEje(EjeRotacion, AnguloRotacion))
        mPendienteAplicacionTransformaciones = True
    End Sub

    Public Sub ReestablecerTransformaciones()
        MatrizTransformacion = CALCMATR_MatrizUnitaria(4)
        mPendienteAplicacionTransformaciones = True
    End Sub

    Private Function CalculoTransformaciones() As Double(,)
        Dim Retorno(3, 3) As Double

        Retorno = MatrizTransformacion
        For i As Integer = 0 To Vertices.GetUpperBound(0)
            Vertices(i).Coordenadas = CALCTRANS3D_TransformarPunto(VerticesOriginales(i), MatrizTransformacion)
        Next

        For i As Integer = 0 To Caras.GetUpperBound(0)
            CALC3D_CalculoNormalCara(Me, i)
            CALC3D_BaricentroCara(Me, i)
            Caras(i).Teselar(Me, i)
        Next

        Centro = CALCTRANS3D_TransformarPunto(CentroOriginal, MatrizTransformacion)

        mPendienteAplicacionTransformaciones = False
        mPendienteCalculoColores = True
        Return Retorno
    End Function

    Public Sub CalcularRepresentaciones(ByVal Camara As Camara)
        Dim Representado(Vertices.GetUpperBound(0)) As Boolean
        For i As Long = 0 To Vertices.GetUpperBound(0)
            If Not Representado(i) Then
                For Each Cara As Cara In Caras
                    If CALC3D_OrientacionCara(Camara, Cara) Then
                        If Cara.Vertices.Contains(i) Then
                            Vertices(i).Representacion = Vertices(i).Coordenadas.Representacion2D(Camara)
                            Representado(i) = True
                        End If
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub ReestablecerRepresentaciones()
        For i As Long = 0 To Vertices.GetUpperBound(0)
            Vertices(i).Representacion = Point.Empty
            Vertices(i).Coordenadas.Nulo = True
        Next
    End Sub

    Public Sub RepresentacionCara(ByVal IndiceCara As Long, ByVal Camara As Camara)
        Dim Retorno(Caras(IndiceCara).Vertices.GetUpperBound(0)) As Point

        For i As Long = 0 To Caras(IndiceCara).Vertices.GetUpperBound(0)
            If Vertices(Caras(IndiceCara).Vertices(i)).Representacion.IsEmpty Then
                Vertices(Caras(IndiceCara).Vertices(i)).Representacion = Vertices(Caras(IndiceCara).Vertices(i)).Coordenadas.Representacion2D(Camara)
            End If
            Retorno(i) = Vertices(Caras(IndiceCara).Vertices(i)).Representacion
        Next

        Caras(IndiceCara).Representacion = Retorno
        Caras(IndiceCara).Baricentro.Representacion = Caras(IndiceCara).Baricentro.Coordenadas.Representacion2D(Camara)
    End Sub

    Public Sub AplicarTransformaciones()

        CalculoTransformaciones()
        ReestablecerRepresentaciones()

        InterpolacionNormales()
    End Sub

    Public Sub AplicarTransformaciones(ByVal InterPolarNormales As Boolean)

        CalculoTransformaciones()
        ReestablecerRepresentaciones()

        If InterPolarNormales Then
            InterpolacionNormales()
        End If
    End Sub

    Public Sub InterpolacionNormales()
        Dim Suma As New Vector3D
        Dim Modulo As Double
        Dim Normal As Vector3D
        Dim NumeroCaras As Integer
        Dim r, g, b As Double
        Dim rr, gg, bb As Byte

        For i As Long = 0 To Vertices.GetUpperBound(0)
            Suma = New Vector3D
            Modulo = 0
            NumeroCaras = Vertices(i).Caras.GetUpperBound(0) + 1
            r = 0
            g = 0
            b = 0
            For j As Long = 0 To Vertices(i).Caras.GetUpperBound(0)
                Normal = CALC3D_CalculoNormalCara(Me, Caras(Vertices(i).Caras(j)))
                r += Caras(j).Color.R
                g += Caras(j).Color.G
                b += Caras(j).Color.B

                Suma += Normal
                Modulo += Normal.Modulo
            Next
            Vertices(i).Normal = Suma * (1 / Modulo)

            rr = CByte(r / NumeroCaras)
            gg = CByte(g / NumeroCaras)
            bb = CByte(b / NumeroCaras)
            Vertices(i).Color = Color.FromArgb(255, rr, gg, bb)
        Next
    End Sub
End Structure
#End Region

#Region "TiposBuffer"

Public Structure CaraBuffer
    Public Indices As Point
    Public PendienteActulizacion As Boolean
    Public DistanciaCamara As Double

    Public Sub New(ByVal IndicePoliedro As Long, ByVal IndiceCara As Long, ByVal Cara As Cara, ByVal Distancia As Double)
        Indices.X = IndicePoliedro
        Indices.Y = IndiceCara
        DistanciaCamara = Distancia
    End Sub
End Structure

Public Structure DataVertice
    Public Indices As Point
    Public Caras() As Point
    Public Normal As Vector3D
    Public Coordenadas As Punto3D


    Public Sub New(ByVal IndicePoliedro As Long, ByVal IndiceVertice As Long, ByVal Poliedro As Poliedro, ByVal Vertice As Vertice)
        Dim Normales As New List(Of Vector3D)
        Dim SumMod As Double
        Dim ContCaras As Long = 0
        Coordenadas = Vertice.Coordenadas
        Indices = New Point(IndicePoliedro, IndiceVertice)

        For Each Cara As Cara In Poliedro.Caras
            If Cara.Vertices.Contains(IndiceVertice) Then
                ReDim Preserve Caras(ContCaras)
                ContCaras += 1
                Caras(Caras.GetUpperBound(0)).X = Array.IndexOf(Poliedro.Caras, Cara)
                Caras(Caras.GetUpperBound(0)).Y = Array.IndexOf(Cara.Vertices, IndiceVertice)
                Normales.Add(CALC3D_CalculoPlanoCara(Poliedro, Cara).VectorNormal)
            End If
        Next

        Normal = New Vector3D(0, 0, 0)
        SumMod = 0
        For Each V As Vector3D In Normales
            Normal += V
            SumMod += V.Modulo
        Next
        Normal *= (1 / SumMod)
    End Sub
End Structure

Public Class ComparadorCaraBuffer
    Implements IComparer(Of CaraBuffer)

    Dim mCamara As Camara

    Public Sub New(ByVal Camara As Camara)
        mCamara = Camara
    End Sub

    Public Function Compare(ByVal x As CaraBuffer, ByVal y As CaraBuffer) As Integer Implements System.Collections.Generic.IComparer(Of CaraBuffer).Compare
        Select Case x.DistanciaCamara
            Case Is = y.DistanciaCamara
                Return 0
            Case Is > y.DistanciaCamara
                Return -1
            Case Is < y.DistanciaCamara
                Return 1
        End Select
    End Function
End Class
#End Region

#Region "Motor"
Public Class Motor3D

#Region "Variables internas"
    Dim BMPDibujo As Bitmap
    Dim PunteroSuperficie As IntPtr
    Dim Superficie As Control

    Dim mPoliedros() As Poliedro
    Dim mFocos() As Foco

    Dim mSombreado As Boolean
    Dim mCalculoColisiones As Boolean
    Dim DibujarEjes As Boolean

    Dim g As Graphics
    Dim Wrapper As DxLibWrapper

    Dim Buffer As New List(Of CaraBuffer)

    Dim TiempoOrdenacion As Double
    Dim TiempoTramsformacion As Double
    Dim TiempoRecargaBuffer As Double
    Dim TiempoRepintado As Double
    Dim TiempoReasignacion As Double
    Dim TiempoSombreado As Double

    Dim AlgoritmoOrdenacion As AlgoritmoOrdenacion

    Dim mShader As Shader
    Dim ShaderModificado As Boolean
    Dim mTeselado As Boolean

    Dim mVerNormalesCaras As Boolean
    Dim mVerNormalesVertices As Boolean
    Dim mVerTriangulacion As Boolean
    Dim mVerDatosVertices As Boolean
    Dim mVerIndicesBuffer As Boolean

    Dim VerDepuracionBuffer As Boolean
    Dim VerDepuracionDibujado As Boolean
    Dim VerDepuracionSombreado As Boolean
    Dim VerDepuracionTodo As Boolean

    Dim PintarFocos As Boolean

    Dim MetodoRellenoPoligonoColor As PINT_DelPintarPoligonoColor
    Dim MetodoRellenoPoligonoColores As PINT_DelPintarPoligonoColores
    Dim MetodoDibujadoPoligonoColor As PINT_DelDibujarPoligonoColor

    Dim SeñalarCaraBuffer As Boolean
    Dim CaraSeñalada As Long
    Dim CamaraModificada As Boolean
    Dim IluminacionModificada As Boolean

    Dim Camara As Camara
    Dim Iluminacion As DatosIluminacion

    Dim Hilo As Thread

    Dim Modificado As Boolean

#End Region
#Region "Propiedades"

    Public ReadOnly Property DEPU_CopiaBuffer() As List(Of CaraBuffer)
        Get
            Return Buffer
        End Get
    End Property

    Public Property DEPU_MostrarNormalesCaras() As Boolean
        Get
            Return mVerNormalesCaras
        End Get
        Set(ByVal value As Boolean)
            If mVerNormalesCaras = value Then Exit Property

            mVerNormalesCaras = value
        End Set
    End Property

    Public Property DEPU_MostrarNormalesVertices() As Boolean
        Get
            Return mVerNormalesVertices
        End Get
        Set(ByVal value As Boolean)
            If mVerNormalesVertices = value Then Exit Property

            mVerNormalesVertices = value
        End Set
    End Property

    Public Property DEPU_MostrarTriangulacion() As Boolean
        Get
            Return mVerTriangulacion
        End Get
        Set(ByVal value As Boolean)
            If mVerTriangulacion = value Then Exit Property
            mVerTriangulacion = value
        End Set
    End Property

    Public Property DEPU_MostrarDatosVertices() As Boolean
        Get
            Return mVerDatosVertices
        End Get
        Set(ByVal value As Boolean)
            If mVerDatosVertices = value Then Exit Property
            mVerDatosVertices = value

        End Set
    End Property

    Public Property DEPU_SeñalarCaraBuffer() As Boolean
        Get
            Return SeñalarCaraBuffer
        End Get
        Set(ByVal value As Boolean)
            If SeñalarCaraBuffer = value Then Exit Property

            SeñalarCaraBuffer = value
        End Set
    End Property

    Public Property DEPU_MostrarIndicesBuffer() As Boolean
        Get
            Return mVerIndicesBuffer
        End Get
        Set(ByVal value As Boolean)
            If mVerIndicesBuffer = value Then Exit Property

            mVerIndicesBuffer = value
        End Set
    End Property

    Public Property DEPU_DepuracionBuffer() As Boolean
        Get
            Return VerDepuracionBuffer
        End Get
        Set(ByVal value As Boolean)
            VerDepuracionBuffer = value
        End Set
    End Property

    Public Property DEPU_DepuracionDibujo() As Boolean
        Get
            Return VerDepuracionDibujado
        End Get
        Set(ByVal value As Boolean)
            VerDepuracionDibujado = value
        End Set
    End Property

    Public Property DEPU_DepuracionSombreado() As Boolean
        Get
            Return VerDepuracionSombreado
        End Get
        Set(ByVal value As Boolean)
            VerDepuracionSombreado = value
        End Set
    End Property

    Public Property DEPU_DepuracionTodo() As Boolean
        Get
            Return VerDepuracionTodo
        End Get
        Set(ByVal value As Boolean)
            VerDepuracionTodo = value
        End Set
    End Property

    Public Property DEPU_CaraSeñalada() As Long
        Get
            Return CaraSeñalada
        End Get
        Set(ByVal value As Long)
            If value < 0 Then value = 0
            If value > Buffer.Count - 1 Then value = Buffer.Count - 1
            If CaraSeñalada = value Then Exit Property

            CaraSeñalada = value
        End Set
    End Property

    Public Property PINT_MetodoPintadoPoligonoColor() As PINT_DelPintarPoligonoColor
        Get
            Return MetodoRellenoPoligonoColor
        End Get
        Set(ByVal value As PINT_DelPintarPoligonoColor)
            MetodoRellenoPoligonoColor = value
        End Set
    End Property

    Public Property PINT_MetodoPintadoPoligonoColores() As PINT_DelPintarPoligonoColores
        Get
            Return MetodoRellenoPoligonoColores
        End Get
        Set(ByVal value As PINT_DelPintarPoligonoColores)
            MetodoRellenoPoligonoColores = value
        End Set
    End Property

    Public Property PINT_MetodoDibujadoPoligonoColor() As PINT_DelDibujarPoligonoColor
        Get
            Return MetodoDibujadoPoligonoColor
        End Get
        Set(ByVal value As PINT_DelDibujarPoligonoColor)
            MetodoDibujadoPoligonoColor = value
        End Set
    End Property

    Public ReadOnly Property PINT_SuperficieDibujo() As Bitmap
        Get
            Return BMPDibujo
        End Get
    End Property

    Public Property PINT_PintarFocos() As Boolean
        Get
            Return PintarFocos
        End Get
        Set(ByVal value As Boolean)
            If PintarFocos = value Then Exit Property

            PintarFocos = value
        End Set
    End Property

    Public Property PINT_Teselacion() As Boolean
        Get
            Return mTeselado
        End Get
        Set(ByVal value As Boolean)
            If value = mTeselado Then Exit Property
            mTeselado = value
        End Set
    End Property

    Public ReadOnly Property POLI_Poliedros() As Poliedro()
        Get
            Return mPoliedros
        End Get
    End Property

    Public Property PINT_Sombreado() As Boolean
        Get
            Return mSombreado
        End Get
        Set(ByVal value As Boolean)
            If mSombreado = value Then Exit Property

            mSombreado = value
        End Set
    End Property

    Public Property PINT_Shader() As Shader
        Get
            Return mShader
        End Get
        Set(ByVal value As Shader)
            If mShader = value Then Exit Property

            mShader = value
            ShaderModificado = True

            If mShader = Shader.GouraudShading Or mShader = Shader.PhongShading Then
                For i As Long = 0 To mPoliedros.GetUpperBound(0)
                    mPoliedros(i).InterpolacionNormales()
                Next
            End If
        End Set
    End Property

    Public ReadOnly Property PINT_ShaderModificado() As Boolean
        Get
            Return ShaderModificado
        End Get
    End Property

    Public ReadOnly Property ILUM_Focos() As Foco()
        Get
            Return mFocos
        End Get
    End Property

    Public ReadOnly Property REPR_CamaraModificada() As Boolean
        Get
            Return CamaraModificada
        End Get
    End Property

    Public ReadOnly Property ILUM_IluminacionModificda() As Boolean
        Get
            Return IluminacionModificada
        End Get
    End Property

    Public ReadOnly Property REPR_Camara() As Camara
        Get
            Return Camara
        End Get
    End Property

    Public ReadOnly Property ILUM_DatosIluminacion() As DatosIluminacion
        Get
            Return Iluminacion
        End Get
    End Property
#End Region

#Region "Eventos"
    Public Event PINT_FinRedibujado()
    Public Event PINT_FinSombreado()

    Public Event BUFF_FinRecargaBuffer(ByVal Buffer As List(Of CaraBuffer))
    Public Event BUFF_FinReasignacionBuffer()
    Public Event BUFF_FinReordenacionBuffer()
    Public Event POLI_FinActualizacionPoliedros()

    Public Event REPR_ModificacionCamara()

    Public Event DEPU_SalidaDepuracionBuffer(ByVal Mensaje As String)
    Public Event DEPU_SalidaDepuracionDibujo(ByVal Mensaje As String)
    Public Event DEPU_SalidaDepuracionSombreado(ByVal Mensaje As String)
    Public Event DEPU_SalidaDepuracionTodo(ByVal Mensaje As String)
#End Region

#Region "Datos/Informacion"
    Public ReadOnly Property INFO_TotalVertices() As Long
        Get
            Dim Vertices As Long = 0
            For i As Long = 0 To mPoliedros.GetUpperBound(0)
                Vertices += mPoliedros(i).Vertices.GetUpperBound(0) + 1
            Next

            Return Vertices
        End Get
    End Property
    Public ReadOnly Property INFO_TotalCaras() As Long
        Get
            Dim Caras As Long = 0
            For i As Long = 0 To mPoliedros.GetUpperBound(0)
                Caras += mPoliedros(i).Caras.GetUpperBound(0) + 1
            Next

            Return Caras
        End Get
    End Property

    Public ReadOnly Property INFO_TotalTriangulos() As Long
        Get
            Dim Triangulos As Long = 0
            For i As Long = 0 To mPoliedros.GetUpperBound(0)
                Triangulos += mPoliedros(i).Triangulos.GetUpperBound(0) + 1
            Next

            Return Triangulos
        End Get
    End Property
    Public ReadOnly Property INFO_CarasEnBuffer() As Long
        Get
            Return Buffer.Count
        End Get
    End Property
    Public ReadOnly Property INFO_TotalPoliedros() As Long
        Get
            Return mPoliedros.GetUpperBound(0) + 1
        End Get
    End Property

    Public ReadOnly Property INFO_TiempoRepintado() As Double
        Get
            Return TiempoRepintado
        End Get
    End Property
    Public ReadOnly Property INFO_TiempoRecargaBuffer() As Double
        Get
            Return TiempoRecargaBuffer
        End Get
    End Property
    Public ReadOnly Property INFO_TiempoReordenacionBuffer() As Double
        Get
            Return TiempoOrdenacion
        End Get
    End Property
    Public ReadOnly Property INFO_TiempoReasignacionIndicesBuffer() As Double
        Get
            Return TiempoReasignacion
        End Get
    End Property
    Public ReadOnly Property INFO_TiempoCalculoTransformaciones() As Double
        Get
            Return TiempoTramsformacion
        End Get
    End Property
    Public ReadOnly Property INFO_TiempoCalculoSombreado() As Double
        Get
            Return TiempoSombreado
        End Get
    End Property

    Public ReadOnly Property INFO_TiempoTotal() As Double
        Get
            Return TiempoOrdenacion + TiempoReasignacion + TiempoRecargaBuffer + TiempoRepintado + TiempoSombreado + TiempoTramsformacion
        End Get
    End Property
#End Region

#Region "Iluminacion"
    Public Sub ILUM_AñadirFoco(ByVal Foco As Foco)
        Dim CopiaFocos() As Foco
        CopiaFocos = mFocos

        ReDim mFocos(mFocos.GetUpperBound(0) + 1)

        For i As Integer = 0 To CopiaFocos.GetUpperBound(0)
            mFocos(i) = CopiaFocos(i)
        Next
        mFocos(mFocos.GetUpperBound(0)) = Foco
    End Sub

    Public Sub ILUM_QuitarFoco(ByVal IndiceFoco As Integer)
        Dim CopiaFocos() As Foco
        Dim Cont As Integer

        If ILUM_Focos.GetUpperBound(0) = 0 Or ILUM_Focos.Count = 0 Or IndiceFoco > ILUM_Focos.GetUpperBound(0) Then Exit Sub

        CopiaFocos = mFocos

        ReDim mFocos(mFocos.GetUpperBound(0) - 1)

        For i As Integer = 0 To CopiaFocos.GetUpperBound(0)
            Select Case i
                Case Is < IndiceFoco
                    Cont = i
                Case Is = IndiceFoco
                    Continue For
                Case Is > IndiceFoco
                    Cont = i - 1
            End Select
            mFocos(Cont) = CopiaFocos(i)
        Next
    End Sub

    Public Sub ILUM_QuitarFoco(ByVal Foco As Foco)
        Dim CopiaFocos() As Foco
        Dim Cont As Integer
        Dim IndiceFoco As Integer = Array.IndexOf(mFocos, Foco)

        If ILUM_Focos.GetUpperBound(0) = 0 Or Not ILUM_Focos.Contains(Foco) Then Exit Sub

        CopiaFocos = mFocos

        ReDim mFocos(mFocos.GetUpperBound(0) - 1)

        For i As Integer = 0 To CopiaFocos.GetUpperBound(0)
            Select Case i
                Case Is < IndiceFoco
                    Cont = i
                Case Is = IndiceFoco
                    Continue For
                Case Is > IndiceFoco
                    Cont = i - 1
            End Select
            mFocos(Cont) = CopiaFocos(i)
        Next
    End Sub

    Public Sub ILUM_EstablerColorFoco(ByVal IndiceFoco As Integer, ByVal Color As Color)
        mFocos(IndiceFoco).Color = Color
        IluminacionModificada = True
    End Sub
    Public Sub ILUM_EstablerPosicionFoco(ByVal IndiceFoco As Integer, ByVal Posicion As Punto3D)
        mFocos(IndiceFoco).Coordenadas = Posicion
        IluminacionModificada = True
    End Sub

    Public Sub ILUM_EstablerDireccionFoco(ByVal IndiceFoco As Integer, ByRef Direccion As Vector3D)
        mFocos(IndiceFoco).VectorDireccion = Direccion
        IluminacionModificada = True
    End Sub

    Public Sub ILUM_EstablerDeltaFoco(ByVal IndiceFoco As Integer, ByVal Delta As Single)
        mFocos(IndiceFoco).Delta = Delta
        IluminacionModificada = True
    End Sub

    Public Sub ILUM_EstablerRadioFoco(ByVal IndiceFoco As Integer, ByVal Radio As Double)
        mFocos(IndiceFoco).Radio = Radio
        IluminacionModificada = True
    End Sub

    Public Sub ILUM_EstablerTipoFoco(ByVal IndiceFoco As Integer, ByVal Tipo As TipoFoco)
        mFocos(IndiceFoco).Tipo = Tipo
        IluminacionModificada = True
    End Sub

    Public Sub ILUM_EstablerBrilloFoco(ByVal IndiceFoco As Integer, ByVal Brillo As Double)
        If Brillo < 0 Then Brillo = 0
        If Brillo > 1 Then Brillo = 0

        mFocos(IndiceFoco).Brillo = Brillo
        IluminacionModificada = True
    End Sub

    Public Sub ILUM_EstablecerAportacionAmbiente(ByVal Aportacion As Double)
        Iluminacion.EstablecerAportaciones(Aportacion, Iluminacion.AportacionDifusa, Iluminacion.AportacionEspecular)
        IluminacionModificada = True
    End Sub

    Public Sub ILUM_EstablecerAportacionDifusa(ByVal Aportacion As Double)
        Iluminacion.EstablecerAportaciones(Iluminacion.AportacionAmbiente, Aportacion, Iluminacion.AportacionEspecular)
        IluminacionModificada = True
    End Sub

    Public Sub ILUM_EstablecerAportacionEspecular(ByVal Aportacion As Double)
        Iluminacion.EstablecerAportaciones(Iluminacion.AportacionAmbiente, Iluminacion.AportacionDifusa, Aportacion)
        IluminacionModificada = True
    End Sub

    Public Sub ILUM_EstablecerAportaciones(ByVal AportacionAmbiente As Double, ByVal AportacionDifusa As Double, ByVal AportacionEspecular As Double)
        Iluminacion.EstablecerAportaciones(AportacionAmbiente, AportacionDifusa, AportacionEspecular)
        IluminacionModificada = True
    End Sub

    Public Sub ILUM_EstablecerConstanteDistancia1(ByVal Constante As Double)
        Iluminacion.EstablecerConstantesDistancia(Constante, Iluminacion.ConstanteDistancia2, Iluminacion.ConstanteDistancia3)
        IluminacionModificada = True
    End Sub

    Public Sub ILUM_EstablecerConstanteDistancia2(ByVal Constante As Double)
        Iluminacion.EstablecerConstantesDistancia(Iluminacion.ConstanteDistancia1, Constante, Iluminacion.ConstanteDistancia3)
        IluminacionModificada = True
    End Sub

    Public Sub ILUM_EstablecerConstanteDistancia3(ByVal Constante As Double)
        Iluminacion.EstablecerConstantesDistancia(Iluminacion.ConstanteDistancia1, Iluminacion.ConstanteDistancia2, Constante)
        IluminacionModificada = True
    End Sub

    Public Sub ILUM_EstablecerConstantesDistancia(ByVal Constante1 As Double, ByVal Constante3 As Double, ByVal Constante2 As Double)
        Iluminacion.EstablecerConstantesDistancia(Constante1, Constante2, Constante3)
        IluminacionModificada = True
    End Sub

    Public Sub ILUM_EstablecerExponenteEspecular(ByVal Exponente As Integer)
        Iluminacion.EstablecerExponenteEspecular(Exponente)
        IluminacionModificada = True
    End Sub


#End Region

#Region "Representación"
    Public Sub REPR_ReiniciarCamara()
        Camara.Reiniciar()
    End Sub
    Public Sub REPR_CambiarRepresentacion(ByVal NuevaPerspectiva As TipoRepresentacion)
        Camara.EstablecerRepresentacion(NuevaPerspectiva)
        CamaraModificada = True
        BUFF_RecargarBuffer()
    End Sub

    Public Sub REPR_EstablecerPosicionCamara(ByVal NuevaPosicion As Punto3D)
        Camara.EstablecerPosicion(NuevaPosicion)
        POLI_ActualizarPoliedros()
    End Sub

    Public Sub REPR_EstablecerVectorDirecionCamara(ByVal NuevaDireccion As Vector3D)
        Camara.EstablecerVectorDireccion(NuevaDireccion)
        CamaraModificada = True
        POLI_ActualizarPoliedros()
    End Sub

    Public Sub REPR_EstablecerDistanciaPlanoProyeccionCamara(ByVal Distancia As Double)
        Camara.EstablecerDistancia(Distancia)
        CamaraModificada = True
        POLI_ActualizarPoliedros()
    End Sub

    Public Sub REPR_IncrementarCoordenadasCamara(ByVal Eje As EnumEje, ByVal Incremento As Double)
        Camara.IncrementarCoordenadaPosicion(Eje, Incremento)
        CamaraModificada = True
        POLI_ActualizarPoliedros()
    End Sub

    Public Sub REPR_EstablecerAnguloCamara(ByVal Eje As EnumEje, ByVal Angulo As Single)
        Camara.EstablecerAngulo(Eje, Angulo)
        CamaraModificada = True
        POLI_ActualizarPoliedros()
    End Sub

#End Region

#Region "Depuración"
    Private Sub SalidaDepuracion(ByVal Mensaje As String, ByVal Depuracion As DepuracionActiva)
        Select Case Depuracion
            Case Global.Motor3D.DepuracionActiva.Buffer
                RaiseEvent DEPU_SalidaDepuracionBuffer(Mensaje)
            Case Global.Motor3D.DepuracionActiva.Dibujado
                RaiseEvent DEPU_SalidaDepuracionDibujo(Mensaje)
            Case Global.Motor3D.DepuracionActiva.Sombreado
                RaiseEvent DEPU_SalidaDepuracionSombreado(Mensaje)
        End Select
        If VerDepuracionTodo Then RaiseEvent DEPU_SalidaDepuracionTodo(Mensaje)
    End Sub
#End Region

#Region "Poliedros"

    Public Sub POLI_AñadirPoliedro(ByVal Poliedro As Poliedro)
        Dim CopiaPoliedros() As Poliedro
        CopiaPoliedros = POLI_Poliedros

        ReDim mPoliedros(mPoliedros.GetUpperBound(0) + 1)

        For i As Integer = 0 To CopiaPoliedros.GetUpperBound(0)
            mPoliedros(i) = CopiaPoliedros(i)
        Next
        mPoliedros(mPoliedros.GetUpperBound(0)) = Poliedro

        BUFF_RecargarBuffer()
    End Sub

    Public Sub POLI_QuitarPoliedro(ByVal IndicePoliedro As Integer)
        Dim CopiaPoliedros() As Poliedro
        Dim CopiaBuffer As New List(Of CaraBuffer)

        If mPoliedros.GetUpperBound(0) = 0 Or IndicePoliedro > mPoliedros.GetUpperBound(0) Then Exit Sub
        CopiaPoliedros = mPoliedros

        ReDim mPoliedros(mPoliedros.GetUpperBound(0) - 1)

        For i As Integer = 0 To mPoliedros.GetUpperBound(0)
            Select Case i
                Case Is < IndicePoliedro
                    mPoliedros(i) = CopiaPoliedros(i)
                Case Is >= IndicePoliedro
                    mPoliedros(i) = CopiaPoliedros(i + 1)
            End Select
        Next

        For Each CaraBuffer As CaraBuffer In Buffer
            Select Case CaraBuffer.Indices.X
                Case Is < IndicePoliedro
                    CopiaBuffer.Add(CaraBuffer)
                Case Is > IndicePoliedro
                    CaraBuffer.Indices.X -= 1
                    mPoliedros(CaraBuffer.Indices.X).Modificado = True
            End Select
            CopiaBuffer.Add(CaraBuffer)
        Next
        Buffer.Clear()
        For Each CaraBuffer As CaraBuffer In CopiaBuffer
            Buffer.Add(CaraBuffer)
        Next

        BUFF_RecargarBuffer()
    End Sub
    Public Sub POLI_QuitarPoliedro(ByVal Poliedro As Poliedro)
        Dim CopiaPoliedros() As Poliedro
        Dim IndicePoliedro As Integer

        If mPoliedros.GetUpperBound(0) = 0 Or Not mPoliedros.Contains(Poliedro) Then Exit Sub

        IndicePoliedro = Array.IndexOf(mPoliedros, Poliedro)
        CopiaPoliedros = mPoliedros

        ReDim mPoliedros(mPoliedros.GetUpperBound(0) - 1)

        For i As Integer = 0 To mPoliedros.GetUpperBound(0)
            Select Case i
                Case Is < IndicePoliedro
                    mPoliedros(i) = CopiaPoliedros(i)
                Case Is >= IndicePoliedro
                    mPoliedros(i) = CopiaPoliedros(i + 1)
            End Select
        Next

        BUFF_RecargarBuffer()
    End Sub

    Public Function POLI_CentroPoliedro(ByVal IndicePoliedro As Integer) As Punto3D
        Return mPoliedros(IndicePoliedro).Centro
    End Function

    Public Sub POLI_TrasladarPoliedro(ByVal Indice As Integer, ByVal Traslacion As Punto3D)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub
        mPoliedros(Indice).Trasladar(Traslacion)
        POLI_ActualizarPoliedro(Indice)
    End Sub

    Public Sub POLI_TrasladarPoliedroADestino(ByVal Indice As Integer, ByVal Destino As Punto3D)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub
        mPoliedros(Indice).TrasladarADestino(Destino)
        POLI_ActualizarPoliedro(Indice)
    End Sub
    Public Sub POLI_ReestablecerTransformacionesPoliedro(ByVal Indice As Integer)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub
        mPoliedros(Indice).ReestablecerTransformaciones()
        POLI_ActualizarPoliedro(Indice)
    End Sub
    Public Sub POLI_EscalarPoliedro(ByVal Indice As Integer, ByVal Escalado As Punto3D)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub
        mPoliedros(Indice).CambiarEscala(Escalado)
        POLI_ActualizarPoliedro(Indice)
    End Sub

    Public Sub POLI_EscalarFijoPoliedro(ByVal Indice As Integer, ByVal Escalado As Punto3D)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub
        mPoliedros(Indice).EscalarFijo(Escalado)
        POLI_ActualizarPoliedro(Indice)
    End Sub

    Public Sub POLI_RotarPoliedro(ByVal Indice As Integer, ByVal EjeRotacion As EnumEje, ByVal AnguloRotacion As Single)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub
        mPoliedros(Indice).Rotar(EjeRotacion, AnguloRotacion)
        POLI_ActualizarPoliedro(Indice)
    End Sub

    Public Sub POLI_RotarPoliedros(ByVal Indices() As Integer, ByVal EjeRotacion As EnumEje, ByVal AnguloRotacion As Single)
        For i As Long = 0 To Indices.GetUpperBound(0)
            If Indices(i) < 0 Or Indices(i) > mPoliedros.GetUpperBound(0) Then Continue For
            mPoliedros(Indices(i)).Rotar(EjeRotacion, AnguloRotacion)
        Next

        POLI_ActualizarPoliedros()
    End Sub

    Public Sub POLI_RotarPoliedros(ByVal PrimerIndice As Integer, ByVal UltimoIndice As Integer, ByVal EjeRotacion As EnumEje, ByVal AnguloRotacion As Single)
        For i As Long = PrimerIndice To UltimoIndice
            If i < 0 Or i > mPoliedros.GetUpperBound(0) Then Continue For
            mPoliedros(i).Rotar(EjeRotacion, AnguloRotacion)
        Next

        POLI_ActualizarPoliedros()
    End Sub

    Public Sub POLI_RotarPoliedro(ByVal Indice As Integer, ByVal RectaRotacion As Recta3D, ByVal AnguloRotacion As Single)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub
        mPoliedros(Indice).Rotar(RectaRotacion, AnguloRotacion)
        POLI_ActualizarPoliedro(Indice)
    End Sub

    Public Sub POLI_RotarPoliedroSobreSiMismo(ByVal Indice As Integer, ByVal EjeRotacion As EnumEje, ByVal AnguloRotacion As Single)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub

        mPoliedros(Indice).RotarAlrededorCentro(EjeRotacion, AnguloRotacion)
        POLI_ActualizarPoliedro(Indice)
    End Sub

    '////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    Public Sub POLI_TrasladarPoliedro(ByVal Indice As Integer, ByVal Traslacion As Punto3D, ByVal Actualizar As Boolean)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub
        mPoliedros(Indice).Trasladar(Traslacion)
        If Actualizar Then POLI_ActualizarPoliedro(Indice)
    End Sub

    Public Sub POLI_TrasladarPoliedroADestino(ByVal Indice As Integer, ByVal Destino As Punto3D, ByVal Actualizar As Boolean)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub
        mPoliedros(Indice).TrasladarADestino(Destino)
        If Actualizar Then POLI_ActualizarPoliedro(Indice)
    End Sub
    Public Sub POLI_ReestablecerTransformacionesPoliedro(ByVal Indice As Integer, ByVal Actualizar As Boolean)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub
        mPoliedros(Indice).ReestablecerTransformaciones()
        If Actualizar Then POLI_ActualizarPoliedro(Indice)
    End Sub
    Public Sub POLI_EscalarPoliedro(ByVal Indice As Integer, ByVal Escalado As Punto3D, ByVal Actualizar As Boolean)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub
        mPoliedros(Indice).CambiarEscala(Escalado)
        If Actualizar Then POLI_ActualizarPoliedro(Indice)
    End Sub

    Public Sub POLI_EscalarFijoPoliedro(ByVal Indice As Integer, ByVal Escalado As Punto3D, ByVal Actualizar As Boolean)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub
        mPoliedros(Indice).EscalarFijo(Escalado)
        If Actualizar Then POLI_ActualizarPoliedro(Indice)
    End Sub

    Public Sub POLI_RotarPoliedro(ByVal Indice As Integer, ByVal EjeRotacion As EnumEje, ByVal AnguloRotacion As Single, ByVal Actualizar As Boolean)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub
        mPoliedros(Indice).Rotar(EjeRotacion, AnguloRotacion)
        If Actualizar Then POLI_ActualizarPoliedro(Indice)
    End Sub

    Public Sub POLI_RotarPoliedros(ByVal Indices() As Integer, ByVal EjeRotacion As EnumEje, ByVal AnguloRotacion As Single, ByVal Actualizar As Boolean)
        For i As Long = 0 To Indices.GetUpperBound(0)
            If Indices(i) < 0 Or Indices(i) > mPoliedros.GetUpperBound(0) Then Continue For
            mPoliedros(Indices(i)).Rotar(EjeRotacion, AnguloRotacion)
        Next

        If Actualizar Then POLI_ActualizarPoliedros()
    End Sub

    Public Sub POLI_RotarPoliedros(ByVal PrimerIndice As Integer, ByVal UltimoIndice As Integer, ByVal EjeRotacion As EnumEje, ByVal AnguloRotacion As Single, ByVal Actualizar As Boolean)
        For i As Long = PrimerIndice To UltimoIndice
            If i < 0 Or i > mPoliedros.GetUpperBound(0) Then Continue For
            mPoliedros(i).Rotar(EjeRotacion, AnguloRotacion)
        Next

        If Actualizar Then POLI_ActualizarPoliedros()
    End Sub

    Public Sub POLI_RotarPoliedro(ByVal Indice As Integer, ByVal RectaRotacion As Recta3D, ByVal AnguloRotacion As Single, ByVal Actualizar As Boolean)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub
        mPoliedros(Indice).Rotar(RectaRotacion, AnguloRotacion)
        POLI_ActualizarPoliedro(Indice)
    End Sub

    Public Sub POLI_RotarPoliedroSobreSiMismo(ByVal Indice As Integer, ByVal EjeRotacion As EnumEje, ByVal AnguloRotacion As Single, ByVal Actualizar As Boolean)
        If Indice < 0 Or Indice > mPoliedros.GetUpperBound(0) Then Exit Sub

        mPoliedros(Indice).RotarAlrededorCentro(EjeRotacion, AnguloRotacion)
        If Actualizar Then POLI_ActualizarPoliedro(Indice)
    End Sub

    Public Sub POLI_CambiarColorPoliedro(ByVal IndicePoliedro As Integer, ByVal Color As Color)
        mPoliedros(IndicePoliedro).EstablecerColores(Color)
    End Sub

    Public Sub POLI_ActualizarPoliedro(ByVal IndicePoliedro As Integer)
        BUFF_ActualizarPoliedro(IndicePoliedro)
        RaiseEvent POLI_FinActualizacionPoliedros()
        IluminacionModificada = True
    End Sub

    Public Sub POLI_ActualizarPoliedros()
        BUFF_ActualizarListaPoliedros()
        RaiseEvent POLI_FinActualizacionPoliedros()
    End Sub
#End Region

#Region "Dibujo/Pintado"
    Public Delegate Sub PINT_DelPintarPoligonoColor(ByVal Vertices() As Point, ByVal Color As Color)
    Public Delegate Sub PINT_DelPintarPoligonoColores(ByVal Vertices() As Point, ByVal Colores() As Color)
    Public Delegate Sub PINT_DelDibujarPoligonoColor(ByVal Vertices() As Point, ByVal Color As Color)

    Private Sub MetodoPredeterminadoRellenoPoligonoColor(ByVal Vertices() As Point, ByVal Color As Color)
        g.FillPolygon(New SolidBrush(Color), Vertices)
    End Sub
    Private Sub MetodoPredeterminadoRellenoPoligonoColores(ByVal Vertices() As Point, ByVal Colores() As Color)
        Try
            Dim Brush As New PathGradientBrush(Vertices)
            Brush.SurroundColors = Colores
            Brush.CenterColor = Colores(0)
            Brush.CenterPoint = Vertices(0)

            g.FillPolygon(Brush, Vertices)
        Catch ex As Exception
            Exit Sub
        End Try

    End Sub
    Private Sub MetodoPredeterminadoDibujadoPoligonoColor(ByVal Vertices() As Point, ByVal Color As Color)
        g.DrawPolygon(New Pen(Color), Vertices)
    End Sub


    Public Sub PINT_RedibujarSombreado(ByVal LimpiarSuperficie As Boolean)
        Dim t As DateTime = Now
        Dim Poligono() As Point
        Dim Path As New Drawing2D.GraphicsPath
        Dim Region As New Region


        If LimpiarSuperficie Then g.Clear(Color.White)


        For Each Poliedro As Poliedro In mPoliedros
            For Each Foco As Foco In mFocos
                For Each Cara As Cara In Poliedro.Caras

                    If VerDepuracionSombreado Then SalidaDepuracion("Generando sombreado de Poliedro(" & Array.IndexOf(mPoliedros, Poliedro) & ").Cara(" & Array.IndexOf(Poliedro.Caras, Cara) & ")...", DepuracionActiva.Sombreado)

                    If CALC3D_CalculoNormalCara(Poliedro, Cara) * New Vector3D(CALC3D_BaricentroCara(Poliedro, Cara), Foco.Coordenadas) > 0 Then
                        Poligono = CALCSOM_PoligonoSombra2D(Poliedro, Cara, New Plano3D(New Punto3D(0, 0, 0), New Punto3D(1, 0, 0), New Punto3D(0, 1, 0)), Foco.Coordenadas, Camara)

                        If VerDepuracionSombreado Then SalidaDepuracion("Visible para Foco(" & Array.IndexOf(mFocos, Foco) & "). Poligono sombra generado" & vbNewLine, DepuracionActiva.Sombreado)

                        Path.AddPolygon(Poligono)
                        'g.DrawPolygon(New Pen(Color.FromArgb(255 / 1, ColorProporcional(0, Poliedro.Caras.GetUpperBound(0) * 100, Array.IndexOf(Poliedro.Caras, Cara) * 100))), PoligonoSombra2D(Poliedro, Cara, New Plano3D(New Punto3D(0, 0, 0), New Punto3D(1, 0, 0), New Punto3D(0, 1, 0)), Foco))
                    Else
                        If VerDepuracionSombreado Then SalidaDepuracion("No visible para Foco(" & Array.IndexOf(mFocos, Foco) & ")" & vbNewLine, DepuracionActiva.Sombreado)
                    End If
                Next
                g.FillPath(New SolidBrush(Color.FromArgb(255 / 1, Color.Black)), Path)
                Path.ClearMarkers()
            Next
        Next

        If VerDepuracionSombreado Then SalidaDepuracion("SOMBREADO FINALIZADO" & vbNewLine, DepuracionActiva.Sombreado)

        TiempoSombreado = (Now - t).TotalMilliseconds
    End Sub

    Private Sub CalcularPosicionFocos()

    End Sub

    Public Sub PINT_RedibujarBuffer(ByVal LimpiarSuperficie As Boolean)
        Do While (True)

            Dim t As DateTime = Now
            Dim CaraBuffer As CaraBuffer
            Dim Brush As New SolidBrush(Color.White)
            Dim Colores() As Integer
            Dim MyColor As Integer
            Dim Triangulos() As SimpleTriangle

            If LimpiarSuperficie Then g.Clear(Color.Black)

            If VerDepuracionDibujado Then SalidaDepuracion("INICIANDO REDIBUJADO DEL BUFFER" & vbNewLine, DepuracionActiva.Dibujado)

            If mShader = Shader.GouraudShading Then
                For i As Integer = 0 To mPoliedros.GetUpperBound(0)
                    If (mPoliedros(i).PendienteCalculoColores OrElse ShaderModificado OrElse IluminacionModificada OrElse CamaraModificada) Then
                        mPoliedros(i).CalcularColores(mFocos, Iluminacion, Camara)
                        If VerDepuracionDibujado Then SalidaDepuracion("Calculando iluminacion de vertices de Poliedro(" & i & ")" & vbNewLine, DepuracionActiva.Dibujado)
                    End If
                Next
            End If

            'Wrapper.BeginUpdateObjects()

            For i As Integer = 0 To Buffer.Count - 1
                CaraBuffer = Buffer(i)
                If VerDepuracionDibujado Then SalidaDepuracion("Dibujando CaraBuffer(" & i & ") [" & mShader.ToString & "]..." & vbNewLine, DepuracionActiva.Dibujado)
                Select Case mShader
                    Case Shader.FlatShading
                        MyColor = CALCSOM_IluminacionModeloPhong(mFocos, Iluminacion, mPoliedros(Buffer(i).Indices.X).Caras(Buffer(i).Indices.Y).Baricentro.Normal, mPoliedros(Buffer(i).Indices.X).Caras(Buffer(i).Indices.Y).Baricentro.Coordenadas, mPoliedros(Buffer(i).Indices.X).Caras(Buffer(i).Indices.Y).Color, Camara).ToArgb

                        Triangulos = mPoliedros(Buffer(i).Indices.X).Caras(Buffer(i).Indices.Y).SimpleTriangles
                        ReDim Colores(mPoliedros(Buffer(i).Indices.X).Caras(Buffer(i).Indices.Y).Representacion.GetUpperBound(0))
                        For j As Integer = 0 To Colores.GetUpperBound(0)
                            Colores(j) = MyColor
                        Next

                        Wrapper.DrawPolygon(mPoliedros(Buffer(i).Indices.X).Caras(Buffer(i).Indices.Y).Representacion, Colores, Triangulos)
                    Case Shader.GouraudShading
                        Triangulos = mPoliedros(Buffer(i).Indices.X).Caras(Buffer(i).Indices.Y).SimpleTriangles
                        ReDim Colores(mPoliedros(Buffer(i).Indices.X).Caras(Buffer(i).Indices.Y).Representacion.GetUpperBound(0))
                        For j As Integer = 0 To Colores.GetUpperBound(0)
                            Colores(j) = mPoliedros(Buffer(i).Indices.X).Vertices(mPoliedros(Buffer(i).Indices.X).Caras(Buffer(i).Indices.Y).Vertices(j)).ColorIluminacion.ToArgb
                        Next

                        Wrapper.DrawPolygon(mPoliedros(Buffer(i).Indices.X).Caras(Buffer(i).Indices.Y).Representacion, Colores, Triangulos)
                End Select
            Next

            'Wrapper.EndUpdateObjects()

            IluminacionModificada = False
            CamaraModificada = False
            ShaderModificado = False

            If VerDepuracionDibujado Then SalidaDepuracion("REDIBUJADO FINALIZADO: " & TiempoRepintado.ToString & " ms" & vbNewLine & vbNewLine, DepuracionActiva.Dibujado)

            TiempoRepintado = (Now - t).TotalMilliseconds

            Wrapper.Refresh()
            RaiseEvent PINT_FinRedibujado()
        Loop
    End Sub

    Public Sub PINT_VisibilidadEjes(ByVal Visible As Boolean)
        DibujarEjes = Visible
    End Sub

    Public Sub PINT_CambiarSuperficie(ByVal PunteroSuperficie As IntPtr)
        Superficie = Control.FromHandle(PunteroSuperficie)
        BMPDibujo = New Bitmap(Superficie.Width, Superficie.Height)
        g = Graphics.FromImage(BMPDibujo)
        g.TranslateTransform(Superficie.Width / 2, Superficie.Height / 2)
    End Sub
#End Region

#Region "Buffer de dibujo"
    Public Sub BUFF_EstablecerAlgoritmoReordenacion(ByVal Algoritmo As AlgoritmoOrdenacion)
        AlgoritmoOrdenacion = Algoritmo
    End Sub
    Public Sub BUFF_ReOrdenarBuffer()
        'Exit Sub
        Dim t As DateTime
        If Buffer.Count = 0 Then Exit Sub
        t = Now

        If VerDepuracionBuffer Then SalidaDepuracion("INICIANDO REORDENACION DEL BUFFER [" & AlgoritmoOrdenacion.ToString & "]..." & vbNewLine, DepuracionActiva.Buffer)
        Select Case AlgoritmoOrdenacion
            Case Global.Motor3D.AlgoritmoOrdenacion.BubbleSort
                Buffer = UTIL_Burbuja(Buffer, Camara.Posicion)
            Case Global.Motor3D.AlgoritmoOrdenacion.ListSort
                Buffer.Sort(New ComparadorCaraBuffer(Camara))
            Case Global.Motor3D.AlgoritmoOrdenacion.QuickSort
                Buffer = UTIL_QuickSort(Buffer, 0, Buffer.Count - 1)
            Case Global.Motor3D.AlgoritmoOrdenacion.BogoSort
                Buffer = UTIL_BogoSort(Buffer, Camara.Posicion)
        End Select

        TiempoOrdenacion = (Now - t).TotalMilliseconds

        If VerDepuracionBuffer Then SalidaDepuracion("REORDENACION DEL BUFFER FINALIZADA: " & TiempoOrdenacion.ToString & " ms" & vbNewLine & vbNewLine, DepuracionActiva.Buffer)

        RaiseEvent BUFF_FinReordenacionBuffer()
    End Sub

    Public Sub BUFF_ActualizarPoliedro(ByVal IndicePoliedro As Integer)
        Dim t As DateTime
        Dim C As CaraBuffer

        If mPoliedros(IndicePoliedro).PendienteAplicacionTransformaciones Then
            t = Now


            If VerDepuracionBuffer Then
                SalidaDepuracion("INICIANDO ACTUALIZACION DE POLIEDRO" & vbNewLine, DepuracionActiva.Buffer)
                SalidaDepuracion("Aplicando transformaciones de Poliedro(" & IndicePoliedro & ")..." & vbNewLine, DepuracionActiva.Buffer)
            End If


            mPoliedros(IndicePoliedro).AplicarTransformaciones(mShader = Shader.GouraudShading Or mShader = Shader.PhongShading)
            'mPoliedros(IndicePoliedro).CalcularRepresentaciones(Camara)
            mPoliedros(IndicePoliedro).Modificado = True

            For i As Long = 0 To Buffer.Count - 1
                If Buffer(i).Indices.X = IndicePoliedro Then
                    C = Buffer(i)
                    C.PendienteActulizacion = True
                    Buffer(i) = C

                    If VerDepuracionBuffer Then SalidaDepuracion("  CaraBuffer(" & i & ") preparada para actualizacion" & vbNewLine, DepuracionActiva.Buffer)
                End If
            Next
            TiempoTramsformacion = (Now - t).TotalMilliseconds

            If VerDepuracionBuffer Then SalidaDepuracion("ACTUALIZACION DE POLIEDRO FINALIZADA: " & TiempoTramsformacion.ToString & " ms" & vbNewLine & vbNewLine, DepuracionActiva.Buffer)

            BUFF_RecargarBuffer()
        Else
            If CamaraModificada Then
                t = Now


                If VerDepuracionBuffer Then
                    SalidaDepuracion("INICIANDO ACTUALIZACION DE POLIEDRO" & vbNewLine, DepuracionActiva.Buffer)
                    SalidaDepuracion("Camara modificada" & vbNewLine, DepuracionActiva.Buffer)
                End If

                For i As Long = 0 To Buffer.Count - 1
                    If Buffer(i).Indices.X = IndicePoliedro Then
                        C = Buffer(i)
                        C.PendienteActulizacion = True
                        Buffer(i) = C

                        If VerDepuracionBuffer Then SalidaDepuracion("  CaraBuffer(" & i & ") preparada para actualizacion" & vbNewLine, DepuracionActiva.Buffer)
                    End If
                Next
                TiempoTramsformacion = (Now - t).TotalMilliseconds

                If VerDepuracionBuffer Then SalidaDepuracion("ACTUALIZACION DE POLIEDRO FINALIZADA: " & TiempoTramsformacion.ToString & " ms" & vbNewLine & vbNewLine, DepuracionActiva.Buffer)

                BUFF_RecargarBuffer()
            End If
        End If
    End Sub
    Public Sub BUFF_ActualizarListaPoliedros()
        Dim t As DateTime = Now
        Dim C As CaraBuffer
        Dim Modificados As Boolean = False

        For i As Integer = 0 To mPoliedros.GetUpperBound(0)
            If mPoliedros(i).PendienteAplicacionTransformaciones Then
                If VerDepuracionBuffer Then SalidaDepuracion("Aplicando transformaciones de Poliedro(" & i & ")..." & vbNewLine, DepuracionActiva.Buffer)

                mPoliedros(i).AplicarTransformaciones(mShader = Shader.GouraudShading Or mShader = Shader.PhongShading)
                'mPoliedros(i).CalcularRepresentaciones(Camara)
                mPoliedros(i).Modificado = True

                For j As Long = 0 To Buffer.Count - 1
                    If Buffer(j).Indices.X = i Then
                        C = Buffer(j)
                        C.PendienteActulizacion = True
                        Buffer(j) = C

                        If VerDepuracionBuffer Then SalidaDepuracion("  CaraBuffer(" & j & ") preparada para actualizacion" & vbNewLine, DepuracionActiva.Buffer)
                    End If
                Next
                Modificados = True
            Else
                If VerDepuracionBuffer Then SalidaDepuracion("Camara modificada" & vbNewLine, DepuracionActiva.Buffer)

                For j As Long = 0 To Buffer.Count - 1
                    If Buffer(j).Indices.X = i Then
                        C = Buffer(j)
                        C.PendienteActulizacion = True
                        Buffer(j) = C

                        If VerDepuracionBuffer Then SalidaDepuracion("  CaraBuffer(" & j & ") preparada para actualizacion" & vbNewLine, DepuracionActiva.Buffer)
                    End If
                Next
                Modificados = True
            End If

        Next
        TiempoTramsformacion = (Now - t).TotalMilliseconds

        If VerDepuracionBuffer Then SalidaDepuracion("ACTUALIZACION POLIEDROS FINALIZADA: " & TiempoTramsformacion.ToString & " ms" & vbNewLine & vbNewLine, DepuracionActiva.Buffer)

        If Modificados Then BUFF_RecargarBuffer()
    End Sub

    Private Sub RecargarBuffer()
        Dim t As DateTime = Now
        Dim CopiaBuffer As New List(Of CaraBuffer)
        Dim RequisitoTrasEliminarPoliedro As Boolean
        Dim Visibilidad As Boolean

        If VerDepuracionBuffer Then SalidaDepuracion("INICIANDO RECARGA DEL BUFFER" & vbNewLine, DepuracionActiva.Buffer)

        CopiaBuffer.Clear()

        For Each CaraBuffer As CaraBuffer In Buffer
            If VerDepuracionBuffer Then SalidaDepuracion("  Exmaninando buffer...CaraBuffer(" & Buffer.IndexOf(CaraBuffer) & "): ", DepuracionActiva.Buffer)
            RequisitoTrasEliminarPoliedro = Not (CaraBuffer.Indices.X > mPoliedros.GetUpperBound(0) OrElse CaraBuffer.Indices.Y > mPoliedros(CaraBuffer.Indices.X).Caras.GetUpperBound(0))
            If RequisitoTrasEliminarPoliedro AndAlso CALC3D_OrientacionCara(Camara, mPoliedros(CaraBuffer.Indices.X).Caras(CaraBuffer.Indices.Y)) Then
                If VerDepuracionBuffer Then SalidaDepuracion("VISIBLE ", DepuracionActiva.Buffer)
                If CaraBuffer.PendienteActulizacion Then
                    CaraBuffer.DistanciaCamara = CALC3D_Distancia(Camara.Posicion, mPoliedros(CaraBuffer.Indices.X).Caras(CaraBuffer.Indices.Y).Baricentro.Coordenadas)
                    mPoliedros(CaraBuffer.Indices.X).RepresentacionCara(CaraBuffer.Indices.Y, Camara)
                    If VerDepuracionBuffer Then
                        SalidaDepuracion("(Actualizada): " & vbNewLine, DepuracionActiva.Buffer)
                        SalidaDepuracion("   --> Indices: Poliedro(" & CaraBuffer.Indices.X & ",Cara(" & CaraBuffer.Indices.Y & ")" & vbNewLine, DepuracionActiva.Buffer)
                        SalidaDepuracion("   --> Distancia a camara: " & CaraBuffer.DistanciaCamara & " (Pixels)" & vbNewLine, DepuracionActiva.Buffer)
                        SalidaDepuracion("   --> Representacion (Poligono 2D):" & vbNewLine, DepuracionActiva.Buffer)
                        For j As Integer = 0 To mPoliedros(CaraBuffer.Indices.X).Caras(CaraBuffer.Indices.Y).Representacion.GetUpperBound(0)
                            SalidaDepuracion("   ===> Vertice(" & j & "): " & mPoliedros(CaraBuffer.Indices.X).Caras(CaraBuffer.Indices.Y).Representacion(j).ToString & vbNewLine, DepuracionActiva.Buffer)
                        Next
                    Else
                        If VerDepuracionBuffer Then SalidaDepuracion(vbNewLine, DepuracionActiva.Buffer)
                    End If
                End If
                CopiaBuffer.Add(CaraBuffer)
                mPoliedros(CaraBuffer.Indices.X).Caras(CaraBuffer.Indices.Y).CargadaEnBuffer = True
            Else
                If VerDepuracionBuffer Then SalidaDepuracion("NO VISIBLE (ELIMINADA)" & vbNewLine, DepuracionActiva.Buffer)
                If RequisitoTrasEliminarPoliedro Then mPoliedros(CaraBuffer.Indices.X).Caras(CaraBuffer.Indices.Y).CargadaEnBuffer = False
            End If
        Next
        Buffer.Clear()

        For Each CaraBuffer As CaraBuffer In CopiaBuffer
            Buffer.Add(CaraBuffer)
        Next

        If VerDepuracionBuffer Then SalidaDepuracion(vbNewLine & "  INICIANDO COMPROBACION DE PERTENENCIA A BUFFER..." & vbNewLine & vbNewLine, DepuracionActiva.Buffer)

        For i As Long = 0 To mPoliedros.GetUpperBound(0)
            For j As Long = 0 To mPoliedros(i).Caras.GetUpperBound(0)
                Visibilidad = CALC3D_OrientacionCara(Camara, mPoliedros(i).Caras(j))
                If VerDepuracionBuffer Then SalidaDepuracion("  Comprobando Poliedro(" & i & ").Cara(" & j & "): ", DepuracionActiva.Buffer)

                If Not mPoliedros(i).Caras(j).CargadaEnBuffer Then
                    If VerDepuracionBuffer Then SalidaDepuracion("[NO CARGADA EN BUFFER]-", DepuracionActiva.Buffer)
                    If CALC3D_OrientacionCara(Camara, mPoliedros(i).Caras(j)) Then
                        Buffer.Add(New CaraBuffer(i, j, mPoliedros(i).Caras(j), CALC3D_Distancia(mPoliedros(i).Caras(j).Baricentro.Coordenadas, Camara.Posicion)))
                        mPoliedros(i).RepresentacionCara(j, Camara)
                        mPoliedros(i).Caras(j).CargadaEnBuffer = True
                        If VerDepuracionBuffer Then
                            SalidaDepuracion("VISIBLE (AÑADIENDO..." & vbNewLine, DepuracionActiva.Buffer)
                            SalidaDepuracion("   --> Indices: Poliedro(" & i & ",Cara(" & j & ")" & vbNewLine, DepuracionActiva.Buffer)
                            SalidaDepuracion("   --> Distancia a camara: " & Buffer(Buffer.Count - 1).DistanciaCamara & " (Pixels)" & vbNewLine, DepuracionActiva.Buffer)
                            SalidaDepuracion("   --> Representacion (Poligono 2D):" & vbNewLine, DepuracionActiva.Buffer)
                            For k As Integer = 0 To mPoliedros(i).Caras(j).Representacion.GetUpperBound(0)
                                SalidaDepuracion("   ===> Vertice(" & k & "): " & mPoliedros(i).Caras(j).Representacion(k).ToString & vbNewLine, DepuracionActiva.Buffer)
                            Next
                            SalidaDepuracion("  ...OK)" & vbNewLine, DepuracionActiva.Buffer)
                        End If

                    Else
                        If VerDepuracionBuffer Then SalidaDepuracion("NO VISIBLE" & vbNewLine, DepuracionActiva.Buffer)
                    End If
                Else
                    If VerDepuracionBuffer Then SalidaDepuracion("[CARGADA EN BUFFER]" & vbNewLine, DepuracionActiva.Buffer)
                End If
                'Cargada = False
                'For k = 0 To IndiceMin
                '    If CopiaBuffer(k).Indices = New Point(i, j) Then
                '        Cargada = True
                '        Exit For
                '    End If
                'Next
            Next
        Next

        'Buffer.Clear()

        'For i As Long = 0 To mPoliedros.GetUpperBound(0)
        '    For j As Long = 0 To mPoliedros(i).Caras.GetUpperBound(0)
        '        If CALC3D_OrientacionCara(Camara, mPoliedros(i), mPoliedros(i).Caras(j)) Then
        '            Buffer.Add(New CaraBuffer(i, mPoliedros(i), mPoliedros(i).Caras(j), Camara))
        '        End If
        '    Next
        'Next

        TiempoRecargaBuffer = (Now - t).Milliseconds

        If VerDepuracionBuffer Then SalidaDepuracion("RECARGA DEL BUFFER FINALIZADA (" & Buffer.Count & " caras): " & TiempoRecargaBuffer.ToString & " ms" & vbNewLine & vbNewLine, DepuracionActiva.Buffer)


        RaiseEvent BUFF_FinRecargaBuffer(Buffer)

        BUFF_ReOrdenarBuffer()
    End Sub

    'Private Sub RecargarBuffer()
    '    Dim t As DateTime = Now
    '    Dim CopiaBuffer As New List(Of CaraBuffer)
    '    Dim Cargada As Boolean
    '    Dim Cara As CaraBuffer
    '    Dim IndicePoliedro, IndiceCara, IndiceVertice As Long
    '    Dim IndiceMin As Long
    '    Dim Visibilidad As Boolean

    '    Dim Ev1, Ev2, Ev3, Ev4 As Boolean

    '    If VerDepuracionBuffer Then SalidaDepuracion("INICIANDO RECARGA DEL BUFFER" & vbNewLine, DepuracionActiva.Buffer)

    '    For Each CaraBuffer As CaraBuffer In Buffer
    '        CopiaBuffer.Add(CaraBuffer)
    '    Next
    '    'ReDim CarasBuffer(0)

    '    For i As Long = 0 To Buffer.Count - 1
    '        Ev1 = Buffer(i).Indices.X > mPoliedros.GetUpperBound(0)
    '        If Not Ev1 Then
    '            Ev2 = Buffer(i).Indices.Y > mPoliedros(Buffer(i).Indices.X).Caras.GetUpperBound(0)
    '            If Not Ev2 Then
    '                Ev3 = mPoliedros(Buffer(i).Indices.X).Caras(Buffer(i).Indices.Y).Vertices.GetUpperBound(0) <> Buffer(i).VerticesPoligonoDibujo.GetUpperBound(0)
    '                Ev4 = Not CALC3D_OrientacionCara(Camara, mPoliedros(Buffer(i).Indices.X), mPoliedros(Buffer(i).Indices.X).Caras(Buffer(i).Indices.Y))
    '            End If
    '        Else
    '            Ev2 = True
    '        End If

    '        If Ev1 OrElse Ev2 OrElse Ev3 OrElse Ev4 Then
    '            CopiaBuffer.Remove(Buffer(i))

    '            If VerDepuracionBuffer Then SalidaDepuracion("CaraBuffer(" & i & ") eliminada" & vbNewLine, DepuracionActiva.Buffer)
    '        Else
    '            If Buffer(i).PendienteActulizacion Then
    '                IndicePoliedro = Buffer(i).Indices.X
    '                IndiceCara = Buffer(i).Indices.Y

    '                Cara.Normal = CALC3D_CalculoNormalCara(mPoliedros(IndicePoliedro), mPoliedros(IndicePoliedro).Caras(IndiceCara))
    '                Cara.Baricentro = CALC3D_BaricentroCara(mPoliedros(IndicePoliedro), mPoliedros(IndicePoliedro).Caras(IndiceCara))

    '                'Cara.VerticesPoligonoDibujo = mPoliedros(Cara.Indices.X).RepresentacionCara(Cara.Indices.Y, Camara)

    '                ReDim Cara.VerticesPoligonoDibujo(mPoliedros(Cara.Indices.X).Caras(Cara.Indices.Y).Vertices.GetUpperBound(0))
    '                For k As Integer = 0 To mPoliedros(Cara.Indices.X).Caras(Cara.Indices.Y).Vertices.GetUpperBound(0)
    '                    Cara.VerticesPoligonoDibujo(k) = mPoliedros(Cara.Indices.X).Vertices(mPoliedros(Cara.Indices.X).Caras(Cara.Indices.Y).Vertices(k)).Coordenadas.Representacion2D(Camara)
    '                Next
    '                Cara.ColorCara = Buffer(i).ColorCara
    '                Cara.Indices = Buffer(i).Indices
    '                Cara.PendienteActulizacion = False

    '                CopiaBuffer(CopiaBuffer.IndexOf(Buffer(i))) = Cara

    '                If VerDepuracionBuffer Then
    '                    SalidaDepuracion("CaraBuffer(" & i & ") actualizada [Indices: Poliedro(" & Cara.Indices.X & ",Cara(" & Cara.Indices.Y & ")]:" & vbNewLine, DepuracionActiva.Buffer)
    '                    SalidaDepuracion("  Vector normal: " & Cara.Normal.ToString & vbNewLine, DepuracionActiva.Buffer)
    '                    SalidaDepuracion("  Baricentro: " & Cara.Baricentro.ToString & vbNewLine, DepuracionActiva.Buffer)
    '                    SalidaDepuracion("  Color: RGB(" & Cara.ColorCara.R & "," & Cara.ColorCara.G & "," & Cara.ColorCara.B & ")" & vbNewLine, DepuracionActiva.Buffer)
    '                    SalidaDepuracion("  Representacion (Poligono 2D):" & vbNewLine, DepuracionActiva.Buffer)
    '                    For j As Integer = 0 To Cara.VerticesPoligonoDibujo.GetUpperBound(0)
    '                        SalidaDepuracion("      Vertice(" & j & "): " & Cara.VerticesPoligonoDibujo(j).ToString & vbNewLine, DepuracionActiva.Buffer)
    '                    Next
    '                End If
    '            End If
    '        End If

    '    Next
    '    Buffer.Clear()
    '    For Each CaraBuffer As CaraBuffer In CopiaBuffer
    '        Buffer.Add(CaraBuffer)
    '    Next

    '    IndiceMin = Buffer.Count - 1
    '    If VerDepuracionBuffer Then SalidaDepuracion("LONGITUD PREVIA DEL BUFFER: " & Buffer.Count & " caras. (Ultimo indice: " & Buffer.Count - 1 & ")" & vbNewLine, DepuracionActiva.Buffer)

    '    For i As Long = 0 To mPoliedros.GetUpperBound(0)
    '        If mPoliedros(i).Modificado Then
    '            For j As Long = 0 To mPoliedros(i).Caras.GetUpperBound(0)
    '                Visibilidad = CALC3D_OrientacionCara(Camara, mPoliedros(i), mPoliedros(i).Caras(j))
    '                If Visibilidad Then
    '                    Cargada = False
    '                    For h As Long = 0 To IndiceMin
    '                        If IndiceMin < 0 Then
    '                            Cargada = False
    '                            If VerDepuracionBuffer Then SalidaDepuracion("Buffer vacio (No pertenencia asegurada)" & vbNewLine, DepuracionActiva.Buffer)
    '                            Exit For
    '                        End If
    '                        If Buffer(h).Indices = New Point(i, j) Then
    '                            Cargada = True
    '                        Else
    '                            Cargada = False
    '                        End If

    '                        If VerDepuracionBuffer Then SalidaDepuracion("  Examinando pertenencia de Poliedro(" & i & ").Cara(" & j & ")...CaraBuffer(" & h & ") [" & IIf(Cargada, "CARGADA]", "NO CARGADA]") & vbNewLine, DepuracionActiva.Buffer)

    '                        If Cargada Then
    '                            If VerDepuracionBuffer Then SalidaDepuracion(vbNewLine, DepuracionActiva.Buffer)
    '                            Exit For
    '                        End If

    '                    Next

    '                    If Not Cargada Then

    '                        Buffer.Add(New CaraBuffer(i, mPoliedros(i), mPoliedros(i).Caras(j), Camara))

    '                        If VerDepuracionBuffer Then
    '                            SalidaDepuracion("Nueva CaraBuffer [Poliedro(" & i & ").Cara(" & j & ")]" & vbNewLine, DepuracionActiva.Buffer)
    '                            SalidaDepuracion("  Vector normal: " & Buffer(Buffer.Count - 1).Normal.ToString & vbNewLine, DepuracionActiva.Buffer)
    '                            SalidaDepuracion("  Baricentro: " & Buffer(Buffer.Count - 1).Baricentro.ToString & vbNewLine, DepuracionActiva.Buffer)
    '                            SalidaDepuracion("  Color: RGB(" & Buffer(Buffer.Count - 1).ColorCara.R & "," & Cara.ColorCara.G & "," & Cara.ColorCara.B & ")" & vbNewLine, DepuracionActiva.Buffer)
    '                            SalidaDepuracion("  Representacion (Poligono 2D):" & vbNewLine, DepuracionActiva.Buffer)
    '                            For k As Integer = 0 To Buffer(Buffer.Count - 1).VerticesPoligonoDibujo.GetUpperBound(0)
    '                                SalidaDepuracion("      Vertice(" & k & "): " & Buffer(Buffer.Count - 1).VerticesPoligonoDibujo(k).ToString & vbNewLine, DepuracionActiva.Buffer)
    '                            Next
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            mPoliedros(i).Modificado = False
    '        End If

    '    Next

    '    If CaraSeñalada < 0 Then CaraSeñalada = 0
    '    If CaraSeñalada > Buffer.Count - 1 Then CaraSeñalada = Buffer.Count - 1

    '    TiempoRecargaBuffer = (Now - t).TotalMilliseconds

    '    If VerDepuracionBuffer Then SalidaDepuracion("RECARGA DEL BUFFER FINALIZADA (" & Buffer.Count & " caras): " & TiempoRecargaBuffer.ToString & " ms" & vbNewLine & vbNewLine, DepuracionActiva.Buffer)

    '    RaiseEvent BUFF_FinRecargaBuffer(Buffer)
    '    BUFF_ReOrdenarBuffer()
    'End Sub

    Public Sub BUFF_RecargarBuffer()
        'Hilo = New Thread(AddressOf RecargaBuffer)
        'Hilo.Start()
        Call RecargarBuffer()
    End Sub

    Public Sub BUFF_EliminarCaraBuffer(ByVal CaraBuffer As CaraBuffer)
        Buffer.Remove(CaraBuffer)
    End Sub

    Public Sub BUFF_AñadirCaraBuffer(ByVal CaraBuffer As CaraBuffer)
        Buffer.Add(CaraBuffer)
    End Sub
#End Region

    Public Sub IniciarRender()
        PINT_RedibujarBuffer(True)
    End Sub

    Public Sub New(ByVal PunteroSuperficie As IntPtr, ByVal Poliedro As Poliedro, ByVal Foco As Foco)
        ReDim mPoliedros(0)
        ReDim mFocos(0)

        Me.PINT_Sombreado = False

        Superficie = Control.FromHandle(PunteroSuperficie)

        Wrapper = New DxLibWrapper(PunteroSuperficie, Superficie.Size)

        BMPDibujo = New Bitmap(Superficie.Width, Superficie.Height)
        g = Graphics.FromImage(BMPDibujo)
        g.TranslateTransform(BMPDibujo.Width / 2, BMPDibujo.Height / 2)

        Iluminacion.EstablecerAportaciones(0, 1, 1)
        Iluminacion.EstablecerConstantesDistancia(0.0000001, 0.0000001, 0.000000001)
        Iluminacion.EstablecerExponenteEspecular(5)

        mFocos(0).Radio = 100
        mFocos(0).VectorDireccion = New Vector3D(-1, -1, -1)
        mFocos(0).Delta = 30

        Camara.Reiniciar()
        Camara.EstablecerDistancia(33)
        Camara.EstablecerRepresentacion(TipoRepresentacion.Isometrica)

        mShader = Shader.FlatShading

        AlgoritmoOrdenacion = Global.Motor3D.AlgoritmoOrdenacion.ListSort

        MetodoDibujadoPoligonoColor = AddressOf MetodoPredeterminadoDibujadoPoligonoColor
        MetodoRellenoPoligonoColor = AddressOf MetodoPredeterminadoRellenoPoligonoColor
        MetodoRellenoPoligonoColores = AddressOf MetodoPredeterminadoRellenoPoligonoColores

        mPoliedros(0) = Poliedro
        mPoliedros(0).EscalarFijo(New Punto3D(2, 2, 2))
        mPoliedros(0).TrasladarADestino(New Punto3D)
        mPoliedros(0).AplicarTransformaciones(False)
        mPoliedros(0).Modificado = True
        mFocos(0) = Foco

        RecargarBuffer()
        'PINT_RedibujarBuffer(True)
    End Sub

    Public Overrides Function ToString() As String
        Return "Motor3D {" & Me.mPoliedros.GetUpperBound(0) + 1 & " poliedros," & mFocos.GetUpperBound(0) + 1 & " focos}"
    End Function
End Class
#End Region
