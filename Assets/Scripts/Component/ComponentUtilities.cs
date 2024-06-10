using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentUtilities
{
    public static int getBaseComponentCount(ComponentData component)
    {
        if (component.ComponentA == null || component.ComponentB == null)
            return 1;
        return getBaseComponentCount(component.ComponentA) + getBaseComponentCount(component.ComponentB);
    }
}
