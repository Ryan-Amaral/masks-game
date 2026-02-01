using Godot;
using System;

public partial class MainMenu : Control
{
	[Export] private Button Start;
	[Export] private Button Exit;
	[Export] private Button Credit;
	[Export] private Panel CreditsPanel;

	public override void _Ready()
	{
		Start.Pressed += OnStartButtonPressed;
		Exit.Pressed += OnExitPressed;
		Credit.Pressed += OnCreditsPressed;
	}

	public void OnStartButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://screens/game.tscn");
	}
	
	public void OnExitPressed()
	{
		GetTree().Quit();
	}
	
	public void OnCreditsPressed()
	{
		CreditsPanel.Visible = true;
	}
}
