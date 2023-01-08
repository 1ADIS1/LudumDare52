using Godot;
using System;

public partial class EntranceDoor : StaticBody3D
{
    [Export] private AnimationPlayer animationPlayer;
    [Export] private PlayerController player;
    [Export] private HarvestSystem harvestSystem;
    [Export] private Furnace furnace;
    [Export] private Timer meatProcessingTimer;
    public bool IsMeatProcessing = false;

    public void Interact()
    {
        if (IsMeatProcessing)
        {
            GD.Print("Meat is being processed...");
            return;
        }

        if (furnace.meatLoaded < furnace.MeatCapacity)
        {
            GD.Print("Load meat first!");
            return;
        }

        IsMeatProcessing = true;
        animationPlayer.Play("ProcessMeat");
        meatProcessingTimer.Start();
        harvestSystem.StopGathering();
        if (player.meat.Visible)
        {
            player.meat.Visible = false;
        }

        GD.Print("Meat Processing has started!");
    }

    public void MeatProcessingTimerTimeout()
    {
        GD.Print("All meat has burnt!");
        animationPlayer.Stop();
        furnace.meatLoaded = 0;
    }
}
