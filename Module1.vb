Module Module1
    Public Const myBorderWidth = 10
    Public iSnakeHeight As Integer = 5
    Public iPointSize As Integer = 5


    Public g_ScreenBackColor As Color = Color.Black
    Public g_ScreenForeColor As Color = Color.White

    Public Const g_ScreenMeasuredHeight As Integer = 35
    Public g_ScreenMeasuredWidth As Integer = 30

    Sub Main()
        Dim frm1 As New Form1
        frm1.ShowDialog()
    End Sub
End Module

