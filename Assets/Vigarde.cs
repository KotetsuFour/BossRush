using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vigarde : PokemonEnemy
{
    public override NPCMove nextAttack(PlayerCharacter opponent)
    {
        if (opponent.type == StaticData.FIRE)
        {
            Attack att = new Attack();
            att.numTargets = 1;
            att.attackStrength = 30;
            att.attackType = StaticData.WATR;
            att.physical = false;

            NPCMove ret = new NPCMove();
            ret.moveName = "Splash";
            ret.moveEffects = new Move[] { att };
            ret.animationTime = 1.5f;
            return ret;
        }
        else if (currentHP <= maxHP / 2)
        {
            Heal hel = new Heal();
            hel.numTargets = 0;
            hel.amount = 30;

            NPCMove ret = new NPCMove();
            ret.moveName = "Heal";
            ret.moveEffects = new Move[] { hel };
            ret.animationTime = 1.5f;
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
            ret.moveName = "Sled";
            ret.moveEffects = new Move[] { att };
            ret.animationTime = 1.5f;
            return ret;
        }
    }
}
