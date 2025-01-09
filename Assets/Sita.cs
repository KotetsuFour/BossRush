using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sita : PlayerCharacter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Slash
    public override Move[] useMove1()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 50;
        att.attackType = StaticData.NORM;
        att.physical = true;

        Move[] ret = { att };
        return ret;
    }
    //Flame Slash
    public override Move[] useMove2()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 50;
        att.attackType = StaticData.FIRE;
        att.physical = false;

        Move[] ret = { att };
        return ret;
    }
    //Cleanse
    public override Move[] useMove3()
    {
        Effect eff = new Effect();
        eff.numTargets = 1;
        eff.effect = StaticData.NORM;

        Move[] ret = { eff };
        return ret;
    }
    //Pyroette
    public override Move[] useMove4()
    {
        AffinityAttack aff = new AffinityAttack();
        aff.numTargets = 4;
        aff.attackStrength = 100;
        aff.attackType = StaticData.FIRE;
        aff.physical = false;

        Effect eff = new Effect();
        eff.numTargets = 1;
        eff.effect = StaticData.FIRE;

        Recoil rec = new Recoil();
        rec.numTargets = 1;
        rec.attackStrength = 20;

        Move[] ret = { aff, eff, rec };
        return ret;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
