using System.Collections.Generic;
using System;

[Serializable]
public class ShopProfile
{
    public int SkinCurrent;
    public List<bool> ListSkinUnlocked = new List<bool>();

    public void SetUnlockSkin(int idSkin)
    {
        ListSkinUnlocked[idSkin] = true;
    }
    public ShopProfile()
    {
        SkinCurrent = 0;
        ListSkinUnlocked.Add(true);
    }
}