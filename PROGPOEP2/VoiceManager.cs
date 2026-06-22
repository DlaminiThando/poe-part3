using System;
using System.IO;
using System.Media;

namespace PROGPOEP2
{
    public static class VoiceManager
    {
        public static void PlayWelcome()
        {
            try
            {
                // Look for the WAV file in multiple locations
                string audioPath = FindAudioFile("greeting.wav");

                if (!string.IsNullOrEmpty(audioPath) && File.Exists(audioPath))
                {
                    using (SoundPlayer player = new SoundPlayer(audioPath))
                    {
                        player.Load();
                        player.PlaySync();  // Plays the sound and waits for it to finish
                    }
                    Console.WriteLine("Voice greeting played successfully!");
                }
                else
                {
                    Console.WriteLine("Voice file not found - using text greeting");
                    DisplayTextGreeting();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Voice error: {ex.Message}");
                DisplayTextGreeting();
            }
        }

        private static string FindAudioFile(string filename)
        {
            // Check multiple possible locations
            string[] possiblePaths = new[]
            {
                filename,  // Same folder as the executable
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename),
                Path.Combine(Environment.CurrentDirectory, filename),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", filename),
                Path.Combine(Directory.GetCurrentDirectory(), filename)
            };

            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    Console.WriteLine($"Found voice file at: {path}");
                    return path;
                }
            }
            return null;
        }

        private static void DisplayTextGreeting()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine("🔊 [Voice Greeting]: Hello! Welcome to the");
            Console.WriteLine("   Cybersecurity Awareness Bot. I'm here to");
            Console.WriteLine("   help you stay safe online.");
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.ResetColor();
        }
    }
}