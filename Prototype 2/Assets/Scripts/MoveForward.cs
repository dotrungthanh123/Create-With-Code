using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 40;
    public int maxHp;
    private int hp;
    public HealthBar hb;

    private void Start() {
        hp = maxHp;
    }

    private void Update() {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void OnHit() {
        hp--;
        if (hb) hb.UpdateHp((float) hp / maxHp); // bullet does not have hb
        if (hp == 0) {
            Destroy(gameObject);
            Destroy(hb.gameObject);
        }
    }
}
