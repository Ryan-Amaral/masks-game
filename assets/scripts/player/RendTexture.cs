using Godot;
using System;

public partial class RendTexture : Godot.TextureRect
{
	[Export] private SubViewport Viewport;
	[Export] private Node3D ModelNode;

	private Tween tween;
	public override void _Ready()
	{
		Texture = Viewport.GetTexture();
	}
	
	public void Highlight()
	{
		tween = CreateTween();
		tween.SetLoops();
		tween.TweenProperty(ModelNode, "rotation:y", -0.1, 1.0).SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.InOut);
		tween.TweenProperty(ModelNode, "rotation:y", 0.1, 1.0).SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.InOut);

	}

	public void Deselect()
	{
		tween.Stop();
		ModelNode.Position = Vector3.Zero;
	}
	
}
