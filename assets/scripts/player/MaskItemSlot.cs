using Godot;
using System;

public partial class MaskItemSlot : Control
{
	[Export] public int MaskId;
	
	private Label Label;
	private TextureRect Image;
	private ColorRect Highlight;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Label = GetNode<Label>("Label");
		Image = GetNode<TextureRect>("Image");
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
	}

	public void Deselect()
	{
		Highlight.SetVisible(false);
	}
}
