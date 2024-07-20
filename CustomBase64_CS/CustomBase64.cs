using System.Text;

namespace CustomBase64_CS
{
    internal class CustomBase64
    {
        internal string CustomAlphabet { get; set; }

        internal CustomBase64()
        {
            CustomAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        }

        internal string EncodeText(string text)
        {
            var bytes = EncodeBytes(Encoding.UTF8.GetBytes(text).ToList());

            return Encoding.UTF8.GetString(bytes.ToArray());
        }

        internal string DecodeText(string text)
        {
            var bytes = DecodeBytes(Encoding.UTF8.GetBytes(text).ToList());

            return Encoding.UTF8.GetString(bytes.ToArray());
        }

        internal List<byte> EncodeBytes(List<byte> bytes)
        {
            List<byte> encoded = new();

            int var1 = 0; int var2 = -6;

            foreach (var b in bytes)
            {
                var1 = (var1 << 8) + b;
                var2 += 8;
                while (var2 >= 0)
                {
                    encoded.Add(Convert.ToByte(CustomAlphabet[(var1 >> var2) & 0x3F]));
                    var2 -= 6;
                }
            }

            int var3 = ((var1 << 8) >> (var2 + 8)) & 0x3F;

            if (var2 > -6) encoded.Add(Convert.ToByte(CustomAlphabet[var3]));

            while (encoded.Count % 4 == 0) encoded.Add(Convert.ToByte('='));

            return encoded;
        }

        internal List<byte> DecodeBytes(List<byte> bytes)
        {

            List<byte> decoded = new();

            List<int> T = new(256);

            for (int i = 0; i < 256; i++)
                T.Add(-1);

            for (int i = 0; i < 64; i++)
                T[CustomAlphabet[i]] = i;

            int var1 = 0;
            int var2 = 0;

            foreach (var b in bytes)
            {
                if (T[b] == -1) break;
                var1 = (var1 << 6) + T[b];
                var2 += 6;
                if (var2 >= 0)
                {
                    decoded.Add((byte)((var1 >> var2) & 0xFF));
                    var2 -= 8;
                }
            }

            return decoded.GetRange(1, decoded.Count - 1);
        }
    }
}
