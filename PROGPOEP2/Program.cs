using CybersecurityChatbotGUI;
using System;
using System.Windows.Forms;

namespace PROGPOEP2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Play voice greeting
            VoiceManager.PlayWelcome();

            // Display ASCII art in console
            AsciiArt.DisplayLogo();

            // Run the GUI
            Application.Run(new MainForm());
        }
    }
}