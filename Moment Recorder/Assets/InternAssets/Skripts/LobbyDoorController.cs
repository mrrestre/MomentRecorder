using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LobbyDoorController : MonoBehaviour
{
    // Store the mesh with the effect
    public GameObject effectMesh;
    public bool isBeingPointedAt = false;
    [SerializeField] private PlayableDirector pd;

    private void Update()
    {
        if (isBeingPointedAt)
        {
            effectMesh.SetActive(true);
        } 
        else
        {
            effectMesh.SetActive(false);
        }

        isBeingPointedAt = false;
    }

    public void OpenDoors()
    {
        pd.Play();
    }


}
