using Godot;
using System;

public partial class MaskItemSlot : Control
{
	[Export] public int MaskId;
	
	private Label Label;
	private RendTexture Image;
	private ColorRect Highlight;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Label = GetNode<Label>("Label");
		Image = GetNode<RendTexture>("Image");
		Highlight = GetNode<ColorRect>("Highlight");
		
		Label.Text = MaskId.ToString();
		SetVisible(false);
	}

	public void Show()
	{
		SetVisible(true);
	}

	public void Select()
	{
		Highlight.SetVisible(true);
		Image.Highlight();
	}

	public void Deselect()
	{
		Highlight.SetVisible(false);
		Image.Deselect();
	}
}
