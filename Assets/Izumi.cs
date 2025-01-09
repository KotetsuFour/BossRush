using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Izumi : PlayerCharacter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Flurry
    public override Move[] useMove1()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 40;
        att.attackType = StaticData.NORM;
        att.physical = true;

        Move[] ret = { att };
        return ret;
    }
    //Block
    public override Move[] useMove2()
    {
        Effect eff = new Effect();
        eff.numTargets = 0;
        eff.effect = StaticData.WOOD;

        Move[] ret = { eff };
        return ret;
    }
    //Wooden Leg
    public override Move[] useMove3()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 50;
        att.attackType = StaticData.WOOD;
        att.physical = true;

        Move[] ret = { att };
        return ret;
    }
    //Spin Kick
    public override Move[] useMove4()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 150;
        att.attackType = StaticData.WOOD;
        att.physical = true;

        Move[] ret = { att };
        return ret;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
