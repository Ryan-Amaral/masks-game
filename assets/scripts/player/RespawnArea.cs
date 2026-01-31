using Godot;
using System;

public partial class RespawnArea : Area3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect(Area3D.SignalName.BodyEntered, new Callable(this, nameof(OnAreaEntered)));

	}

	public void OnAreaEntered(Node3D node)
	{
		GD.Print("OnAreaEntered " +node);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
