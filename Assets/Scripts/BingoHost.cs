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


    public float IdleTime = 1;
    public float MachineTime = 1;
    public float PipeTime = 1;

    private float timer = 0;

    private HostStateEnum State;
    private NumberAnnouncer numberAnnouncer = null;


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
        State = HostStateEnum.HS_Idle;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = IdleTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            switch (State)
            {
                case HostStateEnum.HS_Idle:
                    timer = MachineTime;
                    gameObject.transform.position = MachineLocation;
                    State = HostStateEnum.HS_Machine;
                    break;
                case HostStateEnum.HS_Machine:
                    timer = PipeTime;
                    gameObject.transform.position = PipeLocation;
                    State = HostStateEnum.HS_Pipe;
                    break;
                case HostStateEnum.HS_Pipe:
                    timer = IdleTime;
                    gameObject.transform.position = IdleLocation;
                    State = HostStateEnum.HS_Idle;
                    if(numberAnnouncer != null)
                    {
                        numberAnnouncer.GenerateNextNumber();
                    }
                    break;
                case HostStateEnum.HS_None:
                    break;
                default:
                    break;
            }
        }
    }
}
