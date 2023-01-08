using Godot;
using System;

public partial class TutorialPaper : StaticBody3D
{
    [Export] HarvestSystem harvestSystem;
    [Export] Furnace furnace;

    [Export] int[] furnaceMeatCapacities;
    [Export] ColorRect paper;

    bool HadInteracted = false;
    int currentPhase;

    public override void _Ready()
    {
        paper.Visible = false;
    }

    public void Interact()
    {
        if (HadInteracted)
        {
            GD.Print("Already completed tutorial!");
        }

        harvestSystem.StartGathering();
        paper.Visible = true;
        furnace.MeatCapacity = furnaceMeatCapacities[currentPhase];
        currentPhase++;
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(""))
        {

        }
    }
}
