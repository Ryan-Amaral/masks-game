using Godot;
using System;
using EventHandler = GameJam2026Masks.scripts.EventHandler;

public partial class Platform : StaticBody3D
{
	[Export] public MeshInstance3D TheMesh;
	[Export] public CollisionShape3D Collider;
	[Export] private bool DefaultOn = false;
	[Export] private int MaskId = 1;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TheMesh.Visible = DefaultOn;
		Collider.Disabled = !DefaultOn;
		EventHandler.OnMaskSelected += OnMaskChanged;
	}

	
	private void OnMaskChanged(int mask)
	{
		Enable(mask == MaskId);
			
	}
	
	private void Enable(bool enabled){
		TheMesh.Visible = enabled;
		Collider.Disabled = !enabled;
	}
}
