using Godot;
using System;
using GameJam2026Masks.assets.scripts;
using GameJam2026Masks.scripts;
using EventHandler = GameJam2026Masks.scripts.EventHandler;

public partial class NPC : StaticBody3D, IInteractable
{
	public void OnFocus()
	{
	}

	public void OnLoseFocus()
	{
	}

	public void Interact(Player player)
	{
		EventHandler.OnShowDialogue?.Invoke(new DialogueInfo{Dialogue = "Hi, hope nobody throws around my brethren.", Speaker = "Imp", DisableAfter = 5f});
	}
}
