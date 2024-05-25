using SAAUR.BLL.Interfaces.Tools;
using SAAUR.MODELS.Entities;
using System.Security.Cryptography;
using System.Web.Helpers;

namespace SAAUR.BLL.Tools
{
    public class PasswordTool : IPasswordTool
    {
        private static string pass_char_lowercase = "abcdefghijklmnopqrstuvwxyz";
        private static string pass_char_uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string pass_char_numbers = "1234567890";

        public string Generate(int min_length, int max_length)
        {
            if (min_length <= 0 || max_length <= 0 || min_length > max_length)
                return null;

            char[][] chars_groups = new char[][]
            {
         pass_char_lowercase.ToCharArray(),
         pass_char_uppercase.ToCharArray(),
         pass_char_numbers.ToCharArray(),
            };

            int[] charsLeftInGroup = new int[chars_groups.Length];

            for (int i = 0; i < charsLeftInGroup.Length; i++)
                charsLeftInGroup[i] = chars_groups[i].Length;

            int[] leftGroupsOrder = new int[chars_groups.Length];

            for (int i = 0; i < leftGroupsOrder.Length; i++)
                leftGroupsOrder[i] = i;
            byte[] randomBytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            int seed = BitConverter.ToInt32(randomBytes, 0);

            Random random = new Random(seed);

            char[] password = null;

            if (min_length < max_length)
                password = new char[random.Next(min_length, max_length + 1)];
            else
                password = new char[min_length];

            int nextCharIdx, nextGroupIdx, nextLeftGroupsOrderIdx, lastCharIdx;

            int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            for (int i = 0; i < password.Length; i++)
            {
                if (lastLeftGroupsOrderIdx == 0)
                    nextLeftGroupsOrderIdx = 0;
                else
                    nextLeftGroupsOrderIdx = random.Next(0, lastLeftGroupsOrderIdx);

                nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                if (lastCharIdx == 0)
                    nextCharIdx = 0;
                else
                    nextCharIdx = random.Next(0, lastCharIdx + 1);

                password[i] = chars_groups[nextGroupIdx][nextCharIdx];

                if (lastCharIdx == 0)
                    charsLeftInGroup[nextGroupIdx] = chars_groups[nextGroupIdx].Length;
                else
                {
                    if (lastCharIdx != nextCharIdx)
                    {
                        char temp = chars_groups[nextGroupIdx][lastCharIdx];
                        chars_groups[nextGroupIdx][lastCharIdx] = chars_groups[nextGroupIdx][nextCharIdx];
                        chars_groups[nextGroupIdx][nextCharIdx] = temp;
                    }

                    charsLeftInGroup[nextGroupIdx]--;
                }

                if (lastLeftGroupsOrderIdx == 0)
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                else
                {
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] = leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }

                    lastLeftGroupsOrderIdx--;
                }
            }

            return new string(password);
        }

        public ModelPasswordHash GenerateSalt(string pass)
        {
            string salt = Crypto.GenerateSalt();
            string pwd = pass + salt;
            string hashedPass = Crypto.HashPassword(pwd);
            return new ModelPasswordHash() { hashPass = hashedPass, salt = salt };
        }

        public bool VerifyHashPassword(string pass, string hash, string salt)
        {
            pass = pass + salt;
            var Verify = Crypto.VerifyHashedPassword(hash, pass);
            return Verify;
        }

        public string CryptoEncrypt(string _string)
        {
            string result = string.Empty;
            byte[] encrypted = System.Text.Encoding.Unicode.GetBytes(_string);
            result = Convert.ToBase64String(encrypted);
            return result;
        }

        public string CryptoDecrypt(string _string)
        {
            string result = string.Empty;
            byte[] decrypted = Convert.FromBase64String(_string);
            result = System.Text.Encoding.Unicode.GetString(decrypted);
            return result;
        }
    }
}
