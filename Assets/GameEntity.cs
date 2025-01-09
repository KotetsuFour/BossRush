using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEntity : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    public int physAttack;
    public int specAttack;
    public int physDefense;
    public int specDefense;
    public int type;
    public int activeEffect;

    public void takeDamage(int amount)
    {
        currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
    }

    public abstract class Move
    {
        public int numTargets;
        public abstract void execute(GameEntity user, GameEntity[] targets);
    }
    public class Attack : Move
    {
        public int attackStrength;
        public int attackType;
        public bool physical;
        public override void execute(GameEntity user, GameEntity[] targets)
        {
            foreach (GameEntity ge in targets)
            {
                float damage = Mathf.Max(0,
                    ((attackStrength + (physical ? user.physAttack : user.specAttack))
                    * StaticData.effectiveness(attackType, ge.type))
                     - (physical ? ge.physDefense : ge.specDefense));
                ge.takeDamage(Mathf.RoundToInt(damage));
           }
        }
    }
    public class Recoil : Move
    {
        public int attackStrength;
        public override void execute(GameEntity user, GameEntity[] targets)
        {
            user.takeDamage(attackStrength);
        }
    }
    public class Heal : Move
    {
        public int amount;
        public override void execute(GameEntity user, GameEntity[] targets)
        {
            foreach (GameEntity ge in targets)
            {
                ge.takeDamage(-amount);
            }
        }
    }
    public class Revive : Move
    {
        public override void execute(GameEntity user, GameEntity[] targets)
        {
            foreach (GameEntity ge in targets)
            {
                ge.currentHP = Mathf.Max(1, ge.maxHP / 2);
            }
        }
    }
    public class AffinityAttack : Move
    {
        public int attackStrength;
        public int attackType;
        public bool physical;
        public override void execute(GameEntity user, GameEntity[] targets)
        {
            foreach (GameEntity ge in targets)
            {
                if (ge.type == attackType)
                {
                    ge.takeDamage(-attackStrength);
                }
                else
                {
                    float damage = Mathf.Max(0,
                        ((attackStrength + (physical ? user.physAttack : user.specAttack))
                        * StaticData.effectiveness(attackType, ge.type))
                         - (physical ? ge.physDefense : ge.specDefense));
                    ge.takeDamage(Mathf.RoundToInt(damage));
                }
            }
        }
    }
    public class Effect : Move
    {
        public int effect;
        public override void execute(GameEntity user, GameEntity[] targets)
        {
            foreach (GameEntity ge in targets)
            {
                ge.activeEffect = effect;
            }
        }
    }
}
