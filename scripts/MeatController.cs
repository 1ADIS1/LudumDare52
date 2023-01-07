using Godot;
using System;

public partial class MeatController : MeshInstance3D
{
    [Export] private float CollectionDuration;
    [Export] private int MinGrowDuration;
    [Export] private int MaxGrowDuration;
    [Export] private Vector3 MaxGrowthSize;
    [Export] private Timer CollectionTimer;
    [Export] private CPUParticles3D DestroyParticles;

    public override void _Ready()
    {
        CollectionTimer.WaitTime = CollectionDuration;
        ProcessGrowth();
    }

    public void ProcessGrowth()
    {
        Random random = new Random();

        var scaleTweener = CreateTween();
        scaleTweener.TweenProperty(this, "scale", MaxGrowthSize, random.Next(MinGrowDuration, MaxGrowDuration)).Finished += ()
            =>
            {
                GD.Print("Plant has grown!");
                CollectionTimer.Start();
            };
        scaleTweener.Play();
    }

    public void OnTimerTimeout()
    {
        GD.Print("Plant has exploded!");
        DestroyParticles.Emitting = true;
    }
}
