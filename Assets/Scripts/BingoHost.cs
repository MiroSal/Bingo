using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum HostStateEnum
{
    HS_Idle,
    HS_Machine,
    HS_Pipe,
    HS_None
}

public class BingoHost : MonoBehaviour
{
    public Vector3 MachineLocation = new Vector3(0, 0, 0);
    public Vector3 IdleLocation = new Vector3(0, 0, 0);
    public Vector3 PipeLocation = new Vector3(0, 0, 0);

    [SerializeField]
    private float idleTime = 1;
    [SerializeField]
    private float machineTime = 1;
    [SerializeField]
    private float pipeTime = 1;

    [SerializeField]
    private GameObject smokeEffect;

    private float timer = 0;
    private HostStateEnum state;
    private NumberAnnouncer numberAnnouncer = null;
    private ParticleSystem smokeParticleSystem = null;

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
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
    }

    void Update()
    {
        timer -= Time.deltaTime;

        //loop hosts state with timer at the moment
        if (timer < 0)
        {
            switch (state)
            {
                case HostStateEnum.HS_Idle:
                    timer = machineTime;
                    gameObject.transform.position = MachineLocation;
                    state = HostStateEnum.HS_Machine;
                    break;
                case HostStateEnum.HS_Machine:
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
}
