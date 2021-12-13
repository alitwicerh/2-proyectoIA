using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //NEW
    public List<Tile> neighbours = new List<Tile>();
    public GameObject visitor;

    private SpriteRenderer rend;
    public Color highlightedColor;
    public Color creatableColor;

    public LayerMask obstacles;

    public bool isWalkable;
    public bool isCreatable;

    private GM gm;

    public float amount;
    private bool sizeIncrease;

	private AudioSource source;

    private void Start()
    {
		source = GetComponent<AudioSource>();
        gm = FindObjectOfType<GM>();
        rend = GetComponent<SpriteRenderer>();
    }

    public bool isClear() // does this tile have an obstacle on it. Yes or No?
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, 0.2f, obstacles);
        if (col == null)
        {
            return true;
        }
        else {
            return false;
        }
    }

    public void Highlight() {
		
        rend.color = highlightedColor;
        isWalkable = true;
    }

    public void Reset()
    {
        rend.color = Color.white;
        isWalkable = false;
        isCreatable = false;
    }

    public void SetCreatable() {
        rend.color = creatableColor;
        isCreatable = true;
    }

    private void OnMouseDown()
    {
        if (isWalkable == true) {
            gm.selectedUnit.Move(this);
        } else if (isCreatable == true && gm.createdUnit != null) {
            Unit unit = Instantiate(gm.createdUnit, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            unit.hasMoved = true;
            unit.hasAttacked = true;
            gm.ResetTiles();
            gm.createdUnit = null;
            gm.pathManager.CreateGraph();
        } else if (isCreatable == true && gm.createdVillage != null) {
            Instantiate(gm.createdVillage, new Vector3(transform.position.x, transform.position.y, 0) , Quaternion.identity);
            gm.ResetTiles();
            gm.createdVillage = null;
        }
    }


    private void OnMouseEnter()
    {
        if (isClear() == true) {
			source.Play();
			sizeIncrease = true;
            transform.localScale += new Vector3(amount, amount, amount);
        }
        
    }

    private void OnMouseExit()
    {
        if (isClear() == true)
        {
            sizeIncrease = false;
            transform.localScale -= new Vector3(amount, amount, amount);
        }

        if (isClear() == false && sizeIncrease == true) {
            sizeIncrease = false;
            transform.localScale -= new Vector3(amount, amount, amount);
        }
    }

    //NEW
    public void SetNeighbours(){
        neighbours.Clear();
        Vector2 direction = Vector2.zero;

        for(int i = 0; i < 4; i++){
            switch(i){
                case 0:
                    direction = Vector2.up;
                    break;
                case 1:
                    direction = Vector2.down;
                    break;
                case 2:
                    direction = Vector2.left;
                    break;
                case 3:
                    direction = Vector2.right;
                    break;
            }

            RaycastHit2D collide = Physics2D.Raycast(transform.position, direction, 5f, LayerMask.GetMask("Tile"));
            if(!collide) continue;

            Tile neighbour = collide.transform.GetComponent<Tile>();
            if(neighbour && neighbour.visitor == null){
                neighbours.Add(neighbour);
            }
        }
    }
}