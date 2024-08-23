using UnityEngine;

public class SonarPulse : MonoBehaviour
{
    public float maxDistance = 10f; 
    public int segments = 360;   
    public float speed = 5f;       
    public LayerMask wallMask;
    public float fadeInDuration = 1f;  
    public float fadeOutDuration = 1f; 

    private LineRenderer lineRenderer;
    private float currentRadius;
    private float fadeValue = 0f;
    private bool isFadingOut = false;
    private Material lineMaterial;
    private float fadeOutStartTime;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1; 
        lineRenderer.useWorldSpace = false;
        currentRadius = 0f;

        lineMaterial = new Material(Shader.Find("Custom/FadeLineShader"));
        lineRenderer.material = lineMaterial;

        lineMaterial.SetColor("_Color", Color.white);
        lineMaterial.SetFloat("_Fade", 0f);
    }

    void FixedUpdate()
    {
        if (!isFadingOut)
        {
            fadeValue += Time.deltaTime / fadeInDuration;
            if (fadeValue >= 1f)
            {
                fadeValue = 1f;
                fadeOutStartTime = Time.time;
                isFadingOut = true;
            }
        }
        else
        {
            float elapsedTime = Time.time - fadeOutStartTime;
            fadeValue = 1f - (elapsedTime / fadeOutDuration);
            if (fadeValue <= 0f)
            {
                fadeValue = 0f;
                if (gameObject != null)
                {
                    Destroy(gameObject);
                    return;
                }
            }
        }

        lineMaterial.SetFloat("_Fade", fadeValue);

        currentRadius += speed * Time.deltaTime;

        if (currentRadius >= maxDistance)
        {
            currentRadius = maxDistance; 
            if (!isFadingOut)
            {
                isFadingOut = true;
                fadeOutStartTime = Time.time;
            }
        }

        DrawPulse();
    }

    void DrawPulse()
    {
        float angleStep = 360f / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, currentRadius, wallMask);

            if (hit.collider != null)
            {
                lineRenderer.SetPosition(i, hit.point - (Vector2)transform.position);
                if (hit.collider.CompareTag("Key"))
            {
                lineMaterial.SetColor("_Color", Color.yellow);
            }
            }
            else
            {
                lineRenderer.SetPosition(i, direction * currentRadius);
            }
        }
    }
}
