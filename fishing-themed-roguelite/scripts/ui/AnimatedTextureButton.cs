using Godot;
using System;

[GlobalClass]
public partial class AnimatedTextureButton : TextureButton
{
	[ExportCategory("Hover")] 
	[Export] private Vector2 _hoverScale = new Vector2(1.1f, 1.1f);
	[Export] private float _hoverAnimationLength = 0.1f;
	[Export] private float _unHoverAnimationLength = 0.1f;

	[ExportCategory("Press")] 
	[Export] private Vector2 _pressScale = new Vector2(0.95f, 0.95f);
	[Export] private float _pressAnimationLength1 = 0.1f;
	[Export] private float _pressAnimationLength2 = 0.1f;

	private Tween _animationTween;

	public override void _Ready()
	{
		Pressed += OnButtonPress;

		MouseEntered += OnButtonHover;
		MouseExited += OnButtonUnHover;

		PivotOffsetRatio = Vector2.One / 2.0f;
	}

	private void OnButtonPress()
	{
		if (_animationTween != null && GodotObject.IsInstanceValid(_animationTween))
		{
			_animationTween.Kill();
		}
		_animationTween = CreateTween().SetTrans(Tween.TransitionType.Sine);

		_animationTween.TweenProperty(this, "scale", _pressScale, _pressAnimationLength1);
		_animationTween.Chain().TweenProperty(this, "scale", _hoverScale, _pressAnimationLength2);
	}

	private void OnButtonHover()
	{
		if(Disabled) return;
		
		if (_animationTween != null && GodotObject.IsInstanceValid(_animationTween))
		{
			_animationTween.Kill();
		}
		_animationTween = CreateTween().SetTrans(Tween.TransitionType.Sine);

		_animationTween.TweenProperty(this, "scale", _hoverScale, _hoverAnimationLength);
	}

	private void OnButtonUnHover()
	{	
		if (_animationTween != null && GodotObject.IsInstanceValid(_animationTween))
		{
			_animationTween.Kill();
		}
		_animationTween = CreateTween().SetTrans(Tween.TransitionType.Sine);

		_animationTween.TweenProperty(this, "scale", Vector2.One, _unHoverAnimationLength);
	}
}
