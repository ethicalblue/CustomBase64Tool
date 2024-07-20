Imports System.Text

Namespace CustomBase64_VB
    Friend Class CustomBase64
        Friend Property CustomAlphabet As String

        Friend Sub New()
            CustomAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"
        End Sub

        Friend Function EncodeText(text As String) As String
            Dim bytes = EncodeBytes(Encoding.UTF8.GetBytes(text).ToList())

            Return Encoding.UTF8.GetString(bytes.ToArray())
        End Function

        Friend Function DecodeText(text As String) As String
            Dim bytes = DecodeBytes(Encoding.UTF8.GetBytes(text).ToList())

            Return Encoding.UTF8.GetString(bytes.ToArray())
        End Function

        Friend Function EncodeBytes(bytes As List(Of Byte)) As List(Of Byte)
            Dim encoded As New List(Of Byte)

            Dim var1 = 0
            Dim var2 = -6

            For Each b In bytes
                var1 = (var1 << 8) + b
                var2 += 8
                While var2 >= 0
                    encoded.Add(Convert.ToByte(CustomAlphabet(var1 >> var2 And &H3F)))
                    var2 -= 6
                End While
            Next

            Dim var3 = var1 << 8 >> var2 + 8 And &H3F

            If var2 > -6 Then encoded.Add(Convert.ToByte(CustomAlphabet(var3)))

            While encoded.Count Mod 4 = 0
                encoded.Add(Convert.ToByte("="c))
            End While

            Return encoded
        End Function

        Friend Function DecodeBytes(bytes As List(Of Byte)) As List(Of Byte)
            Dim decoded As New List(Of Byte)

            Dim T As New List(Of Integer)

            For i = 0 To 255
                T.Add(-1)
            Next

            For i = 0 To 63
                Dim myChar As Char
                myChar = CustomAlphabet.Chars(i)
                T(Convert.ToByte(myChar)) = i
            Next

            Dim var1 = 0
            Dim var2 = 0

            For Each b In bytes
                If T(b) = -1 Then Exit For
                var1 = (var1 << 6) + T(b)
                var2 += 6
                If var2 >= 0 Then
                    decoded.Add(var1 >> var2 And &HFF)
                    var2 -= 8
                End If
            Next

            Return decoded.GetRange(1, decoded.Count - 1)

        End Function
    End Class
End Namespace
