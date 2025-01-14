using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foggy : PokemonEnemy
{
    public override NPCMove nextAttack(PlayerCharacter opponent)
    {
        if (opponent.type == StaticData.WATR)
        {
            Attack att = new Attack();
            att.numTargets = 1;
            att.attackStrength = 40;
            att.attackType = StaticData.WIND;
            att.physical = false;

            NPCMove ret = new NPCMove();
            ret.moveName = "Wind Slash";
            ret.moveEffects = new Move[] { att };
            ret.animationTime = 1.5f;
            return ret;
        }
        else
        {
            Attack att = new Attack();
            att.numTargets = 1;
            att.attackStrength = 30;
            att.attackType = StaticData.NORM;
            att.physical = true;

            NPCMove ret = new NPCMove();
            ret.moveName = "Scratch";
            ret.moveEffects = new Move[] { att };
            ret.animationTime = 1.5f;
            return ret;
        }
    }
}
