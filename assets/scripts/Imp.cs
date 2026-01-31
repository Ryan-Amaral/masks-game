using Godot;
using System;

public partial class Imp : RigidBody3D
{
	[Export] public bool IsThrown;
	[Export] public Area3D TriggerArea;
	[Export] public CpuParticles3D Particles;
	[Export] public MeshInstance3D TheMesh;
	
	private bool alreadyExploded = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TriggerArea.BodyEntered += OnTriggerEnter;
	}
	
	private void OnTriggerEnter(Node other)
	{
		if (other == this) return;
		GD.Print("Collided with: ", other.Name);
		if (IsThrown && !alreadyExploded){
			alreadyExploded = true;
			Explode(other);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// If after being thrown by player the trigger detects a solid surface, explode.
		
	}
	
	private async void Explode(Node other){
		// Hide mesh and start particles/explosion.
		TheMesh.Visible = false;
		Particles.Emitting = true;
		
		// Crumble the wall (if applicable).
		//var wall = other.GetNode<CrumbleWall>("Crumble Wall");
		//if (wall != null){
		//	wall.Crumble();
		//}
		
		// Delete after short wait.
		await ToSignal(GetTree().CreateTimer(1f), "timeout");
		QueueFree();
	}
}
