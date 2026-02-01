using Godot;
using System;

public partial class CreditsPanel : Panel
{
	public override void _GuiInput(InputEvent @event)
	{
		if (Input.IsActionPressed("interact") || Input.IsMouseButtonPressed(MouseButton.Left))
		{
			Visible = false;
		} 
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (Input.IsActionPressed("interact") || Input.IsMouseButtonPressed(MouseButton.Left))
		{
			Visible = false;
		} 
	}
}
