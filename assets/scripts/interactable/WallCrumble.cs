using Godot;
using System;
using Godot.Collections;

public partial class WallCrumble : Node3D
{
	private Array<Node> Bricks;
	private CollisionShape3D Collider;
	private Timer Timer;
	public override void _Ready()
	{
		Bricks = GetNode("breakwall").GetChildren();
		Collider = GetNode<CollisionShape3D>("StaticBody3D/CollisionShape3D");
		Timer = GetNode<Timer>("Timer");
	}

	public void Crumble(Vector3 impact)
	{
		foreach (var brick in Bricks)
		{
			if (brick is RigidBody3D rb)
			{
				rb.Freeze = false;
				rb.ApplyImpulse((new Vector3((float)GD.RandRange(-1f,1f),(float)GD.RandRange(-1f,1f),(float)GD.RandRange(-1f,1f))).Normalized() *100f );
			}
		}
		Collider.Disabled = true;
		
		Timer.Start();
		Timer.Timeout += PhaseOut;
	}

	public void PhaseOut()
	{
		foreach (var brick in Bricks)
		{
			if (brick is RigidBody3D rb)
			{
				rb.CollisionLayer = 0;
				rb.CollisionMask = 0;
			}
		}
		
		Timer.Start();
		Timer.Timeout -= PhaseOut;
		Timer.Timeout += Delete;
	}

	private void Delete()
	{
		QueueFree();
	}
}
