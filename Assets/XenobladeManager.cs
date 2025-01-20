using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class XenobladeManager : MonoBehaviour
{
    public static float DAMAGE_LINGER_TIME = 1f;

    [SerializeField] private PlayerCharacter sita;
    [SerializeField] private PlayerCharacter elio;
    [SerializeField] private PlayerCharacter xiao;
    [SerializeField] private PlayerCharacter izum;

    [SerializeField] private XenobladeEnemy drag;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceFromDragon;
    [SerializeField] private float cameraDistanceFromSita;

    [SerializeField] private Camera cam;
    [SerializeField] private Transform normalMenu;
    [SerializeField] private Transform spinMenu;
    [SerializeField] private Slider sitaHealth;
    [SerializeField] private Slider dragonHealth;
    [SerializeField] private Transform win;
    [SerializeField] private Transform lose;


    private float timer;
    private float sitaAngle;
    private SelectionMode selectionMode;
    private PlayerCharacter.MovePackage registeredMove;
    private int numOfRegisteredMove;
    [SerializeField] private float spinSpeed;
    [SerializeField] private float spinTime;

    [SerializeField] private List<string> soundKeys;
    [SerializeField] private List<AudioClip> soundValues;
    [SerializeField] private AudioPlayer audioPlayer;
    private Dictionary<string, AudioClip> soundDictionary;

    // Start is called before the first frame update
    void Awake()
    {
        soundDictionary = new Dictionary<string, AudioClip>();
        for (int q = 0; q < soundKeys.Count; q++)
        {
            soundDictionary.Add(soundKeys[q], soundValues[q]);
        }

        setSitaPosition();
        enableNormalMenu();
        updateHealth();
        selectionMode = SelectionMode.BATTLE;
    }

    private void setSitaPosition()
    {
        sita.transform.position = new Vector3(Mathf.Sin(sitaAngle), 0, Mathf.Cos(sitaAngle)) * distanceFromDragon;
        sita.transform.rotation = Quaternion.LookRotation(drag.transform.position - sita.transform.position);
        Vector3 direction = (drag.transform.position - sita.transform.position).normalized;
        cam.transform.SetPositionAndRotation(sita.transform.position + new Vector3(0, 1, 0) - (direction * cameraDistanceFromSita),
            Quaternion.LookRotation(drag.transform.position + new Vector3(0, 1.25f, 0) - sita.transform.position));
    }

    public void attack1()
    {
        PlayerCharacter.MovePackage move = sita.useMove1();
        numOfRegisteredMove = 1;
        executeMove(move);
    }
    public void attack2()
    {
        PlayerCharacter.MovePackage move = sita.useMove2();
        numOfRegisteredMove = 2;
        executeMove(move);
    }
    public void attack3()
    {
        PlayerCharacter.MovePackage move = sita.useMove3();
        numOfRegisteredMove = 3;
        executeMove(move);
    }
    public void attack4()
    {
        PlayerCharacter.MovePackage move = sita.useMove4();
        numOfRegisteredMove = 4;

        executeMove(move);
    }
    public void executeMove(PlayerCharacter.MovePackage move)
    {
        registeredMove = move;
        disableNormalMenu();

        if (registeredMove.attackParticles != null)
        {
            StaticData.findDeepChild(sita.transform, registeredMove.attackParticles)
                .GetComponent<ParticleEffect>().playTimed(registeredMove.animationTime);
        }
        if (registeredMove.attackSound != null)
        {
            AudioPlayer player = Instantiate(audioPlayer);
            player.playTimed(soundDictionary[registeredMove.attackSound], registeredMove.animationTime);
        }
        sita.GetComponent<Animator>().Play(registeredMove.animationToActivate);
        timer = registeredMove.animationTime;
        if (numOfRegisteredMove == 1)
        {
            sita.move1UsesLeft--;
        }
        else if (numOfRegisteredMove == 2)
        {
            sita.move2UsesLeft--;
        }
        else if (numOfRegisteredMove == 3)
        {
            sita.move3UsesLeft--;
        }
        else if (numOfRegisteredMove == 4)
        {
            sita.move4UsesLeft--;
        }
    }
    private void applyEffects()
    {
        foreach (GameEntity.Move movePart in registeredMove.moveEffects)
        {
            if (movePart.numTargets == 0)
            {
                movePart.execute(sita, new GameEntity[] { sita });
            }
            else if (movePart.numTargets == 1)
            {
                if (movePart is GameEntity.Attack)
                {
                    movePart.execute(sita, new GameEntity[] { drag });
                }
                else if (movePart is GameEntity.Effect)
                {
                    movePart.execute(sita, new GameEntity[] { sita });
                }
            }
            else if (movePart.numTargets == 4)
            {
                movePart.execute(sita, new GameEntity[] { drag });
            }
        }
        updateHealth();
    }
    private void applyEnemyEffects(XenobladeEnemy.MovePackage move)
    {
        bool hit = move.angleRange >= Quaternion.Angle(drag.transform.rotation,
                Quaternion.LookRotation(sita.transform.position - drag.transform.position));
        foreach (GameEntity.Move movePart in move.moveEffects)
        {
            if (movePart.numTargets == 0)
            {
                movePart.execute(drag, new GameEntity[] { drag });
            }
            else if ((movePart.numTargets == 1 || movePart.numTargets == 4)
                && hit)
            {
                movePart.execute(drag, new GameEntity[] { sita });
            }
        }
        if (hit)
        {
            if (move.damageParticles != null)
            {
                StaticData.findDeepChild(sita.transform, move.damageParticles)
                    .GetComponent<ParticleEffect>().playTimed(PokemonManager.DAMAGE_LINGER_TIME);
            }
            if (move.damageSound != null)
            {
                playTimedSound(move.damageSound, PokemonManager.DAMAGE_LINGER_TIME);
            }
        }
        updateHealth();
    }

    private void updateHealth()
    {
        StaticData.findDeepChild(sitaHealth.transform, "SitaName").GetComponent<TextMeshProUGUI>()
            .text = $"Sita ({sita.currentHP}/{sita.maxHP})";
        StaticData.findDeepChild(dragonHealth.transform, "DragonName").GetComponent<TextMeshProUGUI>()
            .text = $"Dheginsea ({drag.currentHP}/{drag.maxHP})";
        sitaHealth.value = (sita.currentHP + 0.0f) / sita.maxHP;
        dragonHealth.value = (drag.currentHP + 0.0f) / drag.maxHP;
    }

    private void enableNormalMenu()
    {
        normalMenu.gameObject.SetActive(true);
        StaticData.findDeepChild(normalMenu, "Amount1").GetComponent<TextMeshProUGUI>().text = sita.move1UsesLeft + "";
        StaticData.findDeepChild(normalMenu, "Amount2").GetComponent<TextMeshProUGUI>().text = sita.move2UsesLeft + "";
        StaticData.findDeepChild(normalMenu, "Amount3").GetComponent<TextMeshProUGUI>().text = sita.move3UsesLeft + "";
        StaticData.findDeepChild(normalMenu, "Amount4").GetComponent<TextMeshProUGUI>().text = sita.move4UsesLeft + "";

        StaticData.findDeepChild(normalMenu, "Attack1").GetComponent<Button>().interactable = sita.move1UsesLeft > 0;
        StaticData.findDeepChild(normalMenu, "Attack2").GetComponent<Button>().interactable = sita.move2UsesLeft > 0;
        StaticData.findDeepChild(normalMenu, "Attack3").GetComponent<Button>().interactable = sita.move3UsesLeft > 0;
        StaticData.findDeepChild(normalMenu, "Attack4").GetComponent<Button>().interactable = sita.move4UsesLeft > 0;
    }
    private void disableNormalMenu()
    {
        normalMenu.gameObject.SetActive(false);
    }

    public void playTimedSound(string soundName, float time)
    {
        Instantiate(audioPlayer).playTimed(soundDictionary[soundName], time);
    }

    private void stopBattle()
    {
        drag.playIdle();
        sita.GetComponent<Animator>().Play("Idle");
    }

    private void spin()
    {
        sita.transform.Rotate(new Vector3(0, spinSpeed * Time.deltaTime, 0));
        elio.transform.rotation = sita.transform.rotation;
        xiao.transform.rotation = sita.transform.rotation;
        izum.transform.rotation = sita.transform.rotation;
    }

    private void swap(PlayerCharacter old, PlayerCharacter replacement)
    {
        Vector3 temp = old.transform.position;
        old.transform.position = replacement.transform.position;
        replacement.transform.position = temp;
        StaticData.findDeepChild(replacement.transform, "NormalDamage")
            .GetComponent<ParticleEffect>().play();
    }
    private void  spinTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            StaticData.findDeepChild(spinMenu, "KeyBack").GetComponent<Slider>()
                .value = timer / spinTime;
        }
        else
        {
            timer = 3f;
            drag.takeDamage(-drag.maxHP / 2);
            updateHealth();
            drag.GetComponent<Animator>().Play("Scream");
            elio.transform.position = new Vector3(0, -10, 0);
            xiao.transform.position = new Vector3(0, -10, 0);
            izum.transform.position = new Vector3(0, -10, 0);
            setSitaPosition();
            spinMenu.gameObject.SetActive(false);
            StaticData.findDeepChild(spinMenu, "KeyS").gameObject.SetActive(false);
            StaticData.findDeepChild(spinMenu, "KeyP").gameObject.SetActive(false);
            StaticData.findDeepChild(spinMenu, "KeyI").gameObject.SetActive(false);
            StaticData.findDeepChild(spinMenu, "KeyN").gameObject.SetActive(false);

            selectionMode = SelectionMode.RESET;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (selectionMode == SelectionMode.BATTLE)
        {
            if (!sita.isAlive())
            {
                stopBattle();
                timer = sita.die();

                selectionMode = SelectionMode.DEATH;
            }
            else if (!drag.isAlive())
            {
                stopBattle();
                timer = spinTime;
                normalMenu.gameObject.SetActive(false);
                spinMenu.gameObject.SetActive(true);
                StaticData.findDeepChild(spinMenu, "KeyS").gameObject.SetActive(true);

                selectionMode = SelectionMode.SITA;
            }
            else if (registeredMove == null)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    sita.GetComponent<Animator>().Play("Left");
                    sitaAngle += Time.deltaTime * moveSpeed;
                    setSitaPosition();
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    sita.GetComponent<Animator>().Play("Right");
                    sitaAngle -= Time.deltaTime * moveSpeed;
                    setSitaPosition();
                }
                else
                {
                    sita.GetComponent<Animator>().Play("Idle");
                }
            }
            else if (timer <= 0)
            {
                setSitaPosition();
                applyEffects();
                enableNormalMenu();
                if (registeredMove.damageParticles != null)
                {
                    StaticData.findDeepChild(drag.transform, registeredMove.damageParticles)
                        .GetComponent<ParticleEffect>().playTimed(DAMAGE_LINGER_TIME);
                }
                if (registeredMove.damageSound != null)
                {
                    Instantiate(audioPlayer).playTimed(soundDictionary[registeredMove.damageSound], DAMAGE_LINGER_TIME);
                }
                registeredMove = null;
            }
            XenobladeEnemy.MovePackage move = drag.dragonAction();
            if (move != null)
            {
                applyEnemyEffects(move);
            }
        }
        else if (selectionMode == SelectionMode.SITA)
        {
            spin();
            spinTimer();

            if (Input.GetKeyDown(KeyCode.S))
            {
                swap(sita, elio);
                timer = spinTime;
                StaticData.findDeepChild(spinMenu, "KeyS").gameObject.SetActive(false);
                StaticData.findDeepChild(spinMenu, "KeyP").gameObject.SetActive(true);
                selectionMode = SelectionMode.ELIOENAI;
            }
        }
        else if (selectionMode == SelectionMode.ELIOENAI)
        {
            spin();
            spinTimer();

            if (Input.GetKeyDown(KeyCode.P))
            {
                swap(elio, xiao);
                timer = spinTime;
                StaticData.findDeepChild(spinMenu, "KeyP").gameObject.SetActive(false);
                StaticData.findDeepChild(spinMenu, "KeyI").gameObject.SetActive(true);
                selectionMode = SelectionMode.XIAOYU;
            }
        }
        else if (selectionMode == SelectionMode.XIAOYU)
        {
            spin();
            spinTimer();

            if (Input.GetKeyDown(KeyCode.I))
            {
                swap(xiao, izum);
                timer = spinTime;
                StaticData.findDeepChild(spinMenu, "KeyI").gameObject.SetActive(false);
                StaticData.findDeepChild(spinMenu, "KeyN").gameObject.SetActive(true);
                selectionMode = SelectionMode.IZUMI;
            }
        }
        else if (selectionMode == SelectionMode.IZUMI)
        {
            spin();
            spinTimer();

            if (Input.GetKeyDown(KeyCode.N))
            {
                swap(izum, sita);
                setSitaPosition();
                sita.GetComponent<Animator>().Play(sita.useMove4().animationToActivate);
                timer = sita.useMove4().animationTime;
                spinMenu.gameObject.SetActive(false);
                selectionMode = SelectionMode.FINAL_ATTACK_ANIMATION;
            }
        }
        else if (selectionMode == SelectionMode.FINAL_ATTACK_ANIMATION)
        {
            if (timer <= 0)
            {
                timer = 5f;
                drag.GetComponent<Animator>().Play("DeathSequence");

                selectionMode = SelectionMode.END;
            }
        }
        else if (selectionMode == SelectionMode.END)
        {
            if (timer <= 0)
            {
                win.gameObject.SetActive(true);
            }
        }
        else if (selectionMode == SelectionMode.DEATH)
        {
            if (timer <= 0)
            {
                lose.gameObject.SetActive(true);
            }
        }
        else if (selectionMode == SelectionMode.RESET)
        {
            if (timer <= 0)
            {
                enableNormalMenu();
                selectionMode = SelectionMode.BATTLE;
            }
        }
    }

    public void quit()
    {
        StaticData.quit();
    }

    public void retry()
    {
        StaticData.retry();
    }

    public enum SelectionMode
    {
        INTRO, BATTLE, SITA, ELIOENAI, XIAOYU, IZUMI, RESET, FINAL_ATTACK_ANIMATION, END, DEATH
    }
}
