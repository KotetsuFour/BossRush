using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokemonManager : MonoBehaviour
{
    public static float SPIN_TIME = 3f;

    [SerializeField] private Sita sita;
    [SerializeField] private Elioenai elio;
    [SerializeField] private Xiaoyu xiao;
    [SerializeField] private Izumi izum;

    [SerializeField] private Vigarde viga;
    [SerializeField] private Foggy fogg;
    [SerializeField] private Teekl teek;
    [SerializeField] private Prongs pron;

    [SerializeField] private Transform sitaPos;
    [SerializeField] private Transform elioPos;
    [SerializeField] private Transform xiaoPos;
    [SerializeField] private Transform izumPos;
    [SerializeField] private Transform playerWheel;
    [SerializeField] private float spinSpeed;
    [SerializeField] private float enemyTransitionSpeed;
    [SerializeField] private float hpChangeSpeed;

    [SerializeField] private Transform enemyPos;
    [SerializeField] private Transform enemyStart;

    [SerializeField] private Color normColor;
    [SerializeField] private Color fireColor;
    [SerializeField] private Color watrColor;
    [SerializeField] private Color windColor;
    [SerializeField] private Color woodColor;

    [SerializeField] private Transform menu;

    private PlayerCharacter currentPlayer;
    private PokemonEnemy currentEnemy;
    private float timer;
    private float targetOrientation;
    private int playerTempHP;
    private int enemyTempHP;
    private float playerHPDifference;
    private float enemyHPDifference;

    private PlayerCharacter.MovePackage registeredMove;
    private PokemonEnemy.NPCMove enemyMove;
    private int numOfRegisteredMove;

    private SelectionMode selectionMode;
    private bool playerTurn;

    [SerializeField] private List<string> soundKeys;
    [SerializeField] private List<AudioClip> soundValues;
    [SerializeField] private AudioPlayer audioPlayer;
    private Dictionary<string, AudioClip> soundDictionary;

    // Start is called before the first frame update
    void Start()
    {
        soundDictionary = new Dictionary<string, AudioClip>();
        for (int q = 0; q < soundKeys.Count; q++)
        {
            soundDictionary.Add(soundKeys[q], soundValues[q]);
        }

        playerTurn = true;
        spinToSita();
        timer = 5;
        selectionMode = SelectionMode.INITIAL_SPIN;
    }

    private void setPlayerPositions()
    {
        sita.transform.SetPositionAndRotation(sitaPos.position, Quaternion.LookRotation(enemyPos.position - sita.transform.position));
        elio.transform.SetPositionAndRotation(elioPos.position, Quaternion.LookRotation(enemyPos.position - elio.transform.position));
        xiao.transform.SetPositionAndRotation(xiaoPos.position, Quaternion.LookRotation(enemyPos.position - xiao.transform.position));
        izum.transform.SetPositionAndRotation(izumPos.position, Quaternion.LookRotation(enemyPos.position - izum.transform.position));
    }

    public void spin()
    {
        disableActions();
        StaticData.findDeepChild(menu, "ToSita").GetComponent<Button>().interactable = sita.isAlive();
        StaticData.findDeepChild(menu, "ToElioenai").GetComponent<Button>().interactable = elio.isAlive();
        StaticData.findDeepChild(menu, "ToXiaoyu").GetComponent<Button>().interactable = xiao.isAlive();
        StaticData.findDeepChild(menu, "ToIzumi").GetComponent<Button>().interactable = izum.isAlive();
        StaticData.findDeepChild(menu, "SpinOptionsPanel").gameObject.SetActive(true);
    }
    public void spinToSita()
    {
        spinToCharacter(sita, 0);
    }
    public void spinToElio()
    {
        spinToCharacter(elio, 90);
    }
    public void spinToXiao()
    {
        spinToCharacter(xiao, 180);
    }
    public void spinToIzum()
    {
        spinToCharacter(izum, 270);
    }
    private void spinToCharacter(PlayerCharacter player, float orientation)
    {
        playerTurn = currentEnemy == null || !currentEnemy.isAlive();
        disableActions();
        StaticData.findDeepChild(menu, "NextPanel").gameObject.SetActive(false);
        if (player == currentPlayer)
        {
            StaticData.findDeepChild(menu, "SpinOptionsPanel").gameObject.SetActive(false);
            if (currentEnemy == null || !currentEnemy.isAlive())
            {
                setEnemy();
            }
            else
            {
                enableActions();
            }
            return;
        }
        timer = SPIN_TIME;
        currentPlayer = player;
        targetOrientation = orientation;
        StaticData.findDeepChild(menu, "PlayerName").GetComponent<TextMeshProUGUI>().text
            = currentPlayer.displayNameWithType;
        StaticData.findDeepChild(menu, "PlayerHPBar").GetComponent<Slider>().value
             = (currentPlayer.currentHP + 0.0f) / currentPlayer.maxHP;
        StaticData.findDeepChild(menu, "PlayerHP").GetComponent<TextMeshProUGUI>().text
            = $"{currentPlayer.currentHP}/{currentPlayer.maxHP}";

        PlayerCharacter.MovePackage att1 = currentPlayer.useMove1();
        PlayerCharacter.MovePackage att2 = currentPlayer.useMove2();
        PlayerCharacter.MovePackage att3 = currentPlayer.useMove3();
        PlayerCharacter.MovePackage att4 = currentPlayer.useMove4();
        StaticData.findDeepChild(menu.transform, "Attack1").GetComponent<Image>()
            .color = StaticData.colorByType[att1.type];
        StaticData.findDeepChild(menu.transform, "Attack2").GetComponent<Image>()
            .color = StaticData.colorByType[att2.type];
        StaticData.findDeepChild(menu.transform, "Attack3").GetComponent<Image>()
            .color = StaticData.colorByType[att3.type];
        StaticData.findDeepChild(menu.transform, "Attack4").GetComponent<Image>()
            .color = StaticData.colorByType[att4.type];
        StaticData.findDeepChild(menu.transform, "Attack1Name").GetComponent<TextMeshProUGUI>()
            .text = att1.moveName;
        StaticData.findDeepChild(menu.transform, "Attack2Name").GetComponent<TextMeshProUGUI>()
            .text = att2.moveName;
        StaticData.findDeepChild(menu.transform, "Attack3Name").GetComponent<TextMeshProUGUI>()
            .text = att3.moveName;
        StaticData.findDeepChild(menu.transform, "Attack4Name").GetComponent<TextMeshProUGUI>()
            .text = att4.moveName;

        selectionMode = SelectionMode.SPIN;
    }
    private PokemonEnemy nextEnemy()
    {
        return viga.isAlive() ? viga : fogg.isAlive() ? fogg : teek.isAlive() ? teek : pron.isAlive() ? pron : null;
    }
    private void setEnemy()
    {
        if (currentEnemy != null)
        {
            Destroy(currentEnemy.gameObject);
        }
        PokemonEnemy enem = nextEnemy();
        if (enem == null)
        {
            //TODO end this battle
        }

        enem.transform.position = enemyStart.position;
        currentEnemy = enem;

        StaticData.findDeepChild(menu, "EnemyName").GetComponent<TextMeshProUGUI>().text
            = currentEnemy.displayName;
        StaticData.findDeepChild(menu, "EnemyHPBar").GetComponent<Slider>().value
             = (currentEnemy.currentHP + 0.0f) / currentEnemy.maxHP;
        StaticData.findDeepChild(menu, "EnemyHP").GetComponent<TextMeshProUGUI>().text
            = $"{currentEnemy.currentHP}/{currentEnemy.maxHP}";

        selectionMode = SelectionMode.ENEMY_RISE;
    }
    private void enableActions()
    {
        if (currentPlayer.isAlive())
        {
            StaticData.findDeepChild(menu, "AttacksPanel").gameObject.SetActive(true);
        }
        StaticData.findDeepChild(menu, "Attack1").GetComponent<Button>().interactable
            = currentPlayer.move1UsesLeft > 0;
        StaticData.findDeepChild(menu, "Attack2").GetComponent<Button>().interactable
            = currentPlayer.move2UsesLeft > 0;
        StaticData.findDeepChild(menu, "Attack3").GetComponent<Button>().interactable
            = currentPlayer.move3UsesLeft > 0;
        StaticData.findDeepChild(menu, "Attack4").GetComponent<Button>().interactable
            = currentPlayer.move4UsesLeft > 0;

        StaticData.findDeepChild(menu, "SpinButton").GetComponent<Button>().interactable = true;
    }
    private void disableActions()
    {
        StaticData.findDeepChild(menu, "SpinOptionsPanel").gameObject.SetActive(false);
        StaticData.findDeepChild(menu, "SpinButton").GetComponent<Button>().interactable = false;
        StaticData.findDeepChild(menu, "AttacksPanel").gameObject.SetActive(false);
    }
    private void killPlayer()
    {
        //currentPlayer.GetComponent<Animator>();
        timer = 2;
        selectionMode = SelectionMode.PLAYER_FALL;
    }
    public void attack1()
    {
        PlayerCharacter.MovePackage move = currentPlayer.useMove1();
        numOfRegisteredMove = 1;
        executeMove(move);
    }
    public void attack2()
    {
        PlayerCharacter.MovePackage move = currentPlayer.useMove2();
        numOfRegisteredMove = 2;
        executeMove(move);
    }
    public void attack3()
    {
        PlayerCharacter.MovePackage move = currentPlayer.useMove3();
        numOfRegisteredMove = 3;
        executeMove(move);
    }
    public void attack4()
    {
        PlayerCharacter.MovePackage move = currentPlayer.useMove4();
        numOfRegisteredMove = 4;
        executeMove(move);
    }
    private void executeMove(PlayerCharacter.MovePackage move)
    {
        registeredMove = move;

        string usesLeftString = "";
        if (numOfRegisteredMove == 1)
        {
            usesLeftString = $"({currentPlayer.move1UsesLeft} remaining)";
        }
        else if (numOfRegisteredMove == 2)
        {
            usesLeftString = $"({currentPlayer.move2UsesLeft} remaining)";
        }
        else if (numOfRegisteredMove == 3)
        {
            usesLeftString = $"({currentPlayer.move3UsesLeft} remaining)";
        }
        else if (numOfRegisteredMove == 4)
        {
            usesLeftString = $"({currentPlayer.move4UsesLeft} remaining)";
        }
        StaticData.findDeepChild(menu, "AttacksPanel").gameObject.SetActive(false);
        StaticData.findDeepChild(menu, "ChoicePanel").gameObject.SetActive(true);
        StaticData.findDeepChild(menu, "ChoicePanel").GetComponent<Image>().color
            = StaticData.colorByType[move.type];
        StaticData.findDeepChild(menu, "ChoiceNote").GetComponent<TextMeshProUGUI>().text
            = $"{move.description} {usesLeftString}";

        disableActions();

        selectionMode = SelectionMode.READY_ATTACK;
    }
    public void confirm()
    {
        playerTurn = false;
        if (registeredMove.attackParticles != null)
        {
            StaticData.findDeepChild(currentPlayer.transform, registeredMove.attackParticles)
                .GetComponent<ParticleEffect>().playTimed(registeredMove.animationTime);
        }
        if (registeredMove.attackSound != null)
        {
            AudioPlayer player = Instantiate(audioPlayer);
            player.playTimed(soundDictionary[registeredMove.attackSound], registeredMove.animationTime);
        }
        currentPlayer.GetComponent<Animator>().Play(registeredMove.animationToActivate);
        timer = registeredMove.animationTime;
        if (numOfRegisteredMove == 1)
        {
            currentPlayer.move1UsesLeft--;
        }
        else if (numOfRegisteredMove == 2)
        {
            currentPlayer.move2UsesLeft--;
        }
        else if (numOfRegisteredMove == 3)
        {
            currentPlayer.move3UsesLeft--;
        }
        else if (numOfRegisteredMove == 4)
        {
            currentPlayer.move4UsesLeft--;
        }

        StaticData.findDeepChild(menu, "ChoicePanel").gameObject.SetActive(false);

        selectionMode = SelectionMode.PLAYER_ATTACK_ANIMATION;
    }
    public void cancel()
    {
        StaticData.findDeepChild(menu, "ChoicePanel").gameObject.SetActive(false);
        enableActions();
    }
    private void startEnemyTurn()
    {
        playerTurn = true;
        enemyMove = currentEnemy.nextAttack(currentPlayer);
        if (enemyMove.attackParticles != null)
        {
            StaticData.findDeepChild(currentEnemy.transform, enemyMove.attackParticles)
                .GetComponent<ParticleEffect>().playTimed(enemyMove.animationTime);
        }
        if (enemyMove.attackSound != null)
        {
            AudioPlayer player = Instantiate(audioPlayer);
            player.playTimed(soundDictionary[enemyMove.attackSound], enemyMove.animationTime);
        }
        timer = enemyMove.animationTime;

        selectionMode = SelectionMode.ENEMY_MOVE;
    }
    private void applyEffects()
    {
        playerTempHP = currentPlayer.currentHP;
        enemyTempHP = currentEnemy.currentHP;

        foreach (GameEntity.Move movePart in registeredMove.moveEffects)
        {
            if (movePart.numTargets == 0)
            {
                movePart.execute(currentPlayer, new GameEntity[] { currentPlayer });
            }
            else if (movePart.numTargets == 1)
            {
                if (movePart is GameEntity.Attack)
                {
                    movePart.execute(currentPlayer, new GameEntity[] { currentEnemy });
                }
                else if (movePart is GameEntity.Heal)
                {
                    PlayerCharacter target = null;
                    if (sita != currentPlayer
                        && (target == null ||
                        (sita.isAlive() && sita.maxHP - sita.currentHP > currentPlayer.maxHP - currentPlayer.currentHP)))
                    {
                        target = sita;
                    }
                    if (elio != currentPlayer
                        && (target == null ||
                        (elio.isAlive() && elio.maxHP - elio.currentHP > currentPlayer.maxHP - currentPlayer.currentHP)))
                    {
                        target = elio;
                    }
                    if (xiao != currentPlayer
                        && (target == null ||
                        (xiao.isAlive() && xiao.maxHP - xiao.currentHP > currentPlayer.maxHP - currentPlayer.currentHP)))
                    {
                        target = xiao;
                    }
                    if (izum != currentPlayer
                        && (target == null ||
                        (izum.isAlive() && izum.maxHP - izum.currentHP > currentPlayer.maxHP - currentPlayer.currentHP)))
                    {
                        target = izum;
                    }
                    if (target != null)
                    {
                        movePart.execute(currentPlayer, new GameEntity[] { target });
                    }
                }
                else if (movePart is GameEntity.Revive)
                {
                    PlayerCharacter target = null;
                    if (sita != currentPlayer
                        && (target == null || !sita.isAlive()))
                    {
                        target = sita;
                    }
                    if (elio != currentPlayer
                        && (target == null || !elio.isAlive()))
                    {
                        target = elio;
                    }
                    if (xiao != currentPlayer
                        && (target == null || !xiao.isAlive()))
                    {
                        target = xiao;
                    }
                    if (izum != currentPlayer
                        && (target == null || !izum.isAlive()))
                    {
                        target = izum;
                    }
                    if (target != null)
                    {
                        movePart.execute(currentPlayer, new GameEntity[] { target });
                    }
                }
            }
            else if (movePart.numTargets == 4)
            {
                movePart.execute(currentPlayer, new GameEntity[] { currentEnemy });
            }
        }
    }
    private void applyEnemyEffects()
    {
        playerTempHP = currentPlayer.currentHP;
        enemyTempHP = currentEnemy.currentHP;

        foreach (GameEntity.Move movePart in enemyMove.moveEffects)
        {
            if (movePart.numTargets == 0)
            {
                movePart.execute(currentEnemy, new GameEntity[] { currentEnemy });
            }
            else if (movePart.numTargets == 1 || movePart.numTargets == 4)
            {
                movePart.execute(currentEnemy, new GameEntity[] { currentPlayer });
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (selectionMode == SelectionMode.ENEMY_RISE)
        {
            if ((currentEnemy.transform.position - enemyPos.position).magnitude <= 0.1)
            {
                currentEnemy.transform.position = enemyPos.position;
                enableActions();
                selectionMode = SelectionMode.STANDBY;
            }
            else
            {
                currentEnemy.transform.Translate(0, Time.deltaTime * enemyTransitionSpeed, 0);
            }
        }
        else if (selectionMode == SelectionMode.ENEMY_FALL)
        {
            if ((currentEnemy.transform.position - enemyStart.position).magnitude <= 0.1)
            {
                Destroy(currentEnemy.gameObject);
                PokemonEnemy next = nextEnemy();
                if (next != null)
                {
                    StaticData.findDeepChild(menu, "NextPanel").gameObject.SetActive(true);
                    StaticData.findDeepChild(menu, "NextNote").GetComponent<TextMeshProUGUI>()
                        .text = $"The demon's next familiar is {next.displayName}.";
                }
                if (!currentPlayer.isAlive())
                {
                    killPlayer();
                }
                else
                {
                    spin();
                }
                selectionMode = SelectionMode.STANDBY;
            }
            else
            {
                currentEnemy.transform.Translate(0, Time.deltaTime * -enemyTransitionSpeed, 0);
            }
        }
        else if (selectionMode == SelectionMode.PLAYER_FALL)
        {
            if (timer <= 0)
            {
                if (sita.isAlive() || elio.isAlive() || xiao.isAlive() || izum.isAlive())
                {
                    spin();
                }
                else
                {
                    //TODO fail
                }
            }
        }
        else if (selectionMode == SelectionMode.HPCHANGE)
        {
            if (playerTempHP == currentPlayer.currentHP
                && enemyTempHP == currentEnemy.currentHP)
            {
                if (currentEnemy.currentHP == 0)
                {
                    selectionMode = SelectionMode.ENEMY_FALL;
                }
                else if (currentPlayer.currentHP == 0)
                {
                    killPlayer();
                }
                else if (playerTurn)
                {
                    enableActions();
                    selectionMode = SelectionMode.STANDBY;
                }
                else
                {
                    startEnemyTurn();
                }
            }
            else
            {
                if (playerTempHP != currentPlayer.currentHP)
                {
                    playerHPDifference += Time.deltaTime * hpChangeSpeed;
                    if (playerHPDifference >= 1f)
                    {
                        int direction = playerTempHP < currentPlayer.currentHP ? 1 : -1;
                        playerHPDifference -= 1f;
                        playerTempHP += direction;
                        StaticData.findDeepChild(menu, "PlayerHPBar").GetComponent<Slider>().value
                            = (playerTempHP + 0.0f) / currentPlayer.maxHP;
                        StaticData.findDeepChild(menu, "PlayerHP").GetComponent<TextMeshProUGUI>().text
                            = $"{playerTempHP}/{currentEnemy.maxHP}";

                    }
                }
                if (enemyTempHP != currentEnemy.currentHP)
                {
                    enemyHPDifference += Time.deltaTime * hpChangeSpeed;
                    if (enemyHPDifference >= 1f)
                    {
                        int direction = enemyTempHP < currentEnemy.currentHP ? 1 : -1;
                        enemyHPDifference -= 1f;
                        enemyTempHP += direction;
                        StaticData.findDeepChild(menu, "EnemyHPBar").GetComponent<Slider>().value
                            = (enemyTempHP + 0.0f) / currentEnemy.maxHP;
                        StaticData.findDeepChild(menu, "EnemyHP").GetComponent<TextMeshProUGUI>().text
                            = $"{enemyTempHP}/{currentEnemy.maxHP}";
                    }
                }
            }
        }
        else if (selectionMode == SelectionMode.SPIN || selectionMode == SelectionMode.INITIAL_SPIN)
        {
            if (timer > 0 || Mathf.Abs(playerWheel.rotation.eulerAngles.y - targetOrientation) > 5f)
            {
                playerWheel.Rotate(new Vector3(0, Time.deltaTime * spinSpeed, 0));
                setPlayerPositions();
            }
            else if (selectionMode == SelectionMode.SPIN)
            {
                if (!currentEnemy.isAlive())
                {
                    setEnemy();
                }
                else if (playerTurn)
                {
                    enableActions();
                }
                else
                {
                    startEnemyTurn();
                }
            }
            else
            {
                setEnemy();
            }
        }
        else if (selectionMode == SelectionMode.PLAYER_ATTACK_ANIMATION)
        {
            if (timer <= 0)
            {
                if (registeredMove.damageParticles != null)
                {
                    StaticData.findDeepChild(currentEnemy.transform, registeredMove.damageParticles)
                        .GetComponent<ParticleEffect>().playTimed(1.5f);
                }
                if (registeredMove.damageSound != null)
                {
                    AudioPlayer player = Instantiate(audioPlayer);
                    player.playTimed(soundDictionary[registeredMove.damageSound], 1.5f);
                }
                timer = 1.5f;
                applyEffects();

                selectionMode = SelectionMode.ENEMY_DAMAGE_ANIMATION;
            }
        }
        else if (selectionMode == SelectionMode.ENEMY_DAMAGE_ANIMATION)
        {
            if (timer <= 0)
            {
                selectionMode = SelectionMode.HPCHANGE;
            }
        }
        else if (selectionMode == SelectionMode.ENEMY_MOVE)
        {
            if (timer <= 0)
            {
                if (enemyMove.damageParticles != null)
                {
                    StaticData.findDeepChild(currentPlayer.transform, enemyMove.damageParticles)
                        .GetComponent<ParticleEffect>().playTimed(1.5f);
                }
                if (enemyMove.damageSound != null)
                {
                    AudioPlayer player = Instantiate(audioPlayer);
                    player.playTimed(soundDictionary[enemyMove.damageSound], 1.5f);
                }
                timer = 1.5f;
                applyEnemyEffects();

                selectionMode = SelectionMode.PLAYER_DAMAGE_ANIMATION;
            }
        }
        else if (selectionMode == SelectionMode.PLAYER_DAMAGE_ANIMATION)
        {
            selectionMode = SelectionMode.HPCHANGE;
        }
    }

    public enum SelectionMode
    {
        INITIAL_SPIN, STANDBY, ENEMY_RISE, ENEMY_FALL, HPCHANGE, SPIN, PLAYER_FALL, READY_ATTACK,
        PLAYER_ATTACK_ANIMATION, ENEMY_DAMAGE_ANIMATION, ENEMY_MOVE, PLAYER_DAMAGE_ANIMATION
    }
}
