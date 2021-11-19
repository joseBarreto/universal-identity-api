using System;
using UniversalIdentity.Domain.Interfaces;

namespace UniversalIdentity.Service.Services
{
    public class UniversalIdentityService : IUniversalIdentityService
    {
        public string GenerateUniversalIdentity()
        {
            var universalId = new string[16];
            var random = new Random();

            for (int i = 0; i < 16; i++)
            {
                universalId[i] = random.Next(0, 9).ToString();
            }

            return string.Concat(universalId);
        }
    }
}
