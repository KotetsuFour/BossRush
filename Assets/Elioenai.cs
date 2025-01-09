using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elioenai : PlayerCharacter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Bubble
    public override Move[] useMove1()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 30;
        att.attackType = StaticData.WATR;
        att.physical = false;

        Move[] ret = { att };
        return ret;
    }
    //Relieve
    public override Move[] useMove2()
    {
        Heal hel = new Heal();
        hel.numTargets = 1;
        hel.amount = specAttack;

        Move[] ret = { hel };
        return ret;
    }
    //Meditate
    public override Move[] useMove3()
    {
        Heal hel = new Heal();
        hel.numTargets = 0;
        hel.amount = specAttack + 30;

        Move[] ret = { hel };
        return ret;
    }
    //Whirlpool
    public override Move[] useMove4()
    {
        AffinityAttack aff = new AffinityAttack();
        aff.numTargets = 4;
        aff.attackStrength = 90;
        aff.attackType = StaticData.WATR;
        aff.physical = false;

        Move[] ret = { aff };
        return ret;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
