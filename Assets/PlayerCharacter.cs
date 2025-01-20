using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCharacter : GameEntity
{
    public string displayName;
    public string displayNameWithType;
    public int move1UsesLeft;
    public int move2UsesLeft;
    public int move3UsesLeft;
    public int move4UsesLeft;

    public abstract MovePackage useMove1();
    public abstract MovePackage useMove2();
    public abstract MovePackage useMove3();
    public abstract MovePackage useMove4();

    public float die()
    {
        GetComponent<Animator>().Play("Death");
        return 2.2f;
    }

    public class MovePackage
    {
        public string moveName;
        public int type;
        public Move[] moveEffects;
        public int numLeft;
        public string description;
        public float animationTime;
        public string animationToActivate;
        public string attackParticles;
        public string damageParticles;
        public string attackSound;
        public string damageSound;
    }
}
