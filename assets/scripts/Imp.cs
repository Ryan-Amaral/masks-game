using Godot;
using System;
using GameJam2026Masks.scripts;

public partial class Imp : RigidBody3D, IThrowable
{
	[Export] public bool IsThrown;
	[Export] public Area3D TriggerArea;
	[Export] public CpuParticles3D Particles;
	[Export] public MeshInstance3D TheMesh;
	
	private CollisionShape3D Collider;
	private Node PrevParent;
	
	private bool alreadyExploded = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TriggerArea.BodyEntered += OnTriggerEnter;
		Collider = TriggerArea.GetChild<CollisionShape3D>(0);
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
		var wall = (WallCrumble)other.GetParent();
		GD.Print("Wall exploded");
		if (wall != null){
			wall.Crumble(GlobalPosition);
		}
		
		// Delete after short wait.
		await ToSignal(GetTree().CreateTimer(1f), "timeout");
		QueueFree();
	}

	public void OnFocus()
	{
	}

	public void OnLoseFocus()
	{
	}

	public void Interact(Player player)
	{
		player.PickUp(this);
	}

	public void Attach(Node3D node)
	{
		PrevParent = GetParent();
		Reparent(node);
		Position = Vector3.Zero;
		Freeze = true;
		Collider.Disabled = true;	
	}

	public void Throw(Vector3 dir, float force)
	{
		IsThrown = true;
		Collider.Disabled = false;	
		Reparent(PrevParent);
		Freeze = false;
		ApplyImpulse(dir*force);
	}

	public void Drop()
	{
		Collider.Disabled = false;	
		Reparent(PrevParent);
		Freeze = false;
	}
}
