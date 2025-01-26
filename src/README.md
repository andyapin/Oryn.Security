# Plugin.Toolkit.Security

![icon](https://handityo.my.id/icon_nuget.png)

[![NuGet](https://img.shields.io/nuget/v/Plugin.Toolkit.Security)](https://www.nuget.org/packages/Plugin.Toolkit.Security) 
[![.NET](https://img.shields.io/badge/.NET%208.0-512BD4?style=flat&logo=dotnet&label=.NET%20Core)](https://dotnet.microsoft.com/en-us/apps/maui)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)


**Secure Your Data, Elevate Your Trust: Plugin.Toolkit.Security** 🚀

The `Plugin.Toolkit.Security` library provides essential security functionalities for C# applications, including working with JSON Web Tokens (JWT), generating MD5 hashes, and performing SHA-256 encryption and decryption.

## 🚀 Features

- **JWT Operations:** Create, verify, and extract claims from JWT tokens.
- **MD5 Hashing:** Generate MD5 hashes for strings.
- **SHA-256 Encryption/Decryption:** Encrypt and decrypt strings using SHA-256.

## 📦 Installation

To install the `Plugin.Toolkit.Security` library, use the NuGet package manager:

```sh
dotnet add package Plugin.Toolkit.Security
```

## 💡 Usage

## **JWT**

### **Parameters**
- `secret`: The secret key used for signing the JWT.
- `algorithm`: The security algorithm to use (e.g., HmacSha256).
- `issuer`: The issuer of the token (default is an empty string).
- `audience`: The audience of the token (default is an empty string).

### **Create**
Generates a JWT with specified claims.
```csharp
string secret = "secret-key";
var security = new SecurityToolkit(secret, SecurityAlgorithm.HmacSha512);
Claim[] claims = new Claim[]
{
    new Claim(JwtRegisteredClaimNames.Sub, UserId)
};
string jwtString = security.JWT.Create(claims);
```

### **Verify**
Verifies the validity of a given JWT.
```csharp
string secret = "secret-key";
var security = new SecurityToolkit(secret, SecurityAlgorithm.HmacSha512);
bool verify = security.JWT.Verify(jwtString);
if (verify)
{
    Console.WriteLine("Token is valid.");
}
else
{
    Console.WriteLine("Token is invalid.");
}
```

### **Claims**
Extracts the claims from a given JWT.
```csharp
string secret = "secret-key";
var security = new SecurityToolkit(secret, SecurityAlgorithm.HmacSha512);
ClaimsPrincipal claims = security.JWT.Claims(jwtString);
if (claims != null)
{
    Console.WriteLine("Token is valid.");
    var subClaim = claims.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
    if (subClaim != null)
    {
        Console.WriteLine($"Sub: {subClaim}");
    }
}
else
{
    Console.WriteLine("Token is invalid.");
}
```

## **MD5**

### **Hash**
Generates an MD5 hash from a given input.
```csharp
var security = new SecurityToolkit("");
string hash = security.MD5.Hash();
```

## **SHA256**

### **Encryption**
Encrypts a given input using SHA-256.
```csharp
string secret = "secret-key";
var security = new SecurityToolkit(secret);
string strToEncrypt = "eating some cake!";
string encryptedStr = security.SHA256.Encryption(strToEncrypt);
```

### **Decryption**
Decrypts a given SHA-256 encrypted input.
```csharp
string secret = "secret-key";
var security = new SecurityToolkit(secret);
string encryptedStr = "xxxxxxxx";
string decryptedStr = security.SHA256.Decryption(encryptedStr);
```

## Contributing
Contributions are welcome! Please open an issue or submit a pull request.

## Contact
If you have any questions or suggestions, please feel free to contact me at andyapin@gmail.com