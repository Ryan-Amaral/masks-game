using Godot;
using System;
using EventHandler = GameJam2026Masks.scripts.EventHandler;

public partial class RespawnArea : Area3D
{
	private Node3D RespawnPoint;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect(Area3D.SignalName.BodyEntered, new Callable(this, nameof(OnAreaEntered)));
		RespawnPoint = GetNode<Node3D>("RespawnPoint");
		
	}

	public void OnAreaEntered(Node3D node)
	{
		if (node is Player player)
		{
			EventHandler.OnRespawnAreaEntered?.Invoke(RespawnPoint.GlobalPosition);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
