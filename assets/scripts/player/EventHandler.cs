using System;
using Godot;

namespace GameJam2026Masks.scripts;

public static class EventHandler
{
	public static Action<int> OnMaskUnlocked;

	public static Action<int> OnMaskSelected;
	public static Action<Vector3> OnRespawnAreaEntered;

}
