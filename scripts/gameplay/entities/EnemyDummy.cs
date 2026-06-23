using Godot;
using System;

public partial class EnemyDummy : StaticBody2D, IDamageable
{
    [Export] public int MaxHealth { get; set; } = 100;

    private int _currentHealth;
    private bool _isDefeated;

    public override void _Ready()
    {
        _currentHealth = MaxHealth;
        GD.Print($"{Name} ready with {_currentHealth} health.");
    }

    public DamageResult ApplyDamage(DamageRequest request)
    {
        if (_isDefeated || request.Amount <= 0)
        {
            return DamageResult.NotApplied(Name.ToString(), _currentHealth, MaxHealth);
        }

        int appliedAmount = Math.Min(request.Amount, _currentHealth);
        _currentHealth = Math.Max(_currentHealth - request.Amount, 0);

        if (_currentHealth == 0)
        {
            _isDefeated = true;
            CombatDebugLog.Write($"{Name} defeated.");
        }

        return DamageResult.AppliedTo(Name.ToString(), appliedAmount, _currentHealth, MaxHealth);
    }
}
