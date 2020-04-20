using System;

namespace Controlsubscriptions
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string resultMessage;
            try
            {
                resultMessage = new InitializeInputData().Display();
            }
            catch (Exception exception)
            {
                resultMessage = exception.Message;
            }

            Console.WriteLine(resultMessage);
            Console.ReadLine();
        }
    }
}
