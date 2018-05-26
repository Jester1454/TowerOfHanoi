using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    public int Size = 0;
    private Tower currentTower;
    private float width;
    private const float SizeOfsset = 0.2f;
    private Transform mTransform;
    public float MovementSpeed = 25f;
    
    public void Initialization(int size, Tower currentTower)
    {
        Size = size;
        width = GetComponent<Renderer>().bounds.size.y;
        mTransform = GetComponent<Transform>();
        this.currentTower = currentTower;
        
        Resize();
        SetStartPosition();
    }
    
    public IEnumerator MoveDisk(Tower tower)
    {
        yield return  StartCoroutine(MoveOnTowerAnimation(tower));
        currentTower = tower;
    }

    private void Resize()
    {
        mTransform.localScale = new Vector3(mTransform.localScale.x - Size*SizeOfsset, mTransform.localScale.y);
    }

    private void SetStartPosition()
    {
        mTransform.position = new Vector3(currentTower.transform.position.x, width * currentTower.CurrentDiskCount + currentTower.BottomPosition.y);
    }

    IEnumerator MoveOnTowerAnimation(Tower tower)
    {
        Vector3 topPosition = new Vector3(mTransform.position.x, currentTower.TopPosition.y + width*2);
        yield return StartCoroutine(MoveTo(topPosition));
        
        Vector3 towerPosition = new Vector3(tower.TopPosition.x, mTransform.position.y);
        yield return StartCoroutine(MoveTo(towerPosition));
        
        Vector3 downTowerPosition = new Vector3(tower.BottomPosition.x, tower.BottomPosition.y + width * tower.CurrentDiskCount);
        yield return StartCoroutine(MoveTo(downTowerPosition));
    }

    IEnumerator MoveTo(Vector3 target)
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        while (mTransform.position != target )
        {
            float step = MovementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            
            yield return waitForEndOfFrame;
        }
    }
    
    
}
