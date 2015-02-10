﻿using System;
using System.Windows.Forms;

namespace PulseAudio.Mixer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm("tcp:localhost"));
        }
    }
}
