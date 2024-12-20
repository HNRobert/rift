﻿using Rift.Runtime;

namespace Rift;

internal class Program
{
    private static void Main(string[] args)
    {
        Bootstrap.Init();
        Bootstrap.Load();
        Bootstrap.Run(args);
        Bootstrap.Shutdown();
    }
}