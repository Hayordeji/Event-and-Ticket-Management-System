using Newtonsoft.Json;
using QRCoder;
using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Services
{
    public class QRGeneratorService : IQRGeneratorService
    {
        public async Task<byte[]> GenerateImage(Ticket ticketData)
        {
            string serializedTicketData = JsonConvert.SerializeObject(ticketData);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(serializedTicketData, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

            byte[] qrCodeImage = qrCode.GetGraphic(20);
            File.WriteAllBytes("./qrcodeImage.png", qrCodeImage);
            return qrCodeImage;
        }
    }
}
