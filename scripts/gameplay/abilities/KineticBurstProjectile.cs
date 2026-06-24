using Godot;

namespace cyberarpg.scripts.gameplay.abilities;

public partial class KineticBurstProjectile : Area2D
{
    [Export] public float Speed { get; set; } = 720.0f;
    [Export] public int Damage { get; set; } = 20;
    [Export] public float LifetimeSeconds { get; set; } = 1.2f;

    private Vector2 _direction = Vector2.Right;
    private double _ageSeconds;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += _direction * Speed * (float)delta;
        _ageSeconds += delta;

        if (_ageSeconds >= LifetimeSeconds)
        {
            QueueFree();
        }
    }

    public void Launch(Vector2 direction)
    {
        if (direction.LengthSquared() <= 0.001f)
        {
            return;
        }

        _direction = direction.Normalized();
        Rotation = _direction.Angle();
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is not IDamageable)
        {
            return;
        }

        CombatDebugLog.Write($"Kinetic Burst hit {body.Name}.");
        DamageResolver.Resolve(new DamageRequest(this, body, Damage, "Kinetic Burst"));
        QueueFree();
    }
}
