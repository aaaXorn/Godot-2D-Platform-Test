using Godot;
using System;

public partial class CharController : RigidBody2D
{
	protected Vector2 input_axis_move = new Vector2();

	[Export]
	protected Node2D gravity_node;
	protected Vector2 gravity_direction = Vector2.Up;

	protected float gravity_angle => Vector2.Down.AngleTo(gravity_direction);
	protected Vector2 rotated_velocity => LinearVelocity.Rotated(RotVelAngle());

	protected float RotVelAngle()
	{
		if(rot_vel_angle_up)
		{
			return Vector2.Up.AngleTo(gravity_direction);
		}
		else
		{
			return Vector2.Down.AngleTo(gravity_direction);
		}
	}

	public bool rot_vel_angle_up = false;

	public void ChangeGravityDirection(Vector2 _dir)
	{
		gravity_direction = _dir;
		gravity_node.GlobalRotation = gravity_direction.AngleTo((rot_vel_angle_up ? Vector2.Down : Vector2.Up));
	}

	protected bool can_move = true;
	
	[Export]
	protected CharStats stats;

	public override void _Ready()
	{
		
	}


	public override void _Process(double _delta)
	{
		base._Process(_delta);

		gravity_node.GlobalPosition = GlobalPosition;
	}

	public override void _PhysicsProcess(double _delta)
	{
		base._PhysicsProcess(_delta);

		Movement(input_axis_move.X, _delta);

		Gravity(_delta);
	}
	
	protected virtual void Movement(float _h_input, double _delta)
	{
		GD.Print(LinearVelocity + " " + rotated_velocity);

		if(_h_input == 0f || !can_move)
		{
			DeccelAboveLimit();

			float _dirX = Mathf.Sign(rotated_velocity.X);
			//LinearVelocity -= new Vector2(_dirX * stats.h_spd_accel * (float)_delta, 0f);
			HorizontalMovement(-_dirX, _delta);

			if((_dirX > 0f && rotated_velocity.X < 0f) || (_dirX < 0f && rotated_velocity.X > 0f))
			{
				LinearVelocity = new Vector2(0f, rotated_velocity.Y).Rotated(gravity_angle);
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

		GD.Print("Angle: " + GlobalRotationDegrees + " " + gravity_angle + " " + gravity_direction);
		GD.Print("Velocity: " + LinearVelocity + rotated_velocity);
	}

	//horizontal movement
	protected void HorizontalMovement(float _h_dir, double _delta)
	{
		HorizontalMovement(_h_dir, 1f, _delta);
	}
	protected virtual void HorizontalMovement(float _h_dir, float _spd_mod, double _delta)
	{
		//uses LinearVelocity to change speed linearly, instead of exponentially
		LinearVelocity += new Vector2(_h_dir * stats.h_spd_accel * _spd_mod * (float)_delta, 0f).Rotated(gravity_angle);
	}

	protected void DeccelAboveLimit()
	{
		//deaccelerates the character if they're above maximum velocity
		if(Math.Abs(rotated_velocity.X) > stats.target_h_spd)
		{
			LinearVelocity = new Vector2(Math.Sign(rotated_velocity.X) * stats.target_h_spd, rotated_velocity.Y).Rotated(gravity_angle);
		}
	}

	protected virtual void Gravity(double _delta)
	{
		//pulls the character towards the direction of gravity exponentially
		ApplyCentralForce(gravity_direction * stats.gravity_force * (float)_delta);
		
		//RotateToGravity(_delta);
		GlobalRotation = gravity_direction.AngleTo(Vector2.Down);
	}

	protected virtual void RotateToGravity( double _delta)
	{
		if(GlobalRotation != gravity_angle)
		{
			float _rot_dir = gravity_angle - GlobalRotation;

			GlobalRotationDegrees += stats.gravity_rot_spd * _rot_dir * (float)_delta;
			//GD.Print(GlobalRotationDegrees + " " + stats.gravity_rot_spd * _rot_dir * (float)_delta);

			/*if((_rot_dir > 0f && Rotation > gravity_angle) || (_rot_dir < 0f && gravity_angle < Rotation))
			{
				Rotation = gravity_angle;
			}*/

			//GD.Print(_rot_dir + " " + GlobalRotation + " " + gravity_angle + " " + gravity_direction.AngleTo(Vector2.Down) + " " + Util2D.DegreeToRadians(stats.gravity_rot_spd));
		}
	}

	
}
