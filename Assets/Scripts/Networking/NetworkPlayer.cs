using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public Animator leftHandAnimator, rightHandAnimator;

    private PhotonView photonView;

    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;
    private Transform XROrigin;
    private Connect4Manager connect4Manager;

    void Start()
    {

        connect4Manager = Connect4Manager.Instance;

        Debug.Log("Starting Network Player");
        photonView = GetComponent<PhotonView>();


        headRig = GameObject.Find("XR Origin/Camera Offset/Main Camera").transform;
        leftHandRig = GameObject.Find("XR Origin/Camera Offset/LeftHand Controller").transform;
        rightHandRig = GameObject.Find("XR Origin/Camera Offset/RightHand Controller").transform;
        XROrigin = GameObject.Find("XR Origin").transform;

        // Here we potentially can adjust the spawning position if in the room.

        if (photonView.IsMine){

            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (photonView.IsMine)
        {

            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);

            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);
        }
    }

    void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}
