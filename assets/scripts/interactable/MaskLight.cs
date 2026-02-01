using Godot;
using System;
using EventHandler = GameJam2026Masks.scripts.EventHandler;

public partial class MaskLight : SpotLight3D
{
	[Export]private int MaskId= 3;
	private bool Showing;
	public override void _Ready()
	{
		EventHandler.OnMaskSelected += Toggle;
		Toggle(0);
	}

	public void Toggle(int mask)
	{
		if (MaskId == 0)
		{
			Visible = true;
			return;
		}

		if (MaskId == mask)
		{
			Visible = true;
		}
		else
		{
			Visible = false;
		}
	}

	public override void _Process(double delta)
	{
	}
}
