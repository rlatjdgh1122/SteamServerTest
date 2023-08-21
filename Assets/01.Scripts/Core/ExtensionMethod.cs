using Core;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class ExtensionMethod
{
    public static void Select<Tkey,Tvalue>(this Dictionary<Tkey, Tvalue> list, Tkey value, Action<Tvalue> selectAct, Action<Tvalue> unSelectAct)
    {
        foreach (Tkey item in Enum.GetValues(typeof(Tkey)))
        {
            if (item.ToString() == value.ToString())
            {
                selectAct(list[value]);
            }
            else
                unSelectAct(list[item]);
        }
    }
}
