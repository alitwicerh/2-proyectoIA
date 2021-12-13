using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    GM gm;
    Dictionary<Tile, List<Tile>> graph = new Dictionary<Tile, List<Tile>>();

    void Awake(){
        gm = FindObjectOfType<GM>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //gm = FindObjectOfType<GM>();
        CreateGraph();
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    public void CreateGraph(){
        graph.Clear();

        PathNavigation[] units = FindObjectsOfType<PathNavigation>();
        foreach(PathNavigation unit in units){
            unit.SetCloseTile();
        }

        gm.tiles.ForEach(tile => tile.SetNeighbours());

        foreach(Tile t in gm.tiles){
            List<Tile> borders = new List<Tile>();

            foreach(Tile neighbour in t.neighbours)
                borders.Add(neighbour);

            graph.Add(t, borders);
        }
    }

    public List<Tile> getPath(Tile source, Tile target){
        if(source == null || target == null)
            return null;
        
        SortedList<float, Tile> bound = new SortedList<float, Tile>();
        Dictionary<Tile, Tile> lastVisited = new Dictionary<Tile, Tile>();
        Dictionary<Tile, float> cost = new Dictionary<Tile, float>();

        lastVisited.Add(source, null);
        cost.Add(source, 0);
        bound.Add(Vector3.Distance(source.transform.position, target.transform.position), source);

        while(bound.Count > 0){
            Tile currentTile = bound.Values[0];
            bound.RemoveAt(0);

            if(currentTile == target)
                break;
            
            foreach(Tile nextTile in graph[currentTile]){
                float newCost = cost[currentTile] + Vector3.Distance(nextTile.transform.position, currentTile.transform.position);
                if(!cost.ContainsKey(nextTile) || newCost < cost[nextTile]){
                    if(bound.ContainsValue(nextTile))
                        bound.RemoveAt(bound.IndexOfValue(nextTile));

                    float key = newCost + Vector3.Distance(nextTile.transform.position, target.transform.position);

                    while(bound.ContainsKey(key))
                        //CHECK AQUI
                        key += 0.0001f;
                    bound.Add(key, nextTile);

                    if(lastVisited.ContainsKey(nextTile))
                        lastVisited.Remove(nextTile);
                    lastVisited.Add(nextTile, currentTile);

                    if(cost.ContainsKey(nextTile))
                        cost.Remove(nextTile);
                    cost.Add(nextTile, newCost);
                }
            }
        }

        List<Tile> path = new List<Tile>();
        Tile way = target;
        path.Add(target);

        if(!lastVisited.ContainsKey(way)){
            return null;
        }

        while(lastVisited[way] != null){
            path.Add(lastVisited[way]);
            way = lastVisited[way];
        }

        path.Reverse();

        return path;
    }
}