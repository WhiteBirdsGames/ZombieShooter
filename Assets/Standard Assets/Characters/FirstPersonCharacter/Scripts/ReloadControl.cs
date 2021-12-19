using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadControl : MonoBehaviour
{
    public WeaponsControl weaponsControl;

    public void OnEndReload()
    {
        weaponsControl.OnEndReload();
    }
}
