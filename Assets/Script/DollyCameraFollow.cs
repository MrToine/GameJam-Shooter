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
        float closestPathPosition = 0f;

        for(int i = 0; i < sampleSteps; i++)
        {
            float t = (float) i / sampleSteps * path.PathLength;

            Vector3 pathPoint = path.EvaluatePositionAtUnit(t, CinemachinePathBase.PositionUnits.Distance);

            var distance = Vector3.Distance(player.position, pathPoint);

            if ( distance < closestDistance )
            {
                closestDistance = distance;
                closestPathPosition = t;
            }

            var dolly = virtualCam.GetCinemachineComponent<CinemachineTrackedDolly>();
            dolly.m_PathPosition = closestPathPosition;
        }
    }
}
