using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Tank;
using System;

public class VehicleViewer : NetworkBehaviour
{
    [SerializeField] private GameObject vehicleViewer = null;
    [SerializeField] private GameObject[] characterSelectDisplayPanels = default;

    [SerializeField] private TMP_Text characterNameText = default;

    [SerializeField] private Character[] characters = default;

    private int currentCharacterIndex = 0;

    [TargetRpc]
    public void TargetEnableVehicleViewer(NetworkConnection target, bool enableObject)
    {
        vehicleViewer.SetActive(enableObject);
    }
    [TargetRpc]
    public void TargetDisableVehicleViewer(NetworkConnection target, bool enableObject)
    {
        vehicleViewer.SetActive(enableObject);
    }
    public override void OnStartClient()
    {
        if (hasAuthority)
        {
            vehicleViewer.SetActive(true);
        }
        characterNameText.text = "  ";

        characterNameText.text = characters[currentCharacterIndex].CharacterName;

        characterSelectDisplayPanels[currentCharacterIndex].transform.GetComponent<Renderer>().material.SetFloat("_Metallic", .45f);

        Health.OnHealthUpdated += HandleHealthUpdates;
    }


    public override void OnStopClient()
    {
        Health.OnHealthUpdated -= HandleHealthUpdates;
    }

    private void HandleHealthUpdates()
    {
        if (hasAuthority)
        {
            vehicleViewer.SetActive(true);
        }
    }

    public void Select()
    {
        CmdSelect(currentCharacterIndex);

        /*NetworkIdentity thisObject = GetComponent<NetworkIdentity>();
        TargetDisableVehicleViewer(thisObject.connectionToClient, false);*/
        TargetDisableVehicleViewer(connectionToClient, false);
        FindObjectOfType<PlayerCameraMounting>().MountCamera();
        if (hasAuthority)
        {
            vehicleViewer.SetActive(false);
        }
        else
        {
            vehicleViewer.SetActive(false);
        }

    }

    [Command(requiresAuthority = false)] 
    public void CmdSelect(int characterIndex, NetworkConnectionToClient sender = null)
    {
        GameObject characterInstance = Instantiate(characters[characterIndex].GameplayCharacterPrefab);

        NetworkServer.Spawn(characterInstance, sender);     
    }
    public void Right()
    {
        characterSelectDisplayPanels[currentCharacterIndex].transform.GetComponent<Renderer>().material.SetFloat("_Metallic", 1f);

        // This is supposed to use characterinstances.count, not characters.length. Instnaces were for the visual though so I am not using them
        currentCharacterIndex = (currentCharacterIndex + 1) % characters.Length;

        characterSelectDisplayPanels[currentCharacterIndex].transform.GetComponent<Renderer>().material.SetFloat("_Metallic", .45f);
        characterNameText.text = characters[currentCharacterIndex].CharacterName;
    }

    public void Left()
    {
        characterSelectDisplayPanels[currentCharacterIndex].transform.GetComponent<Renderer>().material.SetFloat("_Metallic", 1f);

        // This is supposed to use characterinstances.count, not characters.length. Instnaces were for the visual though so I am not using them
        currentCharacterIndex--;
        if(currentCharacterIndex < 0)
        {
            currentCharacterIndex += characters.Length;
        }

        characterSelectDisplayPanels[currentCharacterIndex].transform.GetComponent<Renderer>().material.SetFloat("_Metallic", .45f);
        characterNameText.text = characters[currentCharacterIndex].CharacterName;
    }

}
