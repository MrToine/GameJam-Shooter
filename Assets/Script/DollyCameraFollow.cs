using Cinemachine;
using UnityEngine;

public class DollyCameraFollow : MonoBehaviour
{
    public Transform player;
    public CinemachineVirtualCamera virtualCam;
    public CinemachinePathBase path;
    public float pathOffset = 0f;
    public int sampleSteps =50;

    void Update()
    {
        float closestDistance = float.MaxValue;
        float closestPathPos = 0f;

        for (int i = 0; i <= sampleSteps; i++)
        {
            float t = i / (float)sampleSteps * path.PathLength;
            Vector3 pathPoint = path.EvaluatePositionAtUnit(t, CinemachinePathBase.PositionUnits.Distance);
            float distance = Vector2.Distance(new Vector2(player.position.x, player.position.y), new Vector2(pathPoint.x, pathPoint.y));

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPathPos = t;
            }
        }

        var dolly = virtualCam.GetCinemachineComponent<CinemachineTrackedDolly>();
        if (dolly != null)
        {
            dolly.m_PathPosition = closestPathPos;
        }
    }
}
