using Godot;
using System;
public partial class smoothCamera2d : Camera2D
{
	[Export] private NodePath nodePath;//拖入玩家节点
	[Export] private float movementspeed_x=0.14f;
	[Export] private float movementspeed_y=0.22f;//速度系数
	[Export] private float minspeed_x=0.15f;
	[Export] private float minspeed_y=1f;//最小步长
	Node2D _node;
	private Tween _shakeTween;
	public override void _Ready()
	{
		_node =GetNode<Node2D>(nodePath);
	}
	public override void _PhysicsProcess(double delta)
	{
		if (_node == null) return; // 安全防御
		Vector2 playerposition =_node.GlobalPosition;
		Vector2 camaraposition=GlobalPosition;
		float distans_x=camaraposition.X-playerposition.X;
		float distans_y=camaraposition.Y-playerposition.Y;//玩家与摄像机距离差
		float vx=Mathf.MoveToward(camaraposition.X, playerposition.X,(distans_x*distans_x*movementspeed_x*(float)delta)+minspeed_x);
		float vy=Mathf.MoveToward(camaraposition.Y, playerposition.Y,(distans_y*distans_y*movementspeed_y*(float)delta)+minspeed_y);//移动步长=距离差^2*速度系数*delta+最小步长
		GlobalPosition=new Vector2(vx,vy);//图灵老祖保佑我
	}
	public void cameraShake(float shakePower_x,float shakePower_y)
	{
		if (_shakeTween != null && _shakeTween.IsValid())
        _shakeTween.Kill();//终止上一次抖动
    	Offset = Vector2.Zero;//归零偏移，避免新动画从旧位置开始
		
	}
	public void cameraExplodeShake(float power)
	{
    	if (_shakeTween != null && _shakeTween.IsValid())
        _shakeTween.Kill();//终止上一次抖动
    	Offset = Vector2.Zero;//归零偏移，避免新动画从旧位置开始
		float Angle=(float)GD.RandRange(0,Mathf.Tau);
		Vector2 randomVector=Vector2.FromAngle(Angle);//随机方向向量
		Tween tween = CreateTween();
		tween.TweenProperty(this,"Offset",randomVector*power,0.5);
		tween.TweenProperty(this,"Offset",Vector2.Zero,1.5);
	}
}
