using Godot;
using System;
using GameJam2026Masks.assets.scripts;
using GameJam2026Masks.scripts;
using EventHandler = GameJam2026Masks.scripts.EventHandler;

public partial class NPC : StaticBody3D, IInteractable
{
	[Export] protected int MaskId =2;
	[Export] protected string Dialogue;
	[Export] protected string Speaker;

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
			Showing = true;
			return;
		}

		if (MaskId == mask)
		{
			Visible = true;
			Showing = true;
		}
		else
		{
			Visible = false;
			Showing = false;
		}
	}

	public void OnFocus()
	{
	}

	public void OnLoseFocus()
	{
	}

	public virtual void Interact(Player player)
	{
		EventHandler.OnShowDialogue?.Invoke(new DialogueInfo{Dialogue = Dialogue, Speaker = Speaker, DisableAfter = 5f});
	}

	public bool CanInteract()
	{
		return Showing;
	}
}
