using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethod
{
    public static void Select(this Dictionary<WayType, GameObject> dic, Action action)
    {
        for(int i = 0; i < dic.Count; i++)
        {
            action.Invoke();
        }
    }
}
