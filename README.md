# Plugin.Toolkit.Security

### Plugin.Toolkit.Security Documentation

#### Introduction
The `Plugin.Toolkit.Security` library provides various functionalities for security operations, including working with JSON Web Tokens (JWT), generating MD5 hashes, and performing SHA-256 encryption and decryption.

#### Functions

**JWT (JSON Web Token)**
   - **Constructor:** Initializes a new instance of the `SecurityToolkitJWT` class with the specified algorithm, secret, expiration time, issuer, and audience.
     ```csharp
     SecurityToolkitJWT(SecurityAlgorithm algorithm, string secret, int expires = 5, string issuer = "", string audience = "")
     ```
     **Parameters:**
     - `algorithm`: The security algorithm to use (e.g., HmacSha512).
     - `secret`: The secret key used for signing the JWT.
     - `expires`: The expiration time of the token in minutes (default is 5 minutes).
     - `issuer`: The issuer of the token (default is an empty string).
     - `audience`: The audience of the token (default is an empty string).

   - **Create:** Generates a JWT with specified claims.
     ```csharp
     string Create(IEnumerable<Claim> claims)
     ```
     **Parameters:**
     - `claims`: An enumerable collection of claims to include in the JWT.
     **Returns:** A string representing the generated JWT token.

   - **Verify:** Verifies the validity of a given JWT.
     ```csharp
     bool Verify(string token)
     ```
     **Parameters:**
     - `token`: The JWT to verify.
     **Returns:** `true` if the token is valid; otherwise, `false`.

   - **Claims:** Extracts the claims from a given JWT.
     ```csharp
     ClaimsPrincipal Claims(string token)
     ```
     **Parameters:**
     - `token`: The JWT from which to extract claims.
     **Returns:** A `ClaimsPrincipal` object containing the claims.

**MD5**
   - **Hash:** Generates an MD5 hash from a given input.
     ```csharp
     string Hash(string value = "")
     ```
     **Parameters:**
     - `value`: The input string to hash. If no value is provided, an empty string will be hashed.
     **Returns:** The MD5 hash of the input string.

**SHA256**
   - **Encryption:** Encrypts a given input using SHA-256.
     ```csharp
     string Encryption(string value)
     ```
     **Parameters:**
     - `value`: The input string to encrypt.
     **Returns:** The SHA-256 encrypted string.

   - **Decryption:** Decrypts a given SHA-256 encrypted input.
     ```csharp
     string Decryption(string value)
     ```
     **Parameters:**
     - `value`: The SHA-256 encrypted string to decrypt.
     **Returns:** The decrypted string.

#### Usage Examples

**JWT**
```csharp
string key = "secret-key";
var jwt = new SecurityToolkitJWT(SecurityAlgorithm.HmacSha512, key);

// Verify
bool verify = jwt.Verify("jwt-token");

// Create
Claim[] claims = new Claim[]
{
    new Claim(JwtRegisteredClaimNames.Sub, UserId)
};
string create = jwt.Create(claims);

// Claims
ClaimsPrincipal claims = jwt.Claims("jwt-token");
```

**MD5**
```csharp
var md5 = new SecurityToolkitMD5();
// MD5 hash
string hash = md5.Hash();
```

**SHA256**
```csharp
string key = "secret-key";
var sha = new SecurityToolkitSHA256(key);
string strToEncrypt = "eating some cake!";

// Encryption
string encryptedStr = sha.Encryption(strToEncrypt);

// Decryption
string decryptedStr = sha.Decryption(encryptedStr);
```