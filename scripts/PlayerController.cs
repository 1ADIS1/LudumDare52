using Godot;
using System;

public partial class PlayerController : CharacterBody3D
{
    [Export] private float MovementSpeed = 20f;
    [Export] private float MouseSensitivity = 0.002f; // radians/pixel
    [Export] private float MaxCameraRotationX = 1.2f;
    [Export] private float FallAcceleration = 10f;
    [Export] private Node3D CameraPivot;
    [Export] private Camera3D Camera;

    private Vector3 _velocity;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!IsOnFloor())
        {
            _velocity.y -= FallAcceleration * (float)delta;
        }

        Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_forward", "move_backwards");
        Vector3 direction = (Transform.basis * new Vector3(inputDirection.x, 0, inputDirection.y)).Normalized();
        if (direction != Vector3.Zero)
        {
            _velocity.x = direction.x * MovementSpeed;
            _velocity.z = direction.z * MovementSpeed;
        }
        else
        {
            var tempVelocity = _velocity.MoveToward(direction, MovementSpeed);
            _velocity.x = tempVelocity.x;
            _velocity.z = tempVelocity.z;
        }

        Velocity = _velocity;
        MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            RotateY(-mouseMotion.Relative.x * MouseSensitivity);
            CameraPivot.RotateX(-mouseMotion.Relative.y * MouseSensitivity);
            CameraPivot.Rotation = CameraPivot.Rotation.Clamp(new Vector3(-MaxCameraRotationX, 0, 0),
                new Vector3(MaxCameraRotationX, 0, 0));
        }
    }
}
