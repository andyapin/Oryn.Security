namespace Plugin.Toolkit.Security
{

    public class SecurityToolkit
    {
        public JWTService JWT { get; set; }
        public MD5Service MD5 { get; set; }
        public SHA256Service SHA256 { get; set; }

        /// <summary>
        /// Initializes a new instance of the SecurityToolkit class with the specified parameters.
        /// <para>===<c>JWT</c> | <c>SHA256</c> ===</para>
        /// <code>
        /// Options: 
        /// secret = The secret key to use for encryption and decryption.
        /// </code>
        /// <para>=== <c>JWT</c> ===</para>
        /// <code>
        /// Options: 
        /// algorithm = The security algorithm to use for signing the JWT. Defaults to SecurityAlgorithm.HmacSha256.
        /// issuer = The issuer of the JWT. Defaults to an empty string.
        /// audience = The audience of the JWT. Defaults to an empty string.
        /// </code>
        /// </summary>
        public SecurityToolkit(string secret)
        {
            JWT = new JWTService(secret);
            MD5 = new MD5Service();
            SHA256 = new SHA256Service(secret);
        }

        /// <summary>
        /// Initializes a new instance of the SecurityToolkit class with the specified parameters.
        /// <code>
        /// Options: 
        /// algorithm = The security algorithm to use for signing the JWT. Defaults to SecurityAlgorithm.HmacSha256.
        /// issuer = The issuer of the JWT. Defaults to an empty string.
        /// audience = The audience of the JWT. Defaults to an empty string.
        /// </code>
        /// </summary>
        /// <param name="secret">The secret key to use for encryption and decryption.</param>
        /// <param name="algorithm">The security algorithm to use for signing the JWT. Defaults to SecurityAlgorithm.HmacSha256.</param>
        /// <param name="issuer">The issuer of the JWT. Defaults to an empty string.</param>
        /// <param name="audience">The audience of the JWT. Defaults to an empty string.</param>
        public SecurityToolkit(string secret, SecurityAlgorithm algorithm = SecurityAlgorithm.HmacSha256, string issuer = "", string audience = "")
        {
            JWT = new JWTService(secret, algorithm, issuer, audience);
            MD5 = new MD5Service();
            SHA256 = new SHA256Service(secret);
        }
    }
}
