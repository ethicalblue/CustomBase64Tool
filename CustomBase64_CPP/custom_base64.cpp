#pragma once

#include "custom_base64.h"

std::vector<std::byte> custom_base64::encode(const std::vector<std::byte>& bytes) {

    std::vector<std::byte> encoded { 0 };

    unsigned var1 = 0; signed var2 = -6;

    for (auto b : bytes) {
        var1 = (var1 << 8) + static_cast<unsigned>(b);
        var2 += 8;
        while (var2 >= 0) {
            encoded.push_back(static_cast<std::byte>(custom_alphabet[(var1 >> var2) & 0x3F]));
            var2 -= 6;
        }
    }

    unsigned var3 = ((var1 << 8) >> (var2 + 8)) & 0x3F;

    if (var2 > -6) encoded.push_back(static_cast<std::byte>(custom_alphabet[var3]));

    while (encoded.size() % 4 == 0) encoded.push_back(static_cast<std::byte>('='));

    return encoded;
}

std::vector<std::byte> custom_base64::decode(const std::vector<std::byte>& bytes) {

    std::vector<std::byte> decoded{ 0 };

    std::vector<int> T(256, -1);

    for (int i = 0; i < 64; i++)
        T[custom_alphabet[i]] = i;

    unsigned var1 { 0 };
    signed var2 { -8 };

    for (auto b : bytes)
    {
        if (T[static_cast<unsigned>(b)] == -1) break;
        var1 = (var1 << 6) + T[static_cast<unsigned>(b)];
        var2 += 6;
        if (var2 >= 0)
        {
            decoded.push_back(static_cast<std::byte>((var1 >> var2) & 0xFF));
            var2 -= 8;
        }
    }

    return decoded;
}