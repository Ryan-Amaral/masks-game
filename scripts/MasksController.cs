using System.Collections.Generic;
using Godot;

namespace GameJam2026Masks.scripts;

public class MasksController
{
	 public bool[] Masks = new bool[4];
	 public int SelectedMask;
	 
	 public MasksController()
	 {
		  Masks[0] = true;
	 }
	 
	 public void Add(int maskId)
	 {
		  Masks[maskId] = true;
		  EventHandler.OnMaskUnlocked?.Invoke(maskId);
	 }

	 public void SwitchMask(int maskId)
	 {
		  if (maskId == SelectedMask)
		  {
			   SelectedMask = 0;
			   EventHandler.OnMaskSelected?.Invoke(0);
			   return;
		  }
		  
		  if (Masks[maskId])
		  {
			   SelectedMask = maskId;
			   EventHandler.OnMaskSelected?.Invoke(maskId); 
		  }
		  else
		  {
			   SelectedMask = 0;
			   EventHandler.OnMaskSelected?.Invoke(0);
		  }
	 }

	 public void HandleInput(InputEvent @event)
	 {
		  if (Input.IsActionPressed("slot_1"))
		  {
			   SwitchMask(1);
		  } else if (Input.IsActionPressed("slot_2"))
		  {
			   SwitchMask(2);
		  } else if (Input.IsActionPressed("slot_3"))
		  {
			   SwitchMask(3);
		  } 
	 }
}
