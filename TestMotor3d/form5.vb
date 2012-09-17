Public Class Form5
    Public Sub RecargarArboles(ByVal Buffer As List(Of CaraBuffer), ByVal Poliedros() As Poliedro)
        Dim Nodo As New TreeNode
        Dim N1 As TreeNode

        Tree1.ImageList = ImageList1

        For Each CaraBuffer As CaraBuffer In Buffer
            Nodo.Text = "CaraBuffer(" & Buffer.IndexOf(CaraBuffer) & ")"
            Nodo.Name = "CaraBuffer(" & Buffer.IndexOf(CaraBuffer) & ")"
            Nodo.ImageIndex = 0

            N1 = New TreeNode

        Next
    End Sub

    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class