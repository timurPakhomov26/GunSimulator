
using System;

public class BuilderBase 
{
    public bool MagazineInTrigger{get; set; }
    public bool MagazineInWeapon{get; set; }
    public bool IsCollider{get; set; }
    public Action OnTrigger{get; set; }
    public Action OnReloadMagazine{get; set; }
    public bool Enabled{get; set; }
   
    void SetEnabled();

}
