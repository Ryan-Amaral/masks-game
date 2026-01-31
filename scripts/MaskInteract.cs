using Godot;
using System;
using GameJam2026Masks.scripts;

public partial class MaskInteract : RigidBody3D, IInteractable
{
	[Export]private string MaskId;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	

	public void OnFocus()
	{
	}

	public void OnLoseFocus()
	{
	}

	public void Interact(Player player)
	{
		player.AddMask(MaskId);
	}
}
