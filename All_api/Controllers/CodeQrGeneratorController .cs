using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.IO;
using ZXing;
using ZXing.Mobile;

namespace All_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeQrGeneratorController : ControllerBase
    {
        [HttpGet("GenerateCode")]
        public IActionResult GenerateCode()
        {
       
            string code = GenerateRandomCode();
 
            var barcodeImage = GenerateBarcodeImage(code);

 
            using (var imageStream = new MemoryStream())
            {
                barcodeImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = imageStream.ToArray();
                string base64Image = Convert.ToBase64String(imageBytes);

                return Ok(new
                {
                    Code = code,
                    Barcode = $"data:image/png;base64,{base64Image}"
                });
            }
        }

        private string GenerateRandomCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] codeChars = new char[6];

            for (int i = 0; i < codeChars.Length; i++)
            {
                codeChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(codeChars);
        }

        private Bitmap GenerateBarcodeImage(string code)
        {
     
            var barcode = BarcodeWriter.CreateBarcode(code, BarcodeWriterEncoding.Code128);
            barcode.ResizeTo(300, 100);
            return barcode.ToBitmap();
        }
    }
}
