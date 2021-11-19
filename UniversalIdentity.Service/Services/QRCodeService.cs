using QRCoder;
using UniversalIdentity.Domain.Interfaces;

namespace UniversalIdentity.Service.Services
{
    public class QRCodeService : IQRCodeService
    {
        public string GenerateQRCode(string text)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new Base64QRCode(qrCodeData);
            var qrCodeImageAsBase64 = qrCode.GetGraphic(40);
            return qrCodeImageAsBase64;
        }
    }
}
