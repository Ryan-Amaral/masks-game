using Godot;
using System;
using GameJam2026Masks.assets.scripts;
using GameJam2026Masks.scripts;
using EventHandler = GameJam2026Masks.scripts.EventHandler;

public partial class MaskInteract : StaticBody3D, IInteractable
{
	[Export]private int MaskId;
	[Export]private string MaskName;
	[Export]private string MaskDialogue;


	public override void _Ready()
	{
		Tween tween = CreateTween();
		tween.SetLoops();

		tween.TweenProperty(this, "position:y", -0.5, 3.0).AsRelative().SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.InOut);
		tween.TweenProperty(this, "position:y", 0.5, 3.0).AsRelative().SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.InOut);
		
		Tween spinTween = CreateTween();
		spinTween.SetLoops();
		spinTween.TweenProperty(this, "rotation:y", MathF.Tau, 5f).AsRelative();
	}

	public override void _Process(double delta)
	{
	}
	

	public void OnFocus()
	{
	}

	public void OnLoseFocus()
	{
	}

	public void Interact(Player player)
	{
		player.AddMask(MaskId);
		QueueFree();
		EventHandler.OnShowDialogue?.Invoke(new DialogueInfo{Dialogue = MaskDialogue, Speaker = MaskName});
	}
}
