using Godot;
using System;

public partial class EnemyDummy : StaticBody2D, IDamageable
{
    private const float DamageNumberLifetimeSeconds = 0.65f;
    private const float DamageNumberRiseDistance = 34.0f;
    private const float HitFlashReturnSeconds = 0.12f;
    private const float DefeatedMarkerWidth = 4.0f;

    private static readonly Vector2 DamageNumberSize = new(84.0f, 26.0f);
    private static readonly Color BodyHitFlashColor = new(1.0f, 0.86f, 0.44f, 1.0f);
    private static readonly Color CoreHitFlashColor = new(1.0f, 0.98f, 0.84f, 1.0f);
    private static readonly Color BodyDefeatedColor = new(0.18f, 0.16f, 0.19f, 0.82f);
    private static readonly Color CoreDefeatedColor = new(0.36f, 0.03f, 0.08f, 1.0f);
    private static readonly Color DefeatedMarkerColor = new(1.0f, 0.18f, 0.12f, 1.0f);

    [Export] public int MaxHealth { get; set; } = 100;

    private int _currentHealth;
    private bool _isDefeated;
    private Polygon2D _bodyVisual;
    private Polygon2D _coreVisual;
    private CollisionShape2D _collisionShape;
    private Color _bodyBaseColor;
    private Color _coreBaseColor;
    private Tween _hitFlashTween;

    public override void _Ready()
    {
        _currentHealth = MaxHealth;
        _bodyVisual = GetNodeOrNull<Polygon2D>("Body");
        _coreVisual = GetNodeOrNull<Polygon2D>("Core");
        _collisionShape = GetNodeOrNull<CollisionShape2D>("CollisionShape2D");

        if (_bodyVisual is not null)
        {
            _bodyBaseColor = _bodyVisual.Color;
        }

        if (_coreVisual is not null)
        {
            _coreBaseColor = _coreVisual.Color;
        }

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
        bool defeatedByHit = _currentHealth == 0;

        ShowDamageNumber(appliedAmount);
        PlayHitFlash(defeatedByHit);

        if (defeatedByHit)
        {
            Defeat();
        }

        return DamageResult.AppliedTo(Name.ToString(), appliedAmount, _currentHealth, MaxHealth);
    }

    private void PlayHitFlash(bool settleDefeated = false)
    {
        if (_bodyVisual is null && _coreVisual is null)
        {
            return;
        }

        if (_hitFlashTween is not null && _hitFlashTween.IsValid())
        {
            _hitFlashTween.Kill();
        }

        _hitFlashTween = CreateTween();
        _hitFlashTween.SetParallel();

        if (_bodyVisual is not null)
        {
            _bodyVisual.Color = BodyHitFlashColor;
            _hitFlashTween.TweenProperty(
                    _bodyVisual,
                    "color",
                    settleDefeated ? BodyDefeatedColor : _bodyBaseColor,
                    HitFlashReturnSeconds)
                .SetTrans(Tween.TransitionType.Sine)
                .SetEase(Tween.EaseType.Out);
        }

        if (_coreVisual is not null)
        {
            _coreVisual.Color = CoreHitFlashColor;
            _hitFlashTween.TweenProperty(
                    _coreVisual,
                    "color",
                    settleDefeated ? CoreDefeatedColor : _coreBaseColor,
                    HitFlashReturnSeconds)
                .SetTrans(Tween.TransitionType.Sine)
                .SetEase(Tween.EaseType.Out);
        }
    }

    private void Defeat()
    {
        _isDefeated = true;
        DisableCollision();
        ShowDefeatedMarker();
        CombatDebugLog.Write($"{Name} defeated.");
    }

    private void DisableCollision()
    {
        SetDeferred("collision_layer", 0);
        SetDeferred("collision_mask", 0);

        if (_collisionShape is not null)
        {
            _collisionShape.SetDeferred("disabled", true);
        }
    }

    private void ShowDefeatedMarker()
    {
        Line2D firstSlash = CreateDefeatedMarkerLine();
        firstSlash.AddPoint(new Vector2(-22.0f, -22.0f));
        firstSlash.AddPoint(new Vector2(22.0f, 22.0f));
        AddChild(firstSlash);

        Line2D secondSlash = CreateDefeatedMarkerLine();
        secondSlash.AddPoint(new Vector2(-22.0f, 22.0f));
        secondSlash.AddPoint(new Vector2(22.0f, -22.0f));
        AddChild(secondSlash);
    }

    private static Line2D CreateDefeatedMarkerLine()
    {
        return new Line2D
        {
            Width = DefeatedMarkerWidth,
            DefaultColor = DefeatedMarkerColor,
            ZIndex = 15,
        };
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
