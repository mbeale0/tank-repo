using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Tank;

public class VehicleViewer : NetworkBehaviour
{
    [SerializeField] public GameObject vehicleViewer;
    [SerializeField] private GameObject[] characterSelectDisplayPanels = default;
    [SerializeField] private GameObject characterSelectDisplay = default;
    [SerializeField] private GameObject mainCamera = null;

    [SerializeField] private TMP_Text characterNameText = default;

    [SerializeField] private Character[] characters = default;

    private int currentCharacterIndex = 0;
    
    
    public override void OnStartClient()
    {
        if (hasAuthority)
        {
            vehicleViewer.SetActive(true);
        }
        characterNameText.text = "  ";

        characterNameText.text = characters[currentCharacterIndex].CharacterName;

        characterSelectDisplayPanels[currentCharacterIndex].transform.GetComponent<Renderer>().material.SetFloat("_Metallic", .45f); ;

    }

    public void Select()
    {
        CmdSelect(currentCharacterIndex);

        /* int childCount = transform.childCount;
         for(int i = 0; i < childCount; i++)
         {
             Transform child = transform.GetChild(i);
             child.gameObject.SetActive(false);
         }*/
        if (isLocalPlayer)
        {
            vehicleViewer.SetActive(false);
        }
    }

    [Command(requiresAuthority = false)] 
    public void CmdSelect(int characterIndex, NetworkConnectionToClient sender = null)
    {
        GameObject characterInstance = Instantiate(characters[characterIndex].GameplayCharacterPrefab);
        /*Instantiate(mainCamera, new Vector3(0, 10, 0), Quaternion.identity);
        float timeStart = 0;
        while(timeStart < 1)
        {
            timeStart += Time.deltaTime;
        }*/
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
