#include "custom_base64.h"

int main()
{
    std::string text1 { "ethical.blue Magazine" };

    std::vector<std::byte> text_bytes { 0 };

    for (auto c : text1)
        text_bytes.push_back(static_cast<std::byte>(c));

    custom_base64 base64;

    //base64.custom_alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+/";

    auto encoded = base64.encode(text_bytes);

    std::string encoded_data { 0 };
    encoded_data.assign(reinterpret_cast<std::string::const_pointer>(&encoded[0]),
        encoded.size() / sizeof(std::string::value_type));

    std::cout << encoded_data << "\n";

    auto decoded = base64.decode(encoded);

    std::string plain_data { 0 };
    plain_data.assign(reinterpret_cast<std::string::const_pointer>(&decoded[0]),
        decoded.size() / sizeof(std::string::value_type));

    std::cout << plain_data << "\n";
}