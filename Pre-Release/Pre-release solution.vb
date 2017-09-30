Module Module1
    Dim boatHire(10) As Single, money(10) As Integer, boatReturn(10) As DateTime
    Dim cont As Boolean, ans As Char, boatNumber, boatsAvailable, totalHrs, totalMoney, maxHire, unusedBoats As Integer, hrs As Single
    Dim current, endhire, earliest As DateTime

    Const start = #10:00:00 AM#, ending = #05:00:00 PM#, noOfBoats = 10

    Public Function ReturnDecimal(ByVal output As Single) As Single
        output = (hrs - Math.Floor(hrs))
        Return output
    End Function
    Sub Main()
        'Initializing variables
        For i = 1 To noOfBoats
            boatHire(i) = 0
            boatReturn(i) = start
            money(i) = 0
        Next
        Do
            'Task 1: Hiring times are input and validated.
            cont = True
            While cont = True
                Console.Clear()
                Do 'Validate no of hours for hire 
                    Console.Write("Please input the number of hours for hire: ")
                    hrs = Console.ReadLine
                    If (ReturnDecimal(hrs) <> 0) And (ReturnDecimal(hrs) <> 0.5) Then
                        Console.WriteLine("Only full/half hours are allowed")
                    End If
                Loop Until (ReturnDecimal(hrs) = 0 Or ReturnDecimal(hrs) = 0.5) And hrs > 0

                Do 'Validate the start and end time of the hire
                    Console.Write("Please input the time you want to start the hire: ")
                    current = Console.ReadLine
                    endhire = current.AddHours(hrs)

                    If current < start Then
                        Console.WriteLine("Boat cannot be hired before 10:00 AM. Please re-enter the details.")
                    End If
                    If endhire > ending Then
                        Console.WriteLine("The boat must be returned at 05:00 AM or earlier. Please re-enter the details.")
                    End If
                Loop Until current >= start And endhire <= ending

                'Task 2: 

                'Finding and displaying available boats.
                boatsAvailable = 0
                For i = 1 To noOfBoats
                    If current >= boatReturn(i) Then
                        boatsAvailable = boatsAvailable + 1
                    End If
                Next
                Console.WriteLine("No of boats avalailable: {0}", boatsAvailable)

                'If none are available Then earliest returning boat is calculated and displayed.
                If boatsAvailable = 0 Then
                    earliest = ending
                    For i = 1 To noOfBoats
                        If boatReturn(i) < earliest Then
                            earliest = boatReturn(i)
                            boatNumber = i
                        End If
                    Next
                    Console.WriteLine("Boat {0} returns the earliest at {1}", boatNumber, earliest)
                Else

                    Do 'Validate boat number
                        Console.Write("Please input the boat number of the boat you'd like to hire: ")
                        boatNumber = Console.ReadLine
                        If boatNumber < 1 Or boatNumber > noOfBoats Then
                            Console.WriteLine("Please choose between 1 and {0}", noOfBoats)
                        End If
                    Loop Until boatNumber >= 1 And boatNumber <= noOfBoats


                    If current >= boatReturn(boatNumber) Then
                        boatReturn(boatNumber) = endhire
                        boatHire(boatNumber) = boatHire(boatNumber) + hrs

                        'Calculate hire cost
                        Select Case ReturnDecimal(hrs)
                            Case 0
                                money(boatNumber) = money(boatNumber) + hrs * 20
                            Case 0.5
                                money(boatNumber) = money(boatNumber) + Int(hrs) * 20 + 12
                        End Select
                        Console.WriteLine("Boat {0} has been hired successfully.", boatNumber)
                    Else
                        Console.WriteLine("Boat {0} is not available.", boatNumber)
                    End If
                End If

                Console.WriteLine("Would you like to hire another boat? (Y/N): ")
                ans = Console.ReadLine
                If UCase(ans) = "N" Then
                    cont = False
                End If
            End While
        Loop Until DateTime.Now > #17:00#

        'Task 3- Calculate the money taken for all boats at the end of the day.
        totalHrs = 0
        totalMoney = 0
        unusedBoats = 0
        maxHire = 0

        For i = 1 To noOfBoats
            If boatReturn(i) = start Then
                unusedBoats = unusedBoats + 1
            End If
            If boatHire(i) > maxHire Then
                maxHire = boatHire(i)
                boatNumber = i
            End If
            totalHrs = totalHrs + boatHire(i)
            totalMoney = totalMoney + boatHire(i)
        Next
        Console.WriteLine("Total money taken: {0}", totalMoney)
        Console.WriteLine("Total hours hired: {0}", totalHrs)
        Console.WriteLine("Most used boat is {0}.", boatNumber)
        Console.WriteLine()
        Console.WriteLine("Have a nice day!")
        Console.ReadKey()
    End Sub
End Module


