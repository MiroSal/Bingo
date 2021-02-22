using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bingo Host AI states
enum HostStateEnum
{
    HS_Idle,
    HS_Machine,
    HS_Pipe,
    HS_None
}

public class BingoHost : MonoBehaviour
{
    //Locations
    public Vector3 MachineLocation = new Vector3(0, 0, 0);
    public Vector3 IdleLocation = new Vector3(0, 0, 0);
    public Vector3 PipeLocation = new Vector3(0, 0, 0);

    private AudioSource audioSource = null;

    public AudioClip bingoBallMachineRollSound = null;
    public AudioClip monitorVacuumSound = null;

    [SerializeField]
    private float idleTime = 1;
    [SerializeField]
    private float machineTime = 1;
    [SerializeField]
    private float pipeTime = 1;

    //Particle Effects
    [SerializeField]
    private GameObject smokeEffect;

    //Timer for next state
    private float timer = 0;

    //Curren State
    private HostStateEnum state;

    private NumberAnnouncer numberAnnouncer = null;
    private ParticleSystem smokeParticleSystem = null;

    void OnDrawGizmosSelected()
    {
        //Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(MachineLocation, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(IdleLocation, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(PipeLocation, 0.1f);
    }

    private void Awake()
    {
        numberAnnouncer = FindObjectOfType<NumberAnnouncer>();
        state = HostStateEnum.HS_Idle;
        timer = idleTime;

        if (smokeEffect == null) { Debug.Log("smokeEffect was null"); return; }
        smokeParticleSystem = smokeEffect.GetComponent<ParticleSystem>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        //Loop hosts state with timer at the moment
        if (timer < 0)
        {
            switch (state)
            {
                case HostStateEnum.HS_Idle:
                    if (bingoBallMachineRollSound) PlaySound(bingoBallMachineRollSound);
                    timer = machineTime;
                    gameObject.transform.position = MachineLocation;
                    state = HostStateEnum.HS_Machine;
                    break;
                case HostStateEnum.HS_Machine:
                    if (monitorVacuumSound) PlaySound(monitorVacuumSound);
                    timer = pipeTime;
                    gameObject.transform.position = PipeLocation;
                    state = HostStateEnum.HS_Pipe;
                    break;
                case HostStateEnum.HS_Pipe:
                    timer = idleTime;
                    gameObject.transform.position = IdleLocation;
                    state = HostStateEnum.HS_Idle;
                    if (numberAnnouncer != null)
                    {
                        numberAnnouncer.GenerateNextNumber();
                    }
                    break;
                case HostStateEnum.HS_None:
                    break;
                default:
                    break;
            }
            smokeParticleSystem.Play();
        }
    }

    void PlaySound(AudioClip clipToPlay)
    {
        if (clipToPlay)
        {
            if (audioSource)
                audioSource.PlayOneShot(clipToPlay);
        }
    }
}
