using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlayerDetector : MonoBehaviour
{
    [SerializeField] private int horizontalRays;
    [SerializeField] private int verticalRays;
    [SerializeField] private float fieldOfView;
    [SerializeField] private float verticalFov;
    [SerializeField] private float maxDistance;

    [SerializeField] private int raysPerFrame = 30;
    [SerializeField] private float updateDelay = 0f;

    [SerializeField] private LayerMask visionMask;

    [SerializeField] private float chaseDelay = 1f;

    private RaycastHit[] hits = new RaycastHit[1];
    private int currentRayIndex = 0;
    private int totalRays;
    private Vector3[,] rayDirections;
    [SerializeField] private AggresiveUnitStateMachine stateMachine;
    private float playerVisibleTime;
    private bool playerSeenLastFrame;

    private List<RaycastHit> lastHits = new List<RaycastHit>();
    private Vector3 origin;

    private void Start()
    {
        totalRays = horizontalRays * verticalRays;
        rayDirections = new Vector3[horizontalRays, verticalRays];
        PrecomputeDirections();
        StartCoroutine(VisionRoutine());
    }

    private IEnumerator VisionRoutine()
    {
        while (true)
        {
            lastHits.Clear();

            for (int i = 0; i < raysPerFrame; i++)
            {
                int total = totalRays;
                if (total == 0) yield break;

                int index = currentRayIndex % total;

                int x = index % horizontalRays;
                int y = index / horizontalRays;

                if (x >= horizontalRays || y >= verticalRays)
                {
                    currentRayIndex = 0;
                    continue;
                }

                CastRay(x, y);
                currentRayIndex++;

                if (currentRayIndex >= totalRays)
                    currentRayIndex = 0;
            }

            ProcessVisionLogic();

            if (updateDelay > 0)
                yield return new WaitForSeconds(updateDelay);
            else
                yield return null;
        }
    }

    private void PrecomputeDirections()
    {
        for (int y = 0; y < verticalRays; y++)
        {
            for (int x = 0; x < horizontalRays; x++)
            {
                float yaw = (x / (float) (horizontalRays - 1) - 0.5f) * fieldOfView;
                float pitch = (y / (float) (verticalRays - 1) - 0.5f) * verticalFov;
                rayDirections[x, y] = Quaternion.Euler(-pitch, yaw, 0) * Vector3.forward;
            }
        }
    }

    private void CastRay(int x, int y)
    {
        hits = new RaycastHit[10];

        if (x < 0 || y < 0 || x >= horizontalRays || y >= verticalRays)
            return;

        origin = transform.position;
        Vector3 dir = transform.rotation * rayDirections[x, y];

        if (Physics.RaycastNonAlloc(origin, dir, hits, maxDistance, visionMask) > 0)
        {
            lastHits.Add(hits[0]);
            Debug.DrawRay(origin, dir * hits[0].distance, Color.red, 0.02f);
        }
        else
        {
            Debug.DrawRay(origin, dir * maxDistance, Color.green, 0.02f);
        }
    }

    private void ProcessVisionLogic()
    {
        bool seesPlayer = false;

        Vector3 point = new Vector3(0, 0, 0);

        foreach (var hit in lastHits)
        {
            if (hit.collider == null) continue;

            if (hit.collider.TryGetComponent<PlayerCharacter>(out PlayerCharacter PlayerCharacter))
            {
                point = PlayerCharacter.PlayerTransform.transform.position;
                seesPlayer = true;
                break;
            }
        }

        if (seesPlayer)
        {
            playerVisibleTime += Time.deltaTime;

            if (!playerSeenLastFrame)
            {
                if (point == new Vector3(0, 0, 0))
                    return;

                stateMachine.Inspect(point);
            }

            // if (playerVisibleTime >= chaseDelay)
            // {
            //     stateMachine.Chase();
            // }
        }
        else
        {
            playerVisibleTime = 0f;
        }

        playerSeenLastFrame = seesPlayer;
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            Vector3 start = transform.position;
            for (int y = 0; y < verticalRays; y++)
            {
                for (int x = 0; x < horizontalRays; x++)
                {
                    float yaw = (x / (float) (horizontalRays - 1) - 0.5f) * fieldOfView;
                    float pitch = (y / (float) (verticalRays - 1) - 0.5f) * verticalFov;
                    Vector3 dir = Quaternion.Euler(-pitch, yaw, 0) * transform.forward;
                    Gizmos.DrawRay(start, dir * maxDistance * 0.5f);
                }
            }
        }
    }
}