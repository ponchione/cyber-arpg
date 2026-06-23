using Godot;
using System.Collections.Generic;

public partial class PlayerHud : CanvasLayer
{
    [Export] public NodePath CombatLogLabelPath { get; set; } = "Root/CombatLogPanel/MarginContainer/CombatLog/Entries";
    [Export] public int MaxCombatMessages { get; set; } = 6;

    private readonly Queue<string> _combatMessages = new();
    private Label _combatLogLabel = null!;

    public override void _Ready()
    {
        _combatLogLabel = GetNode<Label>(CombatLogLabelPath);
        CombatDebugLog.MessageLogged += OnCombatMessageLogged;
        RefreshCombatLog();
    }

    public override void _ExitTree()
    {
        CombatDebugLog.MessageLogged -= OnCombatMessageLogged;
    }

    private void OnCombatMessageLogged(string message)
    {
        _combatMessages.Enqueue(message);

        while (_combatMessages.Count > MaxCombatMessages)
        {
            _combatMessages.Dequeue();
        }

        RefreshCombatLog();
    }

    private void RefreshCombatLog()
    {
        if (_combatLogLabel is null)
        {
            return;
        }

        _combatLogLabel.Text = _combatMessages.Count == 0
            ? "No combat events yet."
            : string.Join("\n", _combatMessages);
    }
}
