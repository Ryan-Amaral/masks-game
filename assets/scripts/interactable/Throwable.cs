using Godot;
using System;
using GameJam2026Masks.scripts;

public partial class Throwable : RigidBody3D, IThrowable, IResetable
{
	private CollisionShape3D Collider;
	private Node PrevParent;
	private Vector3 ResetPosition;
	public override void _Ready()
	{
		Collider = GetNode<CollisionShape3D>("Collider");
		ResetPosition = GlobalPosition;
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

	public void Drop()
	{
		Collider.Disabled = false;	
		Reparent(PrevParent);
		Freeze = false;
	}

	public void Reset()
	{
		GlobalPosition = ResetPosition;
		LinearVelocity = Vector3.Zero;
		AngularVelocity = Vector3.Zero;
	}

	public void Attach(Node3D node)
	{
		PrevParent = GetParent();
		Reparent(node);
		Position = Vector3.Zero;
		Freeze = true;
		Collider.Disabled = true;	
	}

	public void Throw(Vector3 direction, float force)
	{
		Collider.Disabled = false;	
		Reparent(PrevParent);
		Freeze = false;
		ApplyImpulse(direction*force);
	}
}
