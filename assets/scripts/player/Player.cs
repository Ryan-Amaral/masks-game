using Godot;
using System;
using GameJam2026Masks.scripts;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	[Export]private float LookSpeed = 0.002f;
	[Export]private float ThrowForce = 15f;
	
	private Vector2 LookRotation;
	private bool Captured = true;
	
	private Node3D Head;
	private RayCast3D RayCast;
	private Label InteractTip;
	private Node3D HeldItemNode;
	
	private IInteractable CurrentInteractable;
	private MasksController MasksController = new MasksController();
	private IThrowable CurrentThrowable;

	private float HoldCounter = 0;
	[Export]private float ThrowingThreshold = 0.5f;
	private bool Interacted = false;
	
	public override void _Ready()
	{
		Input.SetMouseMode(Input.MouseModeEnum.Captured);
		Head = GetNode<Node3D>("Head");
		RayCast = GetNode<RayCast3D>("Head/Camera/RayCast");
		InteractTip = GetNode<Label>("InteractTip");
		HeldItemNode = GetNode<Node3D>("Head/Camera/HeldItemNode");
	}

	public override void _Process(double delta)
	{
		if (RayCast.IsColliding())
		{
			GodotObject collider = RayCast.GetCollider();
			if (collider != null && collider is Node node)
			{
				if (node is IInteractable interactable && interactable.CanInteract())
				{
					CurrentInteractable = interactable;
					InteractTip.Visible = true;
					interactable.OnFocus();
				}
				else
				{
					CancelInteratable();
				}
			}
			else
			{
				CancelInteratable();
			}
		}
		else
		{
			CancelInteratable();
		}

		HandleInteractKey((float)delta);
	}

	private void CancelInteratable()
	{
		if (CurrentInteractable != null)
		{
			CurrentInteractable.OnLoseFocus();
			CurrentInteractable = null;
			InteractTip.Visible = false;
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventKey key)
		{
			if( key.IsActionPressed("esc") )
			{
				Input.SetMouseMode(Input.MouseModeEnum.Visible);
				Captured = false;
			}
			
			
		}

		
		if (Captured && @event is InputEventMouseMotion eventMouseMotion)
		{
			RotateLook(eventMouseMotion.Relative);
		}

		MasksController.HandleInput(@event);
	}

	private void HandleInteractKey(float delta)
	{
		bool justPressed = Input.IsActionJustPressed("interact");
		bool released = Input.IsActionJustReleased("interact");
		bool pressed = Input.IsActionPressed("interact");
		if (justPressed)
		{
			if (!Captured)
			{
				Input.SetMouseMode(Input.MouseModeEnum.Captured);
				Captured = true;
			} else if (CurrentInteractable != null)
			{
				CurrentInteractable.Interact(this);
				Interacted = true;
			}
		}

		if (pressed)
		{
			HoldCounter += delta;
		}

		if (released)
		{
			if (!Interacted && CurrentThrowable != null && HoldCounter < ThrowingThreshold)
			{
				CurrentThrowable.Drop();
				CurrentThrowable = null;
			} else if (!Interacted && CurrentThrowable != null && HoldCounter >= ThrowingThreshold)
			{
				Vector3 dir = (RayCast.GlobalTransform.Basis * RayCast.TargetPosition).Normalized();
				CurrentThrowable.Throw(dir,ThrowForce);
				CurrentThrowable = null;
			}
			
			Interacted = false;
			HoldCounter = 0;
		}
	}

	private void RotateLook(Vector2 motion)
	{
		LookRotation.X -= motion.Y * LookSpeed;
		LookRotation.X = Mathf.Clamp(LookRotation.X, float.DegreesToRadians(-85), float.DegreesToRadians(85));
		LookRotation.Y -= motion.X * LookSpeed;
		
		Transform3D transform = Transform;
		transform.Basis = Basis.Identity;
		Transform = transform;
		
		RotateY(LookRotation.Y);
		Transform3D headTransform = Head.Transform;
		headTransform.Basis = Basis.Identity;
		Head.Transform = headTransform;
		
		Head.RotateX(LookRotation.X);

	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("left", "right", "up", "down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void AddMask(int maskId)
	{
		GD.Print(maskId + " added.");
		MasksController.Add(maskId);
	}

	public void PickUp(IThrowable throwable)
	{
		if (CurrentThrowable != null)
		{
			CurrentThrowable.Drop();
		}
		
		CurrentThrowable = throwable;
		CurrentThrowable.Attach(HeldItemNode);
	}

	public bool IsHolding(string name)
	{
		if (CurrentThrowable == null)
		{
			return false;
		}
		
		return CurrentThrowable.GetName() == name;
	}

	public bool IsHoldingSomething()
	{
		return CurrentThrowable != null;
	}

	public IThrowable TakeHeldObject()
	{
		var temp = CurrentThrowable;
		CurrentThrowable = null;
		return temp;
	}
}
