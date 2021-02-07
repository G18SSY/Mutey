﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Mutey.Output;

namespace Discord.Trial
{
    internal static class Program
    {
        private static void Main()
        {
            List<Process> discordProcesses = Process.GetProcesses()
                .Where(p => p.MainWindowHandle != IntPtr.Zero)
                .Where(p => p.ProcessName.Contains("Discord", StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (discordProcesses.Count == 0)
            {
                Console.WriteLine("Discord is not running...");
                Console.WriteLine("Exitting trial...");

                return;
            }

            if (discordProcesses.Count > 1)
                Console.WriteLine(
                    $"Multiple instances of Discord detected, testing first instance found ({discordProcesses[0].Id})");

            Process process = discordProcesses[0];

            UiAutomationCall call = new TrialCall("Discord", process.MainWindowHandle);

            Console.WriteLine("Call created, press enter to send toggle command...");

            while (true)
            {
                string? line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    Console.WriteLine("Sending toggle...");
                    call.Toggle();
                    Console.WriteLine("Toggle sent");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Exitting trial...");
                    return;
                }
            }
        }
    }
}