Module Program
    Sub Main()
        Dim base64 As CustomBase64_VB.CustomBase64
        base64 = New CustomBase64_VB.CustomBase64

        'base64.CustomAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+/"

        Dim encodedText = base64.EncodeText("ethical.blue Magazine")

        Console.WriteLine(encodedText)
        Console.WriteLine(base64.DecodeText(encodedText))
    End Sub
End Module
