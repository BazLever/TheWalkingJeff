using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float health = 100;
    public float startHealth = 100;
    public float maxHealth = 100;
    public bool isDead = false;
    [Space]
    public float moveSpeed = 10f;
    public float lookSensitivity = 3.5f;
    private float pitch = 0f;

    public AudioSource hitSound;
    public Transform camera;
    private CharacterController charController;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();

        health = startHealth;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            SceneManager.LoadScene(0);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        Movement();
    }

    private void FixedUpdate()
    {
        Look();
    }

    void Look()
    {
        pitch += Input.GetAxis("Mouse X") * lookSensitivity;

        camera.localRotation = Quaternion.Euler(0f, pitch, 0f);
    }

    void Movement()
    {
        charController.Move(GetMoveDirection() * moveSpeed * Time.deltaTime);
    }

    Vector3 GetMoveDirection()
    {
        return (camera.right * Input.GetAxis("Horizontal") + camera.forward * Input.GetAxis("Vertical")).normalized;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        hitSound.Play();

        if (health <= 0)
        {
            health = 0;
            isDead = true;
        }
    }

    public void Heal(float healAmount)
    {
        health += healAmount;

        if (health > maxHealth)
            health = maxHealth;
    }

}


