Public Class Form1
    Dim blnRun As Boolean = False
    Dim Gf As Drawing.Graphics
    Dim clsSnakePoints As New SnakePoints

    '===========================================
    Dim iDestX, iDestY As Integer
    '===========================================
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If blnRun = False Then Exit Sub
        Gf = picScreen.CreateGraphics

        'Gf.Clear(g_ScreenBackColor)
        clsSnakePoints.Move()
        clsSnakePoints.myPrintSnake(Gf, ImageList1.Images(1))
        lblScores.Text = clsSnakePoints.Scrores

        If clsSnakePoints.ChkOut() = True Then
            Beep()
            blnRun = False
            Dim str1 As String = "Game Is Over" & vbNewLine
            str1 &= "You made" & clsSnakePoints.Scrores & " Scores" & vbNewLine
            str1 &= "Would you like to play new game"

            If MsgBox(str1, MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                MsgBox("This facility will be abailable on Next Version")
                End
            End If
            Exit Sub
        End If

    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Up
                clsSnakePoints.SetMoveUp()
            Case Keys.Down
                clsSnakePoints.SetMoveDown()
            Case Keys.Left
                clsSnakePoints.SetMoveLeft()
            Case Keys.Right
                clsSnakePoints.SetMoveRight()
        End Select



    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        clsSnakePoints.InitSnake()
        'SetSnakePoints()
        blnRun = True
        Me.Focus()

    End Sub

    Private ReadOnly Property myBorderColor() As Drawing.Color
        Get
            Return Color.Blue
        End Get
    End Property

    Private Sub DrawBorder()
        ' Gf = Me.CreateGraphics
        'Gf.DrawRectangle(New Drawing.Pen(myBorderColor, myBorderWidth), 0, 0, Me.Width - myBorderWidth, Me.Height - myBorderWidth)
    End Sub
    Private ReadOnly Property iTotalPointWidth() As Integer
        Get
            Return Math.Round(CDbl(picScreen.Width))
        End Get
    End Property
    Private ReadOnly Property iTotalPointHeight() As Integer
        Get
            Return Math.Round(CDbl(picScreen.Width))
        End Get
    End Property

    Private Sub StopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopToolStripMenuItem.Click
        If StopToolStripMenuItem.Text = "Stop" Then
            blnRun = False
            StopToolStripMenuItem.Text = "Play"
        Else
            blnRun = True
            StopToolStripMenuItem.Text = "Stop"
        End If
    End Sub


End Class
