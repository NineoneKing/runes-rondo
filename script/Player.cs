using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;//水平移动速度
	public const float JumpVelocity = -700.0f;//跳跃给予速度

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;//重力加速度
		}
		if (Input.IsActionPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;//跳
		}
		if (Input.IsActionJustReleased("ui_accept") && !IsOnFloor()&&velocity.Y<0)
		{
			velocity.Y = 0;
		}//随地大小跳
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");//速度方向向量
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;//方向向量不等于0向量，走路
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, 50);//等于0，脚滑
		}
		Velocity = velocity;
		MoveAndSlide();//图灵老祖保佑我
	}
}
