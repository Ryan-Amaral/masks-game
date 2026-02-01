using Godot;
using System;
using GameJam2026Masks.scripts;
using EventHandler = GameJam2026Masks.scripts.EventHandler;

public partial class Imp : RigidBody3D, IThrowable
{
	[Export] public int MaskId =2;
	[Export] private bool DefaultOn = false;

	[Export] public bool IsThrown;
	[Export] public Area3D TriggerArea;
	[Export] public CpuParticles3D Particles;
	[Export] public MeshInstance3D TheMesh;
	
	private CollisionShape3D Collider;
	private Node PrevParent;
	
	private bool alreadyExploded = false;
	private bool IsShowing;
	public override void _Ready()
	{
		TriggerArea.BodyEntered += OnTriggerEnter;
		Collider = TriggerArea.GetChild<CollisionShape3D>(0);
		EventHandler.OnMaskSelected += OnSetMask;

		if (DefaultOn)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}

	private void OnSetMask(int maskId)
	{
		if (maskId == MaskId)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}

	private void Show()
	{
		Visible = true;
		IsShowing = true;
	}

	private void Hide()
	{
		Visible = false;
		IsShowing = false;
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
		
		EventHandler.OnMaskSelected -= OnSetMask;
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

	public bool CanInteract()
	{
		return IsShowing;
	}

	public string GetName()
	{
		return "Imp";
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
