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
    public override MovePackage useMove1()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 30;
        att.attackType = StaticData.NORM;
        att.physical = false;

        Move[] packEff = { att };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.NORM;
        ret.moveName = "Quickshot";
        ret.numLeft = move1UsesLeft;
        ret.description = "A standard bow shot";
        ret.animationTime = 0.667f;
        ret.animationToActivate = "Attack1";
        ret.damageParticles = "NormalDamage";

        return ret;
    }
    //Life
    public override MovePackage useMove2()
    {
        Revive rev = new Revive();
        rev.numTargets = 1;

        Move[] packEff = { rev };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.WIND;
        ret.moveName = "Life";
        ret.numLeft = move2UsesLeft;
        ret.description = "Revive a fallen ally.";
        ret.animationTime = 2.367f;
        ret.animationToActivate = "Attack2";

        return ret;
    }
    //Barrage
    public override MovePackage useMove3()
    {
        Attack att = new Attack();
        att.numTargets = 4;
        att.attackStrength = 50;
        att.attackType = StaticData.WIND;
        att.physical = false;

        Move[] packEff = { att };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.WIND;
        ret.moveName = "Barrage";
        ret.numLeft = move3UsesLeft;
        ret.description = "Many arrows guided by wind.";
        ret.animationTime = 1.667f;
        ret.animationToActivate = "Attack3";
        ret.damageParticles = "WindDamage";

        return ret;
    }
    //Cyclone Arrow
    public override MovePackage useMove4()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 100;
        att.attackType = StaticData.WIND;
        att.physical = false;

        Move[] packEff = { att };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.WIND;
        ret.moveName = "Cyclone Arrow";
        ret.numLeft = move4UsesLeft;
        ret.description = "Xiaoyu's ultimate wind move.";
        ret.animationTime = 5f;
        ret.animationToActivate = "Attack4";
        ret.damageParticles = "WindDamage";

        return ret;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
