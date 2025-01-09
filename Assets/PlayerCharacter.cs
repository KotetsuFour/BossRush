using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCharacter : GameEntity
{
    public int move1UsesLeft;
    public int move2UsesLeft;
    public int move3UsesLeft;
    public int move4UsesLeft;

    public abstract Move[] useMove1();
    public abstract Move[] useMove2();
    public abstract Move[] useMove3();
    public abstract Move[] useMove4();
}
