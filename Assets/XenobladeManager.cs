using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XenobladeManager : MonoBehaviour
{
    [SerializeField] private PlayerCharacter sita;
    [SerializeField] private PlayerCharacter elio;
    [SerializeField] private PlayerCharacter xiao;
    [SerializeField] private PlayerCharacter izum;

    [SerializeField] private XenobladeEnemy drag;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceFromDragon;
    [SerializeField] private float cameraDistanceFromSita;

    [SerializeField] private Camera cam;

    private float timer;
    private float sitaAngle;
    private SelectionMode selectionMode;
    private PlayerCharacter.MovePackage registeredMove;

    // Start is called before the first frame update
    void Start()
    {
        setSitaPosition();
        selectionMode = SelectionMode.BATTLE;
    }

    private void setSitaPosition()
    {
        sita.transform.SetPositionAndRotation(new Vector3(Mathf.Sin(sitaAngle), 0, Mathf.Cos(sitaAngle)) * distanceFromDragon,
            Quaternion.LookRotation(drag.transform.position - sita.transform.position));
        Vector3 direction = (drag.transform.position - sita.transform.position).normalized;
        cam.transform.SetPositionAndRotation(sita.transform.position + new Vector3(0, 1, 0) - (direction * cameraDistanceFromSita),
            Quaternion.LookRotation(drag.transform.position + new Vector3(0, 1.25f, 0) - sita.transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            timer -= Time.deltaTime;
        }
        if (!sita.isAlive())
        {

        }
        else if (!drag.isAlive())
        {

        }
        if (selectionMode == SelectionMode.BATTLE)
        {
            if (registeredMove == null)
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
                registeredMove = null;
                setSitaPosition();
                //Apply move
            }
        }
    }

    public enum SelectionMode
    {
        INTRO, BATTLE, SITA, ELIOENAI, XIAOYU, IZUMI, RESET, FINAL_ATTACK_ANIMATION, END
    }
}
