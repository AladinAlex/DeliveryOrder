using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tools
{
    public static class NumberGenerator
    {
        /// <summary>
        /// Генерирует строку из букв и цифр
        /// </summary>
        /// <param name="length">Длина генерируемой строки</param>
        public static string Generate(int length)
        {
            string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; // Допустимые символы

            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(validChars.Length);
                char randomChar = validChars[index];
                stringBuilder.Append(randomChar);
            }

            return stringBuilder.ToString();
        }
    }
}
