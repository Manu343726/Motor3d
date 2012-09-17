Public Class Form2
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ColorDialog1.Color = Color.White
        ColorDialog1.ShowDialog()
        Form1.Motor.ILUM_EstablerColorFoco(0, ColorDialog1.Color)
    End Sub

    Private Sub HScrollBar3_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar3.Scroll
        Label7.Text = HScrollBar3.Value & " %"
        Form1.Motor.ILUM_EstablecerAportacionEspecular(HScrollBar3.Value / 100)
    End Sub

    Private Sub HScrollBar2_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar2.Scroll
        Label6.Text = HScrollBar2.Value & " %"
        Form1.Motor.ILUM_EstablecerAportacionDifusa(HScrollBar2.Value / 100)
    End Sub

    Private Sub HScrollBar1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar1.Scroll
        Label1.Text = HScrollBar1.Value & " %"
        Form1.Motor.ILUM_EstablecerAportacionAmbiente(HScrollBar1.Value / 100)
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        Form1.Motor.ILUM_EstablecerExponenteEspecular(NumericUpDown1.Value)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Value1, Value2, Value3 As Double

        Try
            Value1 = CDbl(TextBox1.Text)
        Catch ex As Exception
            TextBox1.Text = "0,0000001"
            Value1 = 0.0000001
        End Try
        Try
            Value2 = CDbl(TextBox2.Text)
        Catch ex As Exception
            TextBox2.Text = "0,0000001"
            Value2 = 0.0000001
        End Try
        Try
            Value3 = CDbl(TextBox3.Text)
        Catch ex As Exception
            TextBox3.Text = "0,000000001"
            Value3 = 0.000000001
        End Try

        Form1.Motor.ILUM_EstablecerConstantesDistancia(Value1, Value2, Value3)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class