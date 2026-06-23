using Godot;

public partial class PlayerController : CharacterBody2D
{
    private const string MoveLeftAction = "move_left";
    private const string MoveRightAction = "move_right";
    private const string MoveUpAction = "move_up";
    private const string MoveDownAction = "move_down";

    [Export] public float MoveSpeed { get; set; } = 260.0f;
    [Export] public NodePath AimPivotPath { get; set; } = "AimPivot";

    private Node2D _aimPivot = null!;

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
    }

    private void UpdateAimDirection()
    {
        Vector2 aimDirection = GetGlobalMousePosition() - GlobalPosition;

        if (aimDirection.LengthSquared() <= 0.001f)
        {
            return;
        }

        _aimPivot.Rotation = aimDirection.Angle();
    }
}
