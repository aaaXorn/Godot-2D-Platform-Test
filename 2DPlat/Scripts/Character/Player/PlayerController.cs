using Godot;
using System;

public partial class PlayerController : CharController
{
	public override void _Process(double _delta)
	{
		input_axis_move = InputManager.s_singleton.axis_move;
		GD.Print(input_axis_move.X);
	}
}
