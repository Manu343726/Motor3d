Public Class Form3

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call Form1.DestruirHandler(Me)
        Me.Close()
    End Sub

    Public Sub RellenarBuffer(ByVal Buffer As List(Of CaraBuffer))
        Dim Nodo As TreeNode
        Dim SubNodoNormal As TreeNode
        Dim SubNodoBaricentro As TreeNode
        Dim SubNodoColor As TreeNode
        Dim SubNodoIndices As TreeNode
        Dim SubNodoVertices As TreeNode

        TreeView1.ContextMenuStrip = Context

        Try
            TreeView1.Nodes.Clear()
        Catch ex As Exception
            Exit Sub
        End Try


        For Each CaraBuffer As CaraBuffer In Buffer
            Nodo = New TreeNode
            SubNodoNormal = New TreeNode
            SubNodoBaricentro = New TreeNode
            SubNodoIndices = New TreeNode
            SubNodoColor = New TreeNode
            SubNodoVertices = New TreeNode

            Nodo.Name = "Buffer(" & Buffer.IndexOf(CaraBuffer) & ")"
            Nodo.Text = "CaraBuffer(" & Buffer.IndexOf(CaraBuffer) & ")"



            SubNodoIndices.Text = "Indices (Point)"
            SubNodoIndices.Name = "Buffer(" & Buffer.IndexOf(CaraBuffer) & ")Indices"
            SubNodoIndices.Nodes.Add("Poliedro (X): " & CaraBuffer.Indices.X)
            SubNodoIndices.Nodes.Add("Cara (Y): " & CaraBuffer.Indices.Y)
            SubNodoIndices.ImageIndex = 0

            For Each N As TreeNode In SubNodoNormal.Nodes
                N.ImageIndex = SubNodoNormal.ImageIndex
            Next
            For Each N As TreeNode In SubNodoBaricentro.Nodes
                N.ImageIndex = SubNodoBaricentro.ImageIndex
            Next
            For Each N As TreeNode In SubNodoIndices.Nodes
                N.ImageIndex = SubNodoIndices.ImageIndex
            Next
            For Each N As TreeNode In SubNodoColor.Nodes
                N.ImageIndex = SubNodoColor.ImageIndex
            Next
            For Each N As TreeNode In SubNodoVertices.Nodes
                N.ImageIndex = SubNodoVertices.ImageIndex
            Next

            Nodo.Nodes.Add(SubNodoNormal)
            Nodo.Nodes.Add(SubNodoBaricentro)
            Nodo.Nodes.Add(SubNodoIndices)
            Nodo.Nodes.Add(SubNodoColor)
            Nodo.Nodes.Add(SubNodoVertices)
            Nodo.ImageIndex = 0

            TreeView1.Nodes.Add(Nodo)
        Next
        TreeView1 = TreeView1
        TreeView1.Refresh()
    End Sub

    Public Sub RellenarFocos()
        Dim Focos() As Foco = Form1.Motor.ILUM_Focos
        Dim Nodo As TreeNode

        Dim Brillo As TreeNode
        Dim Color As TreeNode
        Dim Coordenadas As TreeNode

        TreeView3.Nodes.Clear()

        For i As Long = 0 To Focos.GetUpperBound(0)
            Nodo = New TreeNode
            Brillo = New TreeNode
            Color = New TreeNode
            Coordenadas = New TreeNode

            Nodo.Name = "Focos(" & i & ")"
            Nodo.Text = "Foco(" & i & ")"

            Coordenadas.Name = "Focos(" & i & ")Coordenadas"
            Coordenadas.Text = "Posición (Punto3D)"
            Coordenadas.Nodes.Add("X: " & Focos(i).Coordenadas.X)
            Coordenadas.Nodes.Add("Y: " & Focos(i).Coordenadas.Y)
            Coordenadas.Nodes.Add("Z: " & Focos(i).Coordenadas.Z)

            Color.Name = "Focos(" & i & ")Color"
            Color.Text = "Color de la luz (RGB)"
            Color.Nodes.Add("R: " & Focos(i).Color.R)
            Color.Nodes.Add("G: " & Focos(i).Color.G)
            Color.Nodes.Add("B: " & Focos(i).Color.B)

            Brillo.Name = "Focos(" & i & ")Brillo"
            Brillo.Text = "Porcentaje de intensidad (Brillo): " & FormatNumber(Focos(i).Brillo * 100, 2) & "%"


            Nodo.Nodes.Add(Coordenadas)
            Nodo.Nodes.Add(Color)
            Nodo.Nodes.Add(Brillo)

            TreeView3.Nodes.Add(Nodo)
        Next
        TreeView3.Refresh()
    End Sub

    Public Sub EventoRepintado()
        If CheckBox1.Checked Then
            Call RellenarPoliedros(Form1.Motor.POLI_Poliedros)
            Call RellenarFocos()
        End If
    End Sub

    Public Sub RellenarPoliedros(ByVal Poliedros() As Poliedro)
        Dim i As Long = 0

        Dim Nodo As TreeNode
        Dim SubNodoNormales As TreeNode
        Dim SubNodoCentro As TreeNode
        Dim SubNodoColorIluminacionVertices As TreeNode
        Dim SubNodoVertices As TreeNode
        Dim SubNodoCaras As TreeNode

        Dim NodoCara As TreeNode

        Try
            TreeView2.Nodes.Clear()
        Catch ex As Exception
            Exit Sub
        End Try


        For Each Poliedro As Poliedro In Poliedros
            Nodo = New TreeNode
            SubNodoNormales = New TreeNode
            SubNodoCentro = New TreeNode
            SubNodoVertices = New TreeNode
            SubNodoCaras = New TreeNode
            SubNodoColorIluminacionVertices = New TreeNode

            Nodo.Name = "Poliedros(" & Array.IndexOf(Poliedros, Poliedro) & ")"
            Nodo.Text = "Poliedro(" & Array.IndexOf(Poliedros, Poliedro) & ")"

            SubNodoCentro.Text = "Centroide (Punto3D)"
            SubNodoCentro.Name = "Poliedro(" & Array.IndexOf(Poliedros, Poliedro) & ")Baricentro"
            SubNodoCentro.Nodes.Add("X: " & Poliedro.Centro.X)
            SubNodoCentro.Nodes.Add("Y: " & Poliedro.Centro.Y)
            SubNodoCentro.Nodes.Add("Z: " & Poliedro.Centro.Z)
            SubNodoCentro.ImageIndex = 5

            SubNodoVertices.Text = "Vértices del poliedro (Punto3D)"
            SubNodoVertices.Name = "Poliedro(" & Array.IndexOf(Poliedros, Poliedro) & ")Vertices"
            For Each Vertice As Vertice In Poliedro.Vertices
                SubNodoVertices.Nodes.Add("Vertice(" & i & "): " & Vertice.Coordenadas.ToString)
                i += 1
            Next
            SubNodoVertices.ImageIndex = 6

            i = 0
            SubNodoColorIluminacionVertices.Text = "Colores asignados tras iluminación de los vértices (RGB)"
            SubNodoColorIluminacionVertices.Name = "Poliedro(" & Array.IndexOf(Poliedros, Poliedro) & ")ColorIluminacionVertices"
            For Each Vertice As Vertice In Poliedro.Vertices
                SubNodoColorIluminacionVertices.Nodes.Add("Color de Vértice(" & i & "): RGB(" & Vertice.ColorIluminacion.R & "," & Vertice.ColorIluminacion.G & "," & Vertice.ColorIluminacion.B & ")")
                i += 1
            Next
            SubNodoColorIluminacionVertices.ImageIndex = 9

            i = 0
            SubNodoNormales.Text = "Normales interpoladas de los vértices (Vector3D)"
            SubNodoNormales.Name = "Poliedro(" & Array.IndexOf(Poliedros, Poliedro) & ")Normales"
            For Each Vertice As Vertice In Poliedro.Vertices
                SubNodoNormales.Nodes.Add("Normal de Vértice(" & i & "): " & Vertice.Normal.ToString)
                i += 1
            Next
            SubNodoNormales.ImageIndex = 8

            SubNodoCaras.Text = "Caras del poliedro (Cara)"
            SubNodoCaras.Name = "Poliedro(" & Array.IndexOf(Poliedros, Poliedro) & ")Caras"
            For Each Cara As Cara In Poliedro.Caras
                NodoCara = New TreeNode
                i = 0
                NodoCara.Text = "Cara(" & Array.IndexOf(Poliedro.Caras, Cara) & ")"
                NodoCara.Name = "Poliedro(" & Array.IndexOf(Poliedros, Poliedro) & ")Cara(" & Array.IndexOf(Poliedro.Caras, Cara) & ")"
                For Each Vertice As Long In Cara.Vertices
                    NodoCara.Nodes.Add("Vértice(" & i & "): " & Vertice)
                    i += 1
                Next
                NodoCara.Nodes.Add("Color: RGB(" & Cara.Color.R & "," & Cara.Color.G & "," & Cara.Color.B & ")")
                SubNodoCaras.Nodes.Add(NodoCara)
            Next
            SubNodoCaras.ImageIndex = 7

            For Each N As TreeNode In SubNodoCentro.Nodes
                N.ImageIndex = SubNodoCentro.ImageIndex
            Next
            For Each N As TreeNode In SubNodoVertices.Nodes
                N.ImageIndex = SubNodoVertices.ImageIndex
            Next
            For Each N As TreeNode In SubNodoCaras.Nodes
                N.ImageIndex = SubNodoCaras.ImageIndex
            Next
            For Each N As TreeNode In SubNodoNormales.Nodes
                N.ImageIndex = SubNodoNormales.ImageIndex
            Next
            For Each N As TreeNode In SubNodoColorIluminacionVertices.Nodes
                N.ImageIndex = SubNodoColorIluminacionVertices.ImageIndex
            Next

            Nodo.Nodes.Add(SubNodoCentro)
            Nodo.Nodes.Add(SubNodoVertices)
            Nodo.Nodes.Add(SubNodoCaras)
            Nodo.Nodes.Add(SubNodoNormales)
            Nodo.Nodes.Add(SubNodoColorIluminacionVertices)

            Nodo.ImageIndex = 4

            TreeView2.Nodes.Add(Nodo)
        Next
        TreeView2.Refresh()
    End Sub

    Private Sub Form3_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

    End Sub

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Form1.destruirhandler(Me)
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim nodocara As TreeNode
        Dim Indice As Long

        nodocara = e.Node
        If e.Node.Level > 0 Then
            Do While nodocara.Level > 0
                nodocara = nodocara.Parent
            Loop
            Indice = TreeView1.Nodes.IndexOf(nodocara)
        Else
            Indice = TreeView1.Nodes.IndexOf(nodocara)
        End If
        Form1.Motor.DEPU_CaraSeñalada = Indice
    End Sub

    Private Sub TreeView1_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            TreeView1.SelectedNode = e.Node
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        RellenarPoliedros(Form1.Motor.POLI_Poliedros)
        RellenarBuffer(Form1.Motor.DEPU_CopiaBuffer())
    End Sub

    Private Sub IrACaraCorrespondienteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IrACaraCorrespondienteToolStripMenuItem.Click
        If TreeView2.Nodes.Count = 0 Then Exit Sub

        Dim NodoPoliedro, NodoCara As TreeNode
        Dim Indices As Point = Form1.Motor.DEPU_CopiaBuffer.Item(Form1.Motor.DEPU_CaraSeñalada).Indices

        NodoPoliedro = TreeView2.Nodes.Item(Indices.X)
        NodoCara = NodoPoliedro.Nodes.Item(2).Nodes.Item(Indices.Y)

        TabControl1.SelectedIndex = 2

        TreeView2.CollapseAll()
        NodoPoliedro.Expand()
        NodoPoliedro.Nodes.Item(2).Expand()
        NodoCara.Expand()
        TreeView2.SelectedNode = NodoCara
    End Sub

    Private Sub EliminarCarabufferSelecionadaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EliminarCarabufferSelecionadaToolStripMenuItem.Click
        Form1.Motor.BUFF_EliminarCaraBuffer(Form1.Motor.DEPU_CopiaBuffer.Item(Form1.Motor.DEPU_CaraSeñalada))
        RellenarBuffer(Form1.Motor.DEPU_CopiaBuffer)
    End Sub
End Class