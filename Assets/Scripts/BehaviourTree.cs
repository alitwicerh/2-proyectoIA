using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Panda;

public class BehaviourTree : MonoBehaviour
{

    public GM gm;
    //int player2Gold = gm.player2Gold;
    public Queue<GameObject> MyUnits = new Queue<GameObject>();

    public GameObject unit;

    public Tile objetivo;

    //public Tile[] tiles;

    public List<Unit> unitsJugador = new List<Unit>();

    public List<Unit> AllUnits;

    public bool activa = false;

    public List<Tile> canReach;

    public int u;


    // Start is called before the first frame update
    void Start()
    {
        
        foreach(GameObject u in GameObject.FindGameObjectsWithTag("IAUnits")){
            MyUnits.Enqueue(u);

        }
        AllUnits = new List<Unit>(FindObjectsOfType<Unit>());
        


        //MyUnits.Enqueue(GameObject.Find("Blue Archer"));
        //tiles = FindObjectsOfType<Tile>();
        //MyUnits = GameObject.FindGameObjectsWithTag("IAUnits");
        //enemies.Push(GameObject.Find("Dark Archer"));
        //MyUnits.Push(GameObject.Find("Blue Archer"));
        //EligeObjetivo();
    }

    /*void Awake() {
        foreach(GameObject u in GameObject.FindGameObjectsWithTag("IAUnits")){
            MyUnits.Enqueue(u);

        }
        AllUnits = new List<Unit>(FindObjectsOfType<Unit>());
    }*/

    // Update is called once per frame
    void Update()
    {
        EligeObjetivo();
    }

    //[Task]
    public void EligeObjetivo(){

        Debug.Log(AllUnits.Count);
        AllUnits[0].IsMoving = true;

        //foreach (Unit u in AllUnits){
        for (u = 0; u < AllUnits.Count; u++){
            if(AllUnits[u].tag == "IAUnits"){
                AllUnits[u].GetBlues();
                AllUnits[u].GetWalkableTiles();
                canReach = AllUnits[u].tilesReach;
                AllUnits[u].isSelected = true;
                gm.selectedUnit = AllUnits[u];
                Debug.Log(AllUnits[u].enemiesInRange.Count);
                if (AllUnits[u].enemiesInRange.Count != 0){
                    
                    foreach (Tile tile in gm.tiles)
                    {
                        if (tile.visitor == AllUnits[u].enemiesInRange[0].transform.gameObject){
                            if (AllUnits[u].isSelected){
                                foreach (Tile t in tile.neighbours){
                                    if (t.isWalkable){
                                        objetivo = t;
                                        break;
                                    }
                                
                                }
                                
                            }

                        }
                    }
                    AllUnits[u].Move(objetivo);
                    AllUnits[u].Attack(AllUnits[u].enemiesInRange[0]);
                   // StartCoroutine(ExampleCoroutine());
                    
                    //u.isSelected = true;
                    //gm.selectedUnit = u;
                    
                    //u.Attack(u.enemiesInRange[0]);
                    //StartCoroutine(ExampleCoroutine());
                }
                else{
                    //unit.GetComponent<Unit>().Move(tile.neighbour[]);
                    Debug.Log("sa");
                    AllUnits[u].Move(canReach[5]);
                    //StartCoroutine(ExampleCoroutine());
                }
                
               //StartCoroutine(ExampleCoroutine());
            }
        }
        StartCoroutine(EndTurnCoroutine());
        //Task.current.Fail();


       /* while (MyUnits.Count != 0){
            unit = MyUnits.Peek();
            MyUnits.Dequeue();
            //Debug.Log(unit);
            unit.GetComponent<Unit>().Getinfo();
            Unit a = unit.GetComponent<Unit>();
            Debug.Log(a);

            a.GetEnemies();
            Debug.Log(a.enemiesInRange.Count);

            // get enemigos
            if (a.enemiesInRange.Count != 0 ){

                unitsJugador = unit.GetComponent<Unit>().enemiesInRange;
                Debug.Log("saassadasda");
                unit.GetComponent<Unit>().isSelected = true;
                gm.selectedUnit = unit.GetComponent<Unit>();

                //Debug.Log(unitsJugador[0]);
                foreach (Tile tile in gm.tiles)
                {
                   // Debug.Log("aaaaeeeaaaaaaaaa");
                    if (tile.visitor.GetComponent<Unit>() == unitsJugador[0]){

                        foreach (Tile t in tile.neighbours){
                            if (t.isWalkable){
                                objetivo = t;
                               // Debug.Log(unitsJugador[0]);
                            }
                            //break;
                        }
                    }
                   // break;
                }
                unit.GetComponent<Unit>().Move(objetivo);
                unit.GetComponent<Unit>().Attack(unitsJugador[0]);


            }
            else{
                //unit.GetComponent<Unit>().Move();
                //unit.GetComponent<Unit>().isSelected = false;
                //gm.selectedUnit = null;
            }
    
        }
        StartCoroutine(ExampleCoroutine());
        //Task.current.Succeed();*/
        
        
    }
    //[Task]
    public void Ads(){

        foreach( GameObject unit in MyUnits){
            Debug.Log(unit);
        }
        StartCoroutine(ExampleCoroutine());
        //Task.current.Succeed();
    }

    //[Task]
    public void B(){

        
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp 1: " + Time.time);
        Debug.Log(u);
        //EligeObjetivo();
        //u.isSelected = false;
        //gm.EndTurn();
        //u += 1;
        
    }

    IEnumerator EndTurnCoroutine()
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp 2: " + Time.time);
        //EligeObjetivo();
        //u.isSelected = false;
        gm.EndTurn();
    }
}
