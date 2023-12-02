using Godot;
using System;

public partial class CharController : RigidBody2D
{
	protected Vector2 input_axis_move = new Vector2();

	protected Vector2 gravity_direction = Vector2.Up;
	protected float rotated_angle => Vector2.Down.AngleTo(gravity_direction);
	protected Vector2 rotated_velocity => LinearVelocity.Rotated(Vector2.Down.AngleTo(gravity_direction));
	
	[Export]
	protected CharStats stats;

	public override void _Ready()
	{
		
	}

	public override void _Process(double _delta)
	{
		
	}

	public override void _PhysicsProcess(double _delta)
	{
		Movement(input_axis_move.X, _delta);

		Gravity(_delta);
	}
	
	protected virtual void Movement(float _h_input, double _delta)
	{
		GD.Print(LinearVelocity + " " + rotated_velocity);

		if(_h_input == 0f)
		{
			DeccelAboveLimit();

			float _dirX = Mathf.Sign(rotated_velocity.X);
			//LinearVelocity -= new Vector2(_dirX * stats.h_spd_accel * (float)_delta, 0f);
			HorizontalMovement(-_dirX, _delta);

			if((_dirX > 0f && rotated_velocity.X < 0f) || (_dirX < 0f && rotated_velocity.X > 0f))
			{
				LinearVelocity = new Vector2(0f, rotated_velocity.Y).Rotated(rotated_angle);
			}
		}
		else if(Math.Sign(rotated_velocity.X) == Math.Sign(_h_input))
		{
			if(Math.Abs(rotated_velocity.X) < stats.target_h_spd)
			{
				//ApplyCentralForce(Vector2.Right * _h_input * stats.h_spd_accel * (float)_delta);
				//LinearVelocity += new Vector2(_h_input * stats.h_spd_accel * (float)_delta, 0f);
				HorizontalMovement(_h_input, _delta);
			}

			DeccelAboveLimit();
		}
		else if(Math.Sign(rotated_velocity.X) != Math.Sign(_h_input))
		{
			//ApplyCentralForce(Vector2.Right * _h_input * stats.h_spd_accel * (float)_delta);
			//LinearVelocity += new Vector2(_h_input * stats.h_spd_accel * (float)_delta, 0f);
			HorizontalMovement(_h_input, _delta);

			DeccelAboveLimit();
		}
	}

	//horizontal movement
	protected void HorizontalMovement(float _h_dir, double _delta)
	{
		HorizontalMovement(_h_dir, 1f, _delta);
	}
	protected virtual void HorizontalMovement(float _h_dir, float _spd_mod, double _delta)
	{
		//uses LinearVelocity to change speed linearly, instead of exponentially
		LinearVelocity += new Vector2(_h_dir * stats.h_spd_accel * _spd_mod * (float)_delta, 0f).Rotated(rotated_angle);
	}

	protected void DeccelAboveLimit()
	{
		//deaccelerates the character if they're above maximum velocity
		if(Math.Abs(rotated_velocity.X) > stats.target_h_spd)
		{
			LinearVelocity = new Vector2(Math.Sign(rotated_velocity.X) * stats.target_h_spd, rotated_velocity.Y).Rotated(rotated_angle);
		}
	}

	protected virtual void Gravity(double _delta)
	{
		//pulls the character towards the direction of gravity exponentially
		ApplyCentralForce(gravity_direction * stats.gravity_force * (float)_delta);
		
		GlobalRotation = gravity_direction.AngleTo(Vector2.Down);
	}
}
