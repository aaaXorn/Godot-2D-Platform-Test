using Godot;
using System;

public partial class Util2D
{
	public static float DegreeToRadians(float _degrees)
	{
		return ((float)Math.PI / 180f) * _degrees;
	}
	public static float RadiansToDegrees(float _radians)
	{
		return (180f / (float)Math.PI) * _radians;
	}
}
