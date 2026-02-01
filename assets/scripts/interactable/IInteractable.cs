namespace GameJam2026Masks.scripts;

public interface IInteractable
{
    public void OnFocus();
    public void OnLoseFocus();

    public void Interact(Player player);
    public bool CanInteract();
}