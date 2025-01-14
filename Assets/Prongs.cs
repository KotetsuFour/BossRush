using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prongs : PokemonEnemy
{
    public override NPCMove nextAttack(PlayerCharacter opponent)
    {
        if (opponent.type == StaticData.WIND)
        {
            Attack att = new Attack();
            att.numTargets = 1;
            att.attackStrength = 30;
            att.attackType = StaticData.WOOD;
            att.physical = false;

            Heal hel = new Heal();
            hel.numTargets = 0;
            hel.amount = specAttack + 30;

            NPCMove ret = new NPCMove();
            ret.moveName = "Leech";
            ret.moveEffects = new Move[] { att, hel };
            ret.animationTime = 1.5f;
            return ret;
        }
        else
        {
            Attack att = new Attack();
            att.numTargets = 1;
            att.attackStrength = 60;
            att.attackType = StaticData.NORM;
            att.physical = true;

            NPCMove ret = new NPCMove();
            ret.moveName = "Impale";
            ret.moveEffects = new Move[] { att };
            ret.animationTime = 1.5f;
            return ret;
        }
    }
}
