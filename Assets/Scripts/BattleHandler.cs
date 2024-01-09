using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private Transform prefabCharacterBattle;

    private void Awake(){

    }

    private void Start(){
        //SpawnCharacter
        SpawnCharacter(true);
        SpawnCharacter(false);
    }

    private void Update(){
        
    }

    private void FixedUpdate(){
        
    }

    private void SpawnCharacter(bool isPlayerTeam){
        Vector2 position;
        if(isPlayerTeam){
            position = new Vector2(-15,0);
        }else{
            position = new Vector2(-10,0);
        }
        Instantiate(prefabCharacterBattle, position, Quaternion.identity);
    }
}
