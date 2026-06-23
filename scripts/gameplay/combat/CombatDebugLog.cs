using Godot;
using System;

public static class CombatDebugLog
{
    public static event Action<string> MessageLogged;

    public static void Write(string message)
    {
        GD.Print(message);
        MessageLogged?.Invoke(message);
    }
}
