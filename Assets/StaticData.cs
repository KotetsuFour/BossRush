using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    public static int NORM = 0;
    public static int FIRE = 1;
    public static int WATR = 2;
    public static int WIND = 3;
    public static int WOOD = 4;

    public static Color[] colorByType = { Color.white, new Color(1, 0.4f, 0.4f), Color.cyan, Color.green, new Color(172f / 255, 124f / 255, 80f / 255) };

    private static float[,] effectivenessChart =
    {
        /*Norm[0]*/{ 1, 1, 1, 1, 1 },
        /*Fire[1]*/{ 1, 1, 0.5f, 1, 2 },
        /*Watr[2]*/{ 1, 2, 1, 0.5f, 1 },
        /*Wind[3]*/{ 1, 1, 2, 1, 0.5f },
        /*Wood[4]*/{ 1, 0.5f, 1, 2, 1 },
    };
    public static float effectiveness(int attackerType, int defenderType)
    {
        return effectivenessChart[attackerType, defenderType];
    }
    public static Transform findDeepChild(Transform parent, string childName)
    {
        LinkedList<Transform> kids = new LinkedList<Transform>();
        for (int q = 0; q < parent.childCount; q++)
        {
            kids.AddLast(parent.GetChild(q));
        }
        while (kids.Count > 0)
        {
            Transform current = kids.First.Value;
            kids.RemoveFirst();
            if (current.name == childName || current.name + "(Clone)" == childName)
            {
                return current;
            }
            for (int q = 0; q < current.childCount; q++)
            {
                kids.AddLast(current.GetChild(q));
            }
        }
        return null;
    }

}
