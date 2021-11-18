using System;

namespace UniversalIdentity.Domain.Settings
{
    /// <summary>
    /// Configurações do JWT
    /// </summary>
    public class JwtSettings
    {
        public const string Jwt = "Jwt";
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public DateTime NewDateExpiry => DateTime.Now.AddDays(1);
    }
}
