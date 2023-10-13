using System.Security.Cryptography;
using app_authors.Dtos.Comment;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace app_authors.Services
{
    public class HashService
    {
        public HashResultDto Hash(string input)
        {
            var salt = new byte[16];

            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            return Hash(input, salt);
        }

        public HashResultDto Hash(string input, byte[] salt)
        {
            var key = KeyDerivation.Pbkdf2(
                password: input,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 32
            );

            var hash = Convert.ToBase64String(key);

            return new HashResultDto
            {
                Hash = hash,
                Salt = salt
            };
        }
    }
}