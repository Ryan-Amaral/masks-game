using Godot;
using System;
using EventHandler = GameJam2026Masks.scripts.EventHandler;

public partial class RespawnHandler : Node3D
{
	private Player Player;
	private Vector3 RespawnPoint;	
	public override void _Ready()
	{
		Player = GetParent().GetNode<Player>("Player");
		EventHandler.OnRespawnAreaEntered = UpdateRespawnPoint;
	}

	public void UpdateRespawnPoint(Vector3 respawnPoint)
	{
		RespawnPoint = respawnPoint;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Player.Position.Y < Position.Y)
		{
			Respawn();
		}
	}

	private void Respawn()
	{
		Player.GlobalPosition = RespawnPoint;
	}
}
