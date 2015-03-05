Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace ComplexNumbers_VB
    Class ComplexNumber
        ' Members of the class
        Private isPolar As Boolean

        Private real As Double
        Private imaginary As Double

        Private radius As Double
        Private angle As Double

        ' Member calling
        Private Function GetReal() As Double
            Return real
        End Function

        Private Sub SetReal(real As Double)
            Me.real = real
        End Sub

        Private Function GetImaginary() As Double
            Return imaginary
        End Function

        Private Sub SetImaginary(i As Double)
            imaginary = i
        End Sub

        Private Function GetRadius() As Double
            If Me.isPolar Then
                Return radius
            Else
                Return ((Me.real * Me.real) + (Me.imaginary * Me.imaginary))
            End If
        End Function

        Private Sub SetRadius(r As Double)
            radius = r
        End Sub

        Private Function GetAngle() As Double
            If Me.isPolar Then
                Return angle
            Else
                Return (Math.Atan(Me.imaginary / Me.real))
            End If
        End Function

        Private Sub SetAngle(a As Double)
            If Me.isPolar Then
                angle = a
            End If
        End Sub

        ' Functions
        Private Function ToString() As String
            Dim str As String = ""

            If Me.isPolar Then
                ' Polar
                If Me.angle > 0 Then
                    ' Positive angle
                    str = Me.radius + "(cos" + Me.angle + " + isin" + Me.angle + ")"
                Else
                    str = Me.radius + "(cos" + Me.angle + " - isin" + -Me.angle + ")"
                End If
            Else
                ' Rect
                If Me.imaginary > 0 Then
                    ' Positive imaginary
                    str = Me.real + " + i" + Me.imaginary
                Else
                    str = Me.real + " - i" + -Me.imaginary
                End If
            End If
            Return str
        End Function

        Private Function AsPolar() As ComplexNumber
            Dim number As New ComplexNumber()
            If Me.isPolar Then
                Return Me
            Else
                number = New ComplexNumber(True, Me.GetRadius(), Me.GetAngle())
            End If

            Return number
        End Function

        Private Function AsCartesian() As ComplexNumber
            Dim number As New ComplexNumber()
            If Not Me.isPolar Then
                Return Me
            Else
                number = New ComplexNumber(False, Me.GetRadius() * Math.Cos(Me.GetAngle()), Me.GetRadius() * Math.Sin(Me.GetAngle()))
            End If

            Return number
        End Function

        Private Function Logarithm() As ComplexNumber
            Dim number As New ComplexNumber()
            number.real = Math.Log(Me.GetRadius())
            number.imaginary = Me.GetAngle()
            number.isPolar = False
            Return number
        End Function

        Private Function Conjugate() As ComplexNumber
            Dim number As New ComplexNumber()

            If Me.isPolar Then
                ' The number is polar
                number.radius = Me.radius
                number.angle = -Me.angle
                number.isPolar = True
            Else
                number.real = Me.real
                number.imaginary = -Me.imaginary
                number.isPolar = False
            End If

            Return number
        End Function

        Private Function Reciprocal() As ComplexNumber
            Dim number As New ComplexNumber()

            number.real = (Me.real) / (Me.real * Me.real + Me.imaginary * Me.imaginary)
            ' Minus sign shows that there is a conjugate of the complex number.
            number.imaginary = -(Me.imaginary) / (Me.real * Me.real + Me.imaginary * Me.imaginary)

            number.isPolar = False
            Return number
        End Function

        ' Operator overloading
        Shared Operator +(a As ComplexNumber, b As ComplexNumber) As ComplexNumber
            ' Add then up
            If Not a.isPolar = Not b.isPolar Then
                Return New ComplexNumber(a.isPolar, a.real + b.real, a.imaginary + b.imaginary)
            Else
                ' Return exception
                Throw New NotImplementedException()
            End If
        End Operator

        Shared Operator -(a As ComplexNumber) As ComplexNumber
            If Not a.isPolar Then
                Return New ComplexNumber(a.isPolar, -a.radius, a.angle)
            Else
                Return New ComplexNumber(a.isPolar, -a.real, -a.imaginary)
            End If
        End Operator

        Shared Operator -(a As ComplexNumber, b As ComplexNumber) As ComplexNumber
            If Not a.isPolar AndAlso Not b.isPolar Then
                Return New ComplexNumber(a.isPolar, a.real - b.real, a.imaginary - b.imaginary)
            Else
                Throw New NotImplementedException()
            End If
        End Operator

        Shared Operator *(a As ComplexNumber, b As ComplexNumber) As ComplexNumber
            Dim number As New ComplexNumber()
            If a.isPolar AndAlso b.isPolar Then
                number.radius = a.radius * b.radius
                number.angle = a.angle + b.angle
                number.isPolar = True
            ElseIf Not b.isPolar AndAlso Not b.isPolar Then
                ' Both are not polar; they're rectangular
                number.real = (a.real * b.real) - (a.imaginary * b.imaginary)
                number.imaginary = (a.real * b.imaginary) + (a.imaginary * b.real)
                number.isPolar = False
            End If
            Return number
        End Operator

        Shared Operator /(a As ComplexNumber, b As ComplexNumber) As ComplexNumber
            Dim number As New ComplexNumber()

            If a.isPolar AndAlso b.isPolar Then
                ' Both are polar numbers
                number.radius = a.radius / b.radius
                number.angle = a.angle - b.angle
                number.isPolar = True
            ElseIf Not a.isPolar AndAlso Not b.isPolar Then
                ' Both are rect;
                number.real = (a.real * b.real) + (a.imaginary * b.imaginary) / ((b.real * b.real) + (b.imaginary * b.imaginary))

                number.imaginary = (a.real * b.real) - (a.imaginary * b.imaginary) / ((b.real * b.real) + (b.imaginary * b.imaginary))
                number.isPolar = False
            End If
            Return number
        End Operator

        Shared Operator =(a As ComplexNumber, b As ComplexNumber) As Boolean
            If b.isPolar AndAlso a.isPolar Then
                ' If both are polar
                If b.angle = a.angle AndAlso b.radius = a.radius Then
                    Return True
                Else
                    Return False
                End If
            ElseIf b.isPolar AndAlso Not a.isPolar Then
                Return False
            ElseIf Not b.isPolar AndAlso a.isPolar Then
                Return False
            Else
                ' Check whether their values are same
                If b.real = a.real AndAlso b.imaginary = a.imaginary Then
                    Return True
                Else
                    Return False
                End If
            End If
        End Operator

        Shared Operator <>(a As ComplexNumber, b As ComplexNumber) As Boolean
            If b.isPolar AndAlso a.isPolar Then
                ' If both are polar
                If b.angle = a.angle AndAlso b.radius = a.radius Then
                    Return True
                Else
                    Return False
                End If
            ElseIf b.isPolar AndAlso Not a.isPolar Then
                Return False
            ElseIf Not b.isPolar AndAlso a.isPolar Then
                Return False
            Else
                ' Check whether their values are same
                If b.real = a.real AndAlso b.imaginary = a.imaginary Then
                    Return True
                Else
                    Return False
                End If
            End If
        End Operator

        ' Constructors
        Public Sub New()
            ' Default
            isPolar = False
            real = 0
            imaginary = 0
        End Sub

        Public Sub New(isPolar As Boolean, firstVal As Double, secondVal As Double)
            ' Custom 
            Me.isPolar = isPolar
            If Me.isPolar Then
                ' Polar
                radius = firstVal
                angle = secondVal
            Else
                real = firstVal
                imaginary = secondVal
            End If
        End Sub
    End Class
End Namespace
