using Godot;
using System;

public class PlayerController : KinematicBody2D
{

    // Base Physics
    [Export]
    private int gravity = 400;
    [Export]
    private int moveSpeed = 150;
    [Export]
    private float friction = .01f;
    private float acceleration;
    [Export]
    private float accelerationOnGround = .3f;
    [Export]
    private float accelerationInAir = .01f;

    // Standards
    private Vector2 velocity = new Vector2();
    private Vector2 previousPos;
    private int facingDirection = 0;
    private int facingDirectionCheck = 0;
    private bool takingDamage = false;
    [Export]
    private int health = 1;
    [Export]
    private int jumpsInAir = 2;
    private int jumpsInAirReset = 2;
    private AnimatedSprite animatedSprite;

    // Jump
    private bool inAir = false;
    [Export]
    private int jumpPower = 300;
    private Godot.KinematicBody2D characterArea;
    [Signal]
    public delegate void Death();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        jumpsInAirReset = jumpsInAir;
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    // public void getDamage(int damage){
    //     health -= damage;
    // }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (velocity.x == moveSpeed)
        {
            moveSpeed += 2;
        }
        else if (velocity.x == -moveSpeed)
        {
            moveSpeed += 2;
        }
        if (IsOnFloor())
        {
            acceleration = accelerationOnGround;
        }
        else
        {
            acceleration = accelerationInAir;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        if (!IsOnFloor())
        {
            velocity.y += gravity * delta;
        }
        else
        {
            velocity.y = 1;
        }

        if (health > 0)
        {
            ProcessMovement(delta);
        }
        facingDirection = 0;

        MoveAndSlide(velocity, Vector2.Up);

        if (inAir && previousPos.y < this.Position.y)
        {
            animatedSprite.Play("Falling");
        }
        else if (IsOnFloor() && Input.IsActionPressed("left") || Input.IsActionPressed("right"))
        {
            animatedSprite.Play("Run");
        }

        previousPos = this.Position;
    }

    private void ProcessMovement(float delta)
    {
        if (Input.IsActionPressed("left"))
        {
            facingDirection -= 1;
            animatedSprite.FlipH = true;
            facingDirectionCheck -= 1;
        }
        if (Input.IsActionPressed("right"))
        {
            facingDirection += 1;
            animatedSprite.FlipH = false;
            facingDirectionCheck += 1;
        }

        if (inAir && jumpsInAir > 0 && Input.IsActionJustPressed("jump"))
        {
            jumpsInAir--;
            velocity.y = -jumpPower;
        }

        if (IsOnCeiling())
        {
            velocity.y = 0;
        }

        if (IsOnFloor())
        {
            if (Input.IsActionJustPressed("jump"))
            {
                inAir = true;
                velocity.y = -jumpPower;
                animatedSprite.Play("Jump");
            }
            else
            {
                jumpsInAir = jumpsInAirReset;
                inAir = false;
            }
            // if (velocity.x <= 25 || velocity.x >= -25)
            // {
            //     animatedSprite.Play("Run");
            // }
        }

        if (facingDirection != 0)
        {
            velocity.x = Mathf.Lerp(velocity.x, facingDirection * moveSpeed, acceleration);
        }
        else
        {
            if (velocity.x < 25 && velocity.x > -25 && !inAir)
            {
                animatedSprite.Play("Idle");
            }
            velocity.x = Mathf.Lerp(velocity.x, 0, friction);
        }
    }

    public void takeDamage()
    {
        if (health > 0)
        {
            takingDamage = true;
            health--;
            velocity = MoveAndSlide(new Vector2(400f * -facingDirectionCheck, -80));
            if (health <= 0)
            {
                health = 0;
                animatedSprite.Stop();
                Hide();
                EmitSignal(nameof(Death));
            }
        }
    }
}
