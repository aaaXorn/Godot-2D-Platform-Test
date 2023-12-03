using Godot;
using System;

public partial class InputManager : Node
{
	public static InputManager s_singleton;

	public InputManager()
	{
		if(s_singleton == null)
		{
			s_singleton = this;
		}
		else
		{
			QueueFree();
		}
	}

	public Vector2 axis_move = new Vector2();
	public bool jump = false;
	public bool attack = false;

	public override void _Process(double delta)
	{
		//horizontal input
		int _h_move = 0;
		//right is positive
		if(Input.IsActionPressed("move_right"))
		{
			_h_move += 1;
		}
		//left is negative
		//if instead of else if so _h_move becomes 0 if both are pressed
		if(Input.IsActionPressed("move_left"))
		{
			_h_move -= 1;
		}

		axis_move.X = _h_move;

		jump = Input.IsActionPressed("jump");
		attack = Input.IsActionPressed("attack");
	}
}
