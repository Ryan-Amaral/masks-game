using Godot;
using System;
using GameJam2026Masks.scripts;
using EventHandler = GameJam2026Masks.scripts.EventHandler;

public partial class RespawnHandler : Node3D
{
	private Player Player;
	private Vector3 RespawnPoint;	
	public override void _Ready()
	{
		Connect(Area3D.SignalName.BodyEntered, new Callable(this, nameof(RespawnObject)));

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

	private void RespawnObject(Node3D node)
	{
		GD.Print(node);
		if (node is IResetable resetable)
		{
			resetable.Reset();
		}
	}

	private void Respawn()
	{
		Player.GlobalPosition = RespawnPoint;
	}
}
