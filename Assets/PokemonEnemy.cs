using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PokemonEnemy : GameEntity
{
    public string displayName;
    public class NPCMove
    {
        public string moveName;
        public Move[] moveEffects;
        public float animationTime;
        public string attackParticles;
        public string damageParticles;
        public string attackSound;
        public string damageSound;
    }

    public abstract NPCMove nextAttack(PlayerCharacter opponent);
}
