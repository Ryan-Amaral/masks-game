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
	
	private void OnTriggerEnter(Node body)
	{
		if (body == this) return;
		GD.Print("Collided with: ", body.Name);
		if (IsThrown && !alreadyExploded){
			alreadyExploded = true;
			Explode();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// If after being thrown by player the trigger detects a solid surface, explode.
		
	}
	
	private async void Explode(){
		TheMesh.Visible = false;
		Particles.Emitting = true;
		await ToSignal(GetTree().CreateTimer(1f), "timeout");
		QueueFree();
	}
}
