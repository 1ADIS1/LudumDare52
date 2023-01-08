using Godot;
using System.Collections.Generic;

public partial class TutorialPaper : StaticBody3D
{
    [Export] HarvestSystem harvestSystem;
    [Export] Furnace furnace;

    [Export] int[] furnaceMeatCapacities;
    [Export] EntranceDoor entranceDoor;
    List<ColorRect> papers;

    bool HadInteracted = false;
    int currentPhase;

    public override void _Ready()
    {
        papers = new List<ColorRect> { GetNode<ColorRect>("Paper1"), GetNode<ColorRect>("Paper2"), GetNode<ColorRect>("Paper3") };
    }

    public void Interact()
    {
        if (HadInteracted)
        {
            GD.Print("Already interacted!");
            return;
        }

        if (currentPhase == papers.Count && !HadInteracted)
        {
            GD.Print("Cuscene!");

            HadInteracted = true;
            entranceDoor.light.LightEnergy = 0;
            entranceDoor.animationPlayer.Play("Ending");
            return;
        }

        papers[currentPhase].Visible = true;
    }

    public override void _Input(InputEvent @event)
    {
        if (currentPhase >= papers.Count)
        {
            return;
        }
        if (Input.IsActionJustPressed("interact") && papers[currentPhase].Visible)
        {
            harvestSystem.StartGathering();
            furnace.MeatCapacity = furnaceMeatCapacities[currentPhase];
            GD.Print("New meat goal: ", furnace.MeatCapacity);

            papers[currentPhase].Visible = false;
            HadInteracted = true;
        }
    }

    public void OnMeatProcessingEnd()
    {
        GD.Print("Go and check new note!");
        currentPhase++;
        HadInteracted = false;
    }
}
