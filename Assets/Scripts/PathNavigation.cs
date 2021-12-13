using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNavigation : MonoBehaviour
{
    GM gm;
    Unit unit;

    public Tile currentTile;
    public List<Tile> currentPath = new List<Tile>();
    public int index { get; set; } = -1;

    void Awake()
    {
        gm = FindObjectOfType<GM>();
        unit = GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        UnitsMovement();
    }

    void UnitsMovement(){
        if(currentPath == null)
            return;

        if(index + 1 >= currentPath.Count && currentPath.Count > 0){
            unit.EndMovement(currentPath[index]);
            index = -1;
            currentPath.Clear();
        }

        if(currentPath.Count == 0)
            return;

        Tile nextTile = currentPath[index + 1];
        float distance = Vector2.Distance(transform.position, nextTile.transform.position);

        if(distance >= 0.15f){
            Vector3 direction = nextTile.transform.position - currentTile.transform.position;
            transform.position += direction.normalized * Time.deltaTime * 3f;
        }
        else{
            index++;
            currentTile = nextTile;
        }
    }

    public void SetCloseTile(){
        float minDistance = Mathf.Infinity;
        Tile tile = null;

        foreach(Tile t in gm.tiles){
            float distance = Vector2.Distance(transform.position, t.transform.position);

            if(distance < minDistance){
                minDistance = distance;
                tile = t;
            }
        }

        tile.visitor = gameObject;
        currentTile = tile;
    }

    public void SetNewPath(List<Tile> path){
        currentTile.visitor = null;
        index = 0;
        currentPath = path;
    }
}