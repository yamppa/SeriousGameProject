using UnityEngine;


public class HandsHelper : MonoBehaviour
{
    

    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject leftMittenModel;
    public GameObject rightMittenModel;

    public void SetMittens()
    {
        leftHand.SetActive(false);
        rightHand.SetActive(false);
        
        leftMittenModel.SetActive(true);
        rightMittenModel.SetActive(true);

        Debug.Log("Set mittens");


    }

    public void SetDefault()
    {
        leftHand.SetActive(true);
        rightHand.SetActive(true);

        leftMittenModel.SetActive(false);
        rightMittenModel.SetActive(false);

        Debug.Log("Set default");
    }
}
