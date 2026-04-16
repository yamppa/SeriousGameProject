using Oculus.Interaction.Locomotion;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private FirstPersonLocomotor fpl;

    private void Awake()
    {
        fpl = GetComponent<FirstPersonLocomotor>();
    }

    private void Update()
    {
        if (fpl.Velocity.magnitude > 0.1f)
        {
            // Play footstep sound
            AudioManager.Instance.PlayPlayerFootsteps(0.5f);
            Debug.Log("Playing footstep sound");
        }

        if (InputSystem.actions["Crouch"].WasPressedThisFrame())
        {
            // Play interaction sound
            fpl.ToggleCrouch();
            Debug.Log("Toggling crouch");
        }
    }
}
