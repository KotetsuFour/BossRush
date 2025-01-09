using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xiaoyu : PlayerCharacter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Quickshot
    public override Move[] useMove1()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 30;
        att.attackType = StaticData.NORM;
        att.physical = false;

        Move[] ret = { att };
        return ret;
    }
    //Life
    public override Move[] useMove2()
    {
        Revive rev = new Revive();
        rev.numTargets = 1;

        Move[] ret = { rev };
        return ret;
    }
    //Barrage
    public override Move[] useMove3()
    {
        Attack att = new Attack();
        att.numTargets = 4;
        att.attackStrength = 50;
        att.attackType = StaticData.WIND;
        att.physical = false;

        Move[] ret = { att };
        return ret;
    }
    //Cyclone Arrow
    public override Move[] useMove4()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 100;
        att.attackType = StaticData.WIND;
        att.physical = false;

        Move[] ret = { att };
        return ret;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
