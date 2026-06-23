using Godot;
using System;

public partial class EnemyDummy : StaticBody2D, IDamageable
{
    private const float DamageNumberLifetimeSeconds = 0.65f;
    private const float DamageNumberRiseDistance = 34.0f;

    private static readonly Vector2 DamageNumberSize = new(84.0f, 26.0f);

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
        ShowDamageNumber(appliedAmount);

        if (_currentHealth == 0)
        {
            _isDefeated = true;
            CombatDebugLog.Write($"{Name} defeated.");
        }

        return DamageResult.AppliedTo(Name.ToString(), appliedAmount, _currentHealth, MaxHealth);
    }

    private void ShowDamageNumber(int amount)
    {
        Label damageLabel = new()
        {
            Text = amount.ToString(),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Size = DamageNumberSize,
            Position = new Vector2(-DamageNumberSize.X / 2.0f, -58.0f),
            ZIndex = 20,
            Modulate = new Color(1.0f, 0.82f, 0.18f, 1.0f),
        };

        damageLabel.AddThemeColorOverride("font_outline_color", new Color(0.08f, 0.02f, 0.02f, 1.0f));
        damageLabel.AddThemeConstantOverride("outline_size", 4);
        damageLabel.AddThemeFontSizeOverride("font_size", 22);

        AddChild(damageLabel);

        Vector2 startPosition = damageLabel.Position;
        Tween tween = CreateTween();
        tween.SetParallel();
        tween.TweenProperty(
                damageLabel,
                "position",
                startPosition + new Vector2(0.0f, -DamageNumberRiseDistance),
                DamageNumberLifetimeSeconds)
            .SetTrans(Tween.TransitionType.Cubic)
            .SetEase(Tween.EaseType.Out);
        tween.TweenProperty(damageLabel, "modulate:a", 0.0f, DamageNumberLifetimeSeconds)
            .SetTrans(Tween.TransitionType.Cubic)
            .SetEase(Tween.EaseType.In);
        tween.SetParallel(false);
        tween.TweenCallback(Callable.From(() => damageLabel.QueueFree()));
    }
}
