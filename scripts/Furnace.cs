using Godot;
using System;

public partial class Furnace : StaticBody3D
{
    [Export] private PlayerController player;
    [Export] public int MeatCapacity;

    public int meatLoaded;

    public void Interact()
    {
        if (player.meat.Visible && meatLoaded < MeatCapacity)
        {
            GD.Print("Meat was loaded!");
            player.meat.Visible = false;
            meatLoaded++;
        }
        else if (meatLoaded == MeatCapacity)
        {
            GD.Print("Furnace is full!");
        }
    }
}
