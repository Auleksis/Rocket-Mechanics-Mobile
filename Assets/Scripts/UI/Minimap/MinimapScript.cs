using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    [SerializeField] ObjectDesignation[] mapObjects;

    [SerializeField] Transform[] originalMapCorners;

    [SerializeField] Transform[] miniMapCorners;

    [SerializeField] TrajectoryRenderer playerTrajectory;

    [SerializeField] MinimapTrajectoryRenderer minimapTrajectory;

    [SerializeField] ObjectDesignation player;

    [SerializeField] Transform minimapTransform;

    private float scaleX, scaleY;



    private void Start()
    {
        MinimapSetUp();
    }

    private void Update()
    {
        UpdateMap();
    }

    private void MinimapSetUp()
    {
        scaleX = Mathf.Abs(miniMapCorners[0].position.x - miniMapCorners[1].position.x) / Mathf.Abs(originalMapCorners[0].position.x - originalMapCorners[1].position.x);

        scaleY = Mathf.Abs(miniMapCorners[0].position.y - miniMapCorners[1].position.y) / Mathf.Abs(originalMapCorners[0].position.y - originalMapCorners[1].position.y);

        foreach (var obj in mapObjects)
        {
            Color color = obj.spaceObject.GetComponent<SpriteRenderer>().material.color;

            string name = obj.designation;

            obj.mapObject.SetUp(color, name);
        }

        UpdateMap();
    }

    private void UpdateMap()
    {

        //ƒобавление планет на миникарту
        foreach(ObjectDesignation obj in mapObjects)
        {
            Vector3 pos = obj.spaceObject.transform.position;

            Vector3 minimapPos = minimapTransform.position + new Vector3(pos.x * scaleX, pos.y * scaleY, pos.z);

            obj.mapObject.transform.position = minimapPos;
        }

        //ƒобавление траектории игрока на миникарту
        minimapTrajectory.Refresh();

        foreach(Vector3 point in playerTrajectory.GetPoints())
        {
            Vector3 minimapPoint = new Vector3(player.mapObject.transform.position.x + (point.x - player.spaceObject.transform.position.x) * scaleX, player.mapObject.transform.position.y + (point.y - player.spaceObject.transform.position.y) * scaleY);

            minimapTrajectory.AddPoint(minimapPoint);
        }

        minimapTrajectory.DrawLine();
    }

    public void ChangeSpaceObject(int index, GameObject obj)
    {
        mapObjects[index].spaceObject = obj;

        MinimapSetUp();
    }
}
