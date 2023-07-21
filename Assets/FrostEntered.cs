using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ZoneState{In, Out };
public class FrostEntered : MonoBehaviour
{
    public FrostEffect frost;
    private ZoneState zoneState = ZoneState.Out;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        frost.FrostAmount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SnowZone"))
        {
            zoneState = ZoneState.In;
            StartFrostTimer();
        }
    }

    private void LateUpdate()
    {
        if (zoneState == ZoneState.In && frost.FrostAmount > 0.4)
        {
            int random = Random.Range(0, 500);

            if (random == 1)
            {
                //damge player
                player.PlayerHit(15);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SnowZone"))
        {
            zoneState = ZoneState.Out;
            StopCoroutine("FrostTimer");
            frost.FrostAmount = 0;
        }
    }

    void StartFrostTimer()
    {
        if(zoneState == ZoneState.In)
        StartCoroutine("FrostTimer");
    }

    IEnumerator FrostTimer()
    {
        yield return new WaitForSeconds(5f);
        frost.FrostAmount += 0.05f;
        StartFrostTimer();
    }
}
