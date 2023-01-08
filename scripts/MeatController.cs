using Godot;
using System;

public partial class MeatController : StaticBody3D
{
    [Export] private float CollectionDuration;
    [Export] private int MinGrowDuration;
    [Export] private int MaxGrowDuration;
    [Export] private Vector3 MaxGrowthSize;
    [Export] private Timer CollectionTimer;
    [Export] private CPUParticles3D DestroyParticles;
    [Export] private MeshInstance3D Mesh;

    public bool CanHarvest = false;
    public Tween scaleTween;
    private Vector3 startingSize;

    [Signal]
    public delegate void InteractedEventHandler(MeatController meatController);

    [Signal]
    public delegate void MeatDestroyedEventHandler(MeatController meatController);

    public override void _Ready()
    {
        scaleTween = Mesh.CreateTween();
        startingSize = Mesh.Scale;
        CollectionTimer.WaitTime = CollectionDuration;
    }

    public void ProcessGrowth()
    {
        Random random = new Random();

        scaleTween.TweenProperty(Mesh, "scale", MaxGrowthSize, random.Next(MinGrowDuration, MaxGrowDuration));
        scaleTween.TweenCallback(new Callable(this, "MeatHasGrown"));
        scaleTween.Play();
    }

    public void MeatHasGrown()
    {
        GD.Print("Plant has grown!");
        CanHarvest = true;
        CollectionTimer.Start();
    }

    public void OnCollectionTimerTimeout()
    {
        DestroyParticles.Restart();
        DestroyParticles.Emitting = true;

        RestartMeat();
        EmitSignal(nameof(MeatDestroyed), this);
    }

    public void Interact()
    {
        EmitSignal("Interacted", this);
    }

    public void Harvest()
    {
        RestartMeat();
    }

    public void RestartMeat()
    {
        CollectionTimer.Stop();
        Mesh.Scale = startingSize;
        CanHarvest = false;
        scaleTween.Kill();
        scaleTween = Mesh.CreateTween();
        ProcessGrowth();
    }
}
