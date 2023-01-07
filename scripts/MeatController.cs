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

    public bool CanHarvest;

    public override void _Ready()
    {
        CollectionTimer.WaitTime = CollectionDuration;
        ProcessGrowth();
    }

    public void ProcessGrowth()
    {
        Random random = new Random();

        var scaleTweener = Mesh.CreateTween();
        scaleTweener.TweenProperty(Mesh, "scale", MaxGrowthSize, random.Next(MinGrowDuration, MaxGrowDuration)).Finished += ()
            =>
            {
                GD.Print("Plant has grown!");
                CollectionTimer.Start();
            };
        scaleTweener.Play();
    }

    public void OnCollectionTimerTimeout()
    {
        GD.Print("Plant has exploded!");
        DestroyParticles.Emitting = true;
    }

    public void Interact()
    {
        GD.Print("Meat collected!");
    }
}
