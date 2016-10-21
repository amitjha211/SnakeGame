Public Class SnakePoint : Inherits SnakePointHeader

    Public Sub New(ByVal iMeasureCurrentX As Integer, ByVal iMeasuredCurrentY As Integer)
        Me.MeasuredCurrentX = iMeasureCurrentX
        Me.MeasuredCurrentY = iMeasuredCurrentY
    End Sub
    Public Sub New()

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class


Public Class SnakePoints
    Dim cln1 As New Collection
    Dim sMoveDirection As String = "-T"
    Dim clsSnakeFood As New SnakeFood
    Dim m_Scrores As Integer = 0
    Dim clsPlaySound As New System.Media.SoundPlayer("C:\Documents and Settings\vicky\My Documents\Visual Studio 2005\Projects\SnakeGame\alarm01.wav")
    Public Property Scrores() As Integer
        Get
            Return m_Scrores
        End Get
        Set(ByVal value As Integer)
            m_Scrores = value
        End Set
    End Property

    Public Function Count() As Integer
        Return cln1.Count
    End Function
    Public Function Add(ByVal iLeft As Integer, ByVal iTop As Integer) As Integer
        cln1.Add(New SnakePoint(iLeft, iTop))
    End Function

    Public Function Add() As Integer
        Dim o As New SnakePoint
        cln1.Add(o)
    End Function
    Sub ClearAll()
        cln1.Clear()
    End Sub
    Default Public ReadOnly Property Items(ByVal i As Integer) As SnakePoint
        Get
            Return cln1.Item(i)
        End Get
    End Property



    '=================================================================

    Sub SetMoveUp()
        sMoveDirection = "-T"
    End Sub
    Sub SetMoveDown()
        sMoveDirection = "+T"
    End Sub
    Sub SetMoveLeft()
        sMoveDirection = "-L"
    End Sub
    Sub SetMoveRight()
        sMoveDirection = "+L"
    End Sub

    Sub Move()
        '--------------------------------------------------------------

        Dim tmpMeasuredCurrentX As Integer = Items(1).MeasuredCurrentX
        Dim tmpMeasuredCurrentY As Integer = Items(1).MeasuredCurrentY



        Select Case sMoveDirection
            Case "-T"
                tmpMeasuredCurrentY -= 1
            Case "+T"
                tmpMeasuredCurrentY += 1
            Case "+L"
                tmpMeasuredCurrentX += 1
            Case "-L"
                tmpMeasuredCurrentX -= 1
        End Select


        If (Items(1).MeasuredCurrentX = clsSnakeFood.MeasuredCurrentX) And _
        (Items(1).MeasuredCurrentY = clsSnakeFood.MeasuredCurrentY) Then
            Me.Add()
            Scrores += 10
            clsSnakeFood.GotoNextPosition()

            'clsPlaySound.Play()
        End If
        For i As Integer = 1 To Count()
            Dim tmpMeasuredCurrentX1 As Integer = Items(i).MeasuredCurrentX
            Dim tmpMeasuredCurrentY1 As Integer = Items(i).MeasuredCurrentY
            Items(i).MeasuredCurrentX = tmpMeasuredCurrentX
            Items(i).MeasuredCurrentY = tmpMeasuredCurrentY
            tmpMeasuredCurrentX = tmpMeasuredCurrentX1
            tmpMeasuredCurrentY = tmpMeasuredCurrentY1

        Next

    End Sub

    Public Function ChkOut() As Boolean
        For i As Integer = 2 To Count()
            If (Items(1).MeasuredCurrentX = Items(i).MeasuredCurrentX) And _
            (Items(1).MeasuredCurrentY = Items(i).MeasuredCurrentY) Then
                Return True
            End If
        Next

    End Function
    Sub myPrintSnake(ByVal gf As Drawing.Graphics, ByVal img As Image)
        'gf.FillRectangle(Brushes.Black, 0, 0, iPointSize * g_ScreenMeasuredWidth, iPointSize * g_ScreenMeasuredHeight)

        Dim img1 As Image = My.Resources.Resources.Butterfly
        'Image.FromFile("D:\Tmp\Softwareofamit\SnakeGame\Butterfly.JPG")
        gf.DrawImage(img1, 0, 0, iPointSize * g_ScreenMeasuredWidth, iPointSize * g_ScreenMeasuredHeight)

        'gf.Clear(Color.Black)
        'gf.Clear(Color.Transparent)

        For i As Integer = 1 To Count()
            Dim tmpLeft As Integer = Items(i).Left
            Dim tmpTop As Integer = Items(i).Top
            'Gf.DrawRectangle(New Drawing.Pen(g_ScreenForeColor, 3), tmpLeft, tmpTop, iPointSize, iPointSize)
            Dim rc1 As New Drawing.RectangleF(tmpLeft, tmpTop, iPointSize, iPointSize)

            gf.FillEllipse(New Drawing.SolidBrush(Color.Yellow), rc1)
            '=========================================================
            rc1.Width = rc1.Width / 2
            rc1.Height = rc1.Height / 2
            gf.FillEllipse(New Drawing.SolidBrush(Color.Green), rc1)
        Next

        clsSnakeFood.PrintSnakeFood(gf)
    End Sub

    Sub InitSnake()
        ClearAll()
        For i As Integer = 1 To 5
            Add()
        Next

        For i As Integer = 1 To Count()
            Items(i).MeasuredCurrentX = g_ScreenMeasuredWidth / 2
            Items(i).MeasuredCurrentY = (g_ScreenMeasuredHeight - 5) + i
        Next
    End Sub

End Class

Public Class SnakePointHeader
    Dim m_MesuredCurrentX, m_MesuredCurrentY As Integer
    Property MeasuredCurrentX() As Integer
        Get
            Return m_MesuredCurrentX
        End Get
        Set(ByVal value As Integer)
            If value < 1 Then
                m_MesuredCurrentX = g_ScreenMeasuredWidth
                Exit Property
            End If
            If value > g_ScreenMeasuredWidth Then
                m_MesuredCurrentX = 1
                Exit Property
            End If
            m_MesuredCurrentX = value
        End Set
    End Property
    Property MeasuredCurrentY() As Integer
        Get
            Return m_MesuredCurrentY
        End Get
        Set(ByVal value As Integer)
            If value < 1 Then
                m_MesuredCurrentY = g_ScreenMeasuredHeight
                Exit Property
            End If
            If value > g_ScreenMeasuredHeight Then
                m_MesuredCurrentY = 1
                Exit Property
            End If

            m_MesuredCurrentY = value
        End Set
    End Property


    Public ReadOnly Property Left() As Integer
        Get
            Return (MeasuredCurrentX - 1) * iSnakeHeight
        End Get
    End Property

    Public ReadOnly Property Top() As Integer
        Get
            Return (MeasuredCurrentY - 1) * iSnakeHeight
        End Get
    End Property

End Class


Public Class SnakeFood : Inherits SnakePointHeader
    Dim cn As New Media.SoundPlayer
    Sub New()
        Me.MeasuredCurrentX = g_ScreenMeasuredWidth / 2
        Me.MeasuredCurrentY = g_ScreenMeasuredHeight / 2
    End Sub
    Sub PrintSnakeFood(ByVal gf As Graphics)
        gf.FillEllipse(New Drawing.SolidBrush(Color.Red), MyBase.Left, MyBase.Top, iSnakeHeight, iSnakeHeight)
    End Sub
    Sub GotoNextPosition()
        MyBase.MeasuredCurrentX += 10
        MyBase.MeasuredCurrentY += 5
    End Sub
End Class