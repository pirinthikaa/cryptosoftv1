using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CryptoSoft
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: CryptoSoft.exe <source file> <destination file>");
                return -1; // Code d'erreur pour "arguments invalides"
            }

            string sourceFilePath = args[0];
            string destinationFilePath = args[1];
            string keyFilePath = "config.txt"; // Chemin vers le fichier de configuration contenant la clé

            try
            {
                // Lecture de la clé de chiffrement
                string key = File.ReadAllText(keyFilePath, Encoding.UTF8);
                if (string.IsNullOrEmpty(key))
                {
                    Console.WriteLine("Encryption key is missing in the config file.");
                    return -2; // Code d'erreur pour "clé de chiffrement manquante"
                }
                // Initialisation et démarrage du chronomètre
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                // Chiffrement ou déchiffrement du fichier
                byte[] fileBytes = File.ReadAllBytes(sourceFilePath); //stocke dans un tableau d'octets (byte[]
                byte[] keyBytes = Encoding.UTF8.GetBytes(key); //utilise Encoding pour obtenir une représentation en octets de la chaîne key en utilisant l'encodage UTF-8
                byte[] resultBytes = new byte[fileBytes.Length]; //créat° nvx tableau d'octet byte[] ^m taille que fileBytes


                //chiffrement XOR est applique avec octet correspondant de la clé
                //utilisation de % pour assurer que si clé courte alors il répète = GARANTI QUE CHAQUE OCTET EST COMBINE
                for (int i = 0; i < fileBytes.Length; i++)
                {
                    resultBytes[i] = (byte)(fileBytes[i] ^ keyBytes[i % keyBytes.Length]);
                }

                // Écriture du fichier résultant
                File.WriteAllBytes(destinationFilePath, resultBytes);

                stopwatch.Stop();
                Console.WriteLine($"File processed successfully in ${stopwatch.ElapsedMilliseconds} ms.");

                // Retourne le temps de cryptage en millisecondes
                return (int)stopwatch.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return -3; // Code d'erreur pour "erreur lors du traitement du fichier"
            }
        }
    }
}
