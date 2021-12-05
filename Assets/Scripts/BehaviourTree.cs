using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class BehaviourTree : MonoBehaviour
{

    public GM gm;
    public List<Unit> enemies = new List<Unit>();

    [Task]
    private void Ads(){

        Debug.Log("A");
        Task.current.Succeed();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
