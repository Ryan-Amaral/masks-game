using Godot;
using System;
using System.Collections.Generic;
using GameJam2026Masks.assets.scripts;
using EventHandler = GameJam2026Masks.scripts.EventHandler;

public partial class DialogueHandler : PanelContainer
{
	[Export]private RichTextLabel Text;
	[Export]private Label SpeakerLabel;
	[Export]private Timer Timer;
	
	private DialogueInfo Current = new DialogueInfo();
	private bool Showing;
	public override void _Ready()
	{
		EventHandler.OnShowDialogue += AddDialogue;
		Visible = false;
		Timer.Timeout += Hide;
	}

	private void AddDialogue(DialogueInfo req)
	{
		Timer.Stop();
		ShowDialogue(req);
	}

	private void Hide()
	{
		Showing = false;
		Visible = false;
	}
	
	private void ShowDialogue(DialogueInfo req)
	{
		Text.Text = req.Dialogue;
		SpeakerLabel.Text = req.Speaker;
		if (req.DisableAfter > 0f)
		{
			Timer.WaitTime = req.DisableAfter;
			Timer.Start();
		}

		Showing = true;
		Visible = true;
	}


	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventKey key)
		{
			if (key.IsActionPressed("confirm") && Showing)
			{
				Hide();
			}
		}
	}
}
