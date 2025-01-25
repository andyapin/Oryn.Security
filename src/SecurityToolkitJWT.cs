using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Plugin.Toolkit.Security
{
    public enum SecurityAlgorithm
    {
        HmacSha256,
        HmacSha384,
        HmacSha512,
        RsaSha256,
        RsaSha384,
        RsaSha512,
        EcdsaSha256,
        EcdsaSha384,
        EcdsaSha512
    }

    public class SecurityToolkitJWT
    {
        private SecurityAlgorithm _algorithm { get; set; }
        private int _expires { get; set; } = 5;
        private string _issuer { get; set; } = "";
        private bool _is_issuer { get; set; } = false;
        private string _audience { get; set; } = "";
        private bool _is_audience { get; set; } = false;
        private byte[] _secret { get; set; }

        /// <summary>
        /// Initializes a new instance of the JwtHelper class.
        /// <para><c>algorithm:</c> The security algorithm to use for signing the JWT.</para>
        /// <para><c>secret:</c> The secret key to use for signing the JWT.</para>
        /// <para><c>expires:</c> The number of minutes before the JWT expires. Defaults to 5.</para>
        /// <para><c>issuer:</c> The issuer of the JWT. Defaults to an empty string.</para>
        /// <para><c>audience:</c> The audience of the JWT. Defaults to an empty string.</para>
        /// </summary>
        /// <param name="algorithm">The security algorithm to use for signing the JWT.</param>
        /// <param name="secret">The secret key to use for signing the JWT.</param>
        /// <param name="expires">The number of minutes before the JWT expires. Defaults to 5.</param>
        /// <param name="issuer">The issuer of the JWT. Defaults to an empty string.</param>
        /// <param name="audience">The audience of the JWT. Defaults to an empty string.</param>
        public SecurityToolkitJWT(SecurityAlgorithm algorithm, string secret, int expires = 5, string issuer = "", string audience = "")
        {
            _algorithm = algorithm;
            _secret = Encoding.UTF8.GetBytes(secret);
            _expires = expires;
            _issuer = issuer;
            _audience = audience;
            if (_issuer != "")
            {
                _is_issuer = true;
            }
            if (_audience != "")
            {
                _is_audience = true;
            }
        }
        private string Algorithms(SecurityAlgorithm _algorithm)
        {
            switch (_algorithm)
            {
                case SecurityAlgorithm.HmacSha256:
                    return SecurityAlgorithms.HmacSha256;
                case SecurityAlgorithm.HmacSha384:
                    return SecurityAlgorithms.HmacSha384;
                case SecurityAlgorithm.HmacSha512:
                    return SecurityAlgorithms.HmacSha512;
                case SecurityAlgorithm.RsaSha256:
                    return SecurityAlgorithms.RsaSha256;
                case SecurityAlgorithm.RsaSha384:
                    return SecurityAlgorithms.RsaSha384;
                case SecurityAlgorithm.RsaSha512:
                    return SecurityAlgorithms.RsaSha512;
                case SecurityAlgorithm.EcdsaSha256:
                    return SecurityAlgorithms.EcdsaSha256;
                case SecurityAlgorithm.EcdsaSha384:
                    return SecurityAlgorithms.EcdsaSha384;
                case SecurityAlgorithm.EcdsaSha512:
                    return SecurityAlgorithms.EcdsaSha512;
            }
            return SecurityAlgorithms.HmacSha256;
        }
        private ClaimsPrincipal DecodeJwt(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_secret),
                ValidateLifetime = true,
                ValidIssuer = _issuer,
                ValidateIssuer = _is_issuer,
                ValidAudience = _audience,
                ValidateAudience = _is_audience,
                ClockSkew = TimeSpan.Zero
            };
            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return principal;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex);
#endif
                return null;
            }
        }

        /// <summary>
        /// Creates a JSON Web Token (JWT) based on the provided claims.
        /// </summary>
        /// <param name="claims">The claims to be included in the JWT.</param>
        /// <returns>A string representation of the JWT.</returns>
        public string Create(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(_secret);
            var signingCredentials = new SigningCredentials(securityKey, Algorithms(_algorithm));
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expires),
                signingCredentials: signingCredentials
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
#if DEBUG
            Console.WriteLine(tokenString);
#endif
            return tokenString;
        }

        /// <summary>
        /// Verifies a JSON Web Token (JWT) and checks its validity.
        /// </summary>
        /// <param name="token">The JWT token to be verified.</param>
        /// <returns>True if the token is valid, false otherwise.</returns>
        public bool Verify(string token)
        {
            var validationParameters = DecodeJwt(token);
            if (validationParameters != null)
            {
#if DEBUG
                Console.WriteLine("Token valid");
#endif
                return true;
            }
            else
            {
#if DEBUG
                Console.WriteLine("Token not valid");
#endif
                return false;
            }
        }

        /// <summary>
        /// Retrieves the ClaimsPrincipal from a JSON Web Token (JWT).
        /// </summary>
        /// <param name="token">The JSON Web Token to retrieve the ClaimsPrincipal from.</param>
        /// <returns>The ClaimsPrincipal if the token is valid, otherwise null.</returns>
        public ClaimsPrincipal Claims(string token)
        {
            var validationParameters = DecodeJwt(token);
            if (validationParameters != null)
            {
                return validationParameters;
            }
            else
            {
                return null;
            }
        }
    }
}
