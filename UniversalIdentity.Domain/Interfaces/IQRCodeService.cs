namespace UniversalIdentity.Domain.Interfaces
{
    public interface IQRCodeService
    {
        string GenerateQRCode(string text);
    }
}
