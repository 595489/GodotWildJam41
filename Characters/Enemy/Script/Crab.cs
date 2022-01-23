using Godot;
using System;

public class Crab : KinematicBody2D
{
    // Declare member variables here. Examples:
    Sprite crab;
    RayCast2D bottomLeft;
    RayCast2D bottomRight;
    private Vector2 velocity;
    private int gravity = 200;
    private int speed = 30;
    private int damage = 1;
    // Godot.NodePath hitboxHit = GetNode<Area2D>("Hitbox/");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        crab = GetNode<Sprite>("Crab");
        bottomLeft = GetNode<RayCast2D>("LeftRaycast");
        bottomRight = GetNode<RayCast2D>("RightRaycast") ;
        velocity.x = speed;

        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _PhysicsProcess(float delta)
  {
      velocity.y += gravity * delta;
      if (velocity.y > gravity){
          velocity.y = gravity;
      }
      if (!bottomRight.IsColliding()) {
          velocity.x = -speed;
      }
      if (!bottomLeft.IsColliding()) {
          velocity.x = speed;
      }
      MoveAndSlide(velocity, Vector2.Up);
  }
//   private void hitboxHit(object body)
// {
//     if(body.GetType().Name.ToString()=="Player"){
        
//         GetNode<PlayerController>("../Player/PlayerController") as PlayerController;
//         PlayerController.getDamage(damage);

//     }
// }
}
