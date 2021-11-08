using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataItem
{
    public int ID;
    public string var1;
    public List<string> var2;
    public bool var3;
    public Color var4;

    public DataItem(DataItem d)
    {
        ID = d.ID;
        var1 = d.var1;
        var2 = d.var2;
        var3 = d.var3;
        var4 = d.var4;

    }
}
