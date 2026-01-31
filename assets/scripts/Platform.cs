using Godot;
using System;

public partial class Platform : StaticBody3D
{
	[Export] public MeshInstance3D TheMesh;
	[Export] public CollisionShape3D Collider;
	[Export] public bool DefaultOn = false;
	
	private bool isMaskActive = false;
	private bool enabled = false;
	private float timer = 0f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TheMesh.Visible = DefaultOn;
		Collider.Disabled = !DefaultOn;
		enabled = DefaultOn;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Tmp remove later, for debug.
		timer += (float)delta;
		if (timer < 3) return;
		timer = 0f;
		isMaskActive = !isMaskActive;
		
		
		enabled = !(DefaultOn == isMaskActive);
		TheMesh.Visible = enabled;
		Collider.Disabled = !enabled;
		GD.Print("enabled: ", enabled);
	}
}
