using Godot;
using System;

public class PlayerController : KinematicBody2D
{
    // Variables for movement:
    private int speed = 800;
    private int gravity = 18000;
    private int jumpForce = 9000;
    private float friction = 0.01f;
    private float acceleration = 0.25f;
    private float drag = 0.1f;
    private float weightAcceleration = 0.3f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Vector2 velocity = new Vector2();
        int direction = 0;
        if(Input.IsActionPressed("right")){
            direction += 1;
        }
        if(Input.IsActionPressed("left") ){
            direction -= 1;
        }
        if (direction != 0){
            velocity.x = Mathf.Lerp(velocity.x, direction*speed, acceleration);
        } else {
            velocity.x = Mathf.LerpAngle(velocity.x, 0, friction);
        }
        if(Input.IsActionJustPressed("jump")){
            if(IsOnFloor()){
                velocity.y -= jumpForce;
            }
        }
        velocity.y += gravity * delta;
        
        MoveAndSlide(velocity, Vector2.Up);
    }
}
