using UnityEngine;
using UnityEngine.XR;


public class HandsHelper : MonoBehaviour
{
    

    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject leftMittenModel;
    public GameObject rightMittenModel;
    bool mitsensOn = false;

    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    public void SetMittens()
    {
        leftHand.SetActive(false);
        rightHand.SetActive(false);
        
        leftMittenModel.SetActive(true);
        rightMittenModel.SetActive(true);

        Debug.Log("Set mittens");

        mitsensOn = true;


    }

    public void SetDefault()
    {
        leftHand.SetActive(true);
        rightHand.SetActive(true);

        leftMittenModel.SetActive(false);
        rightMittenModel.SetActive(false);

        Debug.Log("Set default");

        mitsensOn = false;
    }

    void Update()
    {
        // Check Left Controller
        if (IsButtonPressed(XRNode.LeftHand, CommonUsages.gripButton))
        {
            Debug.Log("Left Grab Pressed!");
            if(mitsensOn)
            {
                leftHandAnimator.SetBool("isGrabbing", true);
            }
        }
        else
        {
            if (mitsensOn)
            {
                leftHandAnimator.SetBool("isGrabbing", false);
            }
        }

        // Check Right Controller
        if (IsButtonPressed(XRNode.RightHand, CommonUsages.gripButton))
        {
            Debug.Log("Right Grab Pressed!");
            if (mitsensOn)
            {
                rightHandAnimator.SetBool("isGrabbing", true);
            }
        }
        else
        {
            if (mitsensOn)
            {
                
                rightHandAnimator.SetBool("isGrabbing", false);
            }
        }
    }

    bool IsButtonPressed(XRNode node, InputFeatureUsage<bool> usage)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(node);
        if (device.isValid)
        {
            if (device.TryGetFeatureValue(usage, out bool isPressed))
            {
                return isPressed;
            }
        }
        return false;
    }
}
