Module Module1
    Sub menu(ByRef b As Single)
        Dim a As String
        Console.WriteLine("1.開始遊戲")
        Console.WriteLine("2.查看遊戲規則")
        Console.WriteLine("3.查看首 10 名的紀錄")
        Console.WriteLine("4.查看最近 10 名玩家的紀錄")
        Console.WriteLine("5.離開")
        Do
            Console.Write("Enter you choice 1 - 5:")
            a = Console.ReadLine()
            b = Val(a)
        Loop Until b >= 1 And b <= 5
    End Sub

    Sub wow(ByVal b, ByRef quit)
        If b = 1 Then
            start_the_game()
        ElseIf b = 2 Then
            view_the_rules_of_the_game()
        ElseIf b = 3 Then
            view_top_10_records()
        ElseIf b = 4 Then
            view_most_recent_10_records()
        ElseIf b = 5 Then
            quit = True
        End If
    End Sub

    Sub temp()
        Dim sr As IO.StreamReader = IO.File.OpenText("C:\vb\text.txt")
        Dim line_string, t As String
        Dim i As Integer
        Dim a(100) As Integer
        i = 1
        line_string = sr.ReadLine()

        While line_string <> Nothing
            a(i) = line_string
            line_string = sr.ReadLine()
            i = i + 1
        End While

        i = i - 1

        For y = 1 To i
            For j = 1 To i - y
                If a(j) > a(j + 1) Then
                    t = a(j)
                    a(j) = a(j + 1)
                    a(j + 1) = t
                End If
            Next
        Next

        For y = 1 To i
            Console.WriteLine(a(y))
        Next
    End Sub

    Sub color(ByVal color As Integer)
        Select Case color
            Case 1
                Console.ForegroundColor = ConsoleColor.Red
                Console.Write("R")
                Console.ResetColor()
            Case 2
                Console.ForegroundColor = ConsoleColor.Green
                Console.Write("G")
                Console.ResetColor()
            Case 3
                Console.ForegroundColor = ConsoleColor.Yellow
                Console.Write("Y")
                Console.ResetColor()
            Case 4
                Console.ForegroundColor = ConsoleColor.Magenta
                Console.Write("M")
                Console.ResetColor()

            Case 5
                Console.ForegroundColor = ConsoleColor.White
                Console.Write("W")
                Console.ResetColor()
            Case 6
                Console.ForegroundColor = ConsoleColor.Black
                Console.BackgroundColor = ConsoleColor.White
                Console.Write("B")
                Console.ResetColor()
            Case Else
        End Select
    End Sub

    Sub time()
        Dim t As Date
        Dim hour, minute, second As Integer
        hour = t.Hour
        t = TimeOfDay
        minute = t.Minute
        second = t.Second
    End Sub

    Sub start_the_game()
        Dim T_or_F As Boolean = False
        Dim haveW As Boolean = False
        Dim i, Target(4), judge(4), temporary_Target(4), First, Finish As Integer
        Dim write_file, name, hint, rmm As String
        Dim input As Char
        Dim time As Date
        Dim sfile As IO.StreamWriter = IO.File.AppendText("U:\ict\2021-2022_ict\VB\mastermind\date.txt")

        T_or_F = False
        For j = 1 To 4
            Target(j) = Int((4 * Rnd()) + 1)
            temporary_Target(j) = Target(j)
            color(Target(j))
        Next

        Console.WriteLine()
        time = TimeOfDay
        First = (time.Hour * 60 * 60) + time.Minute * 60 + time.Second

        For i = 1 To 4
            If T_or_F = False Then
                Console.Write("Trial" & i & ":")
                input = Nothing

                For j = 1 To 4
                    temporary_Target(j) = Target(j)
                    hint = Nothing
                    Do
                        input = Console.ReadKey(True).KeyChar
                        input = Char.ToUpper(input)
                        Select Case input
                            Case "R"
                                judge(j) = 1
                            Case "G"
                                judge(j) = 2
                            Case "Y"
                                judge(j) = 3
                            Case "M"
                                judge(j) = 4

                            Case Else
                                input = Nothing
                        End Select
                    Loop Until input <> Nothing

                    color(judge(j))
                Next
                Console.Write("   ")
                haveW = False
                For j = 1 To 4
                    If judge(j) = temporary_Target(j) Then
                        color(6)
                        judge(j) = "9"
                        temporary_Target(j) = "10"
                    End If
                Next

                For j = 1 To 4
                    For u = 1 To 4
                        If judge(j) = temporary_Target(u) Then
                            Console.Write("W")
                            judge(j) = "9"
                            temporary_Target(u) = "10"
                            haveW = True
                        End If

                    Next
                Next

                If haveW = False Then
                    For j = 1 To 4
                        If judge(j) = "9" Then
                            T_or_F = True
                        Else
                            T_or_F = False
                            Exit For
                        End If
                    Next
                End If

                Console.WriteLine()
            Else
                Exit For
            End If
        Next

        If T_or_F = False Then
            Console.WriteLine("the secret code is")
            For y = 1 To 4
                color(Target(y))
            Next
        Else
            Console.WriteLine("Great, you have gussed  in " & i - 1 & " trials.")
            time = TimeOfDay
            Finish = time.Hour * 60 * 60 + time.Minute * 60 + time.Second
            Console.WriteLine("you have used " & Finish - First & " seconds")
            Do
                Console.Write("Enter your name : ")
                name = Console.ReadLine
                If name.Length < 20 Then
                    For j = 1 To 20 - name.Length
                        name &= " "
                    Next
                End If
            Loop Until name.Length = 20
            rmm = i - 1
            If rmm < 9 Then
                rmm = "0" & rmm
            End If
            write_file = name & rmm & Finish - First
            sfile.WriteLine(write_file)
            sfile.Close()
        End If
    End Sub

    Sub view_the_rules_of_the_game()
        Dim sfile As IO.StreamReader = IO.File.OpenText("U:\ict\2021-2022_ict\VB\mastermind\rules.txt")
        Dim line_string As String
        line_string = sfile.ReadLine()
        While line_string <> Nothing
            Console.WriteLine(line_string)
            line_string = sfile.ReadLine()
        End While
        sfile.Close()
    End Sub

    Sub view_top_10_records()
        Dim sfile As IO.StreamReader = IO.File.OpenText("U:\ict\2021-2022_ict\VB\mastermind\date.txt")
        Dim line_string, temp As String
        Dim x, j, z As Integer
        Dim a(100) As String
        line_string = sfile.ReadLine()

        While line_string <> Nothing
            z = line_string.Length
            x = x + 1
            For i = 1 To z
                a(x) = a(x) & line_string(i)
            Next
            line_string = sfile.ReadLine()
        End While

        For i = 1 To x
            For j = 1 To x - i
                If a(j) > a(j + 1) Then
                    temp = a(j)
                    a(j) = a(j + 1)
                    a(j + 1) = temp
                End If
            Next
        Next


        j = 0
        For j = 1 To 5
            Console.WriteLine("第" & j & "名:" & a(j))
        Next
        sfile.Close()
    End Sub

    Sub view_most_recent_10_records()
        Dim sfile As IO.StreamReader = IO.File.OpenText("U:\ict\2021-2022_ict\VB\mastermind\date.txt")
        Dim line_string As String
        line_string = sfile.ReadLine()
        For i = 1 To 10
            Console.WriteLine(line_string)
            line_string = sfile.ReadLine()
        Next
        sfile.Close()
    End Sub

    Sub Main()
        Dim b As Single
        Dim quit As Boolean
        quit = False
        While quit = False
            menu(b)
            wow(b, quit)
        End While
    End Sub
End Module
