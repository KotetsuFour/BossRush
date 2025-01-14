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
    public override MovePackage useMove1()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 30;
        att.attackType = StaticData.WATR;
        att.physical = false;

        Move[] packEff = { att };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.WATR;
        ret.moveName = "Bubble";
        ret.numLeft = move1UsesLeft;
        ret.description = "A standard water spell.";

        return ret;
    }
    //Relieve
    public override MovePackage useMove2()
    {
        Heal hel = new Heal();
        hel.numTargets = 1;
        hel.amount = specAttack;

        Move[] packEff = { hel };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.WATR;
        ret.moveName = "Relieve";
        ret.numLeft = move2UsesLeft;
        ret.description = "Heals an ally.";

        return ret;
    }
    //Meditate
    public override MovePackage useMove3()
    {
        Heal hel = new Heal();
        hel.numTargets = 0;
        hel.amount = specAttack + 30;

        Move[] packEff = { hel };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.NORM;
        ret.moveName = "Meditate";
        ret.numLeft = move3UsesLeft;
        ret.description = "Heals Elioenai.";

        return ret;
    }
    //Whirlpool
    public override MovePackage useMove4()
    {
        AffinityAttack aff = new AffinityAttack();
        aff.numTargets = 4;
        aff.attackStrength = 90;
        aff.attackType = StaticData.WATR;
        aff.physical = false;

        Move[] packEff = { aff };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.WATR;
        ret.moveName = "Whirlpool";
        ret.numLeft = move4UsesLeft;
        ret.description = "Elioenai's ultimate water move.";

        return ret;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
