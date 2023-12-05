using Godot;
using System;

public partial class PlayerController : CharController
{
	public override void _Process(double _delta)
	{
		base._Process(_delta);

		input_axis_move = InputManager.s_singleton.axis_move;
		//GD.Print(input_axis_move.X);

		if(Input.IsActionPressed("grav_right"))
		{
			ChangeGravityDirection(Vector2.Right);
			rot_vel_angle_up = true;
		}
		else if(Input.IsActionPressed("grav_left"))
		{
			ChangeGravityDirection(Vector2.Left);
			rot_vel_angle_up = true;
		}
		else if(Input.IsActionPressed("grav_up"))
		{
			ChangeGravityDirection(Vector2.Up);
			rot_vel_angle_up = false;
		}
		else if(Input.IsActionPressed("grav_down"))
		{
			ChangeGravityDirection(Vector2.Down);
			rot_vel_angle_up = false;
		}
	}
}
