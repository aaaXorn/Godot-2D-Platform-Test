using Godot;
using System;

public partial class PlayerController : CharController
{
	public override void _Process(double _delta)
	{
		base._Process(_delta);

		input_axis_move = InputManager.s_singleton.axis_move;
		//GD.Print(input_axis_move.X);

		if(InputManager.s_singleton.jump) ChangeGravityDirection(Vector2.Right);
		else if(InputManager.s_singleton.attack) ChangeGravityDirection(Vector2.Left);
	}
}
