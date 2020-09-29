using Google.Cloud.Kms.V1;
using System.IO;
using Google.Protobuf;
 
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Text;

namespace WebApplication1.DataAccess
{
    public class KeyRepository
    {
        public static string Encrypt(string plaintext)
        {
            KeyManagementServiceClient client = KeyManagementServiceClient.Create();

            CryptoKeyName kn = CryptoKeyName.FromUnparsed(new Google.Api.Gax.UnparsedResourceName("projects/cloudprogrammingyf/locations/global/keyRings/pfckeyring1001/cryptoKeys/pfckeys")); /// TO EDIT

            string cipher = client.Encrypt(kn, ByteString.CopyFromUtf8(plaintext)).Ciphertext.ToBase64();

            return cipher;
        }

        public static string Decrypt(string cipher)
        {
            KeyManagementServiceClient client = KeyManagementServiceClient.Create();

            CryptoKeyName kn = CryptoKeyName.FromUnparsed(new Google.Api.Gax.UnparsedResourceName("projects/cloudprogrammingyf/locations/global/keyRings/pfckeyring1001/cryptoKeys/pfckeys")); /// TO EDIT

            string plaintext = client.Decrypt(kn, ByteString.FromBase64(cipher)).Plaintext.ToStringUtf8();

            return plaintext;
        }


    }
}