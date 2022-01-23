using Godot;
using System;

public class GameManager : Node2D
{
    [Export]
    public Position2D respawnPoint2D;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//
//  }

    public void respawnPlayer()
    {
        PlayerController pc = GetNode<PlayerController>("Player");
        // pc.GlobalPosition = respawnPoint2D.GlobalPosition;
        pc.GlobalPosition = GetNode<Position2D>("RespawnPoint").GlobalPosition;
        pc.respawnPlayer();
    }

    private void _on_Player_Death()
    {
        GetTree().ChangeScene("Main.tscn");
    }
}