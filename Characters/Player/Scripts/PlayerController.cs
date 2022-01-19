using Godot;
using System;

public class PlayerController : KinematicBody2D
{
    // Variables for movement:
    // private int speed = 800;
    // private int gravity = 18000;
    // private int jumpForce = 9000;
    // private float friction = 0.01f;
    // private float acceleration = 0.25f;
    // private float drag = 0.1f;
    // private float weightAcceleration = 0.3f;

    // Base Physics
    [Export]
    private int gravity = 400;
    [Export]
    private int moveSpeed = 150;
    [Export]
    private float friction = .01f;
    [Export]
    private float acceleration = .3f;
    [Export]
    private float accelerationInAir = .01f;

    // Standards
    private Vector2 velocity = new Vector2();
    private int facingDirection = 0;
    private int facingDirectionCheck = 0;
    private bool takingDamage = false;
    [Export]
    private int health = 1;

    // Jump
    private bool inAir = false;
    [Export]
    private int jumpPower = 200;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

     // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // if (velocity.x == moveSpeed)
        // {
        //     moveSpeed += 2;
        // }
        // else if (velocity.x == -moveSpeed)
        // {
        //     moveSpeed += 2;
        // }
    }
    public override void _PhysicsProcess(float delta)
    {
        // Vector2 velocity = new Vector2();
        // int direction = 0;
        // if(Input.IsActionPressed("right")){
        //     direction += 1;
        // }
        // if(Input.IsActionPressed("left") ){
        //     direction -= 1;
        // }
        // if (direction != 0){
        //     velocity.x = Mathf.Lerp(velocity.x, direction*speed, acceleration);
        // } else {
        //     velocity.x = Mathf.LerpAngle(velocity.x, 0, friction);
        // }
        // if(Input.IsActionJustPressed("jump")){
        //     if(IsOnFloor()){
        //         velocity.y -= jumpForce;
        //     }
        // }
        // velocity.y += gravity * delta;

        // MoveAndSlide(velocity, Vector2.Up);



        velocity.y += gravity*delta;

        if (health > 0)
        {
            ProcessMovement(delta);
        }
        facingDirection = 0;
        MoveAndSlide(velocity, Vector2.Up);
    }

    private void ProcessMovement(float delta)
    {
        if (Input.IsActionPressed("left"))
        {
            facingDirection -= 1;
            facingDirectionCheck -= 1;
        }
        if (Input.IsActionPressed("right"))
        {
            facingDirection += 1;
            facingDirectionCheck += 1;
        }
        if (facingDirection != 0)
        {
            velocity.x = Mathf.Lerp(velocity.x, facingDirection * moveSpeed, acceleration);
        }
        else {
            velocity.x = Mathf.Lerp(velocity.x, 0, friction);
        }
    }
}
