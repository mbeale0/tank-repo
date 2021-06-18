using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Vehicle", menuName = "Character Selection/New Vehicle")]
public class Character : ScriptableObject
{
    [SerializeField] private string characterName = default;
    [SerializeField] private GameObject characterPreviewPrefab = default;
    [SerializeField] private GameObject gameplayCharacterPrefab = default;

    public string CharacterName => characterName;
    public GameObject CharacterPreviewPrefab => characterPreviewPrefab;
    public GameObject GameplayCharacterPrefab => gameplayCharacterPrefab;

}

