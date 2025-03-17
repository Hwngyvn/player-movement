using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 7f; // Kekuatan lompatan
    public LayerMask groundLayer; // Layer tanah
    public Transform groundCheck; // Posisi pengecekan tanah
    public float groundCheckRadius = 0.2f; // Radius pengecekan tanah
    public int maxJumps = 2; // Jumlah maksimum lompatan


    private Rigidbody2D rb;
    private bool isGrounded;
    private bool _wasGrounded;
    private int jumpCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Cek apakah karakter menyentuh tanah
        bool wasGrounded = isGrounded; // Simpan status sebelumnya
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && !wasGrounded) // Hanya reset saat pertama kali menyentuh tanah
        {
            jumpCount = 0;
        }

        // Jika tombol lompat ditekan dan masih ada lompatan tersisa
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            Jump();
            jumpCount++;
            Debug.Log("Lompatan ke: " + jumpCount);
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // Reset kecepatan vertikal sebelum lompat
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
