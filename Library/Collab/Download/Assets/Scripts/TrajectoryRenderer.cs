using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{

    public float distanceBetweenEachSegment;
    public int numberOfSegmentsAfterBounce;

    public Sprite dotSprite;

    public float dotSize;

    public Transform renderStartTransform;

    private int numberOfSegmentsBeforeBounce;

    private List<Vector3> trajectoryPoints = new List<Vector3>();
    private List<Vector2> pointsBeforeBounce = new List<Vector2>();
    private List<Vector2> pointsAfterBounce = new List<Vector2>();

    private List<GameObject> instancedDots = new List<GameObject>();

    private Vector2 startPosition;

    [SerializeField]
    private LayerMask layerMask;

    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = renderStartTransform.position;
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.OnStateChanged.AddListener(HandleGameStateChanged);
    }

    public void SetTrajectoryPoints(Vector2 targetPosition)
    {
        trajectoryPoints.Clear();
        pointsBeforeBounce.Clear();
        pointsAfterBounce.Clear();

        trajectoryPoints.Add(startPosition);

        Vector2 direction = targetPosition - startPosition;
        direction.Normalize();

        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, Mathf.Infinity, layerMask);

        if(hit.collider != null){
            Vector2 hitPoint = hit.point;
            float distance = Vector2.Distance(hitPoint, startPosition);
            numberOfSegmentsBeforeBounce = Mathf.RoundToInt(distance / distanceBetweenEachSegment);
            Vector2 previousPoint = startPosition;
            for (int i = 0; i < numberOfSegmentsBeforeBounce; i++)
            {
                Vector2 nextPoint = previousPoint + direction * distanceBetweenEachSegment;
                if(Vector2.Distance(nextPoint, startPosition) <= Vector2.Distance(hitPoint, startPosition)){
                    pointsBeforeBounce.Add(nextPoint);
                    previousPoint = nextPoint;
                }
            }

            pointsAfterBounce.Add(hitPoint);
            previousPoint = hitPoint;
            
            if(hit.transform.gameObject.tag == "Up Border"){
                direction = new Vector2(direction.x, -direction.y);
            } else {
                direction = new Vector2(-direction.x, direction.y);
            }
           

            for (int i = 0; i < numberOfSegmentsAfterBounce; i++)
            {
                Vector2 nextPoint = previousPoint + direction * distanceBetweenEachSegment;
                pointsAfterBounce.Add(nextPoint);
                previousPoint = nextPoint;
            }
        }

        foreach(Vector2 vec in pointsBeforeBounce){
            trajectoryPoints.Add(vec);
        }

        foreach(Vector2 vec in pointsAfterBounce){
            trajectoryPoints.Add(vec);
        }
    }

    public void DrawDots(){
        while(instancedDots.Count != trajectoryPoints.Count){
            if(instancedDots.Count < trajectoryPoints.Count){
                CreateDot();
            } else { 
                RemoveDot();
            }
        }
        for (int i = 0; i < trajectoryPoints.Count; i++)
        {
            instancedDots[i].transform.position = trajectoryPoints[i];
        }
    }

    private void CreateDot(){
        var container = new GameObject();
        container.transform.localScale = Vector3.one * dotSize;
        container.transform.parent = renderStartTransform;

        var sr = container.AddComponent<SpriteRenderer>();
        sr.sprite = dotSprite;
        Color newColor = new Color (254, 254, 254, 10);
        sr.color = newColor;

        instancedDots.Add(container);
    }

    private void RemoveDot(){
        Destroy(instancedDots[instancedDots.Count - 1]);
        instancedDots.Remove(instancedDots[instancedDots.Count - 1]);
    }

    private void ClearAllDots(){
        foreach(GameObject dot in instancedDots){
            Destroy(dot);
            instancedDots.Remove(dot);
        }
    }

    public void HandleGameStateChanged(LevelState oldState, LevelState newState){
        if(newState == LevelState.WAITING){
            renderStartTransform.gameObject.SetActive(true);
        }
        if(newState == LevelState.PLAYING){
            renderStartTransform.gameObject.SetActive(false);
        }
    }

}
