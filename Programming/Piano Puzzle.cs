public class PianoPuzzle: MonoBehaviour { 
  private GameObject heldKey = null; 
  private Rigidbody heldKeyRb; 
  private Camera playerCamera; 
  private Quaternion heldKeyOriginalRotation; 
  
  private void Start() 
  { 
    playerCamera = Camera.main; 
  } 
  
  private void Update() 
  { 
    if (Input.GetKeyDown(KeyCode.F)) 
    { 
      if (heldKey == null) 
      { HoldingKey(); 
                           } 
     else 
     { 
       if (!PlacingKey()) { DropKey(); } } } } public void HoldingKey() { RaycastHit hit; if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 8f)) { if (hit.collider.CompareTag("PianoKey")) { heldKey = hit.collider.gameObject; heldKeyRb = heldKey.GetComponent<Rigidbody>(); heldKeyRb.isKinematic = true; heldKey.transform.SetParent(playerCamera.transform); heldKey.transform.localPosition = new Vector3(0, -0.5f, 1); } } } public bool PlacingKey() { RaycastHit hit; if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 3f)) { if (hit.collider.CompareTag("KeySlot")) { KeySlot slot = hit.collider.GetComponent<KeySlot>(); PianoKey key = heldKey.GetComponent<PianoKey>(); if (slot.correctKeyID == key.keyID) { slot.PlaceKey(heldKey); heldKey.transform.SetParent(slot.transform, true); heldKey.transform.position = slot.transform.position; heldKey.transform.rotation = slot.transform.rotation; heldKey.transform.localScale = slot.transform.lossyScale; heldKey.GetComponent<Rigidbody>().isKinematic = true; heldKey = null; heldKeyRb = null; return true; } } } return false; } public void DropKey() { if (heldKey != null) { heldKey.transform.SetParent(null); if (heldKeyRb != null) { heldKeyRb.isKinematic = false; } heldKey = null; heldKeyRb = null; } } }
