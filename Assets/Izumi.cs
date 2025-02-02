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
    public override MovePackage useMove1()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 40;
        att.attackType = StaticData.NORM;
        att.physical = true;

        Move[] packEff = { att };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.WOOD;
        ret.moveName = "Flurry";
        ret.numLeft = move1UsesLeft;
        ret.description = "A barrage of punches.";
        ret.animationTime = 2.967f;
        ret.animationToActivate = "Attack1";
        ret.damageParticles = "NormalDamage";

        return ret;
    }
    //Block
    public override MovePackage useMove2()
    {
        Effect eff = new Effect();
        eff.numTargets = 0;
        eff.effect = StaticData.WOOD;

        Move[] packEff = { eff };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.WOOD;
        ret.moveName = "Block";
        ret.numLeft = move2UsesLeft;
        ret.description = "Negate an attack.";
        ret.animationTime = 2.5f;
        ret.animationToActivate = "Attack2";

        return ret;
    }
    //Wooden Leg
    public override MovePackage useMove3()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 50;
        att.attackType = StaticData.WOOD;
        att.physical = true;

        Move[] packEff = { att };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.WOOD;
        ret.moveName = "Wooden Leg";
        ret.numLeft = move3UsesLeft;
        ret.description = "A powerful kick.";
        ret.animationTime = 2.5f;
        ret.animationToActivate = "Attack3";
        ret.damageParticles = "WoodDamage";

        return ret;
    }
    //Spin Kick
    public override MovePackage useMove4()
    {
        Attack att = new Attack();
        att.numTargets = 1;
        att.attackStrength = 150;
        att.attackType = StaticData.WOOD;
        att.physical = true;

        Move[] packEff = { att };

        MovePackage ret = new MovePackage();
        ret.moveEffects = packEff;
        ret.type = StaticData.WOOD;
        ret.moveName = "Spin Kick";
        ret.numLeft = move4UsesLeft;
        ret.description = "Izumi's ultimate wood move.";
        ret.animationTime = 1.833f;
        ret.animationToActivate = "Attack4";
        ret.damageParticles = "WoodDamage";

        return ret;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
