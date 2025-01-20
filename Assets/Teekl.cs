using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teekl : PokemonEnemy
{
    public override NPCMove nextAttack(PlayerCharacter opponent)
    {
        if (opponent.type == StaticData.WOOD)
        {
            Attack att = new Attack();
            att.numTargets = 1;
            att.attackStrength = 50;
            att.attackType = StaticData.FIRE;
            att.physical = false;

            NPCMove ret = new NPCMove();
            ret.moveName = "Fire Fang";
            ret.moveEffects = new Move[] { att };
            ret.animationTime = 1.5f;
            ret.damageParticles = "FireDamage";
            return ret;
        }
        else
        {
            Attack att = new Attack();
            att.numTargets = 1;
            att.attackStrength = 40;
            att.attackType = StaticData.NORM;
            att.physical = true;

            NPCMove ret = new NPCMove();
            ret.moveName = "Maul";
            ret.moveEffects = new Move[] { att };
            ret.animationTime = 1.5f;
            ret.damageParticles = "NormalDamage";
            return ret;
        }
    }
}
