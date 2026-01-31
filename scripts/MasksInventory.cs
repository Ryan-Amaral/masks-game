using Godot;
using System;
using EventHandler = GameJam2026Masks.scripts.EventHandler;

public partial class MasksInventory : Control
{
	// Called when the node enters the scene tree for the first time.
	
	private MaskItemSlot[] Slots = new MaskItemSlot[3];
	private MaskItemSlot SelectedMask;
	public override void _Ready()
	{
		EventHandler.OnMaskSelected += Update;
		EventHandler.OnMaskUnlocked += Unlock;
		for (int i = 0; i < Slots.Length; i++)
		{
			Slots[i] = GetNode<MaskItemSlot>("HBoxContainer/Mask"+(i+1));
		}
	}

	public void Unlock(int maskId)
	{
		Slots[maskId-1].Show();
		Update(maskId);
	}

	public void Update(int selectedMask)
	{
		if (selectedMask == 0 && SelectedMask != null)
		{
			SelectedMask.Deselect();
			SelectedMask = null;
			return;
		}

		if (SelectedMask != null)
		{
			SelectedMask.Deselect();
		}
		
		Slots[selectedMask-1].Select();
		SelectedMask = Slots[selectedMask-1];
	}
	
}
