using Godot;

namespace GameJam2026Masks.scripts;

public interface IThrowable : IInteractable
{
    public string GetName();
    public void Attach(Node3D node);
    public void Throw(Vector3 dir, float force);
    public void Drop();
}