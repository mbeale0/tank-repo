using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeTriggerScript : MonoBehaviour
{
    [SerializeField] bool _IsDesert;
    [SerializeField] bool _IsForest;
    [SerializeField] bool _IsWinter;

    GameObject GameManager;
    private SoundBankScript _Soundbank;
   // private TankMovement _Player;

   
    void Start()
    {
        _Soundbank = GameObject.Find("SoundBank").GetComponent<SoundBankScript>();
        //_Player = GameObject.Find("Player").GetComponent<TankMovement>();
    }

    void Update()
    {

    }

   /* void OnTriggerEnter3D(Collider3D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Trigger by player");
            if (_IsDesert)
            {
                _Soundbank.PlayBackgroundSound("Desert");
                _Player.SetBiome("Desert");
            }
            else if (_IsForest)
            {
                _Soundbank.PlayBackgroundSound("Forest");
                _Player.SetBiome("Forest");
            }
            else if (_IsWinter)
            {
                _Soundbank.PlayBackgroundSound("Winter");
                _Player.SetBiome("Winter");
            }
        }
    }*/
}
