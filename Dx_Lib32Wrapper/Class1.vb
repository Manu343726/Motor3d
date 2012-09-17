Imports System.Drawing
Imports System.Math
Imports dx_lib32
Imports System.Threading

Public Structure SimpleTriangle
    Public Vert1, Vert2, Vert3 As Long

    Public Sub New(ByVal ValVert1 As Long, ByVal ValVert2 As Long, ByVal ValVert3 As Long)
        Vert1 = ValVert1
        Vert2 = ValVert2
        Vert3 = ValVert3
    End Sub
End Structure

Public Interface DxLibObject
    Sub Draw(ByRef DXG As dx_GFX_Class)
End Interface

Public Class DxLibTriangle
    Implements DxLibObject

    Private mVerts() As Point
    Private mColors() As Integer

    Private DrawCenter As Point

    Private Vertex() As Vertex

    Public Property Verts() As Point()
        Get
            Return mVerts
        End Get
        Set(ByVal value As Point())
            mVerts = value
            SetVertex()
        End Set
    End Property

    Public Property Colors() As Integer()
        Get
            Return mColors
        End Get
        Set(ByVal value() As Integer)
            mColors = value
            SetVertex()
        End Set
    End Property

    Public Sub New(ByVal ValDrawCenter As Point)
        ReDim mVerts(2)
        ReDim mColors(2)

        SetVertex()
    End Sub

    Public Sub New(ByVal Vert1 As Point, ByVal Vert2 As Point, ByVal Vert3 As Point, ByVal Color1 As Integer, ByVal Color2 As Integer, ByVal Color3 As Integer, ByVal ValDrawCenter As Point)
        ReDim mVerts(2)
        ReDim mColors(2)

        DrawCenter = ValDrawCenter

        mVerts(0) = Vert1
        mColors(0) = Color1
        mVerts(1) = Vert2
        mColors(1) = Color2
        mVerts(2) = Vert3
        mColors(2) = Color3

        SetVertex()
    End Sub

    Public Sub New(ByVal VertsPositions() As Point, ByVal VertColors() As Integer, ByVal ValDrawCenter As Point)
        mVerts = VertsPositions
        mColors = VertColors

        DrawCenter = ValDrawCenter

        SetVertex()
    End Sub

    Public Sub SetVertex()
        ReDim Vertex(3)

        Vertex(3).X = mVerts(0).X + DrawCenter.X
        Vertex(3).Y = mVerts(0).Y + DrawCenter.Y
        Vertex(3).Color = mColors(0)
        Vertex(1).X = mVerts(1).X + DrawCenter.X
        Vertex(1).Y = mVerts(1).Y + DrawCenter.Y
        Vertex(1).Color = mColors(1)
        Vertex(0).X = mVerts(2).X + DrawCenter.X
        Vertex(0).Y = mVerts(2).Y + DrawCenter.Y
        Vertex(0).Color = mColors(2)
        Vertex(2).X = mVerts(2).X + DrawCenter.X
        Vertex(2).Y = mVerts(2).Y + DrawCenter.Y
        Vertex(2).Color = mColors(2)
    End Sub

    Public Sub Draw(ByRef DXG As dx_GFX_Class) Implements DxLibObject.Draw
        DXG.DRAW_Trapezoid(Vertex)
    End Sub
End Class

Public Class DxLibPolygon
    Implements DxLibObject

    Private mTriangles() As DxLibTriangle
    Private DrawCenter As Point

    Public Sub New(ByVal Verts() As Point, ByVal Colors() As Integer, ByVal Triangles() As SimpleTriangle, ByVal ValDrawCenter As Point)
        ReDim mTriangles(Triangles.GetUpperBound(0))

        DrawCenter = ValDrawCenter

        For i As Integer = 0 To mTriangles.GetUpperBound(0)
            mTriangles(i) = New DxLibTriangle(Verts(Triangles(i).Vert1), Verts(Triangles(i).Vert2), Verts(Triangles(i).Vert3), Colors(Triangles(i).Vert1), Colors(Triangles(i).Vert2), Colors(Triangles(i).Vert3), DrawCenter)
        Next
    End Sub


    Public Sub Draw(ByRef DXG As dx_lib32.dx_GFX_Class) Implements DxLibObject.Draw
        For Each Triangle As DxLibTriangle In mTriangles
            Triangle.Draw(DXG)
        Next
    End Sub
End Class

Public Class DxLibWrapper
    Private Updating As Boolean
    Private DXG As dx_GFX_Class

    Private Objects As List(Of DxLibObject)
    Private CacheObjects() As DxLibObject

    Private CanvasHandle As IntPtr
    Private CanvasSize As Point

    Private RenderThread As Thread

    Private mDrawCenter As Point

    Dim Render As Boolean
    Private mDrawEnd As Boolean

    Public Property DrawCenter() As Point
        Get
            Return mDrawCenter
        End Get
        Set(ByVal value As Point)
            mDrawCenter = value
        End Set
    End Property

    Public ReadOnly Property DrawEnd() As Boolean
        Get
            Return mDrawEnd
        End Get
    End Property

    Public Sub New(ByVal ValCanvasHandle As IntPtr, ByVal Size As Point)
        Objects = New List(Of DxLibObject)
        Render = False
        DXG = New dx_GFX_Class
        DXG.Init(ValCanvasHandle, Size.X, Size.Y, 32, True, True, True, 60)
        DXG.DEVICE_SetDrawCenter(Size.X / 2, Size.Y / 2)
        CanvasSize = Size
        mDrawCenter = New Point(Size.X / 2, Size.Y / 2)

        RenderThread = New Thread(AddressOf RenderLoop)
    End Sub

    Public Sub Start()
        Objects.CopyTo(CacheObjects)
        DXG.DEVICE_SetDrawCenter(CanvasSize.X / 2, CanvasSize.Y / 2)

        Render = True
        'RenderLoop()
    End Sub

    Public Sub BeginUpdateObjects()
        Objects.Clear()
        Updating = True
    End Sub

    Public Sub AddPolygon(ByVal Verts() As Point, ByVal Colors() As Integer, ByVal Triangles() As SimpleTriangle)
        Objects.Add(New DxLibPolygon(Verts, Colors, Triangles, mDrawCenter))
    End Sub

    Public Sub EndUpdateObjects()
        If Objects.Count > 0 Then
            Updating = False
            CacheObjects = Objects.ToArray
        Else
            Throw New Exception("La actualización de la escena ha finalizado y el buffer de objetos está vacío")
        End If
    End Sub

    Private Sub RenderLoop()
        Do While Render
            mDrawEnd = False
            For Each DxLibObject As DxLibObject In CacheObjects
                DxLibObject.Draw(DXG)
            Next
            DXG.Frame()
            mDrawEnd = True
        Loop
    End Sub

    Public Sub DrawPolygon(ByVal Polygon As DxLibPolygon)
        Polygon.Draw(DXG)
    End Sub

    Public Sub DrawPolygon(ByVal Verts() As Point, ByVal Colors() As Integer, ByVal Triangles() As SimpleTriangle)
        Dim P As New DxLibPolygon(Verts, Colors, Triangles, mDrawCenter)
        P.Draw(DXG)
    End Sub

    Public Sub Refresh()
        DXG.Frame()
    End Sub

    Public Sub Draw()
        For Each DxLibObject As DxLibObject In CacheObjects
            DxLibObject.Draw(DXG)
        Next
        DXG.Frame()
    End Sub

    Public Sub Dispose()
        Render = False
        RenderThread.Suspend()
        DXG.Terminate()
    End Sub
End Class

