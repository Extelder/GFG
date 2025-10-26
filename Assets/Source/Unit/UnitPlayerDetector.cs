using System.Collections;
using UnityEngine;

[ExecuteAlways]
public class UnitPlayerDetector : MonoBehaviour
{
    [Header("Видимость")] [SerializeField] private int horizontalRays = 25;
    [SerializeField] private int verticalRays = 15;
    [SerializeField] private float horizontalFov = 100f;
    [SerializeField] private float verticalFov = 60f;
    [SerializeField] private float viewDistance = 20f;
    [SerializeField] private LayerMask visionMask;
    [SerializeField] private Transform eyesPoint;

    [Header("AI Поведение")] [SerializeField]
    private float inspectTime = 0.2f;

    [SerializeField] private float chaseTime = 0.3f;
    [SerializeField] private AggresiveUnitStateMachine stateMachine;

    private Transform player;
    private bool isSeeingPlayer;
    private float seeTimer;
    private Vector3[,] rayGrid;
    private bool gridDirty = true; 

    private void OnValidate()
    {
        horizontalRays = Mathf.Max(2, horizontalRays);
        verticalRays = Mathf.Max(2, verticalRays);
        horizontalFov = Mathf.Clamp(horizontalFov, 1f, 360f);
        verticalFov = Mathf.Clamp(verticalFov, 1f, 180f);
        viewDistance = Mathf.Max(0.1f, viewDistance);
        inspectTime = Mathf.Max(0f, inspectTime);
        chaseTime = Mathf.Max(inspectTime, chaseTime); // chase >= inspect

        gridDirty = true;
        if (!Application.isPlaying)
            GenerateRayGrid();
    }

    private void Start()
    {
        var pc = FindObjectOfType<PlayerCharacter>();
        if (pc != null)
            player = pc.PlayerTransform;

        if (gridDirty || rayGrid == null)
            GenerateRayGrid();

        StartCoroutine(VisionLoop());
    }

    [ContextMenu("Regenerate Ray Grid")]
    private void GenerateRayGrid()
    {
        if (horizontalRays < 2) horizontalRays = 2;
        if (verticalRays < 2) verticalRays = 2;

        rayGrid = new Vector3[horizontalRays, verticalRays];

        float hx = Mathf.Max(1, horizontalRays - 1);
        float vy = Mathf.Max(1, verticalRays - 1);

        for (int y = 0; y < verticalRays; y++)
        {
            for (int x = 0; x < horizontalRays; x++)
            {
                float tX = x / hx; // в [0,1]
                float tY = y / vy; // в [0,1]
                float yaw = Mathf.Lerp(-horizontalFov * 0.5f, horizontalFov * 0.5f, tX);
                float pitch = Mathf.Lerp(-verticalFov * 0.5f, verticalFov * 0.5f, tY);
                rayGrid[x, y] = Quaternion.Euler(-pitch, yaw, 0f) * Vector3.forward;
            }
        }

        gridDirty = false;
    }

    private IEnumerator VisionLoop()
    {
        WaitForSeconds delay = new WaitForSeconds(0.05f); 
        while (true)
        {
            if (rayGrid == null || gridDirty) GenerateRayGrid();

            bool detected = false;
            if (player == null)
            {
                var pc = FindObjectOfType<PlayerCharacter>();
                if (pc != null) player = pc.PlayerTransform;
            }

            if (player != null)
            {
                Vector3 origin = eyesPoint ? eyesPoint.position : transform.position + Vector3.up * 1.6f;

                for (int y = 0; y < verticalRays; y++)
                {
                    for (int x = 0; x < horizontalRays; x++)
                    {
                        Vector3 dir = transform.rotation * rayGrid[x, y];
                        if (Physics.Raycast(origin, dir, out RaycastHit hit, viewDistance, visionMask))
                        {
                            if (hit.collider.TryGetComponent<PlayerCharacter>(out _))
                            {
                                detected = true;
                                Debug.DrawRay(origin, dir * hit.distance, Color.red, 0.05f);
                            }
                            else
                            {
                                Debug.DrawRay(origin, dir * hit.distance, Color.gray, 0.05f);
                            }
                        }
                        else
                        {
                            Debug.DrawRay(origin, dir * viewDistance, Color.green, 0.05f);
                        }
                    }
                }
            }

            UpdateAI(detected);
            yield return delay;
        }
    }

    private void UpdateAI(bool playerVisible)
    {
        if (playerVisible)
        {
            seeTimer += Time.deltaTime;

            if (!isSeeingPlayer && seeTimer >= inspectTime)
            {
                isSeeingPlayer = true;
                stateMachine?.Inspect(PlayerCharacter.Instance.PlayerTransform.position);
            }

            if (seeTimer >= chaseTime)
            {
                stateMachine?.Chase();
            }
        }
        else
        {
            isSeeingPlayer = false;
            seeTimer = 0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (rayGrid == null || gridDirty)
            GenerateRayGrid();

        if (rayGrid == null) return;

        Vector3 origin = eyesPoint ? eyesPoint.position : transform.position + Vector3.up * 1.6f;
        Gizmos.color = new Color(0f, 0.8f, 1f, 0.7f);

        float gizmoScale = 0.4f;

        for (int y = 0; y < verticalRays; y++)
        {
            for (int x = 0; x < horizontalRays; x++)
            {
                Vector3 dir = transform.rotation * rayGrid[x, y];
                Gizmos.DrawRay(origin, dir * viewDistance * gizmoScale);
            }
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, 0.06f);
    }

    public void MarkGridDirty() => gridDirty = true;
}