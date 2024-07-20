using CustomBase64_CS;

CustomBase64 base64 = new();

//base64.CustomAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+/";

var encodedText = base64.EncodeText("ethical.blue Magazine");

Console.WriteLine(encodedText);
Console.WriteLine(base64.DecodeText(encodedText));