using QRCoder;

namespace TicketPurchaseAPI.Services
{
    public class QRGeneratorService : IQRGeneratorService
    {
        public byte[] GenerateImage(string data)
        {
            
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

            byte[] qrCodeImage = qrCode.GetGraphic(20);
            File.WriteAllBytes("./qrcodeImage.png", qrCodeImage);
            return qrCodeImage;
        }
    }
}
