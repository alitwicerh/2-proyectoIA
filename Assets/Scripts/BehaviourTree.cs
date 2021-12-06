using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class BehaviourTree : MonoBehaviour
{

    public GM gm;


    public Queue<GameObject> MyUnits = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject unit in GameObject.FindGameObjectsWithTag("IAUnits")){
            MyUnits.Enqueue(unit);
        }
        MyUnits.Enqueue(GameObject.Find("Blue Archer"));
            
        //MyUnits = GameObject.FindGameObjectsWithTag("IAUnits");
        //enemies.Push(GameObject.Find("Dark Archer"));
        //MyUnits.Push(GameObject.Find("Blue Archer"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndTurnUnit(){
        GameObject unit = MyUnits.Peek();
        if (unit != null){
            MyUnits.Dequeue();
        
            // get enemigos
            List<Unit> unitsJugador = unit.GetComponent<Unit>().enemiesInRange;
            
            // get casas de los enemigos
            
            // si no hay enemigos cerca de las casas 
                //si hay enemigo atacable -> a* a atacar
                //else si hay alguna casa atacable -> a* a atacar
                //else a* a dirección mas cercana rey/castillo
            // else a* a atacar enemigo
        }
        
    }
    [Task]
    public void Ads(){

        foreach( GameObject unit in MyUnits){
            Debug.Log( unit);
        }
        
        Task.current.Succeed();
    }
}
