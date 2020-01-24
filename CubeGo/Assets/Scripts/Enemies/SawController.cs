using UnityEngine;

public class SawController : MonoBehaviour
{
    public Vector3 startPosition, endPosition, targetPosition;

    public void SetSaw(Vector3 startPosition, Vector3 endPosition)
    {
        this.startPosition = startPosition;
        this.endPosition = endPosition; 
        
        
    }

    private void Update()
    {
        transform.position += (transform.position - targetPosition) * Time.deltaTime * 10f;

        if (targetPosition == startPosition)
        {
            targetPosition = endPosition; 
        } 
        else if (targetPosition == endPosition)
        {
            targetPosition = startPosition; 
        }
    }
}
