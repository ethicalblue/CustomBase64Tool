#pragma once

#include <iostream>
#include <vector>

class custom_base64
{
public:
    std::string custom_alphabet{ "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/" };
    std::vector<std::byte> encode(const std::vector<std::byte>& bytes);
    std::vector<std::byte> decode(const std::vector<std::byte>& bytes);
};