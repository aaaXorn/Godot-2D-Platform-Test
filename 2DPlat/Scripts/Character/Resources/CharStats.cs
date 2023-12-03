using Godot;
using System;

public partial class CharStats : Resource
{
	[Export]
	public float gravity_force {get; protected set;} = 750f;
	[Export]
	public float gravity_rot_spd {get; protected set;} = 45f;

	[Export]
	public float target_h_spd {get; protected set;} = 500f;
	[Export]
	public float h_spd_accel {get; protected set;} = 2000f;
}
