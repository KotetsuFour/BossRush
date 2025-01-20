using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XenobladeEnemy : GameEntity
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotateSpeed;
    private Quaternion rotationToTarget;
    private float timer;
    private SelectionMode selectionMode;
    private MovePackage currentAttack;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().Play("Scream");
        timer = 3f;
    }

    public MovePackage dragonAction()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (selectionMode == SelectionMode.START) {
            if (timer <= 0)
            {
                playIdle();
            }
        }
        else if (selectionMode == SelectionMode.IDLE)
        {
            if (timer <= 0)
            {
                setRotate();
            }
        }
        else if (selectionMode == SelectionMode.ROTATE)
        {
            float distance = Quaternion.Angle(transform.rotation, rotationToTarget);
            if (distance <= 5)
            {
                setAttack();
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, rotationToTarget, rotateSpeed * Time.deltaTime);
            }
        }
        else if (selectionMode == SelectionMode.ATTACK)
        {
            if (timer <= 0)
            {
                playIdle();
                return currentAttack;
            }
        }

        return null;
    }

    public void playIdle()
    {
        timer = Random.Range(3, 7);
        GetComponent<Animator>().Play("Idle01");

        selectionMode = SelectionMode.IDLE;
    }

    private void setAttack()
    {
        MovePackage att = new MovePackage();
        int rand = Random.Range(0, 100);
        if (rand < 40)
        {
            //Basic Attack
            Attack hit = new Attack();
            hit.attackStrength = 40;
            hit.attackType = StaticData.NORM;
            hit.numTargets = 1;
            hit.physical = true;

            Move[] move = { hit };
            att.moveEffects = move;
            att.angleRange = 20;
            att.animationToPlay = "Basic Attack";
            att.damageParticles = "NormalDamage";
            att.animationTime = 1f;
            //att.attackSound = ???
        }
        else if (rand < 70)
        {
            //Claw Attack
            Attack hit = new Attack();
            hit.attackStrength = 60;
            hit.attackType = StaticData.NORM;
            hit.numTargets = 1;
            hit.physical = true;

            Move[] move = { hit };
            att.moveEffects = move;
            att.angleRange = 30;
            att.animationToPlay = "Claw Attack";
            att.damageParticles = "NormalDamage";
            att.animationTime = 1.333f;
            //att.attackSound = ???
        }
        else if (rand < 90)
        {
            //Flame Attack
            Attack hit = new Attack();
            hit.attackStrength = 40;
            hit.attackType = StaticData.FIRE;
            hit.numTargets = 1;
            hit.physical = false;

            Move[] move = { hit };
            att.moveEffects = move;
            att.angleRange = 40;
            att.animationToPlay = "Flame Attack";
            att.attackParticles = "FireBreath";
            att.damageParticles = "FireDamage";
            att.animationTime = 2.333f;
            //att.attackSound = ???
        }
        else
        {
            Heal hel = new Heal();
            hel.numTargets = 0;
            hel.amount = 70;

            Move[] move = { hel };
            att.moveEffects = move;
            att.animationToPlay = "Scream";
            att.attackParticles = "WindDamage";
            att.animationTime = 3f;
            //att.attackSound = ???
        }

        if (att.attackParticles != null)
        {
            Debug.Log(att.attackParticles);
            StaticData.findDeepChild(transform, att.attackParticles).GetComponent<ParticleEffect>()
                .playTimed(att.animationTime);
        }
        if (att.attackSound != null)
        {
            GameObject.Find("XenobladeManager").GetComponent<XenobladeManager>()
                .playTimedSound(att.attackSound, att.animationTime);
        }
        currentAttack = att;
        timer = att.animationTime;
        GetComponent<Animator>().Play(att.animationToPlay);

        selectionMode = SelectionMode.ATTACK;
    }

    private void setRotate()
    {
        rotationToTarget = Quaternion.LookRotation(target.position - transform.position);
        GetComponent<Animator>().Play("Run");

        selectionMode = SelectionMode.ROTATE;
    }

    public class MovePackage
    {
        public Move[] moveEffects;
        public float angleRange;
        public string animationToPlay;
        public string attackParticles;
        public string damageParticles;
        public float animationTime;
        public string attackSound;
        public string damageSound;
    }

    public enum SelectionMode
    {
        START, IDLE, ROTATE, ATTACK, 
    }
}
