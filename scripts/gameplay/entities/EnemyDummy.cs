using Godot;
using System;

public partial class EnemyDummy : StaticBody2D
{
    [Export] public int MaxHealth { get; set; } = 100;

    private int _currentHealth;
    private bool _isDefeated;

    public override void _Ready()
    {
        _currentHealth = MaxHealth;
        GD.Print($"{Name} ready with {_currentHealth} health.");
    }

    public void TakeDamage(int amount)
    {
        if (_isDefeated || amount <= 0)
        {
            return;
        }

        _currentHealth = Math.Max(_currentHealth - amount, 0);
        GD.Print($"{Name} took {amount} damage. Health: {_currentHealth}/{MaxHealth}");

        if (_currentHealth == 0)
        {
            _isDefeated = true;
            GD.Print($"{Name} defeated.");
        }
    }
}
