using Godot;

public partial class PlayerController : CharacterBody2D
{
    private const string MoveLeftAction = "move_left";
    private const string MoveRightAction = "move_right";
    private const string MoveUpAction = "move_up";
    private const string MoveDownAction = "move_down";
    private const string PrimaryFireAction = "primary_fire";

    [Export] public float MoveSpeed { get; set; } = 260.0f;
    [Export] public NodePath AimPivotPath { get; set; } = "AimPivot";
    [Export] public PackedScene KineticBurstScene { get; set; } = null!;
    [Export] public float FireSpawnDistance { get; set; } = 36.0f;

    private Node2D _aimPivot = null!;
    private Vector2 _aimDirection = Vector2.Right;

    public override void _Ready()
    {
        _aimPivot = GetNode<Node2D>(AimPivotPath);
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDirection = Input.GetVector(
            MoveLeftAction,
            MoveRightAction,
            MoveUpAction,
            MoveDownAction);

        Velocity = inputDirection * MoveSpeed;
        MoveAndSlide();

        UpdateAimDirection();

        if (Input.IsActionJustPressed(PrimaryFireAction))
        {
            FireKineticBurst();
        }
    }

    private void UpdateAimDirection()
    {
        Vector2 aimDirection = GetGlobalMousePosition() - GlobalPosition;

        if (aimDirection.LengthSquared() <= 0.001f)
        {
            return;
        }

        _aimDirection = aimDirection.Normalized();
        _aimPivot.Rotation = _aimDirection.Angle();
    }

    private void FireKineticBurst()
    {
        if (KineticBurstScene is null)
        {
            GD.PushWarning("Cannot fire Kinetic Burst because no projectile scene is assigned.");
            return;
        }

        KineticBurstProjectile projectile = KineticBurstScene.Instantiate<KineticBurstProjectile>();
        GetParent().AddChild(projectile);

        projectile.GlobalPosition = GlobalPosition + _aimDirection * FireSpawnDistance;
        projectile.Launch(_aimDirection);

        CombatDebugLog.Write($"Fired Kinetic Burst toward {_aimDirection}.");
    }
}
