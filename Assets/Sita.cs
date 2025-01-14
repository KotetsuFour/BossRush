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
    public override MovePackage useMove1()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 50;
        att.attackType = StaticData.NORM;
        att.physical = true;

        Move[] packEff = { att };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.NORM;
        ret.moveName = "Slash";
        ret.numLeft = move1UsesLeft;
        ret.description = "A standard sword slash.";

        return ret;
    }
    //Flame Slash
    public override MovePackage useMove2()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 50;
        att.attackType = StaticData.FIRE;
        att.physical = false;

        Move[] packEff = { att };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.FIRE;
        ret.moveName = "Flame Slash";
        ret.numLeft = move2UsesLeft;
        ret.description = "A sword slash with some extra heat.";

        return ret;
    }
    //Cleanse
    public override MovePackage useMove3()
    {
        Effect eff = new Effect();
        eff.numTargets = 1;
        eff.effect = StaticData.NORM;

        Move[] packEff = { eff };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.FIRE;
        ret.moveName = "Cleanse";
        ret.numLeft = move3UsesLeft;
        ret.description = "Removes a condition.";

        return ret;
    }
    //Pyroette
    public override MovePackage useMove4()
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
        rec.numTargets = 0;
        rec.attackStrength = 20;

        Move[] packEff = { aff, eff, rec };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.FIRE;
        ret.moveName = "Pyroette";
        ret.numLeft = move4UsesLeft;
        ret.description = "Sita's ultimate flame move.";

        return ret;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
