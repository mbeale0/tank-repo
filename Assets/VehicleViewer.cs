using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VehicleViewer : NetworkBehaviour
{
    [SerializeField] private GameObject[] characterSelectDisplayPanels = default;
    [SerializeField] private GameObject characterSelectDisplay = default;

    [SerializeField] private TMP_Text characterNameText = default;

    [SerializeField] private Character[] characters = default;

    private int currentCharacterIndex = 0;
    private List<GameObject> characterInstances = new List<GameObject>();
    
    public override void OnStartClient()
    {
        characterNameText.text = "  ";
        /*foreach(var character in characters)
        {
            GameObject characterInstance = Instantiate(character.CharacterPreviewPrefab, characterPreviewParents[currentCharacterIndex]);

            //characterInstance.SetActive(false);
            characterInstances.Add(character);
        }
        // Might leave this part off b/c want them on by default. We will see
        characterInstances[currentCharacterIndex].SetActive(true);*/
        characterNameText.text = characters[currentCharacterIndex].CharacterName;

        characterSelectDisplayPanels[currentCharacterIndex].transform.GetComponent<Renderer>().material.SetFloat("_Metallic", .45f); ;

    }

    public void Select()
    {
        CmdSelect(currentCharacterIndex);
        characterSelectDisplay.SetActive(false);
    }

    [Command(requiresAuthority = false)] 
    public void CmdSelect(int characterIndex, NetworkConnectionToClient sender = null)
    {

        FindObjectOfType<MyNetworkManager>().SetStartVehicle(characters[characterIndex].GameplayCharacterPrefab, sender);
        
        
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
