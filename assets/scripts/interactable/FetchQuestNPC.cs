using GameJam2026Masks.assets.scripts;
using Godot;

namespace GameJam2026Masks.scripts;

public partial class FetchQuestNPC : NPC
{
	[Export] private string WantsObject;
	[Export] private string WrongObjectDialogue;
	[Export] private string OnCompleteDialogue;
	[Export] private string CompleteDialogue;
	[Export] private Node3D HoldNode;


	private bool Completed = false;
	public override void Interact(Player player)
	{
		if (Completed)
		{
			EventHandler.OnShowDialogue?.Invoke(new DialogueInfo{Dialogue = CompleteDialogue, Speaker = Speaker, DisableAfter = 5f});
			return;
		}

		if (player.IsHolding(WantsObject))
		{
			EventHandler.OnShowDialogue?.Invoke(new DialogueInfo{Dialogue = OnCompleteDialogue, Speaker = Speaker, DisableAfter = 5f});
			Completed = true;
			var obj = player.TakeHeldObject();
			obj.Attach(HoldNode);
		} else if (player.IsHoldingSomething())
		{
			EventHandler.OnShowDialogue?.Invoke(new DialogueInfo{Dialogue = WrongObjectDialogue, Speaker = Speaker, DisableAfter = 5f});
		}
		else
		{
			EventHandler.OnShowDialogue?.Invoke(new DialogueInfo{Dialogue = Dialogue, Speaker = Speaker, DisableAfter = 5f});
		}
	}

}
