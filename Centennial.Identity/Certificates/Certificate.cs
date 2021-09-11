using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Centennial.Identity.Certificates
{
    public class Certificate
    {
        public static X509Certificate2 Get()
        {
            var assembly = typeof(Certificate).GetTypeInfo().Assembly;
            var names = assembly.GetManifestResourceNames();

            using (var stream = assembly.GetManifestResourceStream($"{names[0]}"))
            {
                return new X509Certificate2(ReadStream(stream), "itriangle@2021");
            }
        }

        private static byte[] ReadStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
