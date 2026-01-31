using System;

namespace GameJam2026Masks.scripts;

public static class EventHandler
{
    public static Action<int> OnMaskUnlocked;

    public static Action<int> OnMaskSelected;
}